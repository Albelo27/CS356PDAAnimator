using System;
using System.Collections.Generic;
using System.Security.Policy;
using System.Text;
using System.Windows.Forms.VisualStyles;

namespace PDAAnimator
{
    internal class Node
    {
        private string name;
        private List<Transition> leavingEdges;
        private bool isAcceptState;
        /// <summary>
        /// A node in the graph representation of a Push-Down Automata
        /// </summary>
        /// <param name="name">The name of the node.</param>
        /// <param name="edges">All leaving transitions.</param>
        /// <param name="acceptState">True if node is an accepting state. Otherwise false.</param>
        public Node(string name, List<Transition> edges, bool acceptState)
        {
            //Constructor go brrrrrrr
            this.name = name;
            this.leavingEdges = edges;
            this.isAcceptState = acceptState;
        }

        public string getName() {
            return name;
        }

        public List<Transition> getLeavingEdges() { return leavingEdges; }

        public void addEdge(Transition edge) {
            leavingEdges.Add(edge);
        }

        public bool isAccept() {
            return isAcceptState;
        }

    }
}
