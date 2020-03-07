using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Atestat_Informatica___Joc_de_Sah.Classes.Board;
using Atestat_Informatica___Joc_de_Sah.Classes.Pieces;
using System.Windows.Forms;

namespace Atestat_Informatica___Joc_de_Sah.Classes
{
    public class ChessGame
    {
        //Variabile-membru
        public static Color m_colorForWhite;
        public static Color m_colorForBlack;
        public static int m_posX;
        public static int m_posY;

        private bool thereIsAPieceSelected;
        private Point selectedPieceCoords;
        public int kingAttacked { get; set; }
        public bool kingMustBeMoved { get; set; }
        private int numberOfMovesPerformed;
        private bool CheckMate;

        formMainMenu FMM = (formMainMenu)Application.OpenForms["formMainMenu"];
        NonAbstract_Pieces nonAbstract_Pieces = new NonAbstract_Pieces();
        public static Countdown_Timer.Timer[] timers { get; set; } = new Countdown_Timer.Timer[3];



        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="colorForWhite"></param>     Utilizatorul poate personaliza culorile tablei
        /// <param name="colorForBlack"></param>     Utilizatorul poate personaliza culorile tablei
        /// <param name="posX"></param>              Coordonata X din care va fi construita tabla
        /// <param name="posY"></param>              Coordonata Y din care va fi construita tabla
        public ChessGame(Color colorForWhite, Color colorForBlack, int posX, int posY)
        {
            m_colorForWhite = colorForWhite;
            m_colorForBlack = colorForBlack;
            m_posX = posX;
            m_posY = posY;

            numberOfMovesPerformed = 0;
            thereIsAPieceSelected = false;
            kingAttacked = 0;
            CheckMate = false;
        }

        private void addSquares()
        {
            for (int y = 1; y <= 8; ++y)
            {
                for (int x = 1; x <= 8; ++x)
                {
                    FMM.panelSah.Controls.Add(Global.chessBoard[y, x].square);
                }
            }
        }

        public void SetUpTimers()
        {
            formMainMenu FMM = new formMainMenu();

           // MessageBox.Show(FMM.comboBox1.SelectedIndex + " ");
            //timers[2] = new Countdown_Timer.Timer(2, formMainMenu.times[FMM.comboBox2.SelectedIndex], 0, 0, new Point(550, 41), formMainMenu.times[FMM.comboBox4.SelectedIndex]);
          //  timers[1] = new Countdown_Timer.Timer(1, formMainMenu.times[FMM.comboBox1.SelectedIndex], 0, 0, new Point(550, 475), formMainMenu.times[FMM.comboBox3.SelectedIndex]);

           // timers[1].StartTimer();
            
            //System.Threading.Thread.Sleep(3000);
        }

        private void AttachClickEventHandler()
        {
            foreach (Control c in FMM.panelSah.Controls)
            {
                if (c is PictureBox)
                {
                    c.Click += pictureBox_Click;
                }
            }
        }
        /// <summary>
        /// Pe langa faptul ca muta vizual piesa, se ocupa de toate celelalte chestiuni de executat in background:
        /// schimbarea datelor din matrici(chessBoard[,], AllPiece[,])
        /// </summary>
        private void HandleAllPieceMovementStuff(int finalX, int finalY)
        {
            nonAbstract_Pieces.movePieceToGivenPosition(selectedPieceCoords.X, selectedPieceCoords.Y, finalX, finalY);
            formMainMenu.whoseTurn = 3 - formMainMenu.whoseTurn;
            thereIsAPieceSelected = false;
            nonAbstract_Pieces.removeHighlightFromAllSquares();

            //Se executa o capturare? Inseamna ca am mutat piesa pe o pozitie corespunzatoare unei piese a adversarului
            if(Global.chessBoard[finalY, finalX].pieceType != Global.PIECE_TYPE.NULL)
            {
                //Pe langa operatiile obisnuite ce trebuie executate la mutarea uneui piese, trebuie sa am in vedere
                //actiunile specifice unei caputrari de piese
                foreach (ChessPiece piece in Global.ListPieces[Convert.ToInt32(Global.chessBoard[finalY, finalX].pieceColor)])
                {
                    if (piece.onBoardLocation.X == finalX && piece.onBoardLocation.Y == finalY)
                    {
                        Global.ListPieces[Convert.ToInt32(Global.chessBoard[finalY, finalX].pieceColor)].Remove(piece);
                        break;
                    }
                }

                nonAbstract_Pieces.Capture_Piece(finalX, finalY);
            }

            //Schimb datele necesare in matrici
            Global.AllPieces[finalY, finalX] = Global.AllPieces[selectedPieceCoords.Y, selectedPieceCoords.X];
            Global.AllPieces[finalY, finalX].onBoardLocation = new Point(finalX, finalY);
            Global.chessBoard[finalY, finalX].pieceType = Global.chessBoard[selectedPieceCoords.Y, selectedPieceCoords.X].pieceType;
            Global.chessBoard[finalY, finalX].pieceColor = Global.AllPieces[finalY, finalX].getPieceColor;


            Global.AllPieces[selectedPieceCoords.Y, selectedPieceCoords.X] = null;
            Global.chessBoard[selectedPieceCoords.Y, selectedPieceCoords.X].pieceType = Global.PIECE_TYPE.NULL;
            Global.chessBoard[selectedPieceCoords.Y, selectedPieceCoords.X].pieceColor = Global.PIECE_COLOR.NULL;

            //backgroundul piesei este la fel cu culoarea noii pozitii?
            Global.AllPieces[finalY, finalX].piece_pbox.BackColor = Global.chessBoard[finalY, finalX].square.BackColor;
        }
        
