using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWorkForDigitalDesign.Classes
{
    public class Lever
    {
        public int x { get; set; }
        public int y { get; set; }
        public int i { get; set; }
        public int j { get; set; }
        public Image Img { get; set; }

        public bool isRotate { get; set; }

        public const int size = 50;

        public Lever(int i, int j)
        {
            x = j * 50;
            y = i * 50;
            this.i = i;
            this.j = j;
            Img = new Bitmap("resources\\lever.png");
            isRotate = false;
        }

        public void RotateLever()
        {
            Img.RotateFlip(RotateFlipType.Rotate90FlipY);
            isRotate = !isRotate;
        }
    }
}
