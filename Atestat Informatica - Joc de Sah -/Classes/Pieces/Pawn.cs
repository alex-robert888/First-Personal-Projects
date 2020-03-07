using Atestat_Informatica___Joc_de_Sah.Classes.Board;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Atestat_Informatica___Joc_de_Sah.Classes.Pieces;

namespace Atestat_Informatica___Joc_de_Sah.Classes
{
    public class Pawn : ChessPiece
    {
        private Square m_square;
        private Image m_image;



        public PictureBox img_piece { get; set; } = new PictureBox();

        /// <summary>
        /// Constructor - Creaza si deseneaza piesa pe tabla
        /// </summary>
        /// <param name="square"></param> //Coordonatele masurand in pixeli a partatului de pe tabla de sah
        /// <param name="image"></param>  //Imaginea corespunzatoare piesei
        public Pawn(Square square, Image image) : base(square, image)
        {
            m_square = square;
            m_image = image;

            getPieceType = Global.PIECE_TYPE.PAWN;
            img_piece = DrawPieceToGivenPosition(square, new PictureBox());
        }

        /// <summary>
        /// Evidentiaza pe tabla de sah mutarile posibile pe care le poate excuta pionul
        /// </summary>
        /// 

        public override void Highlight_Legal_Moves(bool wannaVisuallyHighlight)
        {
          
            int y = onBoardLocation.Y;
            int x = onBoardLocation.X;

            //Stabilesc sensul in care se poate deplasa pionul
            //Daca pionul este alb - se poate misca doar in sus
            //Daca pionul este negru - se poate misca doar in jos
            int sense = (getPieceColor == Global.PIECE_COLOR.WHITE) ? -1 : 1;

            //Exista vreo piesa pe care o poate captura?
            if (x + 1 <= 8 && Global.chessBoard[y + sense, x + 1].pieceColor == 3 - Global.chessBoard[y, x].pieceColor)
            {
                if (wannaVisuallyHighlight)
                {
                    Draw_Highlight_Square(Color.PaleTurquoise, new Point(onBoardLocation.X + 1, onBoardLocation.Y + sense));
                }
                else //Inseamna ca vreau sa completez matricea de cover
                {
                    Global.CoveredArea[Convert.ToInt32(getPieceColor), y + sense, x + 1]++;

                    if (Global.AllPieces[y + sense, x + 1] is King)
                    {
                        Player.WhoAttacksTheKing WATK = new Player.WhoAttacksTheKing();
                        WATK.piece = Global.AllPieces[onBoardLocation.Y, onBoardLocation.X];
                        WATK.sense = new Point(1, sense);
                        Player.PiecesAttackingTheKing.Add(WATK);
                    }
                }
            }

            if (x - 1 >= 1 && Global.chessBoard[y + sense, x - 1].pieceColor == 3 - Global.chessBoard[y, x].pieceColor)
            {
                if (wannaVisuallyHighlight)
                {
                    Draw_Highlight_Square(Color.PaleTurquoise, new Point(onBoardLocation.X - 1, onBoardLocation.Y + sense));
                }
                else //Inseamna ca vreau sa completez matricea de cover
                {
                    Global.CoveredArea[Convert.ToInt32(getPieceColor), y + sense, x - 1]++;
                    this.AllPossibleMoves.Add(new Point(x - 1, y + sense));
                    if (Global.AllPieces[y + sense, x - 1] is King)
                    {
                        Player.WhoAttacksTheKing WATK = new Player.WhoAttacksTheKing();
                        WATK.piece = Global.AllPieces[onBoardLocation.Y, onBoardLocation.X];
                        WATK.sense = new Point(-1, sense);
                        Player.PiecesAttackingTheKing.Add(WATK);
                    }
                }

            }

            //E campul din fata pionului liber?
            if (Global.chessBoard[y + sense, x].pieceType != Global.PIECE_TYPE.NULL)
            {
                //daca nu e liber, pionul nu se poate misca nicaieri
                return;
            }
            //daca e liber, ma pot misca cu cel putin o pozitie si evidentiez acest lucru pe tabla
            if (wannaVisuallyHighlight)
                Draw_Highlight_Square(Color.PaleTurquoise, new Point(onBoardLocation.X, onBoardLocation.Y + sense));
            else
            {
                this.AllPossibleMoves.Add(new Point(x, y + sense));
                Global.CoveredArea[Convert.ToInt32(getPieceColor), y + sense, x]++;
            }
            //Pot sa mut pionul cu doua pozitii?
            if ((y == 2 && getPieceColor == Global.PIECE_COLOR.BLACK) == false && (y == 7 && getPieceColor == Global.PIECE_COLOR.WHITE) == false)
            {
                //Pionul nu se afla pe pozitia initiala, deci mutarea cu doua pozitii nu permisa
                return;
            }
            if (Global.chessBoard[y + 2 * sense, x].pieceType != Global.PIECE_TYPE.NULL)
            {
                //pionul nu se poate misca 2 pozitii
                return;
            }
            if (wannaVisuallyHighlight)
                Draw_Highlight_Square(Color.PaleTurquoise, new Point(onBoardLocation.X, onBoardLocation.Y + 2 * sense));
            else
            {
                this.AllPossibleMoves.Add(new Point(x, y + 2 * sense));
                Global.CoveredArea[Convert.ToInt32(getPieceColor), y + 2 * sense, x]++;
            }
        }
    }
}
