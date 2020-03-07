using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Atestat_Informatica___Joc_de_Sah.Forms;
using Atestat_Informatica___Joc_de_Sah.Classes;

namespace Atestat_Informatica___Joc_de_Sah.Classes.Board
{
    public class Square
    {
        private static Color m_color;
        private static Point m_position;
        public PictureBox square = new PictureBox();

        #region GetSet Members
        public Global.PIECE_TYPE pieceType { get; set; }
        public Global.PIECE_COLOR pieceColor { get; set; }

        #endregion 

        /// <summary>
        /// Constructor ce creeaza un nou obiect
        /// </summary>
        /// <param name="color"></param> Color of square
        /// <param name="position"></param> Position of square
        public Square(Color color, Point position)
        {
            m_color = color;
            m_position = position;

            pieceType = Global.PIECE_TYPE.NULL;
            Draw_Square();
        }

        /// <summary>
        /// Constructor ce NU creeaza un nou obiect(folosit ca sa acces o metoda)
        /// </summary>
        public Square()
        {

        }
        /// <summary>
        /// Pe baza coordonatelor in functie de pixeli a pictureBox-ului primit, se identifica indicile patratului
        /// </summary>
        /// <param name="pbox"></param> pictureBox-ul primit
        /// <returns></returns>
        public Point getSquareLocation(PictureBox pbox)
        {
            int x = (pbox.Location.X - 40) / 60 + 1;
            int y = (pbox.Location.Y - 40) / 60 + 1;
            return new Point(x, y);
        }

        /// <summary>
        /// Deseneaza patratul curent pe tabla de sah
        /// </summary>
        private void Draw_Square()
        {
            square.Location = m_position;
            square.BackColor = m_color;
            square.Width = Global.square_width;
            square.Height = Global.square_height;
        }
    }
}
