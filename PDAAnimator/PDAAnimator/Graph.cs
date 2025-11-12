using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.Json.Nodes;
using System.Collections;

namespace PDAAnimator
{   
    internal class Graph
    {
        Dictionary<Node, Transition[]> graph = new Dictionary<Node, Transition[]>();
        string fileLocation;
        JsonArray states;
        string initialState;
        string[] acceptStates;
        //I don't want to talk about this...
        List<List<(string,string)>> transitionTable = new List<List<(string,string)>>();

        public Graph(string fileLocation) {
            this.fileLocation = fileLocation;
        }






        //create the PDA structure based on the formal description provided in the document
        public Boolean loadGraph() {
            if (parseJson()) { 
                return true;
            }
            return false;
        }


        private Boolean parseJson() {
            try
            {
                StreamReader sr = new StreamReader(fileLocation);
                string jsonPDA = sr.ReadToEnd();
                System.Diagnostics.Debug.WriteLine(jsonPDA);
                JsonNode node = JsonNode.Parse(jsonPDA)!;
                states = node!["Q"]!.AsArray()!;
                initialState = node["initial-state"]!.GetValue<string>();

                return true;
            }
            catch (IOException e) {

                return false;
            }
        }

        public int getStateCount() {
            return states.Count;
        }
    }
}
