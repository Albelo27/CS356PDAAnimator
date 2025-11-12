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
            Graph PDA = new Graph(@"C:\Users\troym\OneDrive\Documents\code\CS357\SamplePDA.txt");
            if (PDA.loadGraph())
            {//load graph loads the JSON and checks the structure before creating the PDA
                /*while (traverse) {
                   PDA.step(inputChar);
                   update animator
                   increment input string
                 check condition for ending traversal
                }*/


                //TROY
                //PDA.getStateCount() will return the number of states from the json file that I have included in the repo.
                //its called SamplePDA.txt and make sure that you correct the file path(line 22), it has to be absolute rn, or the program will eat shit and die
                //best of luck

            }
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
    }
}
