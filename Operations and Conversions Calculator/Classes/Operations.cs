using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Operations_and_Conversions_Calculator.Classes;

namespace Operations_and_Conversions_Calculator
{
    class Operations
    {

        private static Dictionary<string, Func<string, string, int, string>> dict_operations = new Dictionary<string, Func<string, string, int, string>>()
        {
            {"+", Add_Numbers},
            {"-", Substract_Numbers},
            {"*", Multiply_Numbers },
            {"/", Divide_Numbers}
        };

        public static Dictionary<char, int> dict_dig = new Dictionary<char, int>()
        {
            { '0', 0},
            { '1', 1},
            { '2', 2},
            { '3', 3},
            { '4', 4},
            { '5', 5},
            { '6', 6},
            { '7', 7},
            { '8', 8},
            { '9', 9 },
            { 'A', 10},
            { 'B', 11},
            { 'C', 12},
            { 'D', 13},
            { 'E', 14},
            { 'F', 15}
        };


        public static string string_remainder { get; set; }

        Operations()
        {
            Initialize_Parameters();
        }

        private static void Initialize_Parameters()
        {
        }

        public static string Execute_Operation(string operation, string num_1, string num_2, int numeric_base)
        {
            return dict_operations[operation].Invoke(num_1, num_2, numeric_base);
        }

        private static void Fill_with_extra_zeros_the_number_with_less_digits(ref string num_1, ref string num_2)
        {
            if (num_1.Length == num_2.Length)
            {
                return;
            }

            int diff = Math.Abs(num_1.Length - num_2.Length);
            string zeros = "";

            for (int i = 1; i <= diff; ++i)
            {
                zeros += "0";
            }

            // if first number has less digits than the second one
            if (num_1.Length < num_2.Length)
            {
                // swap numbers 
                string temp = num_1;
                num_1 = num_2;
                num_2 = temp;
            }

            num_2 = num_2.Insert(0, zeros);
        }

        /// <summary>
        /// Performs the addition of two numbers in a given base
        /// </summary>
        /// <param name="num_1"></param> First number
        /// <param name="num_2"></param> Second Number
        /// <param name="numeric_base"></param> Base
        public static string Add_Numbers(string num_1, string num_2, int numeric_base)
        {
            char[] result = new char[100];
            int remainder = 0;

            Fill_with_extra_zeros_the_number_with_less_digits(ref num_1, ref num_2);
            int length = num_1.Length;
            for (int i = length - 1; i >= 0; --i)
            {
                int digit_result = remainder + dict_dig[num_1[i]] + dict_dig[num_2[i]];
                remainder = digit_result / numeric_base;
                digit_result = digit_result % numeric_base;

                if (digit_result < 10)
                    result[i] = (char)('0' + digit_result);
                else
                    result[i] = (char)('A' + (digit_result - 10));
            }

            string string_result = new string(result, 0, length);

            if (remainder == 1)
            {
                string_result = string_result.Insert(0, "1");
            }

            return string_result;
        }

        private static void Swap(ref string s1, ref string s2)
        {
            string tmp = s1;
            s1 = s2;
            s2 = tmp;
        }

        public static string Multiply_Numbers(string num_1, string num_2, int numeric_base)
        {
            char[] result = new char[100];
            int remainder = 0;

            if((num_1.Length != 1 && num_2.Length != 1) && numeric_base != 10)
            {
                throw new ArgumentException("Neither the first, nor the second operand is 1 digit.");
            }            

            if (num_1.Length < num_2.Length)
            {
                Swap(ref num_1, ref num_2);
            }

            if (num_2 == "0" || num_1 == "0") return "0";

            int length = num_1.Length;
            for (int i = length - 1; i >= 0; --i)
            {
                int digit_result = remainder + dict_dig[num_1[i]] * dict_dig[num_2[0]];
                remainder = digit_result / numeric_base;
                digit_result = digit_result % numeric_base;

                if (digit_result < 10)
                    result[i] = (char)('0' + digit_result);
                else
                    result[i] = (char)('A' + (digit_result - 10));
            }

            string string_result = new string(result, 0, length);

            if (remainder > 0)
            {
                string_result = string_result.Insert(0, ((char)('0' + remainder)).ToString());
            }

            return string_result;
        }

