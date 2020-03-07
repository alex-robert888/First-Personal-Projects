using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Atestat_Informatica___Joc_de_Sah.Classes;
using Atestat_Informatica___Joc_de_Sah.Forms;

namespace Atestat_Informatica___Joc_de_Sah.Forms
{
    public partial class FormLogIn : Form
    {
        private ValidareTextBox VTB = new ValidareTextBox();
        private Database database = new Database();

        public FormLogIn()
        {
            InitializeComponent();
        }
        
       


        private bool email_valid_si_existent()
        {
            if(VTB.Email_In_Format_Corespunzator(textBox1) == false)
            {
                MessageBox.Show("Email in format necorespunzator!");
                textBox1.Clear();
                textBox1.BackColor = Color.PaleGoldenrod;
                return false;
            }
                
            else if (VTB.NuContineCaractereSpeciale(textBox1) == false)
            {
                MessageBox.Show("Nu este permisa utilizarea caracterelor speciale!");
                textBox1.Clear();
                textBox1.BackColor = Color.PaleGoldenrod;
                return false;
            }
    
            else if (!VTB.Exista_In_DB(textBox1))
            {
                DialogResult YesOrNo = MessageBox.Show("Email-ul introdus nu corespunde niciunui utilizator. Doriti sa va creati un cont nout?", "Eroare autentificare", MessageBoxButtons.YesNo);

                switch (YesOrNo)
                {
                    case DialogResult.Yes:
                        this.Close();
                        FormCreareCont FCC = new FormCreareCont();
                        FCC.ShowDialog();
                        break;
                    default:
                        textBox1.Clear();
                        textBox1.BackColor = Color.PaleGoldenrod;
                        break;
                }
                return false;
            }

            return true;
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void FormLogIn_Load(object sender, EventArgs e)
        {
            this.BackColor = Global.Color_DarkGrey;

            groupBox1.Visible = true;
            groupBox2.Visible = false;
        }

        private void ButtonContinuati2_Click(object sender, EventArgs e)
        {
            if(database.ParolaPotrivitaEmail(textBox1.Text, textBox2.Text))
            {
                this.Close();
                formMainMenu FMM = new formMainMenu();
                //FMM.Close();
                FMM.Show();
            }
            else
            {
               //MessageBox.Show("Parola introdusa nu corespunde contului dat.");
            }
        }

        private void ButtonContinuati_Click(object sender, EventArgs e)
        {
            if (email_valid_si_existent())
            {
                groupBox1.Visible = false;
                groupBox2.Visible = true;
            }

            Global.conn.Close();

        }
    }
}
