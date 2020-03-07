using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Atestat_Informatica___Joc_de_Sah.Classes;

namespace Atestat_Informatica___Joc_de_Sah.Classes
{
    public class ValidareTextBox
    {
        public ValidareTextBox()
        {
        }

        public bool Exista_In_DB(TextBox txtBox)
        {
            Global.conn.Open();

            SqlCommand CMD_check = new SqlCommand("SELECT COUNT(*) FROM Users where Email = @email", Global.conn);
            CMD_check.Parameters.AddWithValue("@email", txtBox.Text);

            bool exs =  (Convert.ToInt32(CMD_check.ExecuteScalar()) > 0);
            Global.conn.Close();
            return exs;
        }

        public bool NuContineCaractereSpeciale(TextBox txtBox)
        {
            char[] Email = txtBox.Text.ToCharArray();

            foreach (char c in Email)
            {
                if (char.IsLetterOrDigit(c) == false && c != '@' && c != '.') return false;
            }

            return true;
        }

        public bool Email_In_Format_Corespunzator(TextBox txtBox)
        {
            return (txtBox.Text.Contains('@') == true && txtBox.Text.Contains('.') == true);
        }

        public bool Reintroducere_Corecta_Parola(TextBox txtBox1, TextBox txtBox2)
        {
            return (txtBox1.Text.Equals(txtBox2.Text));
        }
    }
}
