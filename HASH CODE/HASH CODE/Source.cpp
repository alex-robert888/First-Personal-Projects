

#include <iostream>
#include <fstream>
#include <vector>
#include <algorithm>

const int MAX = 100000;


std::ifstream in("inputb.txt");
std::ofstream out("outputb.txt");

int B, L, D;
std::vector<int> scores(MAX);

bool frecv_total[MAX];

struct Lib {
	int id;
	int num_books;
	int sign_time;
	int books_a_day;
	std::vector<int> books;
	long long int total_sum = 0;
	long long int ratio = 0;
	int diff_books = 0;
};

struct Sol_Lib {
	int id;
	int num_books = 0;
	std::vector<int> books;
};


std::vector<Lib> libs;

struct Comp_Books {
	bool operator()(int a, int b) {
		return (scores[a] > scores[b]);
	}
} mycomp_books; 

struct Comp_Libs {
	bool operator()(Lib a, Lib b) {
		return (a.ratio > b.ratio);
	}
} mycomp_libs;


bool frecv_books[MAX];
std::vector<Sol_Lib> sols;

void greedy() {
	int current_day = 0;
	int curr_lib = -1;
	int curr_sign_time = 0;

	while (current_day < D) {

		if (curr_lib < L) {
			if (curr_sign_time == 0) {
				++curr_lib;

				if (curr_lib < L) {
					curr_sign_time = libs[curr_lib].sign_time - 1;

					Sol_Lib tmp;
					tmp.id = libs[curr_lib].id;
					sols.push_back(tmp);
				}

			}
			else {
				--curr_sign_time;
			}
		}


		for (int i = 0; i < curr_lib; ++i) {
			int countR = 0;
			while(libs[i].books.size() > 0 && countR < libs[i].books_a_day) {
				if (frecv_books[libs[i].books[0]] == true) {
					libs[i].books.erase(libs[i].books.begin());
					continue;
				}

				frecv_books[libs[i].books[0]] = true;
				sols[i].num_books += 1;
				sols[i].books.push_back(libs[i].books[0]);
				libs[i].books.erase(libs[i].books.begin());
				++countR;
			}
		}

		++current_day;
	}
}

void print_sols()
{
	out << sols.size() << '\n';

	for (int i = 0; i < sols.size(); ++i) {

		if (sols[i].num_books != 0) {

			out << sols[i].id << ' ' << sols[i].num_books << '\n';

			bool entered = false;

			for (int j = 0; j < sols[i].books.size(); ++j) {
				out << sols[i].books[j] << ' ';
				entered = true;
			}

			if (entered == true)
				out << '\n';
		}
	}
}

int main() {
	
	in >> B >> L >> D;

	// read scores
	for (int i = 0; i < B; ++i) {
		in >> scores[i];
	}

	// for each lib, 3 props
	for (int i = 0; i < L; ++i) {
		Lib temp;
		temp.id = i;
		in >> temp.num_books >> temp.sign_time >> temp.books_a_day;



		for (int j = 1; j <= temp.num_books; ++j) {
			int b;
			in >> b;
			temp.books.push_back(b);

			if (frecv_total[b] == false) {
				temp.diff_books += 1;
				frecv_total[b] = true;
			}

			temp.total_sum += scores[b];
		}

		temp.ratio = (temp.total_sum / temp.sign_time) * temp.diff_books;

		std::sort(temp.books.begin(), temp.books.end(), mycomp_books);

		libs.push_back(temp);
	}

	std::sort(libs.begin(), libs.end(), mycomp_libs);

	greedy();
	print_sols();

	return 0;
}