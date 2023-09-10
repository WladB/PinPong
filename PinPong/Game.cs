using System;
using System.Drawing;
using System.Windows.Forms;
namespace PinPong
{
    class Game
    {
        P_area form;
        public Game(P_area area)
        {
            form = area;
        }


        Ball ball;
        Platform PlatformPlayer;
        Platform AiPlatform;
        int power = 0;

        PictureBox PlayerPBox;
        PictureBox AiPBox;
        PictureBox P_ball;
        PictureBox Line;
        CheckBox PauseButton;

        Label AiScore;
        Label PlayerScore;
        Label PauseLabel;

        ProgressBar PrBar;

        Timer GameTimer;
        Timer PowerTimer;
        Timer vidlik;

        Panel PausePanel;
        public void Start()
        {
            form.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            PlayerPBox = new PictureBox();
            PlayerPBox.BackColor = Color.Green;
            PlayerPBox.Size = new Size(133, 12);
            PlayerPBox.Top = 516;
            PlayerPBox.Left = 550 - (PlayerPBox.Width / 2);

            PlayerPBox.Visible = true;
            PlayerPBox.Enabled = true;
            PlatformPlayer = new Platform(false, PlayerPBox, form);


            AiPBox = new PictureBox();
            AiPBox.BackColor = Color.Green;
            AiPBox.Size = new Size(133, 12);
            AiPBox.Top = 40;
            AiPBox.Left = 550 - (AiPBox.Width / 2);

            AiPBox.Visible = true;
            AiPBox.Enabled = true;
            AiPlatform = new Platform(true, AiPBox, form);

            Line = new PictureBox();
            Line.BackColor = Color.Green;
            Line.BorderStyle = BorderStyle.Fixed3D;
            Line.Location = new Point(-4, 252);
            Line.Size = new Size(1088, 11);
            Line.Visible = true;
            Line.Enabled = true;

            AiScore = new Label();
            AiScore.Text = "0";
            AiScore.Top = -1;
            AiScore.AutoSize = true;
            AiScore.Left = -3;
            AiScore.Font = new Font(AiScore.Font.FontFamily, 12, FontStyle.Bold);


            PlayerScore = new Label();
            PlayerScore.Text = "0";
            PlayerScore.AutoSize = true;
            PlayerScore.Top = 533;
            PlayerScore.Left = -4;
            PlayerScore.Font = new Font(AiScore.Font.FontFamily, 12, FontStyle.Bold);

            PauseLabel = new Label();
            PauseLabel.Text = "Гол!";
            PauseLabel.Parent = PausePanel;
            PauseLabel.Top = 70;
            PauseLabel.Left = 164;
            PauseLabel.AutoSize = true;
            PauseLabel.Font = new Font(AiScore.Font.FontFamily, 30, FontStyle.Bold);

            PrBar = new ProgressBar();
            PrBar.Step = 1;
            PrBar.Maximum = 150;
            PrBar.Location = new Point(850, 267);
            PrBar.Minimum = 0;
            PrBar.Size = new Size(205, 23);

            PauseButton = new CheckBox();
            PauseButton.Focus();
            PauseButton.Text = "  ||";
            PauseButton.Checked = true;
            PauseButton.Size = new Size(46, 30);
            PauseButton.Visible = true;
            PauseButton.Enabled = true;
            PauseButton.Top = 12;
            PauseButton.Left = 1009;
            PauseButton.Font = new Font(AiScore.Font.FontFamily, 7.8f, FontStyle.Bold);
            PauseButton.Appearance = Appearance.Button;

            PausePanel = new Panel();
            PausePanel.BackColor = Color.FromArgb(224, 224, 224);
            PausePanel.Top = 156;
            PausePanel.Left = 335;
            PausePanel.Size = new Size(449, 186);
            PausePanel.Visible = false;
            PausePanel.Enabled = true;

            GameTimer = new Timer();
            GameTimer.Interval = 50;
            GameTimer.Tick += GameTimer_Tick;
            GameTimer.Enabled = true;

            PowerTimer = new Timer();
            PowerTimer.Interval = 1;
            PowerTimer.Tick += PowerTimer_Tick;
            PowerTimer.Enabled = false;


            vidlik = new Timer();
            vidlik.Interval = 1;
            vidlik.Enabled = false;

            P_ball = new PictureBox();

            P_ball.Top = 252;
            P_ball.Left = 523;
            P_ball.Visible = true;
            P_ball.Enabled = true;
            ball = new Ball(P_ball, form, GameTimer, PowerTimer, PausePanel, vidlik, PlayerPBox, AiPBox);

            vidlik.Tick += ball.pause_Tick;

            form.Controls.Add(P_ball);
            form.Controls.Add(PlayerPBox);
            form.Controls.Add(AiPBox);
            form.Controls.Add(AiScore);
            form.Controls.Add(PlayerScore);
            PausePanel.Controls.Add(PauseLabel);
            form.Controls.Add(PausePanel);
            PausePanel.BringToFront();
            PauseButton.CheckedChanged += PauseCheckBox_Click;
            form.Controls.Add(PauseButton);
            form.Controls.Add(PrBar);
            form.Controls.Add(Line);
            PlatformPlayer.Local = PlayerPBox;
            AiPlatform.Local = AiPBox;
            ball.Local = P_ball;

            form.KeyPreview = true;
            form.KeyDown += P_area_KeyDown;
            form.KeyUp += P_area_KeyUp;
        }
        public void PauseCheckBox_Click(object sender, EventArgs e)
        {
            if (PauseButton.Checked == false)
            {
                PowerTimer.Enabled = false;
                GameTimer.Enabled = false;
                PausePanel.Visible = true;
                PauseLabel.Text = "Пауза";
            }
            else
            {
                PauseLabel.Text = "Гол!";
                GameTimer.Enabled = true;
                PausePanel.Visible = false;

            }
            PauseLabel.Left = (PausePanel.Width / 2) - (PauseLabel.Width / 2);
        }


