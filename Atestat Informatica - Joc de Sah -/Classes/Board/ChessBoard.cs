using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Atestat_Informatica___Joc_de_Sah.Classes;
using Atestat_Informatica___Joc_de_Sah.Classes.Board;
using Atestat_Informatica___Joc_de_Sah.Forms;
using System.Windows.Forms;
using Atestat_Informatica___Joc_de_Sah.Classes.Pieces;

namespace Atestat_Informatica___Joc_de_Sah.Classes
{

    public class ChessBoard
    {
        public static Color m_colorForWhite;
        public static Color m_colorForBlack;
        public static int m_posX;
        public static int m_posY;
        formMainMenu FMM = (formMainMenu)Application.OpenForms["formMainMenu"];

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="colorForWhite"></param>     Utilizatorul poate personaliza culorile tablei
        /// <param name="colorForBlack"></param>     Utilizatorul poate personaliza culorile tablei
        /// <param name="posX"></param>              Coordonata X din care va fi construita tabla
        /// <param name="posY"></param>              Coordonata Y din care va fi construita tabla
        public ChessBoard(Color colorForWhite, Color colorForBlack, int posX, int posY)
        {
            m_colorForWhite = colorForWhite;
            m_colorForBlack = colorForBlack;
            m_posX = posX;
            m_posY = posY;
        }

        /// <summary>
        /// Va construi in memorie si va desena tabla de sah pe panelSah, din Form-ul Main Menu 
        /// </summary>
        public void Build_And_Draw_Chess_Board()
        {
            for(int y = 1; y <= 8; ++y)
            {
                for(int x = 1; x <= 8; ++x)
                {
                    Color thisSquareColor = ((x + y) % 2 == 0) ? m_colorForWhite : m_colorForBlack;
                    Point position = new Point(m_posX + (x - 1) * Global.square_width, m_posY + (y - 1) * Global.square_width);

                    Global.chessBoard[y, x] = new Square(thisSquareColor, position);
                }
            }
        }

        public void Fill_Table_With_Highlight_Squares()
        {
            for(int y = 1; y <= 8; ++y)
            {
                for(int x = 1; x <= 8; ++x)
                {
                    Global.HLSquares[y, x] = new PictureBox();
                    Global.HLSquares[y, x].BackColor = Global.HighLight_Color;
                    Global.HLSquares[y, x].Width = Global.square_width;
                    Global.HLSquares[y, x].Height = Global.square_height;
                    Global.HLSquares[y, x].Location = new Point(40 + (x - 1) * 60, 40 + (y - 1) * 60);
                    FMM.panelSah.Controls.Add(Global.HLSquares[y, x]);
                    Global.HLSquares[y, x].BringToFront();
                    Global.HLSquares[y, x].Visible = false;
                }
            }
        }

        /// <summary>
        /// Adds a given control to a given panel
        /// </summary>
        private void AddControlToPanel(Control givenControl)
        {
            FMM.panelSah.Controls.Add(givenControl);
            givenControl.BringToFront();
        }


        


        /// <summary>
        /// Asez toate piesele de fiecare culoare in pozitiile lor initiale specifice
        /// </summary>
        private void Aranjare_Ture()
        {
            //setez tipul de piesa pentru celula
            Global.chessBoard[1, 1].pieceType = Global.PIECE_TYPE.ROOK;
            Global.chessBoard[1, 8].pieceType = Global.PIECE_TYPE.ROOK;
            Global.chessBoard[8, 1].pieceType = Global.PIECE_TYPE.ROOK;
            Global.chessBoard[8, 8].pieceType = Global.PIECE_TYPE.ROOK;

            Global.chessBoard[1, 1].pieceColor = Global.PIECE_COLOR.BLACK;
            Global.chessBoard[1, 8].pieceColor = Global.PIECE_COLOR.BLACK;
            Global.chessBoard[8, 1].pieceColor = Global.PIECE_COLOR.WHITE;
            Global.chessBoard[8, 8].pieceColor = Global.PIECE_COLOR.WHITE;

            //desenez turele in celulele corespunzatoare
            Rook blackRook1 = new Rook(Global.chessBoard[1, 1], Image.FromFile("Black Rook.png"));
            AddControlToPanel(blackRook1.img_piece);
            Global.AllPieces[1, 1] = blackRook1;
            Console.WriteLine(Convert.ToInt32(Global.PIECE_COLOR.BLACK));
            Global.ListPieces[Convert.ToInt32(Global.PIECE_COLOR.BLACK)].Add(blackRook1);

            Rook blackRook2 = new Rook(Global.chessBoard[1, 8], Image.FromFile("Black Rook.png"));
            AddControlToPanel(blackRook2.img_piece);
            Global.AllPieces[1, 8] = blackRook2;
            Global.ListPieces[Convert.ToInt32(Global.PIECE_COLOR.BLACK)].Add(blackRook2);

            Rook whiteRook1 = new Rook(Global.chessBoard[8, 1], Image.FromFile("White Rook.png"));
            AddControlToPanel(whiteRook1.img_piece);
            Global.AllPieces[8, 1] = whiteRook1;
            Global.ListPieces[Convert.ToInt32(Global.PIECE_COLOR.WHITE)].Add(whiteRook1);

            Rook whiteRook2 = new Rook(Global.chessBoard[8, 8], Image.FromFile("White Rook.png"));
            AddControlToPanel(whiteRook2.img_piece);
            Global.AllPieces[8, 8] = whiteRook2;
            Global.ListPieces[Convert.ToInt32(Global.PIECE_COLOR.WHITE)].Add(whiteRook2);
        }

