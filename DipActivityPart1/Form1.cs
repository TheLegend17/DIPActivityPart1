using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DipActivityPart1
{
    public partial class Form1 : Form
    {
        Bitmap loaded, process;
        Bitmap imageB, imageA, finalOutput;
        public Form1()
        {
            InitializeComponent();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        private void greyScaleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            process = new Bitmap(loaded.Width, loaded.Height);
            for (int x = 0; x < loaded.Width; x++)
            {
                for (int y = 0; y < loaded.Height; y++)
                {
                    Color pixel = loaded.GetPixel(x, y);
                    int grey = (pixel.R+pixel.G+pixel.B)/ 3;
                    Color greyscale = Color.FromArgb(grey, grey, grey);
                    process.SetPixel(x, y, greyscale);
                }
                pictureBox2.Image = process;
            }
        }

        private void basicCopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            process = new Bitmap(loaded.Width, loaded.Height);
            for (int x = 0; x < loaded.Width; x++)
            {
                for (int y = 0; y < loaded.Height; y++)
                {
                    Color pixel = loaded.GetPixel(x, y);
                    process.SetPixel(x, y, pixel);
                }
            }
            pictureBox2.Image = process;
        }

        private void colorInversionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            process = new Bitmap(loaded.Width, loaded.Height);
            for (int x = 0; x < loaded.Width; x++)
            {
                for (int y = 0; y < loaded.Height; y++)
                {
                    Color pixel = loaded.GetPixel(x, y);
                    Color newInvertedPixel = Color.FromArgb(255-pixel.R, 255-pixel.G, 255-pixel.B);
                    process.SetPixel(x, y, newInvertedPixel);
                }
            }
            pictureBox2.Image = process;
        }

        private void histogramToolStripMenuItem_Click(object sender, EventArgs e)
        {
            process = new Bitmap(loaded.Width, loaded.Height);
            Color sample;
            Color gray;
            Byte graydata;
            //Grayscale Convertion;
            for (int x = 0; x < loaded.Width; x++)
            {
                for (int y = 0; y < loaded.Height; y++)
                {
                    sample = loaded.GetPixel(x, y);
                    graydata = (byte)((sample.R + sample.G + sample.B) / 3);
                    gray = Color.FromArgb(graydata, graydata, graydata);
                    loaded.SetPixel(x, y, gray);
                }
            }

            //histogram 1d data;
            int[] histdata = new int[256]; // array from 0 to 255
            for (int x = 0; x < loaded.Width; x++)
            {
                for (int y = 0; y < loaded.Height; y++)
                {
                    sample = loaded.GetPixel(x, y);
                    histdata[sample.R]++; // can be any color property r,g or b
                }
            }

            // Bitmap Graph Generation
            // Setting empty Bitmap with background color
            process = new Bitmap(256, 800);
            for (int x = 0; x < 256; x++)
            {
                for (int y = 0; y < 800; y++)
                {
                    process.SetPixel(x, y, Color.White);
                }
            }
            // plotting points based from histdata
            for (int x = 0; x < 256; x++)
            {
                for (int y = 0; y < Math.Min(histdata[x] / 5, process.Height - 1); y++)
                {
                    process.SetPixel(x, (process.Height - 1) - y, Color.Black);
                }
            }
            pictureBox2.Image = process;
        }

        private void sepiaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            process = new Bitmap(loaded.Width, loaded.Height);
            for (int x = 0; x < loaded.Width; x++)
            {
                for (int y = 0; y < loaded.Height; y++)
                {
                    Color pixel = loaded.GetPixel(x, y);
                    int tr = Convert.ToInt32((0.393 * pixel.R) + (0.769 * pixel.G) + (0.189 * pixel.B));
                    int tg = Convert.ToInt32((0.349 * pixel.R) + (0.686 * pixel.G) + (0.168 * pixel.B));
                    int tb = Convert.ToInt32((0.272 * pixel.R) + (0.534 * pixel.G) + (0.131 * pixel.B));
                    if (tr > 255)
                        tr = 255;                 
                    if (tg > 255)
                        tg = 255;
                    if (tb > 255)
                        tb = 255;
                    Color p = Color.FromArgb(tr, tg, tb);
                    process.SetPixel(x, y, p);
                }
            }
            pictureBox2.Image = process;
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.ShowDialog(this);
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            saveFileDialog1.Filter = "All Files|*.*|Bitmap Files (*)|*.bmp;*.gif;*.jpg";
            pictureBox2.Image.Save(saveFileDialog1.FileName);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog2.ShowDialog();
        }

        private void openFileDialog3_FileOk(object sender, CancelEventArgs e)
        {
            imageA = new Bitmap(openFileDialog3.FileName);
            pictureBox2.Image=imageA;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            finalOutput = new Bitmap(imageB.Width, imageB.Height);
            Color myGreen = Color.FromArgb(0, 0, 255);
            int greyGreen = Convert.ToInt32((myGreen.R + myGreen.G + myGreen.B) / 3);
            int threshold = 5;
            for (int x = 0; x < imageB.Width; x++)
            {
                for (int y = 0; y < imageB.Height; y++)
                {
                    Color pixel = imageB.GetPixel(x, y);
                    Color backPixel = imageA.GetPixel(x, y);
                    int grey = (pixel.R + pixel.G + pixel.B) / 3;
                    int subtractValue = Math.Abs(grey - greyGreen);
                    if (subtractValue < threshold)
                        finalOutput.SetPixel(x, y, backPixel);
                    else
                        finalOutput.SetPixel(x, y, pixel);
                }
            }
            pictureBox3.Image = finalOutput;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            openFileDialog3.ShowDialog();
        }

        private void openFileDialog2_FileOk(object sender, CancelEventArgs e)
        {
            imageB = new Bitmap(openFileDialog2.FileName);
            pictureBox1.Image = imageB;
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            loaded = new Bitmap(openFileDialog1.FileName);
            pictureBox1.Image = loaded;
        }
    }
}