        private void P_area_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode.ToString() == "A" && PlayerPBox.Location.X > form.ClientRectangle.Left) && PauseButton.Checked && !ball.IsPaused)
            {
                PlatformPlayer.moveLeft = false;
                PowerTimer.Enabled = true;
                PlayerPBox.Left -= 5;
            }
            else if ((e.KeyCode.ToString() == "D" && PlayerPBox.Location.X + 133 < form.ClientRectangle.Right) && PauseButton.Checked && !ball.IsPaused)
            {
                PlatformPlayer.moveLeft = true;
                PowerTimer.Enabled = true;
                PlayerPBox.Left += 5;
            }
            if (PlayerPBox.Location.X + 133 >= form.ClientRectangle.Right || PlayerPBox.Location.X <= form.ClientRectangle.Left)
            {
                PrBar.Value = 0;
                power = 0;
                PowerTimer.Enabled = false;
            }
        }
        public void GameTimer_Tick(object sender, EventArgs e)
        {
            P_ball.Top += 1 * ball.speedT;
            P_ball.Left += 1 * ball.speedL;
            ball.collisionWall(PlayerScore, AiScore);
            PlatformPlayer.collisionPlatform(ball, power);
            AiPlatform.collisionPlatform(ball, power);
            AiPlatform.Ai(ball);

            if (!PowerTimer.Enabled)
            {
                power = 0;
            }
        }
        private void P_area_KeyUp(object sender, KeyEventArgs e)
        {
            if (PauseButton.Checked == true)
            {
                PrBar.Value = 0;
                power = 0;
                PowerTimer.Enabled = false;
            }
        }
        public void PowerTimer_Tick(object sender, EventArgs e)
        {
            power = (power + 1 <= 150) ? power + 1 : power;
            PrBar.Value = power;
        }
    }
}