        private void Aranjare_Cai()
        {
            
            Global.chessBoard[1, 2].pieceType = Global.PIECE_TYPE.KNIGHT;
            Global.chessBoard[1, 7].pieceType = Global.PIECE_TYPE.KNIGHT;
            Global.chessBoard[8, 2].pieceType = Global.PIECE_TYPE.KNIGHT;
            Global.chessBoard[8, 7].pieceType = Global.PIECE_TYPE.KNIGHT;

            Global.chessBoard[1, 2].pieceColor = Global.PIECE_COLOR.BLACK;
            Global.chessBoard[1, 7].pieceColor = Global.PIECE_COLOR.BLACK;
            Global.chessBoard[8, 2].pieceColor = Global.PIECE_COLOR.WHITE;
            Global.chessBoard[8, 7].pieceColor = Global.PIECE_COLOR.WHITE;

            //desenez turele in celulele corespunzatoare
            Knight blackKnight1 = new Knight(Global.chessBoard[1, 2], Image.FromFile("Black Knight.png"));
            AddControlToPanel(blackKnight1.img_piece);
            Global.AllPieces[1, 2] = blackKnight1;
            Global.ListPieces[Convert.ToInt32(Global.PIECE_COLOR.BLACK)].Add(blackKnight1);

            Knight blackKnight2 = new Knight(Global.chessBoard[1, 7], Image.FromFile("Black Knight.png"));
            AddControlToPanel(blackKnight2.img_piece);
            Global.AllPieces[1, 7] = blackKnight2;
            Global.ListPieces[Convert.ToInt32(Global.PIECE_COLOR.BLACK)].Add(blackKnight2);

            Knight whiteKnight1 = new Knight(Global.chessBoard[8, 2], Image.FromFile("White Knight.png"));
            AddControlToPanel(whiteKnight1.img_piece);
            Global.AllPieces[8, 2] = whiteKnight1;
            Global.ListPieces[Convert.ToInt32(Global.PIECE_COLOR.WHITE)].Add(whiteKnight1);

            Knight whiteKnight2 = new Knight(Global.chessBoard[8, 7], Image.FromFile("White Knight.png"));
            AddControlToPanel(whiteKnight2.img_piece);
            Global.AllPieces[8, 7] = whiteKnight2;
            Global.ListPieces[Convert.ToInt32(Global.PIECE_COLOR.WHITE)].Add(whiteKnight2);
        }

        private void Aranjare_Nebuni()
        {
            Global.chessBoard[1, 3].pieceType = Global.PIECE_TYPE.BISHOP;
            Global.chessBoard[1, 6].pieceType = Global.PIECE_TYPE.BISHOP;
            Global.chessBoard[8, 3].pieceType = Global.PIECE_TYPE.BISHOP;
            Global.chessBoard[8, 6].pieceType = Global.PIECE_TYPE.BISHOP;

            Global.chessBoard[1, 3].pieceColor = Global.PIECE_COLOR.BLACK;
            Global.chessBoard[1, 6].pieceColor = Global.PIECE_COLOR.BLACK;
            Global.chessBoard[8, 3].pieceColor = Global.PIECE_COLOR.WHITE;
            Global.chessBoard[8, 6].pieceColor = Global.PIECE_COLOR.WHITE;

            //desenez turele in celulele corespunzatoare
            Bishop blackBishop1 = new Bishop(Global.chessBoard[1, 3], Image.FromFile("Black Bishop.png"));
            AddControlToPanel(blackBishop1.img_piece);
            Global.AllPieces[1, 3] = blackBishop1;
            Global.ListPieces[Convert.ToInt32(Global.PIECE_COLOR.BLACK)].Add(blackBishop1);

            Bishop blackBishop2 = new Bishop(Global.chessBoard[1, 6], Image.FromFile("Black Bishop.png"));
            AddControlToPanel(blackBishop2.img_piece);
            Global.AllPieces[1, 6] = blackBishop2;
            Global.ListPieces[Convert.ToInt32(Global.PIECE_COLOR.BLACK)].Add(blackBishop2);

            Bishop whiteBishop1 = new Bishop(Global.chessBoard[8, 3], Image.FromFile("White Bishop.png"));
            AddControlToPanel(whiteBishop1.img_piece);
            Global.AllPieces[8, 3] = whiteBishop1;
            Global.ListPieces[Convert.ToInt32(Global.PIECE_COLOR.WHITE)].Add(whiteBishop1);

            Bishop whiteBishop2 = new Bishop(Global.chessBoard[8, 6], Image.FromFile("White Bishop.png"));
            AddControlToPanel(whiteBishop2.img_piece);
            Global.AllPieces[8, 6] = whiteBishop2;
            Global.ListPieces[Convert.ToInt32(Global.PIECE_COLOR.WHITE)].Add(whiteBishop2);
        }

