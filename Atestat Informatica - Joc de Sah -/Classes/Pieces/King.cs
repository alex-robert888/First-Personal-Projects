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
    public class King : ChessPiece
    {
        public PictureBox img_piece { get; set; } = new PictureBox();

        private NonAbstract_Pieces nonAbstract_Pieces = new NonAbstract_Pieces();
        private readonly Point[] KingPossibleSensesToMove;

        private int KingIsAttacked;

        public King(Square square, Image image) : base(square, image)
        {
            getPieceType = Global.PIECE_TYPE.KING;

            img_piece = DrawPieceToGivenPosition(square, new PictureBox());

            KingPossibleSensesToMove = new Point[8] { new Point(-1, -1), new Point(1, -1), new Point(1, 1), new Point(-1, 1), new Point(0, -1), new Point(1, 0), new Point(0, 1), new Point(-1, 0) };
        }

        /// <summary>
        /// Voi folosi acest DFS cand verific daca regele e in sah
        /// </summary>
        public void DFS(int sns, bool wannaVisuallyHighlight)
        {
            int currX = onBoardLocation.X + KingPossibleSensesToMove[sns].X;
            int currY = onBoardLocation.Y + KingPossibleSensesToMove[sns].Y;
            //Console.WriteLine("Nou DFS: " + currY + " " + currX);
            while (nonAbstract_Pieces.WithinBounds(currX, currY) && Global.chessBoard[currY, currX].pieceColor != Global.chessBoard[onBoardLocation.Y, onBoardLocation.X].pieceColor)
            {
                //Console.WriteLine("curr: " + currY + " " + currX);

                if (Global.chessBoard[currY, currX].pieceColor == 3 - Global.chessBoard[onBoardLocation.Y, onBoardLocation.X].pieceColor)
                {
                    ++KingIsAttacked;
                    return;
                }

                currX += KingPossibleSensesToMove[sns].X;
                currY += KingPossibleSensesToMove[sns].Y;
            }

        }

        public override void Highlight_Legal_Moves(bool wannaVisuallyHighlight)
        {
            int oppColor = Convert.ToInt32(3 - getPieceColor);

            for (int sns = 0; sns < 8; ++sns)
            {
                int currX = onBoardLocation.X + KingPossibleSensesToMove[sns].X;
                int currY = onBoardLocation.Y + KingPossibleSensesToMove[sns].Y;
                //Console.WriteLine("Nou DFS: " + currY + " " + currX);
                if (nonAbstract_Pieces.WithinBounds(currX, currY) && Global.chessBoard[currY, currX].pieceColor != Global.chessBoard[onBoardLocation.Y, onBoardLocation.X].pieceColor && Global.CoveredArea[oppColor, currY, currX] == 0)
                {
                    //Console.WriteLine("curr: " + currY + " " + currX);
                    if(wannaVisuallyHighlight)
                        Draw_Highlight_Square(Global.HighLight_Color, new Point(currX, currY));
                    else
                    {
                        this.AllPossibleMoves.Add(new Point(currX, currY));
                        ++Global.CoveredArea[Convert.ToInt32(getPieceColor), currY, currX];
                    }
                }
            }
        }

        public int IsKingInCheck()
        {
            Player player = new Player();

            //construiesc matricea de cover pentru piesele oponentului
            return Global.CoveredArea[Convert.ToInt32(3 - getPieceColor), onBoardLocation.Y, onBoardLocation.X];
        }
    }
}
