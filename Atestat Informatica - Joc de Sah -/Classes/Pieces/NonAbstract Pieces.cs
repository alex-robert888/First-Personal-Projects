using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Atestat_Informatica___Joc_de_Sah.Classes.Pieces
{
    public class NonAbstract_Pieces
    {
        public NonAbstract_Pieces()
        {

        }

        public bool WithinBounds(int currY, int currX)
        {
            return (currY >= 1 && currY <= 8 && currX >= 1 && currX <= 8);
        }

        public bool ValidMove(int finalX, int finalY)
        {
            //MessageBox.Show("asdada");
            return (Global.Highlighted_Square[finalY, finalX] == true);
        }

        /// <summary>
        /// Muta piesa dintr-o celula in alta
        /// </summary>
        /// <param name="startX"></param>
        /// <param name="startY"></param> Coordonatele pozitiei initiale
        /// <param name="finalY"></param>
        /// <param name="finalX"></param> Coordonatele pozitie finale
        public void movePieceToGivenPosition(int startX, int startY, int finalX, int finalY)
        {
            Global.AllPieces[startY, startX].piece_pbox.Location = new Point(40 + (finalX - 1) * 60, 40 + (finalY - 1) * 60);
            Global.AllPieces[startY, startX].piece_pbox.BringToFront();
        }
        public void removeHighlightFromAllSquares()
        {
            
            while(ChessPiece.queue_of_high_pboxes.Count > 0)
            {
                PictureBox pbox = ChessPiece.queue_of_high_pboxes.Dequeue();
                pbox.Visible = false;
            }

            for(int i = 1; i <= 8; ++i)
            {
                for(int j = 1; j <= 8; ++j)
                {
                    Global.Highlighted_Square[i, j] = false;
                }
            }
        }

        public void Capture_Piece(int finalX, int finalY)
        {
            Global.AllPieces[finalY, finalX].piece_pbox.SendToBack();
            Global.AllPieces[finalY, finalX] = null;
        }
    }
}