        private void Aranjare_Regine()
        {
            Global.chessBoard[1, 4].pieceType = Global.PIECE_TYPE.QUEEN;
            Global.chessBoard[8, 4].pieceType = Global.PIECE_TYPE.QUEEN;

            Global.chessBoard[1, 4].pieceColor = Global.PIECE_COLOR.BLACK;
            Global.chessBoard[8, 4].pieceColor = Global.PIECE_COLOR.WHITE;

            //desenez turele in celulele corespunzatoare
            Queen blackQueen1 = new Queen(Global.chessBoard[1, 4], Image.FromFile("Black Queen.png"));
            AddControlToPanel(blackQueen1.img_piece);
            Global.AllPieces[1, 4] = blackQueen1;
            Global.ListPieces[Convert.ToInt32(Global.PIECE_COLOR.BLACK)].Add(blackQueen1);

            Queen whiteQueen1 = new Queen(Global.chessBoard[8, 4], Image.FromFile("White Queen.png"));
            AddControlToPanel(whiteQueen1.img_piece);
            Global.AllPieces[8, 4] = whiteQueen1;
            Global.ListPieces[Convert.ToInt32(Global.PIECE_COLOR.WHITE)].Add(whiteQueen1);
        }

        private void Aranjare_Regi()
        {
            Global.chessBoard[1, 5].pieceType = Global.PIECE_TYPE.KING;
            Global.chessBoard[8, 5].pieceType = Global.PIECE_TYPE.KING;

            Global.chessBoard[1, 5].pieceColor = Global.PIECE_COLOR.BLACK;
            Global.chessBoard[8, 5].pieceColor = Global.PIECE_COLOR.WHITE;

            //desenez turele in celulele corespunzatoare
            King blackKing1 = new King(Global.chessBoard[1, 5], Image.FromFile("Black King.png"));
            AddControlToPanel(blackKing1.img_piece);
            Global.AllPieces[1, 5] = blackKing1;
            Global.ListPieces[Convert.ToInt32(Global.PIECE_COLOR.BLACK)].Add(blackKing1);

            King whiteKing1 = new King(Global.chessBoard[8, 5], Image.FromFile("White King.png"));
            AddControlToPanel(whiteKing1.img_piece);
            Global.AllPieces[8, 5] = whiteKing1;
            Global.ListPieces[Convert.ToInt32(Global.PIECE_COLOR.WHITE)].Add(whiteKing1);

            Player.KingPosition[Convert.ToInt32(Global.PIECE_COLOR.WHITE)] = new Point(5, 8);
            Player.KingPosition[Convert.ToInt32(Global.PIECE_COLOR.BLACK)] = new Point(5, 1);

        }

        private void Aranjare_Pioni()
        {
            for(int i = 1; i <= 8; ++i)
            {
                Global.chessBoard[2, i].pieceType = Global.PIECE_TYPE.PAWN;
                Global.chessBoard[7, i].pieceType = Global.PIECE_TYPE.PAWN;

                Global.chessBoard[2, i].pieceColor = Global.PIECE_COLOR.BLACK;
                Global.chessBoard[7, i].pieceColor = Global.PIECE_COLOR.WHITE;

                Pawn blackPawn1 = new Pawn(Global.chessBoard[2, i], Image.FromFile("Black Pawn.png"));
                AddControlToPanel(blackPawn1.img_piece);
                Global.AllPieces[2, i] = blackPawn1;
                Global.ListPieces[Convert.ToInt32(Global.PIECE_COLOR.BLACK)].Add(blackPawn1);

                Pawn whitePawn1 = new Pawn(Global.chessBoard[7, i], Image.FromFile("White Pawn.png"));
                AddControlToPanel(whitePawn1.img_piece);
                Global.AllPieces[7, i] = whitePawn1;
                Global.ListPieces[Convert.ToInt32(Global.PIECE_COLOR.WHITE)].Add(whitePawn1);
            }

        }

        private void Refresh_And_Reinit()
        {
            Global.ListPieces[0] = new List<ChessPiece>();
            Global.ListPieces[1] = new List<ChessPiece>();
            Global.ListPieces[2] = new List<ChessPiece>();
        }

        /// <summary>
        /// Asez toate piesele de fiecare culoare in pozitiile lor initiale specifice
        /// </summary>
        public void Configuratie_Initiala()
        {
            Refresh_And_Reinit();

            Aranjare_Ture();
            Aranjare_Cai();
            Aranjare_Nebuni();
            Aranjare_Regine();
            Aranjare_Regi();
            Aranjare_Pioni();



        }
    }
}
