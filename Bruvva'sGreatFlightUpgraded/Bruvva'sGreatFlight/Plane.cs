using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;

namespace Bruvva_sGreatFlight
{
    class Plane
    {
        public int x, y, width, height;// create new integers called x, y, width and height for the plane
        public Image planeImage;// create a new image for the plane
        public Rectangle planeRec;// create a new rectangle for the plane
        public int score, highscore;// create new integers called highscore and score for the plane
        public Plane(int xspacing , int yspacing)// create new integers called xspacing and yspacing
        {
            x = xspacing;// this is to set up the spacing on the x axis (horizontally) between each plane. This spacing code is in the actual form
            y = yspacing;// this is to set up the spacing on the y axis (vertically) between each plane. This spacing code is in the actual form
            width = 585;// width of rectangle
            height = 203;// height of rectangle
            planeImage = Image.FromFile("plane2.gif");// image source
            planeRec = new Rectangle(x, y, width, height);// use integers to determine coordinates and dimensions of rectangle
        }
        public void drawPlane(Graphics g)
        {
            g.DrawImage(planeImage, planeRec);// draw the plane image in the plane rectangle
        }
        public void movePlane()
        {
            x -= 20;// move from right to left on the panel at a rate of 30 pixels each movement. The timer determines the frequency
            planeRec.Location = new Point(x, y);// change the location of plane when moved
            if (planeRec.Location.X == -200)
            {
                score += 2;// add 2 to score when plane hits left of panel
            }
                if (score > highscore)
                {
                    highscore = score;// display the current score as the high score
            }
                if (planeRec.Location.X < -1650)// why this is a different set of code to the score code is that if the plane moved back to the left of the plane at the point of scoring on the panel it would look unrealistic so I added this code to make plane move back to the left of the panel when it had exited off it
                {
                    x = 2040;// set x coordinate of plane to this figure
                    planeRec.Location = new Point(x, y);// change the location (coordinates) of plane
            }
            
        }
    }
}
