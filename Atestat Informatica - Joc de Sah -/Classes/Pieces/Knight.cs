using Atestat_Informatica___Joc_de_Sah.Classes.Board;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Atestat_Informatica___Joc_de_Sah.Classes.Pieces;

namespace Atestat_Informatica___Joc_de_Sah.Classes.Pieces
{
    public class Knight : ChessPiece
    {
        public PictureBox img_piece { get; set; } = new PictureBox();

        private NonAbstract_Pieces nonAbstract_Pieces = new NonAbstract_Pieces();
        private readonly Point[] KnightPossibleSensesToMove;

        public Knight(Square square, Image image) : base(square, image)
        {
            getPieceType = Global.PIECE_TYPE.KNIGHT;
            img_piece = DrawPieceToGivenPosition(square, new PictureBox());

            KnightPossibleSensesToMove = new Point[8] { new Point(-2, -1), new Point(-1, -2), new Point(1, -2), new Point(2, -1), new Point(2, 1), new Point(1, 2), new Point(-1, 2), new Point(-2, 1) };
        }

        public override void Highlight_Legal_Moves(bool wannaVisuallyHighlight)
        {
            for(int sns = 0; sns < 8; ++sns)
            {
                Point temp = new Point(onBoardLocation.X + KnightPossibleSensesToMove[sns].X, onBoardLocation.Y + KnightPossibleSensesToMove[sns].Y);
                if(nonAbstract_Pieces.WithinBounds(temp.X, temp.Y) && Global.chessBoard[temp.Y, temp.X].pieceColor != Global.chessBoard[onBoardLocation.Y, onBoardLocation.X].pieceColor)
                {
                    if (wannaVisuallyHighlight)
                        Draw_Highlight_Square(Global.HighLight_Color, temp);
                    else
                    {
                        Global.CoveredArea[Convert.ToInt32(getPieceColor), temp.Y, temp.X]++;
                        this.AllPossibleMoves.Add(new Point(temp.X, temp.Y));

                        if (Global.AllPieces[temp.Y, temp.X] is King)
                        {
                            //MessageBox.Show("Knight");
                            Player.WhoAttacksTheKing WATK = new Player.WhoAttacksTheKing();
                            WATK.piece = Global.AllPieces[onBoardLocation.Y, onBoardLocation.X];
                            WATK.sense = KnightPossibleSensesToMove[sns];
                            Player.PiecesAttackingTheKing.Add(WATK);
                        }
                    }

                }

            }
        }

    }
}
