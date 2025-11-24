using System.Text.Json;
using System.Text.Json.Nodes;


namespace PDAAnimator
{   
    internal class Graph
    {
        public Dictionary<string, Node> graph = new Dictionary<string, Node>();
        //construction variables
        private string fileLocation;
        private string initialState;
        // JSON parse 
        private JsonArray jsonStates;
        private JsonArray jsonAcceptStates;
        private JsonArray jsonTransTable;
        //traversal variables
        private Stack<char> stack = new Stack<char>();
        private List<Node> acceptStates = new List<Node>();
        private string head;
        private char[] inputString;
        private int currentChar;

        //public struct NonDeterminism {
        //    char currentChar;
        //    Stack<> instanceStack;
        //}

        public Graph(string fileLocation) {
            this.fileLocation = fileLocation;
        }

        //create the PDA structure based on the formal description provided in the document
        public Boolean loadGraph() {
            if (parseJson()) {
                bool test = true;
                currentChar = 0;
                while (test) {
                    int numOut = stepTraversal();
                    if (numOut == 0 || numOut == 2) {
                        test = false;
                    }
                }
                return true;
            }
            return false;
        }

        public void stepController() {
            //check if non-determinism

            //add to List of nondeterministic data

            //feed into step Traversal
        }
        
        /// <summary>
        /// Attempt to traverse a single step in the graph.
        /// </summary>
        /// <returns>0 if the graph entered a reject/illegal state. 1 if the traversal was successful. 2 if the PDA entered an accepting state.</returns>
        public int stepTraversal()
        {
            Node currentNode;
            if (graph.TryGetValue(head, out currentNode!)) {
                foreach (Transition t in currentNode.getLeavingEdges())
                {
                    //filter for end of input string, as epsilon transitions can occur while the input string is over
                    var testChar = 'e';
                    if (currentChar < inputString.Length)
                    {
                        testChar = inputString[currentChar];
                    }
                    if (t.getInput() == testChar)
                    { //is it a character transition
                        var transition = attemptTransitionPath(t);
                        graph.TryGetValue(head, out Node here);
                        var accept = isAcceptState(here);
                        if (transition && accept)
                        {
                            return 2;
                        }
                        else if (transition)
                        {
                            return 1;
                        }
                    }
                    else if (t.getInput() == 'e')
                    { //is it an epsilon transition
                        testChar = 'e'; //force epsilon transition
                        var transition = attemptTransitionPath(t);
                        graph.TryGetValue(head, out Node here);
                        var accept = isAcceptState(here);
                        if (transition && accept) {
                            return 2;
                        }
                        else if (transition) {
                            return 1;
                        }
                    }
                }
            }
            return 0;
        }

        //If the input on the transition branch matches the current char of the input string or if you are at an epsilon transition, run this function.
        // It tests if the stack state can be updated according to the transition and returns appropriately.
        private bool attemptTransitionPath(Transition t) {
            if (t.getReadStack() == 'e') { //epsilon transition time
                if (t.getWriteStack() != 'e')
                {
                    stack.Push(t.getWriteStack());
                }
                head = t.getToNode().getName();
                if (t.getInput() != 'e')
                { // do not increment on epsilon transitions
                    currentChar++;
                }
                return true;
                
            } else if (stack.TryPeek(out char r) && t.getReadStack() == r) { //ensure stack is not empty before Peek
                stack.Pop();
                if (t.getWriteStack() != 'e')
                {
                    stack.Push(t.getWriteStack());
                }
                head = t.getToNode().getName();
                if (t.getInput() != 'e') { // do not increment on epsilon transitions
                    currentChar++;
                }
                return true;
            }
            return false;
        }
        

        private Boolean parseJson() {
            try
            {
                //setup
                StreamReader sr = new StreamReader(fileLocation);
                string jsonPDA = sr.ReadToEnd();
                JsonNode node = JsonNode.Parse(jsonPDA)!;
                //begin graph object
                jsonStates = node!["Q"]!.AsArray()!;
                initialState = node["initial-state"]!.GetValue<string>();
                jsonAcceptStates = node["accept-states"]!.AsArray()!;
                jsonTransTable = node["transition-table"]!.AsArray();
                string jsonInputString = node["input-string"]!.ToString();
                //set initial state
                head = initialState;
                //set input string
                inputString = jsonInputString.ToCharArray();
                //create nodes 
                foreach (JsonNode q in jsonStates) {
                    bool isAcceptState = jsonAcceptStates.Contains(q);
                    List<Transition> transgender = new List<Transition>();
                    Node newNode = new Node(q.ToString(), transgender, isAcceptState);
                    graph.Add(newNode.getName(), newNode);
                    foreach (JsonNode j in jsonAcceptStates) {
                        if (j.ToString().Equals(q.ToString())) {
                            acceptStates.Add(newNode);
                        }
                    }
                }
                //make the transitions
                foreach (JsonNode t in jsonTransTable) {
                    //I don't want to talk about how hacky this is
                    var readStack = t["pop"]!.ToString().ToCharArray()[0];
                    var writeStack = t["push"]!.ToString().ToCharArray()[0];
                    var input = t["input"]!.ToString().ToCharArray()[0];
                    Node toNode;
                    Node fromNode;
                    if (graph.ContainsKey(t["state"]!.ToString()))
                    {
                        graph.TryGetValue(t["state"]!.ToString(), out fromNode);
                    } else {
                        throw new JsonException("improper state format in transition table");
                    }
                    if (graph.ContainsKey(t["to-state"]!.ToString()))
                    {
                        graph.TryGetValue(t["to-state"]!.ToString(), out toNode);
                    }
                    else {
                        throw new JsonException("improper to-state format in transition table");
                    }
                        Transition newTransition = new Transition(input, readStack, writeStack, fromNode!, toNode!);
                    fromNode.addEdge(newTransition);
                }
                System.Diagnostics.Debug.WriteLine(graph.Count);
                return true;
            }
            catch (Exception e) {
                return false;
            }
        }
        public int getStateCount()
        {
            return jsonStates.Count;
        }

        public char[] getStack()
        {
            return stack.ToArray();
        }

        public string getInputString()
        {
            return inputString.ToString();
        }

        public char[] getInputStringArry()
        {
            return inputString;
        }
        public int getCurrentChar()
        {
            return currentChar;
        }

        public string getCurrentNode()
        {
            return head;
        }

        private bool isAcceptState(Node node)
        {
            return acceptStates.Contains(node) && node.getLeavingEdges().Count == 0 && stack.Count == 0;
        }

        //example JSON:
        /*
         *{
    "Q": [
        "q0",
        "q1",
        "q2",
        "q3"
    ],
    "sigma": [
        "0",
        "1"
    ],
    "gamma": [
        "0",
        "1",
        "$"
    ],
    "transition-table": [
        {
            "state": "q0",
            "input": "e",
            "to-state": "q1",
            "pop": "e",
            "push": "$"
        },
        {
            "input": "0",
            "to-state": "q1",
            "state": "q1",
            "pop": "e",
            "push": "0"
        },
        {
            "input": "1",
            "to-state": "q2",
            "state": "q1",
            "pop": "0",
            "push": "e"
        },
        {
            "input": "1",
            "state": "q2",
            "to-state": "q2",
            "pop": "0",
            "push": "e"
        },
        {
            "input": "e",
            "to-state": "q3",
            "state": "q2",
            "pop": "$",
            "push": "e"
        }
    ],
    "initial-state": "q0",
    "accept-states": [
        "q3"
    ],
    "input-string": "000111"
} 
*/
    }
}
