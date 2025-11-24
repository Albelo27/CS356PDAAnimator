using System.Drawing;
using System.Windows.Forms;
using System;
using System.Security.Cryptography.X509Certificates;
using System.Runtime.Intrinsics.X86;

namespace PDAAnimator
{
    public partial class Form1 : Form
    {
        Graph PDA;
        bool truth = false;
        public Form1()
        {
            PDA = new Graph(@"C:\Users\troym\OneDrive\Documents\code\CS357\SamplePDA.txt");
            truth = PDA.loadGraph();
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //This might need to be here all the time
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            //Graph PDA = new Graph(@"C:\Users\troym\OneDrive\Documents\code\CS357\SamplePDA.txt");
            if (truth)
            {
                Graphics g = e.Graphics;
                Brush myDrawingBrush = new SolidBrush(Color.Cyan);
                System.Drawing.Font drawFont = new System.Drawing.Font("Arial", 16);
                System.Drawing.Font drawFontSmall = new System.Drawing.Font("Arial", 10);
                System.Drawing.SolidBrush drawBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Yellow);
                System.Drawing.StringFormat drawFormat = new System.Drawing.StringFormat();


                //array of locations for the nodes (To a maximum of 6)
                Zooble[] locs = new Zooble[6];
                
                //number of nodes to print (We have limited it to 6 states) 
                int numNodes = PDA.getStateCount();

                float centerX = 250;
                float centerY = 250;

                int radius = 150;

                //math to organize the circles in a circle
                float x = 150;
                float y = 150;
                for (int i = 0; i < numNodes; i++)
                {
                    //how the location is changed from node to node
                    float angles = i * (2 * MathF.PI / numNodes);

                    x = centerX + MathF.Cos(angles) * radius;
                    y = centerY + MathF.Sin(angles) * radius;

                    //draws the state
                    g.FillEllipse(myDrawingBrush, x - 25, y - 25, 50, 50); // center the circle

                    //logs the sate's location
                    Zooble q = new Zooble(x, y);
                    locs[i] = q;
                    
                }


                //Print the transitions
                var graph = PDA.graph;
                for(int i = 0; i < numNodes; i++)
                {
                    String stateName = "q" + i;
                    Node currState = graph[stateName];
                    Pen pen = new Pen(Color.White);

                    int startX = (int)locs[i].getX();
                    int startY = (int)locs[i].getY();

                    int endX = 0;
                    int endY = 0;

                    //trackers for number of transitions made to each node for presentation purposes
                    int selfCounter = 0;
                    int zeroCounter = 0;
                    int oneCounter = 0;
                    int twoCounter = 0;
                    int threeCounter = 0;
                    int fourCounter = 0;
                    int fiveCounter = 0;

                    foreach (Transition transit in currState.getLeavingEdges())
                    {
                        String toState = transit.getToNode().getName();

                        while (true)
                        { 
                            if (toState == stateName)
                            {
                                g.DrawEllipse(pen, new Rectangle(startX, startY, 50, 50));
                                g.DrawString((transit.getInput() + ", " + transit.getReadStack() + " \u2192 " + transit.getWriteStack()),
                                    drawFontSmall, drawBrush, startX + 50, startY + 50 + (selfCounter * 10), drawFormat);
                                selfCounter++;
                                break;
                            }
                            else if (toState == "q0")
                            {
                                endX = (int)locs[0].getX();
                                endY = (int)locs[0].getY();
                                g.DrawLine(pen, startX, startY, endX, endY);
                                g.DrawString((transit.getInput() + ", " + transit.getReadStack() + " \u2192 " + transit.getWriteStack()), 
                                    drawFontSmall, drawBrush, ((startX+endX)/2), ((startY+endY)/2) + (zeroCounter*10), drawFormat);
                                zeroCounter++;
                                break;
                            }
                            else if (toState == "q1")
                            {
                                endX = (int)locs[1].getX();
                                endY = (int)locs[1].getY();
                                g.DrawLine(pen, startX, startY, endX, endY);
                                g.DrawString((transit.getInput() + ", " + transit.getReadStack() + " \u2192 " + transit.getWriteStack()), 
                                    drawFontSmall, drawBrush, ((startX + endX) / 2), ((startY + endY) / 2) + (oneCounter * 10), drawFormat);
                                oneCounter++;
                                break;
                            }
                            else if (toState == "q2")
                            {
                                endX = (int)locs[2].getX();
                                endY = (int)locs[2].getY();
                                g.DrawLine(pen, endX, endY, startX, startY);
                                g.DrawString((transit.getInput() + ", " + transit.getReadStack() + " \u2192 " + transit.getWriteStack()), 
                                    drawFontSmall, drawBrush, ((startX + endX) / 2), ((startY + endY) / 2) + (twoCounter * 10), drawFormat);
                                twoCounter++;
                                break;
                            }
                            else if (toState == "q3")
                            {
                                endX = (int)locs[3].getX();
                                endY = (int)locs[3].getY();
                                g.DrawLine(pen, startX, startY, endX, endY);
                                g.DrawString((transit.getInput() + ", " + transit.getReadStack() + " \u2192 " + transit.getWriteStack()), 
                                    drawFontSmall, drawBrush, ((startX + endX) / 2), ((startY + endY) / 2) + (threeCounter * 10), drawFormat);
                                threeCounter++;
                                break;
                            }
                            else if (toState == "q4")
                            {
                                endX = (int)locs[4].getX();
                                endY = (int)locs[4].getY();
                                g.DrawLine(pen, startX, startY, endX, endY);
                                g.DrawString((transit.getInput() + ", " + transit.getReadStack() + " \u2192 " + transit.getWriteStack()), 
                                    drawFontSmall, drawBrush, ((startX + endX) / 2), ((startY + endY) / 2) + (fourCounter * 10), drawFormat);
                                fourCounter++;
                                break;
                            }
                            else if (toState == "q5")
                            {
                                endX = (int)locs[5].getX();
                                endY = (int)locs[5].getY();
                                g.DrawLine(pen, startX, startY, endX, endY);
                                g.DrawString((transit.getInput() + ", " + transit.getReadStack() + " \u2192 " + transit.getWriteStack()), 
                                    drawFontSmall, drawBrush, ((startX + endX) / 2), ((startY + endY) / 2) + (fiveCounter * 10), drawFormat);
                                fiveCounter++;

                                break;
                            }
                        }
                    }

                }
                

                //Print the input String we're parsing
                int textX = 200;
                int textY = 0;
                string input = PDA.getInputString();
                g.DrawString("Input String: " + input, drawFont, drawBrush, textX, textY, drawFormat);


                //visual representation of the stack

                stack(g, PDA.getStack(), 0, drawFont, drawBrush, myDrawingBrush, drawFormat);

                //Code for implementing non-determinism
                //char[] currentStack = PDA.getStack();
                //int j = 0;
                //foreach (Nondeterminism non in PDA.step) 
                //{
                //    stack(g, non.getStack(), j, drawFont, drawBrush, myDrawingBrush, drawFormat);
                //    j++;
                //}




                //display current read char
                char currRead = input[PDA.getCurrentChar()];
                currChar(g, currRead, 0, drawBrush, myDrawingBrush, drawFont, drawFormat);

                //code for implementing non-determinism
                //int k = 0;
                //foreach (Nondeterminism non in PDA.step)
                //{
                //    currChar(g, non.character, k, drawBrush, myDrawingBrush, drawFont, drawFormat);
                //    k++;

                //}

                //display currently processed input
                inState(g, PDA.getCurrentNode(), 50, locs);


            }
            else
            {
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
        //To visually represent the stack: 
        //g = graphics where drawing happens
        //stk = the stack being represented
        //stackCount = number of stacks being represented (for display purposes)
        //rest of variable for drawing
        public void stack(Graphics g, char[] stk, int stackCount, Font drawFont, Brush drawBrush,Brush myDrawingBrush, StringFormat drawFormat)
        {

            //location of stack visual
            int stackX = 500;
            int stackY = 400;

            stackX += 60 * stackCount;

            int numInStack = stk.Length;

            for (int i = 0; i < numInStack; i++)
            {
                char currChar = stk[i];
                String s = currChar.ToString();
                g.FillRectangle(myDrawingBrush, stackX, stackY, 50, 50);
                g.DrawString(s, drawFont, drawBrush, stackX + 10, stackY + 5, drawFormat);
                stackY -= 60;
            }
        }

        //To display character being processed 
        //g = graphics where drawing happens
        //c = character being processed
        //numCurrs = number of items being processed
        //rest of variables for drawing
        public void currChar(Graphics g, char c, int numCurrs, Brush drawBrush, Brush myDrawingBrush, Font drawFont, StringFormat drawFormat)
        {
            int curCharX = 550;
            int curCharY = 0;

            curCharX += numCurrs * 60;
            String str = c.ToString();
            //String reader = "B";
            g.FillRectangle(myDrawingBrush, curCharX, curCharY, 50, 50);
            g.DrawString(str, drawFont, drawBrush, curCharX + 10, curCharY + 5, drawFormat);
            g.DrawString("Current Char", drawFont, drawBrush, curCharX + 60, curCharY + 5, drawFormat);

        }

        //g = graphics where drawing happens
        //stateName = name of state currently being processed
        //size = size of circle
        //myDrawingBrush = for drawing the "processing" sphere
        public void inState(Graphics g, String stateName, int size, Zooble[] graph)
        {
            //setting up location for "processing"
            float x = 0;
            float y = 0;

            if(stateName == "q0")
            {
                x = graph[0].getX();
                y = graph[0].getY();
            }
            else if(stateName == "q1")
            {
                x = graph[1].getX();
                y = graph[1].getY();
            }
            else if (stateName == "q2")
            {
                x = graph[2].getX();
                y = graph[2].getY();
            }
            else if (stateName == "q3")
            {
                x = graph[3].getX();
                y = graph[3].getY();
            }
            else if (stateName == "q4")
            {
                x = graph[4].getX();
                y = graph[4].getY();
            }
            else if (stateName == "q5")
            {
                x = graph[5].getX();
                y = graph[5].getY();
            }
            Brush myDrawingBrush = new SolidBrush(Color.Green);

            g.FillEllipse(myDrawingBrush, x - 25, y - 25, size, size);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PDA.stepTraversal();
            System.Diagnostics.Debug.WriteLine("button");
            this.Refresh();
        }
        public struct Zooble
        {
            float X;
            float Y;

            public Zooble(float x, float y)
            {
                X = x;
                Y = y;
            }
            public float getX() { return X; }
            public float getY() { return Y; }
        }
    }
}
