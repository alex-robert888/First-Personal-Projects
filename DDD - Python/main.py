

from Entities.Client import Client
from Entities.Movie import Movie
from Entities.Rental import Rental

from Repository.MemoryBased_Repository import MemoryBasedRepository
from Repository.FileBased_Repository import FileBasedRepository
from Repository.BinaryFileBased_Repository import BinaryFileBasedRepository
from Repository.JSONBased_Repository import JSONBasedRepository
from Repository.XMLBased_Repository import XMLBasedRepository
from Repository.SQLBased_Repository import SQLBasedRepository

from Services.ServicesClients import ServicesClients
from Services.ServicesMovies import ServicesMovies
from Services.ServicesRentals import ServicesRentals
from UI.UI import UI

from Errors.Errors import *

import datetime
import atexit
import signal
from configparser import ConfigParser
#from datetime import  datetime
from dateutil.parser import parse


class MainApp(object):

    '''

    A class that incorporates the whole app and its basic data structures

    Attributes
    ----------
    :param: repo_clients, repo_movies, repo_rentals : Repo()
    Repositories in which clients, movies, rentals are stored, respectively

    :param: services_clients, services_movies, services_rentals : ServicesClients(), ServicesMovies(), ServicesRentals()
    Services corresponding to each domain. For clients and movies, operations are: add, remove, update and list entities

    :param: ui : UI()
    Main app user interface

    '''

    def __init__(self):
        # repositories
        self.repo_clients, self.repo_movies, self.repo_rentals = self.read_properties_file()

        # services
        self.services_clients = ServicesClients(self.repo_clients)
        self.services_movies = ServicesMovies(self.repo_movies)
        self.services_rentals = ServicesRentals(self.repo_rentals)

        # ui
        self.ui = UI(self.services_clients, self.services_movies, self.services_rentals)

        self.repository_type = None

        # loads data to persistent reposoitory when user exits application
        atexit.register(self.write_data_to_persistent_repository)

    def init_clients(self):
        '''

        Initializes the repository of clients with 5 default entities.

        '''

        self.repo_clients.add_new_entity(Client(1, "Joshua"))
        self.repo_clients.add_new_entity(Client(2, "Sue"))
        self.repo_clients.add_new_entity(Client(3, "Philip"))
        self.repo_clients.add_new_entity(Client(4, "Josh"))
        self.repo_clients.add_new_entity(Client(5, "Ralph"))

    def init_movies(self):
        '''

        Initializes the repository of movies with 5 default entities

        '''

        self.repo_movies.add_new_entity(Movie(1, "Once Upon a Time In Hollywood", "The end of a Hollywood movies era", "comedy"))
        self.repo_movies.add_new_entity(Movie(2, "The Sixth Sense", "A startling twist", "thriller"))
        self.repo_movies.add_new_entity(Movie(3, "Ice Age", "Humor at another level", "comedy"))
        self.repo_movies.add_new_entity(Movie(4, "The Dark Knight", "Absolutely revolutionary for its genre", "action"))
        self.repo_movies.add_new_entity(Movie(5, "Godfather", "Masterpiece illustrating what gangsters look like", "drama"))

    def init_rentals(self):
        '''

        Initializes the repository of rentals with 3 default entities

        '''

        self.repo_rentals.add_new_entity(Rental(1, 3, 2, datetime.date(2019, 5, 23), datetime.date(2019, 10, 2), datetime.date(2019, 11, 20)))
        self.repo_rentals.add_new_entity(Rental(2, 4, 1, datetime.date(2019, 7, 14), datetime.date(2019, 10, 14), datetime.date(2000, 1, 1)))
        self.repo_rentals.add_new_entity(Rental(3, 3, 5, datetime.date(2019, 5, 30), datetime.date(2019, 12, 30), datetime.date(2000, 1, 1)))

    def init_everything(self):
        '''

        Initializes the repositories with default elements

        '''
        self.init_clients()
        self.init_movies()
        self.init_rentals()

    def read_properties_file(self):
        config = ConfigParser()
        config.read('settings.properties')
        self.repository_type = config.get('repo_type', 'repository')

        file_client = config.get('repo_type', 'clients')
        file_movie = config.get('repo_type', 'movies')
        file_rentals = config.get('repo_type', 'rentals')

        repo_clients = None
        repo_movies = None
        repo_rentals = None

        if self.repository_type == "memory_based":
            repo_clients = MemoryBasedRepository()
            repo_movies = MemoryBasedRepository()
            repo_rentals = MemoryBasedRepository()

        elif self.repository_type == "file_based":
            repo_clients = FileBasedRepository(file_client)
            repo_movies = FileBasedRepository(file_movie)
            repo_rentals = FileBasedRepository(file_rentals)

        elif self.repository_type == "binary_file_based":
            repo_clients = BinaryFileBasedRepository(file_client)
            repo_movies = BinaryFileBasedRepository(file_movie)
            repo_rentals = BinaryFileBasedRepository(file_rentals)

        elif self.repository_type == "JSON_based":
            repo_clients = JSONBasedRepository(file_client)
            repo_movies = JSONBasedRepository(file_movie)
            repo_rentals = JSONBasedRepository(file_rentals)

        elif self.repository_type == "XML_based":
            repo_clients = XMLBasedRepository()
            repo_movies = XMLBasedRepository()
            repo_rentals = XMLBasedRepository()

        elif self.repository_type == "SQL_based":
            repo_clients = SQLBasedRepository(file_client)
            repo_movies = SQLBasedRepository(file_movie)
            repo_rentals = SQLBasedRepository(file_rentals)

        else:
            raise InvalidRepositoryTypeError

        return repo_clients, repo_movies, repo_rentals

    #def convert_str

    def write_data_to_persistent_repository(self):
        if self.repository_type == "memory_based":
            return

        if self.repository_type == "file_based":
            self.repo_clients.write_data_to_file()
            self.repo_movies.write_data_to_file()
            self.repo_rentals.write_data_to_file()

        elif self.repository_type == "binary_file_based":
            self.repo_clients.write_to_binary_file()
            self.repo_movies.write_to_binary_file()
            self.repo_rentals.write_to_binary_file()

        elif self.repository_type == "JSON_based":
            self.repo_clients.write_data_to_json("clients")
            self.repo_movies.write_data_to_json("movies")
            self.repo_rentals.write_data_to_json("rentals")

        elif self.repository_type == "SQL_based":
            self.repo_clients.write_data_to_mysql()
            self.repo_movies.write_data_to_mysql()
            self.repo_rentals.write_data_to_mysql()

    def read_data_from_repository(self):
        if self.repository_type == "file_based":
            clients = self.repo_clients.read_data_from_file()
            for c in clients:
                c = c.strip()
                c = c.split(";")
                print(c)
                self.repo_clients.add_new_entity(Client(int(c[0]), c[1]))

            movies = self.repo_movies.read_data_from_file()
            for m in movies:
                m = m.strip()
                m = m.split(";")
                self.repo_movies.add_new_entity(Movie(int(m[0]), m[1], m[2], m[3]))

            rentals = self.repo_rentals.read_data_from_file()
            for r in rentals:
                r = r.strip()
                r = r.split(";")

                rented_date = parse(r[3])
                due_date = parse(r[4])
                return_date = parse(r[5])
                self.repo_rentals.add_new_entity(Rental(int(r[0]), int(r[1]), int(r[2]), rented_date.date(), due_date.date(), return_date.date()))
        if self.repository_type == "binary_file_based":
            self.repo_clients.repo = self.repo_clients.read_from_binary_file()
            self.repo_movies.repo = self.repo_movies.read_from_binary_file()
            self.repo_rentals.repo = self.repo_rentals.read_from_binary_file()

        if self.repository_type == "JSON_based":
            clients = self.repo_clients.read_data_from_json()
            for c in clients["clients"]:
                self.repo_clients.add_new_entity(Client(int(c["id"]), c["name"]))

            movies = self.repo_movies.read_data_from_json()
            for m in movies["movies"]:
                self.repo_movies.add_new_entity(Movie(int(m["id"]), m["title"], m["description"], m["genre"]))

            print("adasdasd")
            rentals = self.repo_rentals.read_data_from_json()
            for r in rentals["rentals"]:
                rented_date = parse(r["rented_date"])
                due_date = parse(r["due_date"])
                return_date = parse(r["return_date"])
                self.repo_rentals.add_new_entity(Rental(int(r["id"]), int(r["client_id"]), int(r["movie_id"]), rented_date.date(), due_date.date(), return_date.date()))

        if self.repository_type == "SQL_based":
            clients = self.repo_clients.read_data_from_mysql()
            for c in clients:
                self.repo_clients.add_new_entity(Client(int(c[0]), c[1]))

            movies = self.repo_movies.read_data_from_mysql()
            for m in movies:
                self.repo_movies.add_new_entity(Movie(int(m[0]), m[1], m[2], m[3]))

            rentals = self.repo_rentals.read_data_from_mysql()
            for r in rentals:
                # rented_date = parse(r[3])
                # due_date = parse(r[4])
                # return_date = parse(r[5])
                self.repo_rentals.add_new_entity(Rental(int(r[0]), int(r[1]), int(r[2]), r[3], r[4], r[5]))

    def run(self):
        """
        Runs the application, that is, carries out all prerequisites(initializations and tests) and starts the menu
        loop

        """
        # prerequisites
        self.read_properties_file()
        if self.repository_type == "memory_based":
            self.init_everything()
        else:
            self.read_data_from_repository()

        # start the menu loop, handled in the UI class
        self.ui.start_menu_loop()


def main():
    # run main app
    main_app = MainApp()
    main_app.run()


if __name__ == "__main__":
    main()