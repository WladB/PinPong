using System;
using System.Drawing;
using System.Windows.Forms;

namespace PinPong
{
    class Platform
    {
        public int SizeW = 133;
        public int SizeH = 12;
        public int speed = 6;
        public Color color = Color.Black;
        bool typeAi = false;
        public bool moveLeft = false;
        public PictureBox Local;
        Random rand = new Random();
        P_area Form;
        public Platform(bool ai, PictureBox local, P_area form)
        {
            typeAi = ai;
            Local = local;
            Local.Size = new Size(SizeW, SizeH);
            Local.BackColor = color;
            Form = form;
        }
        public void collisionPlatform(Ball ball, int power)
        {

            if (ball.Local.Bounds.IntersectsWith(Local.Bounds))
            {
                if (!typeAi)
                {
                    if (power <= 10)
                    {
                        if (moveLeft)
                        {
                            power = rand.Next(-2, -1);
                        }
                        else if (!moveLeft)
                        {
                            power = rand.Next(1, 2);
                        }
                    }
                    if (power > 10 && power < 50)
                    {
                        if (moveLeft)
                        {
                            power = 3;
                        }
                        else if (!moveLeft)
                        {
                            power = -3;
                        }
                    }
                    else if (power >= 50 && power < 70)
                    {
                        if (moveLeft)
                        {
                            power = 4;
                        }
                        else if (!moveLeft)
                        {
                            power = -4;
                        }
                    }
                    else if (power >= 70 && power < 100)
                    {
                        if (moveLeft)
                        {
                            power = 5;
                        }
                        else if (!moveLeft)
                        {
                            power = -5;
                        }
                    }
                    else if (power >= 100 && power < 120)
                    {
                        if (moveLeft)
                        {
                            power = 6;
                        }
                        else if (!moveLeft)
                        {
                            power = -6;
                        }
                    }
                    else if (power >= 120 && power < 150)
                    {
                        if (moveLeft)
                        {
                            power = 7;
                        }
                        else if (!moveLeft)
                        {
                            power = -7;
                        }
                    }
                    else if (power >= 150)
                    {
                        if (moveLeft)
                        {
                            power = 8;
                        }
                        else if (!moveLeft)
                        {
                            power = -8;
                        }
                    }
                }
                else
                {
                    power = rand.Next(-8, 8);
                }
                if (ball.Local.Bottom > Local.Top || ball.Local.Top > Local.Bottom)
                {
                    if (!typeAi)
                    {
                        ball.Local.Top = Local.Location.Y - 11;
                    }
                    else
                    {
                        ball.Local.Top = Local.Location.Y + 11;
                    }
                }
                else
                {
                    if (ball.Local.Left > Local.Right)
                    {
                        ball.Local.Left -= 15;
                    }
                    else if (ball.Local.Right > Local.Left)
                    {
                        ball.Local.Left += 15;
                    }
                }
                ball.speedL = power;
                if (ball.Local.Location.X <= Local.Bounds.Left || ball.Local.Location.X >= Local.Bounds.Right)
                {
                    ball.speedL *= -1;
                    ball.speedT *= -1;
                }
                else { ball.speedT *= -1; }
            }
        }
        public void Ai(Ball ball)
        {
            if (ball.speedL >= 0 && Local.Location.X + Local.Width < Form.ClientRectangle.Right && ball.Local.Location.X >= Local.Location.X + rand.Next(30, 110))
            {

                Local.Left += 1 * speed;
            }
            else if (ball.speedL < 0 && Local.Location.X > Form.ClientRectangle.Left && ball.Local.Location.X <= Local.Location.X + rand.Next(30, 110))
            {

                Local.Left -= 1 * speed;
            }
        }
    }
}