        public static string Substract_Numbers(string num_1, string num_2, int numeric_base)
        {
            char[] result = new char[100];
            int remainder = 0;

            Fill_with_extra_zeros_the_number_with_less_digits(ref num_1, ref num_2);
            int length = num_1.Length;
            for (int i = length - 1; i >= 0; --i)
            {
                int digit_result = remainder + dict_dig[num_1[i]] - dict_dig[num_2[i]];
                remainder = 0;
                if (digit_result < 0)
                {
                    remainder = -1;
                    digit_result += numeric_base;
                }
                    
                digit_result %= numeric_base;
                result[i] = (char)('0' + digit_result);

                if (digit_result < 10)
                    result[i] = (char)('0' + digit_result);
                else
                    result[i] = (char)('A' + (digit_result - 10));
            }

            string string_result = new string(result, 0, length);
            string_result = string_result.TrimStart(new Char[] { '0' });
            return string_result;
        }

  

        private static void init_with_zero(char[] txt)
        {
            for (int i = 0; i < 64; ++i) txt[i] = '0';
        }

        public static string Divide_Numbers(string num_1, string num_2, int numeric_base)
        {
            char[] result = new char[164];

            init_with_zero(result);                 

            int remainder = 0;

            if (num_2.Length != 1 && num_2 != "10")
            {
                MessageBox.Show(num_2);
                throw new ArgumentException("Error! The second operand must be 1 digit.");
            }

            if (num_1.Length == num_2.Length && num_1[0] < num_2[0])
            {
                string_remainder = num_1;
                return "0";
            }

            int length = num_1.Length;
            if (numeric_base != 16)
                for (int i = 0; i <= length - 1; ++i)
                {
                    remainder = remainder * 10 + dict_dig[num_1[i]];
                    remainder = Convert.ToInt32(Conversions.ExecuteConversion("Succ. Div. or Subst.", remainder.ToString(), numeric_base, 10));
                    result[i] = (char)('0' + (remainder / dict_dig[num_2[0]]));
                    remainder %= dict_dig[num_2[0]];
                }
            else
            {
                num_1 = Conversions.RP(num_1, 16, 2);
                num_1 = Conversions.ExecuteConversion("Succ. Div. or Subst.", num_1, 2, 10);
                num_2 = Conversions.RP(num_2, 16, 2);
                num_2 = Conversions.ExecuteConversion("Succ. Div. or Subst.", num_2, 2, 10);
               
                //num_1 = Conversions.ExecuteConversion("Succ. Div. or Subst.", num_1, 16, 10);
                //num_2 = Conversions.ExecuteConversion("Succ. Div. or Subst.", num_2, 16, 10);

                int int_num_1 = Convert.ToInt32(num_1);
                int int_num_2 = Convert.ToInt32(num_2);

                string str_result = (int_num_1 / int_num_2).ToString();
                str_result = Conversions.ExecuteConversion("Succ. Div. or Subst.", str_result, 10, 2);
                str_result = Conversions.RP(str_result, 2, 16);
                int r = int_num_1 % int_num_2; 
                if(r < 10)
                {
                    string_remainder = r.ToString();
                }
                else
                {
                    string_remainder = ((char)('A' + (r - 10))).ToString();
                }
                return str_result;
            }
            string string_result = new string(result, 0, length);
            string_result = string_result.TrimStart(new Char[] { '0' });
            //string_result = Invert_String(string_result);
            //string_result = string_result.TrimStart(new Char[] { '0' });
            string_remainder = remainder.ToString();

            return string_result;
        }
    }

   
}
