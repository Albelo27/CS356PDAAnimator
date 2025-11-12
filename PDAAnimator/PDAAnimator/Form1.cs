using System.Drawing;
using System.Windows.Forms;

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
            Graph PDA = new Graph(@"C:\Users\canth\Documents\Development\PDAAnimator\CS356PDAAnimator\SamplePDA.txt");
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

                //number of nodes to print
                int numNodes = PDA.getStateCount();

                //math to organize the circles in a circle
                int x = 0;
                int y = 0;
                for (int i = 0; i < numNodes; i++)
                {
                    g.FillEllipse(myDrawingBrush, x, y, 50, 50);
                    x += 100;
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
