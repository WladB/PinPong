/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
*/
using System;
using System.Drawing;


using System.Windows.Forms;
namespace PinPong
{
    public class Ball
    {
        public int SizeW = 10;
        public int SizeH = 10;
        public int speedT = 6;
        public int speedL = 0;
        public bool IsPaused = false;
        public Color color = Color.Red;
        P_area Form;
        public PictureBox Local;
        Random rand = new Random();
        int tim;
        Panel PausePanel;
        Timer GameTimer;
        Timer PowerTimer;
        Timer Vidlik;
        PictureBox PlayerPlat;
        PictureBox AiPlat;

        public Ball(PictureBox local, P_area form, Timer gameTimer, Timer powerTimer, Panel panel, Timer vidlik, PictureBox playerPlat, PictureBox aiPlat)
        {

            Local = local;
            Form = form;
            Local.Size = new Size(SizeW, SizeH);
            Local.BackColor = color;
            PausePanel = panel;
            GameTimer = gameTimer;
            this.PowerTimer = powerTimer;
            Vidlik = vidlik;
            PlayerPlat = playerPlat;
            AiPlat = aiPlat;

        }

        void pause()
        {

            tim = 0;
            PausePanel.Visible = true;
            GameTimer.Enabled = false;
            PowerTimer.Enabled = false;
            Vidlik.Enabled = true;
            IsPaused = true;
        }

        public void pause_Tick(object sender, EventArgs e)
        {
            tim++;

            if (tim > 75)
            {
                GameTimer.Enabled = true;

                PausePanel.Visible = false;
                tim = 0;
                Vidlik.Enabled = false;
                IsPaused = false;
            }

        }

        public void collisionWall(Label ScorePlayer, Label ScoreAi)
        {
            if (Convert.ToInt32(ScoreAi.Text) < 5 && Convert.ToInt32(ScorePlayer.Text) < 5 && (!(Local.Location.X <= Form.ClientRectangle.Left) && !(Local.Location.X + SizeH >= Form.ClientRectangle.Right)))
            {
                (PausePanel.Controls[0] as Label).Text = "Гол!";
                PausePanel.Controls[0].Left = (PausePanel.Width / 2) - (PausePanel.Controls[0].Width / 2);
            }



            if (Local.Location.Y <= Form.ClientRectangle.Top)
            {


                ScorePlayer.Text = Convert.ToString(Convert.ToInt32(ScorePlayer.Text) + 1);
                Local.Left = (Form.Width / 2);
                AiPlat.Left = (Form.Width / 2) - (AiPlat.Width / 2);
                PlayerPlat.Left = (Form.Width / 2) - (PlayerPlat.Width / 2);
                Local.Top = 79;

                if (Convert.ToInt32(ScorePlayer.Text) >= 5)
                {

                    (PausePanel.Controls[0] as Label).Text = "Перемога";
                    PausePanel.Controls[0].Left = (PausePanel.Width / 2) - (PausePanel.Controls[0].Width / 2);
                    ScorePlayer.Text = "0";
                    ScoreAi.Text = "0";
                }

                pause();

                speedT *= -1;
                speedL = -1;

            }
            if (Local.Location.Y + SizeW >= Form.ClientRectangle.Bottom)
            {
                ScoreAi.Text = Convert.ToString(Convert.ToInt32(ScoreAi.Text) + 1);
                Local.Left = 550;
                AiPlat.Left = (Form.Width / 2) - (AiPlat.Width / 2);
                PlayerPlat.Left = (Form.Width / 2) - (PlayerPlat.Width / 2);
                Local.Top = 472;
                if (Convert.ToInt32(ScoreAi.Text) >= 5)
                {

                    (PausePanel.Controls[0] as Label).Text = "Програш";
                    PausePanel.Controls[0].Left = (PausePanel.Width / 2) - (PausePanel.Controls[0].Width / 2);
                    ScorePlayer.Text = "0";
                    ScoreAi.Text = "0";
                }
                pause();
                speedT *= -1;
                speedL = rand.Next(-1, 1);

            }

            if (Local.Location.X <= Form.ClientRectangle.Left || Local.Location.X + SizeH >= Form.ClientRectangle.Right)
            {

                if (Local.Location.X < Form.ClientRectangle.Left)
                {
                    Local.Left = Form.ClientRectangle.Left + 10;
                }
                if (Local.Location.X + SizeH > Form.ClientRectangle.Right)
                {
                    Local.Left = Form.ClientRectangle.Right - 10;
                    speedL *= -1;
                }
                else { speedL *= -1; }
            }
        }
    }
}
