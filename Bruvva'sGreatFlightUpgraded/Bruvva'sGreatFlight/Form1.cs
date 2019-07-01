using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Windows.Forms;

namespace Bruvva_sGreatFlight
{
    public partial class Form1 : Form
    {
        Graphics g;// create an abbreviation for the graphics class called 'g'
        Seagull[] seagull = new Seagull[5];// create 5 seagulls and use code from seagull class
        Random xspeed = new Random();// create a random for the seagull speed called xspeed
        Random yspeed = new Random();// create a random for the poo speed called yspeed
        Donkey donkey = new Donkey();// create donkey and use code from donkey class
        Plane[] plane = new Plane[2];// create 2 planes and use code from plane class
        Poo[] poo = new Poo[6];// create 6 pieces of poo and use code from poo class
        Form hscores = new HighScoresProper.HighScores();
        int score, lives, highscore, newhighscore;// create new integers called score, lives and highscore

        private void checkLives()
        {
            if (lives == 0)// if the lives are equal to 0 (all lives have been used up)
            {
                btnHighScores.Enabled = true;
                score = int.Parse(lblScore.Text);// score is eqaul to the number in the score label
                tmrDonkey.Enabled = false;// turn off donkey timer (stop donkey from moving)
                tmrSeagull.Enabled = false;// turn off seagull timer (stop seagulls from moving)
                tmrSeagull.Interval = 130;// set interval of seagull timer back to orginal value
                txtLives.Text = null;// clear lives textbox so user can re enter lives and play again
                tmrPoo.Enabled = false;// turn off poo timer (stop poo from moving)
                tmrPlane.Enabled = false;// turn off plane timer (stop planes from moving)
                txtLives.Enabled = true;// allow access to lives textbox so user can re enter lives and play again without having to re enter the game
                txtLives.Focus();//direct user focus to the lives textbox
                mnuStart.Enabled = false;// don't allow user to play game when no lives are left
                Cursor.Show();// show the mouse cursor whether mouse is moved or not
                if (score > highscore)// if the current score is greater than the high score
                {
                    highscore = int.Parse(lblHighScore.Text);// display the high score in the high score textbox
                }
                MessageBox.Show("You died!\nReset your lives and click to start to play again", "Game Over!");// show message to user alerting them that they have run out of lives and to play again, reset their lives
                for (int i = 0; i < 5; i++)// for each of the 5 seagulls (array used because it is more efficient than coding each one individually)
                {
                    int y = 10 + (i * 165);// for the spacing between the seagulls we use an array to multiply the i integer for each seagull by 165 and add 10 to it so that then becomes their y coordinate on the panel
                    seagull[i] = new Seagull(y);// for each seagull create and set the y coordiante just calculated
                }
                for (int i = 0; i < 6; i++)// for each of the 6 pieces of poo (array used because it is more efficient than coding 6 pieces of poo individually)
                {
                    int x = 5 + (i * 270); ;// for the spacing between the poo we use an array to multiply the i integer for each poo by 270 and add 5 to it so that then becomes their x coordinate on the panel
                    poo[i] = new Poo(x);// for each poo create and set the x coordiante just calculated
                }
                for (int i = 0; i < 2; i++)// for each of the 2 planes (array used because it is more efficient than coding each one individually)
                {
                    int y = (i * 380) + 90; ;// for the spacing vertically between the plane we use an array to multiply the i integer for each plane by 380 and add 90 to it so that then becomes their y coordinate on the panel
                    int x = (i * 2035) + 2040; ;// for the spacing horizontally between the spacing we use an array to multiply the i integer for each seagull by 2035 and add 2040 to it so that then becomes their x coordinate on the panel
                    plane[i] = new Plane(x, y);// for each plane create and set the y and x coordiante just calculated
                }
            }
        }
        private void scoringsystem()
        {
            score = 0;// this code seems weird but without it the score goes up at random points in the game so this is to make sure that for each plane/seagull the score goes up by 2/1
            score += plane[0].score;// add 2 to the score for this plane
            score += plane[1].score;// add 2 to the score for this plane. I would us an array but doing it manually seemed to work unlike the array
            for (int i = 0; i < 5; i++)// for each of the 5 seagulls (array used because it is more efficient than coding each one individually)
            {
                score += seagull[i].score;// for each seagull increase the score by 1 when it reaches the end of panel. This code is in the class therefore the 'seagull.' code
            }
            lblScore.Text = score.ToString();// display score in the score label
            if (score >= highscore)// if the score is greater than or equal to the highscore
            {
                highscore = score;// set the integers so they are equal if this criteria is meet
                lblHighScore.Text = highscore.ToString();// display the highscore in the highscore label
            }
        }
        private void tmrSeagull_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < 5; i++)// for each of the 5 seagulls (array used because it is more efficient than coding 5 seagulls individually)
            {
                scoringsystem();// refer back to the code under the scoringsytem heading so this code doesn't have to be repeated and cause the game to be faulty
                seagull[i].moveSeagull();// for each seagull in this array refer to the moveseagull code in the seagull class
                if (donkey.donkeyRec.IntersectsWith(seagull[i].seagullRec))// if the donkey hits the seagull
                {
                    lives -= 1;// decrease the lives by 1
                    seagull[i].x = 1330;// move the seagull in question to this specific x coordinate (right side of panel)
                    txtLives.Text = lives.ToString();// display new lives value in the lives textbox
                    checkLives();// refer to the code under the checklives heading so this code doesn't have to be repeated and cause the game to be faulty
                }
                if (score == 50)// when the score hits 50
                {
                    tmrSeagull.Interval = 120;// decrease the timer interval to 120. This speeds up the seagulls making them move faster across the panel create a sense of difficulty as the game progresses
                }
                if (score == 100)// when the score hits 100
                {
                    tmrSeagull.Interval = 110;// decrease the timer interval to 110. This speeds up the seagulls making them move faster across the panel create a sense of difficulty as the game progresses
                }
                if (score == 150)// when the score hits 150
                {
                    tmrSeagull.Interval = 100;// decrease the timer interval to 100. This speeds up the seagulls making them move faster across the panel create a sense of difficulty as the game progresses
                }
                if (score == 200)// when the score hits 200
                {
                    tmrSeagull.Interval = 90;// decrease the timer interval to 90. This speeds up the seagulls making them move faster across the panel create a sense of difficulty as the game progresses
                }
                if (score == 275)// when the score hits 275
                {
                    tmrSeagull.Interval = 80;// decrease the timer interval to 80. This speeds up the seagulls making them move faster across the panel create a sense of difficulty as the game progresses
                }
                if (score == 350)// when the score hits 350
                {
                    tmrSeagull.Interval = 65;// decrease the timer interval to 70. This speeds up the seagulls making them move faster across the panel create a sense of difficulty as the game progresses
                }
                if (score == 500)// when the score hits 500
                {
                    tmrSeagull.Interval = 50;// decrease the timer interval to 60. This speeds up the seagulls making them move faster across the panel create a sense of difficulty as the game progresses
                }
                if (score == 650)// when the score hits 650
                {
                    tmrSeagull.Interval = 35;// decrease the timer interval to 60. This speeds up the seagulls making them move faster across the panel create a sense of difficulty as the game progresses
                }


            }
            pnlBruuva.Invalidate();
        }


            private void mnuStart_Click(object sender, EventArgs e)
        {
            mnuStart.Enabled = false;
            btnHighScores.Enabled = false;
            Cursor.Hide();// hide the mouse cursor so the player only thinks about controlling the donkey
            scoringsystem();// refer back to the code under the scoringsytem heading so this code doesn't have to be repeated and cause the game to be faulty
            lives = int.Parse(txtLives.Text);// lives integer is equal to the figure entered in the lives textbox by the player
            tmrSeagull.Enabled = true;// turn on the seagull timer allowing them to move when this button is clicked
            tmrDonkey.Enabled = true;// turn on the donkey timer allowing him to move when this button is clicked
            tmrPoo.Enabled = true;// turn on the poo timer allowing them to move when this button is clicked
            tmrPlane.Enabled = true;// turn on the plane timer allowing them to move when this button is clicked
            txtLives.Enabled = false;// don't allow access to this textbox so the user can't change the content of it and 'break' the game
            txtName.Enabled = false;// don't allow access to this textbox so the user can't change the content of it and 'break' the game
            tmrSeagull.Interval = 130;// set the seagull timer interval to this value. Why I only did this for the seagulls is because the other timers have reasonably high values so it isn't going to let the game crash half way through
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {// display this message when the game loads with the instructions for the player
            MessageBox.Show("Bruvva the donkey has just been launched into the air and he needs help to avoid obstacles in the air around him. \nUse your mouse to move Bruvva around in the sky. \nAvoid the seagulls and the planes and you score points (1 per seagulls and 2 per plane). \nIf any obstacles hit you, a life is lost! \nAs you get higher scores, the seagulls, poo and planes move faster. \nIf you hit a plane, the game is over and you have to restart your progress. \nEnter your name and enter the number of lives you wish to start with (1-9) \nFeel free to play the game in full screen for the best experience.\nAfter that, click Start to begin. Good luck!", "Game Instructions");
            txtName.Focus();// direct user focus to the name textbox
            mnuStart.Enabled = false;// don't allow player to start the game without entering their name or the number of lives
            highscore = 0;// set the highscore to 0 so it is the high score for the current round
        }

        private void mnuStop_Click(object sender, EventArgs e)
        {
            Cursor.Show();//show the cursor when the button is clicked so the user can quit with knowing where the mouse is to exit more efficiently
            tmrDonkey.Enabled = false;// turn off the donkey timer which stops it from moving
            tmrSeagull.Enabled = false;// turn off the seagull timer which stops them from moving
            tmrPoo.Enabled = false;// turn off the poo timer which stops them from moving
            tmrPlane.Enabled = false;// turn off the plane timer which stops them from moving
            mnuStart.Enabled = true;
        }

        private void pnlBruuva_MouseMove(object sender, MouseEventArgs e)
        {
            donkey.donkeyRec.X = e.X;// the x coordinate of the mouse is equal to the x coordinate of the donkey rectangle and image
            donkey.donkeyRec.Y = e.Y;// the y coordinate of the mouse is equal to the x coordinate of the donkey rectangle and image
            if (donkey.donkeyRec.X > 1270)// if the donkey goes outside the panel horizontally
            {
                donkey.donkeyRec.X = 1270;// keep the x coordinate of the donkey at that point and this is so the user can't 'break' the game by cheating

            }
            if (donkey.donkeyRec.Y > 660)// if the donkey goes outside the panel vertically
            {
                donkey.donkeyRec.Y = 660;// keep the y coordinate of the donkey at that point and this is so the user can't 'break' the game by cheating

            }

        }

        private void txtName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 32)// each key on the keyboard has a number for the keychar code. I set it so only capital letters can be pressed and they would be valid
            {
                txtName.Enabled = false;//don't allow access to the name textbox once this is entered
                mnuStart.Enabled = false;// don't allow access to the menu start button when this has been entered
            }
            if (e.KeyChar > 64 && e.KeyChar < 123)// each key on the keyboard has a number for the keychar code. I set it so only capital letters can be pressed and they would be valid
            {

            }
            else
            {
               // MessageBox.Show("Please enter any capital letter", "Error");// display this error message to noifty the user they have entered an invalid character
                e.Handled = true;// don't show the invalid character entered in the name textbox
                txtName.Focus();// direct user focus back to the name textbox

            }
        }

        private void txtLives_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar > 48 && e.KeyChar < 58)// each key on the keyboard has a number for the keychar code. I set it so only numbers 1-9 can be pressed and they would be valid
            {
                txtLives.Enabled = false;//don't allow access to the lives textbox once this is entered
                mnuStart.Enabled = true;//allow access to the menu start button when this has been entered
            }
            else
            {

                MessageBox.Show("Please enter any number from 1-9", "Error");// display this error message to noifty the user they have entered an invalid character
                e.Handled = true;// don't show the invalid character entered in the lives textbox
                txtLives.Focus();// direct user focus back to the lives textbox
            }
        }

        private void tmrDonkey_Tick(object sender, EventArgs e)
        {
            tmrDonkey.Interval = 1;// set the timer interval for the donkey to 1. This is so the user doesn't experience any delay when they move the mouse and the donkey doesn't move with it
            this.Invalidate();
        }

        private void tmrPoo_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < 6; i++)// for each of the 6 pieces of poo (array used because it is more efficient than coding 6 pieces of poo individually)
            {
                poo[i].movePoo();// for each piece of poo in this array refer to the movepoo code in the poo class
                if (donkey.donkeyRec.IntersectsWith(poo[i].pooRec))// if the donkey hits any piece of poo in this array
                {
                    poo[i].y = -220;// set the x coordinate of the poo in question to this value which moves it back to the top of the panel
                    lives -= 1;// decrease the lives by 1
                    txtLives.Text = lives.ToString();// display the updated lives figure in the lives textbox so the user can keep tracking of how many lives are left
                    checkLives();// refer to the code under the checklives heading so this code doesn't have to be repeated and cause the game to be faulty
                }
            }
            if (score == 50)// when the score is equal to 50
            {
                tmrPoo.Interval -= 1;// decrease the timer interval by 1. This speeds up the poo making them move faster down the panel create a sense of difficulty as the game progresses
            }
            if (score == 100)// when the score is equal to 100
            {
                tmrPoo.Interval -= 1;// decrease the timer interval by 1. This speeds up the poo making them move faster down the panel create a sense of difficulty as the game progresses
            }
            if (score == 150)// when the score is equal to 150
            {
                tmrPoo.Interval -= 1;// decrease the timer interval by 1. This speeds up the poo making them move faster down the panel create a sense of difficulty as the game progresses
            }
            if (score == 200)// when the score is equal to 200
            {
                tmrPoo.Interval -= 1;// decrease the timer interval by 1. This speeds up the poo making them move faster down the panel create a sense of difficulty as the game progresses
            }
            if (score == 275)// when the score is equal to 275
            {
                tmrPoo.Interval -= 1;// decrease the timer interval by 1. This speeds up the poo making them move faster down the panel create a sense of difficulty as the game progresses
            }
            if (score == 350)// when the score is equal to 350
            {
                tmrPoo.Interval -= 1;// decrease the timer interval by 1. This speeds up the poo making them move faster down the panel create a sense of difficulty as the game progresses
            }
            if (score == 500)// when the score is equal to 500
            {
                tmrPoo.Interval -= 1;// decrease the timer interval by 1. This speeds up the poo making them move faster down the panel create a sense of difficulty as the game progresses
            }

        }

        private void tmrPlane_Tick(object sender, EventArgs e)
        {
            scoringsystem();// refer back to the code under the scoringsytem heading so this code doesn't have to be repeated and cause the game to be faulty
            plane[0].movePlane();// refer back to the moveplane code in the plane class for this plane to move
            plane[1].movePlane();// refer back to the moveplane code in the plane class for this plane to move. Why I haven't used an array is because it didn't seem to work. Coding it manually seemed to work just fine and for only 2 planes it isn't too much of a time waster to do so.
            if (donkey.donkeyRec.IntersectsWith(plane[0].planeRec))// if the donkey hits this particular plane
            {
                    lives -= int.Parse(txtLives.Text);//decrease the lives by the number currently in the lives textbox. This is so the user dies completely when they hit a plane
                    txtLives.Text = lives.ToString();//display the new lives value in this textbox (in this case 0)
                    checkLives();//refer back to the code under the checklives heading so the repeated code doesn't make the game faulty
                }
            if (donkey.donkeyRec.IntersectsWith(plane[1].planeRec))// if the donkey hits this particular plane
            {
                lives -= int.Parse(txtLives.Text);//decrease the lives by the number currently in the lives textbox. This is so the user dies completely when they hit a plane
                txtLives.Text = lives.ToString();//display the new lives value in this textbox (in this case 0)
                checkLives();//refer back to the code under the checklives heading so the repeated code doesn't make the game faulty
            }
            if (score == 50)// when the score is equal to 50
            {
                tmrPlane.Interval = 46;// decrease the timer interval by 1. This speeds up the planes making them move faster down the panel create a sense of difficulty as the game progresses
            }
            if (score == 100)// when the score is equal to 100
            {
                tmrPlane.Interval = 42;// decrease the timer interval by 1. This speeds up the planes making them move faster down the panel create a sense of difficulty as the game progresses
            }
            if (score == 150)// when the score is equal to 150
            {
                tmrPlane.Interval = 38;// decrease the timer interval by 1. This speeds up the planes making them move faster down the panel create a sense of difficulty as the game progresses
            }
            if (score == 200)// when the score is equal to 200
            {
                tmrPlane.Interval = 34;// decrease the timer interval by 1. This speeds up the planes making them move faster down the panel create a sense of difficulty as the game progresses
            }
            if (score == 275)// when the score is equal to 275
            {
                tmrPlane.Interval = 30;// decrease the timer interval by 1. This speeds up the planes making them move faster down the panel create a sense of difficulty as the game progresses
            }
            if (score == 350)// when the score is equal to 350
            {
                tmrPlane.Interval = 26;// decrease the timer interval by 1. This speeds up the planes making them move faster down the panel create a sense of difficulty as the game progresses
            }
            if (score == 500)// when the score is equal to 500
            {
                tmrPlane.Interval = 22;// decrease the timer interval by 1. This speeds up the planes making them move faster down the panel create a sense of difficulty as the game progresses
            }
            if (score == 650)// when the score is equal to 650
            {
                tmrPlane.Interval = 14;// decrease the timer interval by 1. This speeds up the planes making them move faster down the panel create a sense of difficulty as the game progresses
            }



        }

        private void txtName_Click(object sender, EventArgs e)
        {
            MessageBox.Show("To enter your name, type it in then press the space bar to continue. \nIf you wish to delete anything in the name you entered, highlight the part you wish to delete then use the 'Delete' key; not the 'Backspace' key.", "Name Instructions");

        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
           
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 115)// each key on the keyboard has a number for the keychar code. I set it so only capital letters can be pressed and they would be valid
            {
                Cursor.Show();//show the cursor when the button is clicked so the user can quit with knowing where the mouse is to exit more efficiently
                tmrDonkey.Enabled = false;// turn off the donkey timer which stops it from moving
                tmrSeagull.Enabled = false;// turn off the seagull timer which stops them from moving
                tmrPoo.Enabled = false;// turn off the poo timer which stops them from moving
                tmrPlane.Enabled = false;// turn off the plane timer which stops them from moving
                mnuStart.Enabled = true;
            }
        }

        private void btnHighScores_Click(object sender, EventArgs e)
        {
            newhighscore = int.Parse(lblHighScore.Text);
            //instantiate a new HighScores Form called highScores
            // see if score is greater than lowest score
            String s = null;
            int lowScore;
            try
            {
                System.IO.StreamReader inputFile = new System.IO.StreamReader(Application.StartupPath + "\\highscores.txt");
                for (int i = 0; i < 20; i++)
                    s = inputFile.ReadLine();
                lowScore = Convert.ToInt32(s);
                inputFile.Close();
            }
            catch (Exception ex)
            {
                lowScore = 0;

            }
            if (newhighscore > lowScore) //if the score is high enough, it opens highscore form and adds in the new score
            {
                Form hscores2 = new HighScoresProper.HighScores(newhighscore, txtName.Text); //open new form with name, score
                hscores2.ShowDialog();
            }
            else
            {
                // Display the Form without passing a score and name

                hscores.ShowDialog();
            }
        
    }

        public Form1()
        {
            InitializeComponent();
            for (int i = 0; i < 5; i++)// for each of the 5 seagulls (array used because it is more efficient than coding each one individually)
            {
                int y = 10 + (i * 165);// for the spacing between the seagulls we use an array to multiply the i integer for each seagull by 165 and add 10 to it so that then becomes their y coordinate on the panel
                seagull[i] = new Seagull(y);// for each seagull create and set the y coordiante just calculated
            }
            for (int i = 0; i < 6; i++)// for each of the 6 pieces of poo (array used because it is more efficient than coding 6 pieces of poo individually)
            {
                int x = 5 + (i * 270); ;// for the spacing between the poo we use an array to multiply the i integer for each poo by 270 and add 5 to it so that then becomes their x coordinate on the panel
                poo[i] = new Poo(x);// for each poo create and set the x coordiante just calculated
            }
            for (int i = 0; i < 2; i++)// for each of the 2 planes (array used because it is more efficient than coding each one individually)
            {
                int y = (i * 380) + 90; ;// for the spacing vertically between the plane we use an array to multiply the i integer for each plane by 380 and add 90 to it so that then becomes their y coordinate on the panel
                int x = (i * 2035) + 2040; ;// for the spacing horizontally between the spacing we use an array to multiply the i integer for each seagull by 2035 and add 2040 to it so that then becomes their x coordinate on the panel
                plane[i] = new Plane(x, y);// for each plane create and set the y and x coordiante just calculated
            }

            typeof(Panel).InvokeMember("DoubleBuffered", BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic, null, pnlBruuva, new object[] { true });// this code is to stop the panel and everything in it from flickering creating a solid image for the user
        }

    private void pnlBruuva_Paint(object sender, PaintEventArgs e)
        {
            g = e.Graphics;
            donkey.drawDonkey(g);// draw the donkey image on the panel. To do this refer back to the draw donkey code in the donkey class
            for (int i = 0; i < 5; i++)// for each of the 5 seagulls (array used because it is more efficient than coding each one individually)
            {
                int rndmspeed = xspeed.Next(5, 80);// set their speed to a random number between 5 and 80 so they each move at different speeds across the panel. It makes the game more interesting if they all go at different speeds
                seagull[i].x -= rndmspeed;// change the x movement of the seagull to the random number (speed) generated
                seagull[i].drawSeagull(g);// draw the seagull image on the panel. To do this refer back to the draw seagull code in the seagull class
            }
            for (int i = 0; i < 6; i++)// for each of the 6 pieces of poo (array used because it is more efficient than coding each one individually)
            {
                int rndmspeed2 = yspeed.Next(2, 55);// set their speed to a random number between 2 and 55 so they each move at different speeds across the panel. It makes the game more interesting if they all go at different speeds
                poo[i].y += rndmspeed2;// change the y movement of the poo to the random number (speed) generated
                poo[i].drawPoo(g);// draw the poo image on the panel. To do this refer back to the draw poo code in the poo class
            }
                plane[0].drawPlane(g);// refer back to the drawplane code in the plane class for this plane to move. Why I haven't used an array is because it didn't seem to work. Coding it manually seemed to work just fine and for only 2 planes it isn't too much of a time waster to do so.
            plane[1].drawPlane(g);// refer back to the drawplane code in the plane class for this plane to move. Why I haven't used an array is because it didn't seem to work. Coding it manually seemed to work just fine and for only 2 planes it isn't too much of a time waster to do so.
            // why I didn't set the planes to move at random speeds is so they enter the panel at the right time. If both planes are on the plane it's impossible for the player to move past them so this is why I did this
        }
    }
}
