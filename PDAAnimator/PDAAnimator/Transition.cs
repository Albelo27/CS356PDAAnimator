using System;
using System.Collections.Generic;
using System.Text;

namespace PDAAnimator
{
    internal class Transition
    {
        string input;
        string readStack;
        string writeStack;
        Node fromNode;
        Node toNode;
        //Me when I construct:
        public Transition(string input, string readStack, string writeStack, Node fromNode, Node toNode)
        {
            this.input = input;
            this.readStack = readStack;
            this.writeStack = writeStack;
            this.fromNode = fromNode;
            this.toNode = toNode;
        }

        public string getInput() { return input; }
        public string getReadStack() { return readStack; }
        public string getWriteStack() { return writeStack; }
        public Node getFromNode() { return fromNode; }
        public Node getToNode() { return toNode; }
    }
}
