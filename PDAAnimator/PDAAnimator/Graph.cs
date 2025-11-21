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

        public Graph(string fileLocation) {
            this.fileLocation = fileLocation;
        }

        public void stepTraversal() {
            System.Diagnostics.Debug.WriteLine("Hi Troy!");
        }

        //create the PDA structure based on the formal description provided in the document
        public Boolean loadGraph() {
            if (parseJson()) { 
                return true;
            }
            return false;
        }
        public int getStateCount()
        {
            return jsonStates.Count;
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
                //create nodes 
                foreach (JsonNode q in jsonStates) {
                    bool isAcceptState = jsonAcceptStates.Contains(q);
                    List<Transition> transgender = new List<Transition>();
                    Node newNode = new Node(q.ToString(), transgender, isAcceptState);
                    graph.Add(newNode.getName(), newNode);
                }
                //make the transitions
                foreach (JsonNode t in jsonTransTable) {
                    //System.Diagnostics.Debug.WriteLine(t);
                    var readStack = t["pop"]!.ToString();
                    var writeStack = t["push"]!.ToString();
                    var input = t["input"]!.ToString();
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
            "to-state": "q1",
            "state": "q1",
            "pop": "e",
            "push": "1"
        },
        {
            "input": "e",
            "state": "q1",
            "to-state": "q2",
            "pop": "e",
            "push": "e"
        },
        {
            "input": "0",
            "to-state": "q2",
            "state": "q2",
            "pop": "0",
            "push": "e"
        },
        {
            "input": "1",
            "to-state": "q2",
            "state": "q2",
            "pop": "1",
            "push": "e"
        },
        {
            "input": "e",
            "state": "q2",
            "to-state": "q3",
            "pop": "$",
            "push": "e"
        }
    ],
    "initial-state": "q0",
    "accept-states": [
        "q3"
    ]
} 
*/
    }
}
