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


    public class Bishop : ChessPiece
    {
        public PictureBox img_piece { get; set; } = new PictureBox();

        private NonAbstract_Pieces nonAbstract_Pieces = new NonAbstract_Pieces();
        public Point[] BishopPossibleSensesToMove { get; set; } = new Point[4] { new Point(-1, -1), new Point(1, -1), new Point(1, 1), new Point(-1, 1) };

        public Bishop(Square square, Image image) : base(square, image)
        {
            getPieceType = Global.PIECE_TYPE.BISHOP;
            img_piece = DrawPieceToGivenPosition(square, new PictureBox());

            //BishopPossibleSensesToMove = new Point[4] { new Point(-1, -1), new Point(1, -1), new Point(1, 1), new Point(-1, 1) };
        }

        public Bishop()
        {

        }

        public void DFS(int sns, bool wannaVisuallyHighlight)
        {
            int currX = onBoardLocation.X + BishopPossibleSensesToMove[sns].X;
            int currY = onBoardLocation.Y + BishopPossibleSensesToMove[sns].Y;
            //Console.WriteLine("Nou DFS: " + currY + " " + currX);
            while (nonAbstract_Pieces.WithinBounds(currX, currY) && Global.chessBoard[currY, currX].pieceColor != Global.chessBoard[onBoardLocation.Y, onBoardLocation.X].pieceColor)
            {
                //Console.WriteLine("curr: " + currY + " " + currX);
                if (wannaVisuallyHighlight)
                    Draw_Highlight_Square(Global.HighLight_Color, new Point(currX, currY));
                else
                {
                    Global.CoveredArea[Convert.ToInt32(getPieceColor), currY, currX]++;
                    this.AllPossibleMoves.Add(new Point(currX, currY));

                    if (Global.AllPieces[currY, currX] is King)
                    {
                        Player.WhoAttacksTheKing WATK = new Player.WhoAttacksTheKing();
                        WATK.piece = Global.AllPieces[onBoardLocation.Y, onBoardLocation.X];
                        WATK.sense = BishopPossibleSensesToMove[sns];
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

                currX += BishopPossibleSensesToMove[sns].X;
                currY += BishopPossibleSensesToMove[sns].Y;
            }

        }

        public override void Highlight_Legal_Moves(bool wannaVisuallyHighlight)
        {
            //Console.WriteLine("Bishop: " + onBoardLocation.Y + " " + onBoardLocation.X);

            //if(Global.PinnedPieces[onBoardLocation.Y, onBoardLocation.X] > 0)
           // {
            //    return;
            //}
            for (int sns = 0; sns < 4; ++sns)
            {
                DFS(sns, wannaVisuallyHighlight);
            }
        }
    }
}
