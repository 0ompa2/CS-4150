using System;
using System.Collections.Generic;
using System.Text;

namespace GetShorty
{
    class Graph
    {
        private Dictionary<String, Vertex> vertices;

        /// <summary>
        /// Constructs an empty graph.
        /// </summary>
        public Graph()
        {
            vertices = new Dictionary<String, Vertex>();
        }

        /// <summary>
        /// Adds to the graph an edge from the vertex name1 to the vertex name2.
        /// </summary>
        /// <param name="name1"></param>
        /// <param name="name2"></param>
        public void addWeightedEdge(String name1, String name2, float weight)
        {
            Vertex vertex1;
            vertices.TryGetValue(name1, out vertex1);

            Vertex vertex2;
            vertices.TryGetValue(name2, out vertex2);

            vertex1.addWeightedEdge(vertex2, weight);
        }

        public class Edge
        {
            private Vertex otherEnd;
            private float weight;

            /// <summary>
            /// Constructs an edge with one vertex at one end of it
            /// </summary>
            /// <param name="_other"></param>
            public Edge(Vertex _other, float _weight)
            {
                otherEnd = _other;
                weight = _weight;
            }

            /// <summary>
            /// Getter method for the vertex being pointed to by this edge
            /// </summary>
            /// <returns></returns>
            public Vertex getOtherVertex()
            {
                return otherEnd;
            }
            /// <summary>
            /// Getter method for the weight of the edge
            /// </summary>
            /// <returns></returns>
            public float getWeight()
            {
                return weight;
            }
        }

        public class Vertex
        {
            public String name;
            private List<Edge> adj;
            public bool visited = false;


            /// <summary>
            /// Constructs a vertex object with an empty list of edges
            /// </summary>
            /// <param name="_name"></param>
            public Vertex(String _name)
            {
                name = _name;
                adj = new List<Edge>();
            }

            /// <summary>
            /// Adds an edge to this vertex's list of edges
            /// Associates a cost weight to this edge between the vertices
            /// Associates the other end of the edge to the otherVertex
            /// </summary>
            /// <param name="otherVertex"></param>
            /// <param name="weight"></param>
            public void addWeightedEdge(Vertex otherVertex, float weight)
            {
                adj.Add(new Edge(otherVertex, weight));
            }

            /// <summary>
            /// Getter method for the list of this vertex's edges
            /// </summary>
            /// <returns></returns>
            public List<Edge> getEdges()
            {
                return adj;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="start"></param>
        /// <returns></returns>
        public static double dijkstras(Vertex start)
        {
            double bestcost = 0;
            SortedSet<KeyValuePair<Vertex, double>> q = new SortedSet<KeyValuePair<Vertex,double>>();
            q.Add(new KeyValuePair<Vertex, double>(start, 1));

            while (q.Count != 0)
            {
                KeyValuePair<Vertex, double> temp = q.Min;
                q.Remove(temp);

                foreach (Edge edge in temp.Key.getEdges())
                {
                    if (temp.Value < edge.getWeight() * temp.Value)
                    {
                        double newTemp = edge.getWeight() * temp.Value;
                        q.Remove(temp);
                        q.Add(new KeyValuePair<Vertex, double>(temp.Key, newTemp));
                        bestcost = newTemp;
                    }
                }
            }
            return bestcost;
        }

        static void Main(string[] args)
        {
            string line;

            int n; // intersections [VERTEX]
            int m; // corridors [EDGE]
            float reducingFactor = 0; // [WEIGHT]

            // Grab the first set of ints, which will be a corridor / intersection pair
            line = Console.ReadLine();
            string[] first = line.Split();
            n = int.Parse(first[0]);
            m = int.Parse(first[1]);

            while (n > 0 && m > 0)
            {
                Graph map = new Graph();
                Vertex start = null;
                bool isStart = false;

                for (int i = 0; i < m; i++)
                {
                    line = Console.ReadLine();
                    string[] next = line.Split();
                    bool isFactor = float.TryParse(next[2], out reducingFactor);

                    Vertex vertex1 = new Vertex(next[0]);
                    Vertex vertex2 = new Vertex(next[1]);

                    // If the vertex already exists in the vertices Dictionary, an exception
                    // will be thrown, but no one gives a shit, so soldier on.
                    try
                    {
                        map.vertices.Add(next[0], vertex1);
                    }
                    catch (ArgumentException) { }
                    try
                    {
                        map.vertices.Add(next[1], vertex2);
                    }
                    catch (ArgumentException) { }

                    map.addWeightedEdge(next[0], next[1], reducingFactor);
                    if (!isStart)
                    {
                        start = vertex1;
                        isStart = true;
                    }
                }

                double d = (dijkstras(start));
                if (d == 1)
                {
                    Console.Out.WriteLine("1.0000");
                }
                else
                {
                    Console.Out.WriteLine(Math.Round((Decimal)d, 4));
                }

                // Grab the next set of corridor / intersection pairs
                line = Console.ReadLine();
                string[] nextPair = line.Split();
                n = int.Parse(nextPair[0]);
                m = int.Parse(nextPair[1]);
            }
            //Console.ReadLine();
        }
    }
}

