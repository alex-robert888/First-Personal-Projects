
//////////////////////////////////////////////MAIN MENU////////////////////////////////////////////////////


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
using Atestat_Informatica___Joc_de_Sah.Classes.DatabaseHandler;

namespace Atestat_Informatica___Joc_de_Sah
{
    public partial class formMainMenu : Form
    {
        #region get set
        public static Global.PIECE_COLOR whoseTurn { get; set; }
        public static int ID_connected_user { get; set; }

        public static Color ColorForBlack { get; set; }
        public static Color ColorForWhite { get; set; }
        #endregion
        #region Class objects 

        User user = new User();
        PersonalizeazaTabla personalizeazaTabla = new PersonalizeazaTabla();

        #endregion

        public static readonly int[] times = new int[9] { 1, 2, 3, 5, 10, 15, 30, 45, 60 };
        public static readonly int[] delays = new int[] { 2, 3, 5, 15, 30, 60};

        public formMainMenu()
        {
            InitializeComponent();
        }

        private void FormMainMenu_Load(object sender, EventArgs e)
        {
            SomeDesignStuff();
            Initializations();

            ColorForWhite = Color.FromName("WhiteSmoke");
            ColorForBlack = Color.FromName("DarkOrchid");
        }

        #region Event Handlers
        private void ButtonExit_Click(object sender, EventArgs e) => Application.Exit();

        private void ButtonLogIn_Click(object sender, EventArgs e)
        {
            FormLogIn FLI = new FormLogIn();
            FLI.ShowDialog();
        }

        private void ButtonSignIn_Click(object sender, EventArgs e)
        {
            FormCreareCont FCC = new FormCreareCont();
            FCC.ShowDialog();
        }

        private void ButtonNewGame_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrWhiteSpace(comboBox1.Text) || string.IsNullOrWhiteSpace(comboBox2.Text) || string.IsNullOrWhiteSpace(comboBox3.Text))
            {
                MessageBox.Show("Ati lasat campuri goale! Completarea tuturor este obligatorie!");
                return;
            }
            panelSah.Visible = true;

            whoseTurn = Global.PIECE_COLOR.WHITE;

            //MessageBox.Show(ColorForBlack + "  " + ColorForWhite);
            ChessGame chessGame = new ChessGame(ColorForBlack, ColorForWhite, 40, 40);

            //MessageBox.Show(comboBox1.SelectedIndex + " ");
            ChessGame.timers[2] = new Classes.Countdown_Timer.Timer(2, formMainMenu.times[comboBox2.SelectedIndex], 1, 1, new Point(550, 41), formMainMenu.times[comboBox3.SelectedIndex]);
            ChessGame.timers[1] = new Classes.Countdown_Timer.Timer(1, formMainMenu.times[comboBox1.SelectedIndex], 1, 1, new Point(550, 475), formMainMenu.times[comboBox3.SelectedIndex]);


            chessGame.InitGame();

        }


        private void ButtonExit2_Click(object sender, EventArgs e) => Application.Exit();

        #endregion

        private void SomeDesignStuff()
        {
            //Form
            this.BackColor = Color.Gray;

            //Panel din stanga
            this.panelStanga.BackColor = Global.Color_DarkGrey;

            //Panel din mijloc
            this.panelMijloc.BackColor = Global.Color_DarkGrey;

            this.panelSah.BackColor = Global.Color_DarkGrey;
        }

        /// <summary>
        /// Functie ce executa instructiunile de initializare la deschiderea aplicatiei(Meniul principal) 
        /// </summary>
        private void Initializations()
        {
            panelSah.Visible = false;

            //user.Previously_Connected_User();
        }

        private void ResetEverything()
        {
            //panelSah.Hide();

            for(int i = 1; i <= 8; ++i)
            {
                for(int j = 1; j <= 8; ++j)
                {
                    Global.AllPieces[i, j] = null;
                    if(Global.AllPieces[i, j] != null) Global.AllPieces[i, j].piece_pbox.SendToBack();
                    Global.CellsThatCanBeBlocked[i, j] = false;
                    Global.chessBoard[i, j].square.BringToFront();
                    Global.CoveredArea[1, i, j] = 0;
                    Global.CoveredArea[2, i, j] = 0;
                    Global.ListPieces[1].Clear();
                    Global.ListPieces[0].Clear();
                    Global.ListPieces[2].Clear();
                    Player.PiecesAttackingTheKing.Clear();
                }
            }
        }

        private void GameOver()
        {
            ChessGame.timers[1].t.Stop();
            ChessGame.timers[2].t.Stop();

            formMainMenu FMM = new formMainMenu();
            this.panelSah.Visible = false;

            ResetEverything();

            this.Refresh();
        }

        private void FormMainMenu_FormClosing(object sender, FormClosingEventArgs e)
        {
            ChessGame.timers[1].t.Stop();
            ChessGame.timers[2].t.Stop();
        }

        private void PanelSah_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            PersonalizeazaTabla personalizeazaTabla = new PersonalizeazaTabla();
            personalizeazaTabla.ShowDialog();
        }

        private void ButtonAmPierdutBlack_Click(object sender, EventArgs e)
        {
            FormGameOverWhite form = new FormGameOverWhite();
            form.Show();
            GameOver();
        }

        private void ButtonAmPierdutWhite_Click(object sender, EventArgs e)
        {
            FormGameOveBlackr form = new FormGameOveBlackr();
            form.Show();
            GameOver();
        }

        private void PanelStanga_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Button2_Click(object sender, EventArgs e)
        {

        }

        private void Button3_Click(object sender, EventArgs e)
        {

        }

        private void Button1_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog folderBrowserDialog = new OpenFileDialog();

            if(folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                pictureBox3.Image = Image.FromFile(folderBrowserDialog.FileName);
                pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }

        private void GroupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        
        }

        private void PanelMijloc_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Label4_Click(object sender, EventArgs e)
        {

        }

        private void PictureBox3_Click(object sender, EventArgs e)
        {
            
        }

        private void PictureBox3_MouseHover(object sender, EventArgs e)
        {
            pictureBox3.Width = 175;
            pictureBox3.Height = 175;
            pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void PictureBox3_MouseLeave(object sender, EventArgs e)
        {
            pictureBox3.Width = 96;
            pictureBox3.Height = 82;
            pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void ButtonRemizaBlack_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Accepti remiza?", "Some Title", MessageBoxButtons.YesNo);

            if(dialogResult == DialogResult.Yes)
            {
                FormGameOverDraw form = new FormGameOverDraw();
                form.Show();
                GameOver();
            }
            
        }
    }
}
