using System;
using System.Collections.Generic;
using System.Text;

namespace PDAAnimator
{
    internal class Transition
    {
        char input;
        char readStack;
        char writeStack;
        Node fromNode;
        Node toNode;
        //Me when I construct:
        public Transition(char input, char readStack, char writeStack, Node fromNode, Node toNode)
        {
            this.input = input;
            this.readStack = readStack;
            this.writeStack = writeStack;
            this.fromNode = fromNode;
            this.toNode = toNode;
        }

        public char getInput() { return input; }
        public char getReadStack() { return readStack; }
        public char getWriteStack() { return writeStack; }
        public Node getFromNode() { return fromNode; }
        public Node getToNode() { return toNode; }
    }
}
