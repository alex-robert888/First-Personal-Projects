using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Atestat_Informatica___Joc_de_Sah.Classes;
using Atestat_Informatica___Joc_de_Sah.Forms;

namespace Atestat_Informatica___Joc_de_Sah.Forms
{
    public partial class FormCreareCont : Form
    {
        private ValidareTextBox VTB = new ValidareTextBox();
        private List<TextBox> TxtBoxes= new List<TextBox>();
        private Database DB = new Database();

        public FormCreareCont()
        {
            InitializeComponent();
        }

        private void FormCreareCont_Load(object sender, EventArgs e)
        {
            this.BackColor = Global.Color_DarkGrey;

            TxtBoxes.Add(textBoxConfParola);    //0
            TxtBoxes.Add(textBoxParola);        //1
            TxtBoxes.Add(textBoxEmail);         //2
            TxtBoxes.Add(textBoxNume);          //3
            TxtBoxes.Add(textBoxPrenume);       //4
            TxtBoxes.Add(textBoxUsername);      //5
            TxtBoxes.Add(textBoxTelefon);       //6
        }

        private bool Campuri_Goale()
        {
            bool are = false;
            foreach(TextBox tbox in TxtBoxes)
            {
                if (string.IsNullOrWhiteSpace(tbox.Text) && tbox.Name != "textBoxTelefon")
                {
                    tbox.Clear();
                    tbox.BackColor = Color.PaleGoldenrod;
                    are = true;
                }
            }

            return are;
        }


        private void ButtonCreatiContul_Click(object sender, EventArgs e)
        {
            if (Campuri_Goale())
            {
                MessageBox.Show("Nu ati completat toate campurile obligatorii");
            }
            else if (!VTB.Email_In_Format_Corespunzator(textBoxEmail))
            {
                MessageBox.Show("Email in format necorespunzator!");
                textBoxEmail.Clear();
                textBoxEmail.BackColor = Color.PaleGoldenrod;
            }

            else if (VTB.NuContineCaractereSpeciale(textBoxConfParola) == false
                || VTB.NuContineCaractereSpeciale(textBoxNume) == false || VTB.NuContineCaractereSpeciale(textBoxPrenume) == false
                || VTB.NuContineCaractereSpeciale(textBoxUsername) == false
                )
            {
                MessageBox.Show("Nu este permisa utilizarea caracterelor speciale!");
            }

            else if (VTB.Exista_In_DB(textBoxEmail))
            {
                DialogResult YesOrNo = MessageBox.Show("Email-ul introdus corespunde deja altui utilizator. Doriti sa va logati pe contul respectiv?", "Eroare autentificare", MessageBoxButtons.YesNo);

                switch (YesOrNo)
                {
                    case DialogResult.Yes:
                        this.Close();
                        FormLogIn FLI = new FormLogIn();
                        FLI.ShowDialog();
                        break;
                    default:
                        textBoxEmail.Clear();
                        textBoxEmail.BackColor = Color.PaleGoldenrod;
                        break;
                }
            }
            else if (VTB.Reintroducere_Corecta_Parola(textBoxParola, textBoxConfParola) == false)
            {
                MessageBox.Show("Parola nu a fost corect confirmata!");
                textBoxParola.Clear();
                textBoxConfParola.Clear();
                textBoxParola.BackColor = Color.PaleGoldenrod;
                textBoxConfParola.BackColor = Color.PaleGoldenrod;
            }
            
            else
            {
                DB.InsertUser(TxtBoxes);
            }
        }

        private void TextBoxEmail_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
