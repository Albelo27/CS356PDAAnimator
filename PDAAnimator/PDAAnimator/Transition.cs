namespace PDAAnimator
{
    internal class Transition
    {
        char input;
        char readStack;
        char writeStack;
        Node fromNode;
        Node toNode;
        /// <summary>
        /// A transition on a PDA from one node to itself or another node.
        /// </summary>
        /// <param name="input">The character read from the input string when moving over this transition.</param>
        /// <param name="readStack">The character that must be popped from the stack in order to pass through this transition.</param>
        /// <param name="writeStack">The character that must be pushed to the stack in order to pass through this transition.</param>
        /// <param name="fromNode">The node from which this transition originates.</param>
        /// <param name="toNode">The node this transition leads to.</param>
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
