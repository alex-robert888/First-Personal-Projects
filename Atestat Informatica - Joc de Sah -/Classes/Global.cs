using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Atestat_Informatica___Joc_de_Sah.Classes.Board;

namespace Atestat_Informatica___Joc_de_Sah.Classes
{
    public class Global
    {
        public static readonly Color Color_DarkGrey = Color.FromArgb(255, 36, 39, 64);
        public static readonly Color HighLight_Color = Color.PaleTurquoise;
        public static readonly SqlConnection conn = new  SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=JocDeSah;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        public static string username_logat = "";
        public static readonly int board_pos_X = 80;
        public static readonly int board_pos_Y = 80;
        public static readonly int square_width = 60;
        public static readonly int square_height = 60;
        public static readonly int piece_width = 60;
        public static readonly int piece_height = 60;
        public static readonly int board_width = 400;
        public static readonly int board_height = 400;
        public static Square[,] chessBoard = new Square[9, 9];
        public static PictureBox[,] HLSquares = new PictureBox[9, 9];
        public static ChessPiece[,] AllPieces = new ChessPiece[9, 9];
        public static bool[,] Highlighted_Square = new bool[9, 9];
        public static List<ChessPiece>[] ListPieces = new List<ChessPiece>[3];
        public static int[,,] CoveredArea = new int[3, 9, 9];
        public static int[,] PinnedPieces = new int[9, 9];
        public static bool[,] CellsThatCanBeBlocked = new bool[9, 9];
        public enum PIECE_TYPE
        {
            NULL,
            ROOK,
            KNIGHT,
            BISHOP,
            QUEEN,
            KING,
            PAWN
        };

        public enum PIECE_COLOR
        {
            NULL,
            WHITE,
            BLACK,
        };

        public enum SQUARE_COLOR
        {
            NULL,
            WHITE,
            BLACK,
        };
    }
}
