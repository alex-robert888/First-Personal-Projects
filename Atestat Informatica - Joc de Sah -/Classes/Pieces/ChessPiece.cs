using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Atestat_Informatica___Joc_de_Sah.Classes.Board;
using System.Windows.Forms;

namespace Atestat_Informatica___Joc_de_Sah.Classes
{
    public abstract class ChessPiece
    {
        public static Image m_image;
        public static Square m_square;

        public PictureBox piece_pbox { get; set; }

        public Global.PIECE_TYPE getPieceType { get; set; }
        public Point onBoardLocation { get; set; }
        public Global.PIECE_COLOR getPieceColor { get; set; }
        public static Queue<PictureBox> queue_of_high_pboxes = new Queue<PictureBox> ();

        public List<Point> AllPossibleMoves = new List<Point> ();

        public List<Point> MovesToCaptureOrBlockChecker { get; set; }
        protected formMainMenu FMM = (formMainMenu)Application.OpenForms["formMainMenu"];

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="posX"></param> Coordonata x la care trebuie desenata piesa
        /// <param name="posY"></param> Coordonata y la care trebuie desenata piesa
        /// <param name="color"></param> Culoarea piesei
        public ChessPiece(Square square, Image image)
        {
            m_image = image;
            m_square = square;

            onBoardLocation = new Point((square.square.Location.X - 40) / 60 + 1, (square.square.Location.Y - 40) / 60 + 1);
            getPieceColor = Global.chessBoard[onBoardLocation.Y, onBoardLocation.X].pieceColor;
        }

        public ChessPiece()
        {

        }
        
        public PictureBox DrawPieceToGivenPosition(Square square, PictureBox pbox)
        {
            pbox = new PictureBox();
            pbox.SizeMode = PictureBoxSizeMode.StretchImage;
            pbox.Location = new Point(square.square.Location.X, square.square.Location.Y);
            pbox.Image = m_image;
            pbox.Width = Global.piece_width;
            pbox.Height = Global.piece_height;
            pbox.BackColor = square.square.BackColor;

            piece_pbox = pbox;

            return pbox;
        }

        public void Highlight_Legal_Moves_Visually()
        {
            foreach(Point pt in this.AllPossibleMoves)
            {
                //Console.WriteLine("Da am venit aici");
                Draw_Highlight_Square(Global.HighLight_Color, pt);
            }
        }

        /// <summary>
        /// Evidentiaza mutarile posibile pentru piesa selectata
        /// </summary>
        public abstract void Highlight_Legal_Moves(bool wannaVisuallyHighlight);

        /// <summary>
        /// Seteaza vizibilitatea higlight square-ului la true 
        /// </summary>
        /// <param name="color"></param>
        /// <param name="point"></param>
        public void Draw_Highlight_Square(Color color, Point point)
        {
             Global.HLSquares[point.Y, point.X].Visible = true;
             Global.HLSquares[point.Y, point.X].BringToFront();
             queue_of_high_pboxes.Enqueue(Global.HLSquares[point.Y, point.X]);
             Global.Highlighted_Square[point.Y, point.X] = true;
        }




        
       
    }
}
