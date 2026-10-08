using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JoguinhoBreakout
{
    public partial class Form1 : Form
    {

        bool goLeft;
        bool goRight;
        bool isGamerOver;
        int score;
        int bally;
        int ballx;
        int playerSpeed;

        Random rnd = new Random();

        PictureBox[] blocksArray;

        public Form1()
        {
            InitializeComponent();

            PlaceBlocks();
        }

        private void setupGame()
        {
            isGamerOver = false;
            score = 0;
            ballx = 5;
            bally = 5;
            playerSpeed = 12;

            txtScore.Text = "Score: " + score;

            gameTimer.Start();

            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && (string)x.Tag == "blocks")
                {
                    x.BackColor = Color.FromArgb(
                        rnd.Next(256),
                        rnd.Next(256),
                        rnd.Next(256)
                    );
                }
            }
        }

        private void gameOver(string message)
        {
            isGamerOver = true;

            gameTimer.Stop();

            txtScore.Text = "Score: " + score + " " + message;
        }

        private void PlaceBlocks()
        {
            blocksArray = new PictureBox[15];

            int a = 0;

            int top = 50;
            int left = 100;

            for (int i = 0; i < blocksArray.Length; i++)
            {
                blocksArray[i] = new PictureBox();

                blocksArray[i].Height = 32;
                blocksArray[i].Width = 100;
                blocksArray[i].Tag = "blocks";
                blocksArray[i].BackColor = Color.White;

                if (a == 5)
                {
                    top = top + 50;
                    left = 100;
                    a = 0;
                }

                if (a < 5)
                {
                    a++;

                    blocksArray[i].Left = left;
                    blocksArray[i].Top = top;

                    this.Controls.Add(blocksArray[i]);

                    left = left + 130;
                }
            }

            setupGame();
        }

        private void removeBlocks()
        {
            foreach (PictureBox x in blocksArray)
            {
                this.Controls.Remove(x);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void MainGameTimeEvent(object sender, EventArgs e)
        {
            txtScore.Text = "Score:  " + score;

            if (goLeft == true && player.Left > 0)
            {
                player.Left -= playerSpeed;
            }

            if (goRight == true && player.Left < 700)
            {
                player.Left += playerSpeed;
            }

            ball.Left += ballx;
            ball.Top += bally;

            if (ball.Left < 0 || ball.Left > 775)
            {
                ballx = -ballx;
            }

            if (ball.Top < 0)
            {
                bally = -bally;
            }

            if (ball.Bounds.IntersectsWith(player.Bounds))
            {
                bally = rnd.Next(5, 12) * -1;

                if (ballx < 0)
                {
                    ballx = rnd.Next(5, 12);
                }
                else
                {
                    ballx = rnd.Next(5, 12);
                }
            }

            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && (string)x.Tag == "blocks")
                {
                    if (ball.Bounds.IntersectsWith(x.Bounds))
                    {
                        score += 1;

                        bally = -bally;

                        this.Controls.Remove(x);

                        break;
                    }
                }
            }

            if (score == 15)
            {
                gameOver("Voce ganhou!! Aperta Enter para continuar: ");
            }

            if (ball.Top > 580)
            {
                gameOver("Voce perdeu!! Aperta Enter para continuar");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void keyisdown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goLeft = true;
            }

            if (e.KeyCode == Keys.Right)
            {
                goRight = true;
            }
        }

        private void keyisup(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goLeft = false;
            }

            if (e.KeyCode == Keys.Right)
            {
                goRight = false;
            }

            if (e.KeyCode == Keys.Enter && isGamerOver == true)
            {
                removeBlocks();

                PlaceBlocks();
            }
        }
    }
}