using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Atestat_Informatica___Joc_de_Sah.Forms
{
    public partial class FormGuest : Form
    {
        public FormGuest()
        {
            InitializeComponent();
        }

        private void FormGuest_Load(object sender, EventArgs e)
        {

        }

        private void PictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {

        }

        private void ButtonExit2_Click(object sender, EventArgs e) => Application.Exit();

        private void Button2_Click(object sender, EventArgs e)
        {
            FormLogIn FLI = new FormLogIn();
            FLI.ShowDialog();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            FormCreareCont FCC = new FormCreareCont();
            FCC.ShowDialog();
        }
    }
}
