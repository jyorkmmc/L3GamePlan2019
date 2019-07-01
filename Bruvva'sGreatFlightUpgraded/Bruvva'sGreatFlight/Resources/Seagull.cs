using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;

namespace Bruvva_sGreatFlight
{
    class Seagull
    {
        public int x, y, width, height;// create new integers called x, y, width and height for the seagulls
        public Image seagullImage;// create a new image for the seagull
        public Rectangle seagullRec;// create a new rectangle for the seagull image to be stored in
        public int score, highscore;// create new integers called highscore and score for the seagulls
        public Seagull(int spacing)// create a new integer called spacing
        {
            x = 1345;// x value of seagull
            y = spacing;// this is to set up the spacing between each seagull. The spacing code is in the actual form
            width = 40;// width of rectangle
            height = 32;// height of rectangle
            seagullImage = Image.FromFile("seagull.gif");// image source
            seagullRec = new Rectangle(x, y, width, height);// use integers to determine coordinates and dimensions of rectangle
        }
        public void drawSeagull(Graphics g)
        {
            g.DrawImage(seagullImage, seagullRec);// draw the seagull image in the seagull rectangle
        }
        public void moveSeagull()
        {
            x -= 30;// move from right to left on the panel at a rate of 30 pixels each movement. The timer determines the frequency
            seagullRec.Location = new Point(x, y);// change the location of seagull when moved
            if (seagullRec.Location.X < -10)
            {
                score += 1;// add 1 to score when seagull hits left of panel                 
                if (score > highscore)
                {
                    highscore = score;// display the current score as the high score
                }
                x = 1345;// move seagull back to right side of panel
                seagullRec.Location = new Point(x, y);// change the location (coordinates) of seagull
            }
        }
    }
}
