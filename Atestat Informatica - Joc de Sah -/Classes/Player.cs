using Atestat_Informatica___Joc_de_Sah.Classes.Pieces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Atestat_Informatica___Joc_de_Sah.Classes
{
    public class Player
    {
        public Player()
        {

        }

        public struct WhoAttacksTheKing
        {
            public ChessPiece piece;
            public Point sense;
        }

        public static Point[] KingPosition { get; set; } = new Point[3];
        public static List<WhoAttacksTheKing> PiecesAttackingTheKing = new List<WhoAttacksTheKing>();

        /// <summary>
        /// Marhceaza in matricea CoveredArea[,,] toate celule cu care au tangenta piesele unui jucator de culoare data
        /// </summary>
        public void Determine_Area_Covered_By_Player(Global.PIECE_COLOR pieceColor)
        {
            for(int i = 1; i <= 8; ++i)
            {
                for(int j = 1; j <= 8; ++j)
                {
                    Global.CoveredArea[Convert.ToInt32(pieceColor), i, j] = 0;
                }
            }
            
            int picol = Convert.ToInt32(pieceColor);
            //MessageBox.Show(pieceColor + " " + Global.ListPieces[picol].Count + "");
            foreach (ChessPiece piece in Global.ListPieces[picol])
            {
                piece.Highlight_Legal_Moves(false);
            }

            for (int i = 1; i <= 8; ++i)
            {
                for (int j = 1; j <= 8; ++j)
                {
                    Console.Write(Global.CoveredArea[Convert.ToInt32(pieceColor), i, j]); 
                }
                Console.Write('\n');
            }

            //MessageBox.Show(Player.PiecesAttackingTheKing.Count+ " ");
        }

        private void DFS(int sns, Point KingPosition, Point[] PossibleSensesToMove, Global.PIECE_TYPE SlidePieceBesidesQueen)
        {
            NonAbstract_Pieces nonAbstract_Pieces = new NonAbstract_Pieces();

            //MessageBox.Show("asdasdasd");
            int currX = KingPosition.X + PossibleSensesToMove[sns].X;
            int currY = KingPosition.Y + PossibleSensesToMove[sns].Y;

            int piecesOfSameColourEncountered = 0;
            //bool 

            List<Point> tempList = new List<Point> ();
            Point PinnedPieceCoords = new Point(0, 0);
            while (nonAbstract_Pieces.WithinBounds(currX, currY))
            {
                tempList.Add(new Point(currX, currY));
                if (Global.chessBoard[currY, currX].pieceColor == Global.AllPieces[KingPosition.Y, KingPosition.X].getPieceColor)
                {
                    ++piecesOfSameColourEncountered;

                    if(piecesOfSameColourEncountered > 1)
                    {
                        break;
                    }
                    else
                    {
                        PinnedPieceCoords = new Point(currX, currY);
                    }
                }

                else if(Global.chessBoard[currY, currX].pieceColor == 3 - Global.AllPieces[KingPosition.Y, KingPosition.X].getPieceColor
                    && (Global.chessBoard[currY, currX].pieceType == Global.PIECE_TYPE.QUEEN || Global.chessBoard[currY, currX].pieceType == SlidePieceBesidesQueen))
                {
                    if(piecesOfSameColourEncountered == 1)
                    {
                        //MessageBox.Show("Pinned piece: " + PinnedPieceCoords.Y + " " + PinnedPieceCoords.X);
                        Global.PinnedPieces[PinnedPieceCoords.Y, PinnedPieceCoords.X]++;

                       
                        foreach(Point pt in tempList)
                        {
                            Global.AllPieces[PinnedPieceCoords.Y, PinnedPieceCoords.X].AllPossibleMoves.Add(pt);
                        }
                    }
                    break;
                }

                currX += PossibleSensesToMove[sns].X;
                currY += PossibleSensesToMove[sns].Y;
            }
        }


        public void FindPinnedPieces(Point KingPosition)
        {
            //ma uit pe diagonala
            Bishop bishop = new Bishop();
            for (int sns = 0; sns < 4; ++sns)
            {
                DFS(sns, KingPosition, bishop.BishopPossibleSensesToMove, Global.PIECE_TYPE.BISHOP);
            }

            //ma uit pe verticala si orizontala
            Rook rook = new Rook();
            for (int sns = 0; sns < 4; ++sns)
            {
                DFS(sns, KingPosition, rook.RookPossibleSensesToMove, Global.PIECE_TYPE.ROOK);
            }
        }

        private void WhichCellsCanBeBlocked()
        {
            for(int i = 1; i <= 8; ++i)
            {
                for(int j = 1; j <= 8; ++j)
                {
                    Global.CellsThatCanBeBlocked[i, j] = false;
                }
            }

            Point Location = PiecesAttackingTheKing[0].piece.onBoardLocation;
            Point sense = PiecesAttackingTheKing[0].sense;

            Point curr = Location;
            while (Global.chessBoard[curr.Y, curr.X].pieceType != Global.PIECE_TYPE.KING)
            {
                Global.CellsThatCanBeBlocked[curr.Y, curr.X] = true;

                curr.Y += sense.Y;
                curr.X += sense.X;

                Console.WriteLine("Cells that can be blocked: " + curr.Y + " " + curr.X);
            } 

        }

        public void LookForMovesToCaptureOrBlockChecker()
        {
            WhichCellsCanBeBlocked();
        }
    }
}
