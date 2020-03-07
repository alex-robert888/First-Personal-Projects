using Atestat_Informatica___Joc_de_Sah.Classes.Board;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Atestat_Informatica___Joc_de_Sah.Classes.Pieces
{
    public class Rook : ChessPiece
    {
        public PictureBox img_piece { get; set; } = new PictureBox();

        public static Image m_image;


        private NonAbstract_Pieces nonAbstract_Pieces = new NonAbstract_Pieces();
        public Point[] RookPossibleSensesToMove { get; set; } = new Point[4] { new Point(0, -1), new Point(1, 0), new Point(0, 1), new Point(-1, 0) };
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="posX"></param> Coordonata X pentru tura
        /// <param name="posY"></param> Coordonata Y pentru tura
        /// <param name="color"></param> Culoarea turei
        /// <param name="image"></param> Imaginea turei, in fuunctie de culoare
        public Rook(Square square, Image image) : base(square, image)
        {
            m_image = image;
            getPieceType = Global.PIECE_TYPE.ROOK;

            img_piece = DrawPieceToGivenPosition(square, new PictureBox());
            //RookPossibleSensesToMove = new Point[4] { new Point(0, -1), new Point(1, 0), new Point(0, 1), new Point(-1, 0)};
        }

        public Rook()
        {
        }

        public void DFS(int sns, bool wannaVisuallyHighlight)
        {
            int currX = onBoardLocation.X + RookPossibleSensesToMove[sns].X;
            int currY = onBoardLocation.Y + RookPossibleSensesToMove[sns].Y;
            //Console.WriteLine("Nou DFS: " + currY + " " + currX);
            while (nonAbstract_Pieces.WithinBounds(currX, currY) && Global.chessBoard[currY, currX].pieceColor != Global.chessBoard[onBoardLocation.Y, onBoardLocation.X].pieceColor)
            {
                if(wannaVisuallyHighlight)
                    Draw_Highlight_Square(Global.HighLight_Color, new Point(currX, currY));
                else
                {
                    Global.CoveredArea[Convert.ToInt32(getPieceColor), currY, currX]++;
                    this.AllPossibleMoves.Add(new Point(currX, currY));

                    if (Global.AllPieces[currY, currX] is King)
                    {
                       // MessageBox.Show("Rook");
                        Player.WhoAttacksTheKing WATK = new Player.WhoAttacksTheKing();
                        WATK.piece = Global.AllPieces[onBoardLocation.Y, onBoardLocation.X];
                        WATK.sense = RookPossibleSensesToMove[sns];
                        Player.PiecesAttackingTheKing.Add(WATK);
                    }
                }

                if (wannaVisuallyHighlight && Global.chessBoard[currY, currX].pieceColor == 3 - Global.chessBoard[onBoardLocation.Y, onBoardLocation.X].pieceColor)
                {
                    return;
                }
                if (!wannaVisuallyHighlight && Global.chessBoard[currY, currX].pieceType != Global.PIECE_TYPE.NULL && Global.chessBoard[currY, currX].pieceType != Global.PIECE_TYPE.KING)
                {
                    return;
                }

                currX += RookPossibleSensesToMove[sns].X;
                currY += RookPossibleSensesToMove[sns].Y;
            }

        }

        public override void Highlight_Legal_Moves(bool wannaVisuallyHighlight)
        {
            for (int sns = 0; sns < 4; ++sns)
            {
                DFS(sns, wannaVisuallyHighlight);
            }
        }

    }
}
