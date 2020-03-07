using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Atestat_Informatica___Joc_de_Sah.Classes.Countdown_Timer
{
    public class Timer
    {
        public System.Timers.Timer t { get; set; } = new System.Timers.Timer();
        public TextBox tbox { get; set; } = new TextBox();
        public object Dispatcher { get; private set; }

        private int m_player_color;
        private int m_min_limit;
        private int m_sec_limit;
        private int m_milisec_limit;
        private int m_delay;

        private int curr_min;
        private int curr_sec;
        private int curr_milisec;

        const int tbox_width = 263;
        const int tbox_height = 55;

        public Timer(int player_color, int min_limit, int sec_limit, int milisec_limit, Point tbox_position, int delay)
        {
            m_player_color = player_color;
            m_min_limit = min_limit;
            m_sec_limit = sec_limit;
            m_milisec_limit = milisec_limit;
            m_delay = delay;

            curr_min = min_limit;
            curr_sec = sec_limit;
            curr_milisec = milisec_limit;

            DrawTimerTextBox(tbox_position);

            t.Interval = 1; //1 second
            t.Elapsed += T_Elapsed;

            TextBox.CheckForIllegalCrossThreadCalls = false;
        }

        private void DrawTimerTextBox(Point tbox_position)
        {
            tbox.Location = tbox_position;
            tbox.Width = tbox_width;
            tbox.Height = tbox_height;
            tbox.Enabled = true;
            tbox.Font = new Font("Microsoft Sans Serif", 25, FontStyle.Bold);
            tbox.ForeColor = Color.Black;
            tbox.BackColor = Color.WhiteSmoke;
            tbox.TextAlign = HorizontalAlignment.Center;
            tbox.Enabled = false;
            tbox.Text = string.Format("{0}:{1}:{2}", curr_min.ToString().PadLeft(2, '0'), curr_sec.ToString().PadLeft(2, '0'), curr_milisec.ToString().PadLeft(2, '0'));
            // TextBox.CheckForIllegalCrossThreadCalls = false;

            formMainMenu FMM = (formMainMenu)Application.OpenForms["formMainMenu"];
            FMM.panelSah.Controls.Add(tbox);

        }

        delegate void SetTextCallback(string text);

        private void SetText(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.tbox.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText);
                tbox.Invoke(d, new object[] { text });
            }
            else
            {
                this.tbox.Text = text;
            }
        }

        private void T_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            curr_milisec -= 1;

            if(curr_min < 0 && curr_sec < 0 && curr_milisec < 0)
            {
                if(m_player_color == 1)
                {
                    formMainMenu FMM = new formMainMenu();
                    FMM.buttonAmPierdutWhite.PerformClick();
                }
                else
                {
                    formMainMenu FMM = new formMainMenu();
                    FMM.buttonAmPierdutBlack.PerformClick();
                }
                StopTimer();
                return;
            }

            if (curr_milisec == 0)
            {
                curr_milisec = 99;
                curr_sec -= 1;
            }

            if (curr_sec == 0)
            {
                curr_sec = 59;
                curr_min -= 1;
            }

            if (curr_min == 0 && curr_sec <= 10)
            {
                //MessageBox.Show("asdasd");
                tbox.BackColor = Color.Red;
            }
            else if (curr_min == 0 && curr_sec <= 30)
            {
                tbox.BackColor = Color.DarkOrange;
            }



            SetText(string.Format("{0}:{1}:{2}", curr_min.ToString().PadLeft(2, '0'), curr_sec.ToString().PadLeft(2, '0'), curr_milisec.ToString().PadLeft(2, '0')));
            //tbox.Text = string.Format("{0}:{1}:{2}", curr_min.ToString().PadLeft(2, '0'), curr_sec.ToString().PadLeft(2, '0'), curr_milisec.ToString().PadLeft(2, '0'));
        }

        public void StartTimer() => t.Start();
        public void StopTimer()
        {
            curr_sec += m_delay;
            if(curr_sec >= 60)
            {
                curr_min += 1;
                curr_sec %= 60;
            }
            SetText(string.Format("{0}:{1}:{2}", curr_min.ToString().PadLeft(2, '0'), curr_sec.ToString().PadLeft(2, '0'), curr_milisec.ToString().PadLeft(2, '0')));
            t.Stop();
        }
    }

}
