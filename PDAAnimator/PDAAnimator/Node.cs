using System;
using System.Collections.Generic;
using System.Text;

namespace PDAAnimator
{
    internal class Node
    {
        string name;
        Transition[] leavingEdges;

        public Node(string name, Transition[] edges)
        {
            //Constructor go brrrrrrr
            this.name = name;
            this.leavingEdges = edges;
        }
    }
}
