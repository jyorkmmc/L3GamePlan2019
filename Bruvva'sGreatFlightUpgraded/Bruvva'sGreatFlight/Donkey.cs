using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;

namespace Bruvva_sGreatFlight
{
    class Donkey
    {
        public int x, y, width, height;// create new integers called x, y, width and height for the donkey
        public Image donkey;// create a new image for the donkey

        public Rectangle donkeyRec;// create a new rectangle for the donkey

        public Donkey()
        {
            x = 10;// x value of donkey
            y = 20;// y value of donkey
            width = 120;// width of donkey rectangle
            height = 59;// height of donkey rectangle
            donkey = Image.FromFile("donkey.gif");// image source
            donkeyRec = new Rectangle(x, y, width, height);// use integers to determine coordinates and dimensions of rectangle
        }
        public void drawDonkey(Graphics g)
        {
            g.DrawImage(donkey, donkeyRec);// draw the donkey image in the donkey rectangle
        }
    }
}
