using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Operations_and_Conversions_Calculator.Classes
{
    class Conversions
    {
        private static Dictionary<string, Func<string, int, int, string>> dict_conversions = new Dictionary<string, Func<string, int, int,string>>()
        {
            {"Succesive Divisions Method", Succesive_Divisions_Method},
            {"Substitution Method", Substitution_Method},
            {"10 As Interm. Base", Ten_As_Intermediate_Base},
            {"Rapid Conversion", Rapid_Conversion}
        };

        private static List<string> list_rapid_conversion_into_base_2 = new List<string>()
        {
            "0000", "0001",
            "0010", "0011",
            "0100", "0101", "0110", "0111",
            "1000", "1001", "1010", "1011", "1100", "1101", "1110", "1111"
        };

        public static Dictionary<char, string> dict_to_base_2 = new Dictionary<char, string>()
        {
            { '0', "0000"},
            { '1', "0001"},
            { '2', "0010"},
            { '3', "0011"},
            { '4', "0100"},
            { '5', "0101"},
            { '6', "0110"},
            { '7', "0111"},
            { '8', "1000"},
            { '9', "1001" },
            { 'A', "1010"},
            { 'B', "1011"},
            { 'C', "1100"},
            { 'D', "1101"},
            { 'E', "1110"},
            { 'F', "1111"}
        };

        public static Dictionary<string, char> dict_from_base_2 = new Dictionary<string, char>()
        {
            { "0000", '0'},
            { "0001", '1'},
            { "0010", '2'},
            { "0011", '3'},
            { "0100", '4'},
            { "0101", '5'},
            { "0110", '6'},
            { "0111", '7'},
            { "1000", '8'},
            { "1001", '9'},
            { "1010", 'A'},
            { "1011", 'B'},
            { "1100", 'C'},
            { "1101", 'D'},
            { "1110", 'E'},
            { "1111", 'F'}
        };

        Conversions()
        {

        }
        private static string Invert_String(string to_reverse)
        {
            string new_string = "";
            for (int i = to_reverse.Length - 1; i >= 0; --i)
            {
                new_string += to_reverse[i];
            }
            return new_string;
        }

        private static string Succesive_Divisions_Method(string num_1, int source_base, int destination_base)
        {
            // divide by destination base
            // operations must be executed in the source base
            char[] result = new char[100];
            int countR = 0;

            while(num_1 != "0")
            {
                if(destination_base != 10)
                    num_1 = Operations.Divide_Numbers(num_1, destination_base.ToString(), source_base);
                else
                    num_1 = Operations.Divide_Numbers(num_1, "A", source_base);

                if (num_1 == "") num_1 = "0";

                result[countR++] = (char)('0' + Convert.ToInt32(Operations.string_remainder));
            }

            string string_result = new string(result, 0, countR);
            string_result = Invert_String(string_result);
            return string_result;
         }

        private static string Substitution_Method(string num_1, int source_base, int destination_base)
        {
            string sum = "0";
            string pow = "1";

            string sb = source_base.ToString();
            if (sb == "10") sb = "A";

            for (int i = num_1.Length - 1; i >= 0; --i)
            {
                char digit = num_1[i];
                string prod = "";
                prod = Operations.Multiply_Numbers(digit.ToString(), pow.ToString(), destination_base);
                sum = Operations.Add_Numbers(prod, sum.ToString(), destination_base);
                pow = Operations.Multiply_Numbers(pow.ToString(), sb, destination_base);
            }

            return sum.ToString();
        }

        private static string Ten_As_Intermediate_Base(string num_1, int source_base, int destination_base)
        {
            string num_in_base_10 = ExecuteConversion("Succ. Div. or Subst.", num_1, source_base, 10);
            return ExecuteConversion("Succ. Div. or Subst.", num_in_base_10, 10, destination_base);
        }


        
        public static string RP(string num_1, int source_base, int destination_base)
        {
            string result = "";

            if(!((source_base == 2 || source_base == 4 || source_base == 8 || source_base == 16) &&
                (destination_base == 2 || destination_base == 4 || destination_base == 8 || destination_base == 16)))
            {
                return "Invalid Bases";
            }

            
            if (destination_base != 2)
            {  
                 string strHex = Convert.ToInt32(num_1, 2).ToString("X");
                 return strHex;         
            }
            

            else
            {
                int how_many = 0;
                if (source_base == 4) how_many = 2;
                if (source_base == 8) how_many = 3;
                if (source_base == 16) how_many = 4;

                for(int i = 0; i < num_1.Length; ++i)
                {
                    string group = "";
                    for(int j = 4 - how_many; j <= 3; ++j)
                    {
                        group += list_rapid_conversion_into_base_2[Operations.dict_dig[num_1[i]]][j].ToString();
                    }

                    result += group;
                }

                result.TrimStart(new Char[] { '0' });
            }

            return result;
        }
        
        private static string Rapid_Conversion_With_Destination_Base_2(string num, int source_base)
        {
            int how_many;
            if (source_base == 4) how_many = 2;
            else if (source_base == 8) how_many = 3;
            else how_many = 4;

            string result = "";

            foreach (char c in num)
            {
                for (int i = 4 - how_many; i < 4; ++i)
                {
                    result += dict_to_base_2[c][i];
                }
            }

            return result;
        }

        private static string Rapid_Conversion_With_Source_Base_2(string num, int destination_base)
        {
            int how_many;
            if (destination_base == 4) how_many = 2;
            else if (destination_base == 8) how_many = 3;
            else how_many = 4;

            string result = "";
            string group = "";
            for (int i = 1; i <= num.Length % how_many; ++i)
            {
                group += '0';
            }

            int j = 0;
            while (j < num.Length)
            {
                group += num[j];

                if (group.Length == how_many)
                {
                    for (int i = how_many; i < 4; ++i) group = "0" + group;
                    result += dict_from_base_2[group];
                    group = "";
                }

                ++j;
            }

            return result;
        }

        /// <summary>
        /// Executes a rapid conversions provided that both the source and destination bases are either 2, 4, 8 or 16
        /// </summary>
        /// <param name="num"> The number to be converted </param>
        /// <param name="source_base"> The source base of the number </param>
        /// <param name="destination_base"> The destination base of the number </param>
        private static string Rapid_Conversion(string num, int source_base, int destination_base)
        {
            if (!((source_base == 2 || source_base == 4 || source_base == 8 || source_base == 16) &&
                (destination_base == 2 || destination_base == 4 || destination_base == 8 || destination_base == 16)))
            {
                return "Invalid Bases";
            }

            if (destination_base == 2)
            {
                num = Rapid_Conversion_With_Destination_Base_2(num, source_base);
            }

            else if (source_base == 2)
            {
                num = Rapid_Conversion_With_Source_Base_2(num, destination_base);
            }
            else
            {
                num = Rapid_Conversion_With_Destination_Base_2(num, source_base);
                num = Rapid_Conversion_With_Source_Base_2(num, destination_base);
            }

            num.TrimStart(new Char[] { '0' });

            return num;
        }
        public static string ExecuteConversion(string conversion, string num_1, int source_base, int destination_base)
        {
            if (source_base == destination_base) return num_1;

            if (conversion == "10 As Interm. Base" || conversion == "Rapid Conversion")
            {
                return dict_conversions[conversion].Invoke(num_1, source_base, destination_base);
            }
            else
            {
                if (source_base < destination_base) return dict_conversions["Substitution Method"].Invoke(num_1, source_base, destination_base);
                else return dict_conversions["Succesive Divisions Method"].Invoke(num_1, source_base, destination_base);
            }
        }
    }
}
