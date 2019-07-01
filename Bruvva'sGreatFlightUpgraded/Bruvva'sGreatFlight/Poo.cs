using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;

namespace Bruvva_sGreatFlight
{
    class Poo
    {
        public int x, y, width, height;// create new integers called x, y, width and height for the poo
        public Image pooImage;// create a new image for the seagull
        public Rectangle pooRec;// create a new image for the seagull
        public Poo(int spacing)// create a new integer called spacing
        {
            x = spacing;// this is to set up the spacing between each poo. The spacing code is in the actual form
            y = -220;// y value of poo
            width = 5;// width of poo rectangle
            height = 15;// height of poo rectangle
            pooImage = Image.FromFile("poo.gif");// image source
            pooRec = new Rectangle(x, y, width, height);// use integers to determine coordinates and dimensions of rectangle
        }
        public void drawPoo(Graphics g)
        {
            g.DrawImage(pooImage, pooRec);// draw the poo image in the poo rectangle
        }
        public void movePoo()
        {
            y += 10;// move from up to down the panel at a rate of 10 pixels each movement. The timer determines the frequency
            pooRec.Location = new Point(x, y);// change the location of poo when moved
            if (pooRec.Location.Y > 1400)
            {
                y = -220;// move poo back to the top of panel
                pooRec.Location = new Point(x, y);// change the location (coordinates) of poo
            }
        }
    }
}