        bool isThisCheckmate()
        {
            return false;
        }

        private void RemoveIncorrectHighlights()
        {
            //MessageBox.Show("remove incorrect highlights");
            for(int i = 1; i <= 8; ++i)
            {
                for(int j = 1; j <= 8; ++j)
                {
                    if(i == 6)
                    //MessageBox.Show("" + Global.Highlighted_Square[i, j] + Global.CellsThatCanBeBlocked[i, j]);
                    if(Global.Highlighted_Square[i, j] == true && Global.CellsThatCanBeBlocked[i, j] == false)
                    {
                        Global.HLSquares[i, j].Visible = false;
                    }
                }
            }
        }

        private void pictureBox_Click(object sender, EventArgs e)
        { 


            PictureBox pictureBox = (PictureBox)sender;

            //calculez coordonatele patratului selectat
            Square square = new Square();
            Point square_location = square.getSquareLocation(pictureBox);

            switch (thereIsAPieceSelected)
            {
           

                //daca nu exista piesa selectata, jucatorul trebuie sa selcteze una
                case false:



                    //daca regele este atacat de mai mult decat o piesa, atunci regele trebuie mutat in mod obligatoriu
                    if (kingAttacked >= 2)
                    {
                        //MessageBox.Show(formMainMenu.whoseTurn + " King is attacked! King must be moved");
                        kingMustBeMoved = true;
                    }

                    //daca regele e atacat de o singura piesa, este posibil sa blochez cu o alta piesa sahul?
                    else if(kingAttacked == 1)
                    {
                        //Player player = new Player();
                        //Player.Determine_Area_Covered_By_Player(formMainMenu.whoseTurn);
                        //kingMustBeMoved = true;
                    }

                    //MessageBox.Show(Global.PinnedPieces[square_location.Y, square_location.X] + "");

                    if (kingMustBeMoved && square_location.X == Player.KingPosition[Convert.ToInt32(formMainMenu.whoseTurn)].X && square_location.Y == Player.KingPosition[Convert.ToInt32(formMainMenu.whoseTurn)].Y)
                    {
                        Global.AllPieces[square_location.Y, square_location.X].Highlight_Legal_Moves_Visually();
                        thereIsAPieceSelected = true;
                        selectedPieceCoords = new Point(square_location.X, square_location.Y);
                    }



                    else if (!kingMustBeMoved && Global.chessBoard[square_location.Y, square_location.X].pieceType != Global.PIECE_TYPE.NULL
                    && Global.chessBoard[square_location.Y, square_location.X].pieceColor == formMainMenu.whoseTurn)
                    {
                        Global.AllPieces[square_location.Y, square_location.X].Highlight_Legal_Moves_Visually();

                        if(kingAttacked == 1)
                        {
                            RemoveIncorrectHighlights();
                        }

                        thereIsAPieceSelected = true;
                        selectedPieceCoords = new Point(square_location.X, square_location.Y);
                    }
                    break;

                //daca exista piesa selectata, jucatorul trebuie sa aleaga pozitia in care muta sau sa selecteze alta piesa
                default: //case true:

                    int finalX = square_location.X;
                    int finalY = square_location.Y;

                    bool ok = true;
                    if (kingAttacked == 1 && Global.AllPieces[selectedPieceCoords.Y, selectedPieceCoords.X].getPieceType != Global.PIECE_TYPE.KING)
                    {
                        ok = Global.CellsThatCanBeBlocked[finalY, finalX];
                    }

                    //daca jucatorul a optat pentru o mutare corecta
                    if (ok == true && (Global.chessBoard[square_location.Y, square_location.X].pieceColor != Global.chessBoard[selectedPieceCoords.Y, selectedPieceCoords.X].pieceColor)
                    && nonAbstract_Pieces.ValidMove(finalX, finalY))
                    {
                        HandleAllPieceMovementStuff(finalX, finalY);

                        int color = Convert.ToInt32(3 - Global.chessBoard[square_location.Y, square_location.X].pieceColor);

                        if (Global.chessBoard[finalY, finalX].pieceType == Global.PIECE_TYPE.KING)
                        {
                            Player.KingPosition[3 - color] = new Point(finalX, finalY);
                        }


                        for (int i = 1; i <= 8; ++i)
                        {
                            for (int j = 1; j <= 8; ++j)
                            {
                                Global.PinnedPieces[i, j] = 0;
                            }
                        }

                        //caut piesele "pinned" de rege, adica care nu pot fi mutate oriunde 
                        //fiindca as lasa regele descoperit(in sah)
                        Player player = new Player();

                        //completez matricile de cover
                        foreach(ChessPiece piece in Global.ListPieces[1])
                        {
                            piece.AllPossibleMoves.Clear();
                        }
                        foreach (ChessPiece piece in Global.ListPieces[2])
                        {
                            piece.AllPossibleMoves.Clear();
                        }
                        Player.PiecesAttackingTheKing.Clear();

                        player.FindPinnedPieces(Player.KingPosition[color]);
                        player.FindPinnedPieces(Player.KingPosition[3 - color]);

                        player.Determine_Area_Covered_By_Player(3 - formMainMenu.whoseTurn);
                        player.Determine_Area_Covered_By_Player(formMainMenu.whoseTurn);


                        King king = (King)Global.AllPieces[Player.KingPosition[color].Y, Player.KingPosition[color].X];


                        //determin daca regele e in sah, respectiv numarul de piese care ataca regele
                        kingAttacked = king.IsKingInCheck();


                        if (kingAttacked > 0)
                        {
                            if(isThisCheckmate() == true)
                            {
                                //CheckMate = true;
                                //MessageBox.Show("SAH MAT");
                            }

                            if(kingAttacked == 1)
                            {
                                player.LookForMovesToCaptureOrBlockChecker();
                            }
                            //Console.WriteLine(king.getPieceColor + " In check");
                        }
                        else
                        {
                            kingMustBeMoved = false;
                            Console.WriteLine(king.getPieceColor + " NOT in check");
                        }


                        timers[Convert.ToInt32(formMainMenu.whoseTurn)].StartTimer();
                        timers[3 - Convert.ToInt32(formMainMenu.whoseTurn)].StopTimer();

                    }

                    //Vrea sa selecteze o piesa noua?
                    else if (!kingMustBeMoved && Global.chessBoard[square_location.Y, square_location.X].pieceType != Global.PIECE_TYPE.NULL
                    && Global.chessBoard[square_location.Y, square_location.X].pieceColor == formMainMenu.whoseTurn)
                    {
                        nonAbstract_Pieces.removeHighlightFromAllSquares();
                        Global.AllPieces[square_location.Y, square_location.X].Highlight_Legal_Moves_Visually();
                        if (kingAttacked == 1)
                        {
                            RemoveIncorrectHighlights();
                        }
                        thereIsAPieceSelected = true;
                        selectedPieceCoords = new Point(square_location.X, square_location.Y);
                    }

                    break;
            }



        }
    

    /// <summary>
    /// Aceasta va fi functia chemata din form-ul Main Menu pentru inceperea jocului de sah
    /// la apasarea butonului "Joc Nou"
    /// </summary>
        public void InitGame()
        {
            ChessBoard m_chessBoard = new ChessBoard(m_colorForWhite, m_colorForBlack, m_posX, m_posY);

            //Desenare tabla de joc
            m_chessBoard.Build_And_Draw_Chess_Board();

            //Adaugare controale(patratelele de la tabla de sah) in form-ul FormMainMenu
            addSquares();     

            //Aranjez piesele de sah in configuratia initiala specifica
            m_chessBoard.Configuratie_Initiala();

            m_chessBoard.Fill_Table_With_Highlight_Squares();

            //Atasez event-ul de click pentru piesele de sah
            AttachClickEventHandler();;

            timers[1].StartTimer();
            //timers[2].StopTimer();

            Player player = new Player();
            player.Determine_Area_Covered_By_Player(3 - formMainMenu.whoseTurn);
            player.Determine_Area_Covered_By_Player(formMainMenu.whoseTurn);


        }

    }
}
