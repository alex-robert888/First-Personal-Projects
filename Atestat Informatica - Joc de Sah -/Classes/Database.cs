using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Atestat_Informatica___Joc_de_Sah.Classes;
using Atestat_Informatica___Joc_de_Sah.Forms;
using System.Windows.Forms;

namespace Atestat_Informatica___Joc_de_Sah.Classes
{
    public class Database
    {
        Global global = new Global();

        public Database() { }

        public void InsertUser(List<TextBox> textBoxes)
        {
            Global.conn.Open();

            SqlCommand CMD_insert = new SqlCommand("INSERT INTO Users VALUES(@user, @nume, @prenume, @email, @telefon, @parola)", Global.conn);
            //MessageBox.Show(textBoxes[5].Text);
            CMD_insert.Parameters.AddWithValue("@user", textBoxes[5].Text);
            CMD_insert.Parameters.AddWithValue("@nume", textBoxes[3].Text);
            CMD_insert.Parameters.AddWithValue("@prenume", textBoxes[4].Text);
            CMD_insert.Parameters.AddWithValue("@email", textBoxes[2].Text);
            CMD_insert.Parameters.AddWithValue("@telefon", textBoxes[6].Text);
            CMD_insert.Parameters.AddWithValue("@parola", textBoxes[1].Text);

            CMD_insert.ExecuteNonQuery();
            Global.conn.Close();
        }

        public bool ParolaPotrivitaEmail(string email, string parola)
        {
            Global.conn.Open();

            SqlCommand CMD_select = new SqlCommand("SELECT Parola from Users where Email = @email", Global.conn);
            CMD_select.Parameters.AddWithValue("@email", email);

            bool bun = parola.Equals(CMD_select.ExecuteScalar().ToString());

            Global.conn.Close();

            return bun;
        }
    }
}
