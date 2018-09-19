using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace MendalForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
          
        }

        void  DrawMImage(double initx, double inity,double mag, uint iterations)
        {
            Size sz = new Size();
            double fMagLevel = mag;
            double fViewRectReal = initx, fViewRectImg = inity;
            sz.Width = pictureBox1.Width;
            sz.Height = pictureBox1.Height;
            System.Drawing.Bitmap flag = new System.Drawing.Bitmap(sz.Height, sz.Width);

            Debug.WriteLine("Size:" + sz.ToString());
            for (int y = 0; y < flag.Height; ++y)
            {
                double fCImg = fViewRectImg + y * fMagLevel;
                for (int x = 0; x < flag.Width; ++x)
                {
                    double fCReal = fViewRectReal + x * fMagLevel;
                    double fZReal = fCReal;
                    double fZImg = fCImg;

                    // apply the formula...

                    //bool bInside = true;
                    int n;
                    for (n = 0; n < iterations; n++)
                    {
                        double fZRealSquared = fZReal * fZReal;
                        double fZImgSquared = fZImg * fZImg;

                        // have we escaped?

                        if (fZRealSquared + fZImgSquared > 4)
                        {
                            //bInside = false;
                            break;
                        }

                        fZImg = 2 * fZReal * fZImg + fCImg;
                        fZReal = fZRealSquared - fZImgSquared + fCReal;
                    }

                    if (n == iterations)
                    {
                       n = 0;
                    }

                    flag.SetPixel(x, y, Color.FromArgb(n % 0xff, n * 10 % 0xff, n * 100 % 0xff));
                   
                }
           }
            Debug.WriteLine("done");
            pictureBox1.Image = flag;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DrawMImage(-2.3, -1.0, 0.005, 4* 1024);
        }

        private void pictureBox1_SizeChanged(object sender, EventArgs e)
        {
            //DrawMImage(-2.3, -1.0, 0.002, 4 * 1024);
        }
    }

}
