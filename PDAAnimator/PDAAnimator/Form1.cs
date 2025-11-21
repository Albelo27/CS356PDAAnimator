using System.Drawing;
using System.Windows.Forms;
using System;

namespace PDAAnimator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //This might need to be here all the time
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graph PDA = new Graph(@"C:\Users\troym\OneDrive\Documents\code\CS357\SamplePDA.txt");
            if (PDA.loadGraph())
            {//load graph loads the JSON and checks the structure before creating the PDA
                /*while (traverse) {
                   PDA.step(inputChar);
                   update animator
                   increment input string
                 check condition for ending traversal
                }*/

                Graphics g = e.Graphics;
                Brush myDrawingBrush = new SolidBrush(Color.Cyan);
                //g.FillRectangle(myDrawingBrush, 50, 50, 50, 50);

                //number of nodes to print (We have limited it to 6 states) 
                int numNodes = PDA.getStateCount();

                float centerX = 200;
                float centerY = 200;

                int radius = 150;

                //math to organize the circles in a circle
                float x = 150;
                float y = 150;
                for (int i = 0; i < numNodes; i++)
                {

                    float angles = i * (2 * MathF.PI / numNodes); // radians

                    x = centerX + MathF.Cos(angles) * radius;
                    y = centerY + MathF.Sin(angles) * radius;

                    g.FillEllipse(myDrawingBrush, x - 25, y - 25, 50, 50); // center the circle

                    System.Diagnostics.Debug.WriteLine("Hi");

                    Thread.Sleep(500);

                
                }

            }
            else {
                //crash
                Graphics g = e.Graphics;
                Brush myDrawingBrush = new SolidBrush(Color.DarkRed);
                Font drawFont = new Font("Arial", 27);
                StringFormat drawFormat = new StringFormat();
                string drawString = "Input file not recognized.\nTry Again.";
                g.DrawString(drawString, drawFont, myDrawingBrush, 150, 70, drawFormat);
                drawFont.Dispose();
                myDrawingBrush.Dispose();
                g.Dispose();
                Thread.Sleep(3000);
                Application.Exit();
                Environment.Exit(0);
            }
            
        }
    }
}
