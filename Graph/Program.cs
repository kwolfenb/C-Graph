using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph
{
    public class Graph
    {
        public Dictionary<string, List<string>> adjacencyList;
        public Graph()
        {
            this.adjacencyList = new Dictionary<string, List<string>>() { };
        }

        public void addVertex(string vertex) // Add new vertex
        {
            if (!this.adjacencyList.ContainsKey(vertex))
            {
                this.adjacencyList.Add(vertex, new List<string>());
            }
            else
            {
                Console.WriteLine("this vertex is in use");
            }
        }

        public void removeVertex(string vertex) // Remove Vertex
        {
            while (this.adjacencyList[vertex].Count > 0)
            {
                var adjacentVertex = this.adjacencyList[vertex][0];
                this.adjacencyList[vertex].RemoveAt(0);
                this.removeEdge(adjacentVertex, vertex);
            }
            this.adjacencyList.Remove(vertex);
        }
        public void addEdge(string v1, string v2) // New edge between 2 vertices
        {
            if (this.adjacencyList.ContainsKey(v1) && this.adjacencyList.ContainsKey(v2))
            {
                this.adjacencyList[v1].Add(v2);
                this.adjacencyList[v2].Add(v1);
            }
            else
            {
                Console.WriteLine("Error: Vertex does not exist");
            }
        }
        public void removeEdge(string v1, string v2)
        {
            if (this.adjacencyList.ContainsKey(v1) && this.adjacencyList.ContainsKey(v2))
            {
                this.adjacencyList[v1].Remove(v2);
                this.adjacencyList[v2].Remove(v1);
            }
        }

        public List<string> depthFirstTraversalRecursive(string start)
        {
            List<string> result = new List<string>();
            Dictionary<string, bool> visitedVerts = new Dictionary<string, bool>();

            void dFS(string v)
            {
                if (v == "")
                {
                    return;
                }
                if (visitedVerts.ContainsKey(v))
                {
                    visitedVerts[v] = true;
                }
                else
                {
                    visitedVerts.Add(v, true);
                }

                result.Add(v);
                foreach (var neighbor in this.adjacencyList[v])
                {
                    if (!visitedVerts.ContainsKey(neighbor))
                    {
                        visitedVerts.Add(neighbor, false);
                    }
                    if (visitedVerts[neighbor] == false)
                    {
                        dFS(neighbor);
                    }
                }
            }
            dFS(start);
            return result;
        }

        public List<string> depthFirstTraversalIterative(string start)
        {
            List<string> stack = new List<string>();
            Dictionary<string, bool> visited = new Dictionary<string, bool>();
            List<string> result = new List<string>();
            stack.Add(start);
            while (stack.Count > 0)
            {
                string v = stack[0];
                stack.RemoveAt(0);
                if (visited.ContainsKey(v))
                {
                    if (!visited[v])
                    {
                        visited[v] = true;
                        result.Add(v);
                    }
                }
                else
                {
                    visited.Add(v, true);
                    result.Add(v);
                }
                foreach (string vert in this.adjacencyList[v])
                {
                    if (!visited.ContainsKey(vert))
                    {
                        stack.Add(vert);
                    }
                }
            }
            return result;
        }



    }
    class Program
    {
        static void Main(string[] args)
        {
            Graph graph = new Graph();
            graph.addVertex("A");
            graph.addVertex("B");
            graph.addVertex("C");
            graph.addVertex("D");
            graph.addVertex("E");
            graph.addVertex("F");

            graph.addEdge("A", "B"); graph.addEdge("A", "C"); graph.addEdge("B", "D"); graph.addEdge("C", "E"); graph.addEdge("D", "E"); graph.addEdge("D", "F"); graph.addEdge("E", "F");
            foreach (var vert in graph.adjacencyList)
            {
                string result = vert.Key + ": ";
                if (vert.Value.Count > 0)
                {
                    foreach (var edge in vert.Value)
                    {
                        result += (edge + " ");
                    }
                    Console.WriteLine(result);
                }
                else
                {
                    Console.WriteLine(result);
                }
            }

            var iterativeString = graph.depthFirstTraversalIterative("A");
            Console.WriteLine("Iterative list:");
            foreach (var x in iterativeString) { Console.WriteLine(x); }

            var recursiveString = graph.depthFirstTraversalRecursive("A");
            Console.WriteLine("Recursive list:");
            foreach (var x in recursiveString) { Console.WriteLine(x); }
        }
    }
}
