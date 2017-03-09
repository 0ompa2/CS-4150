using System;
using System.Collections.Generic;
using System.Text;

namespace RumorMill
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
        /// Get the graphs list of vertices
        /// </summary>
        /// <returns></returns>
        public Dictionary<String, Vertex> getVertices()
        {
            return this.vertices;
        }

        /// <summary>
        /// Adds to the graph an edge from the vertex name1 to the vertex name2.
        /// </summary>
        /// <param name="name1"></param>
        /// <param name="name2"></param>
        public void addEdge(String name1, String name2)
        {
            Vertex vertex1;
            vertices.TryGetValue(name1, out vertex1);

            Vertex vertex2;
            vertices.TryGetValue(name2, out vertex2);
           
            vertex1.addEdge(vertex2);
        }

        public class Edge
        {
            private Vertex otherEnd;

            /// <summary>
            /// Constructs an edge with one vertex at one end of it
            /// </summary>
            /// <param name="_other"></param>
            public Edge(Vertex _other)
            {
                otherEnd = _other;
            }

            /// <summary>
            /// Getter method for the vertex being pointed to by this edge
            /// </summary>
            /// <returns></returns>
            public Vertex getOtherVertex()
            {
                return otherEnd;
            }
        }

        public class Vertex
        {
            private String name;
            private LinkedList<Edge> adj;

            /// <summary>
            /// Constructs a vertex object with an empty list of edges
            /// </summary>
            /// <param name="_name"></param>
            public Vertex(String _name)
            {
                name = _name;
                adj = new LinkedList<Edge>();
            }

            /// <summary>
            /// Adds an edge to this vertex's list of edges
            /// Associates the other end of the edge to the otherVertex
            /// </summary>
            /// <param name="otherVertex"></param>
            public void addEdge(Vertex otherVertex)
            {
                adj.AddLast(new Edge(otherVertex));
            }

            /// <summary>
            /// Getter method for the list of this vertex's edges
            /// </summary>
            /// <returns></returns>
            public LinkedList<Edge> getEdges()
            {
                return adj;
            }

            /// <summary>
            /// Getter method for this vertex's identifying name
            /// </summary>
            /// <returns></returns>
            public String getName()
            {
                return name;
            }
        }

        /// <summary>
        /// Performs Breadth First Search on a graph, unique to this problem set
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="startName"></param>
        /// <returns>
        /// Dictionary<string, int> with where KEY is vertex name, and VALUE is 
        /// it's distance from the root, according to level. 
        /// </returns>
        public static HashSet<string> benFranklinSchool(Graph graph, Vertex start)
        {
            // Return a HashSet for faster iterations later in the printing
            HashSet<string> sortedRows = new HashSet<string>();
            sortedRows.Add(start.getName());

            // key is distance from root, Value is all vertices at that distance
            Dictionary<int, SortedSet<String>> toSort = new Dictionary<int, SortedSet<string>>();

            // Key is vertex name, value is it's distance from the root
            Dictionary<string, int> distance = new Dictionary<string, int>();

            // Initialize dictionary with all distances to infinity
            foreach (Vertex v in graph.getVertices().Values)
            {
                distance.Add(v.getName(), int.MaxValue);
            }

            distance[start.getName()] = 0;

            Queue<Vertex> q = new Queue<Vertex>();
            q.Enqueue(start);

            while (q.Count != 0)
            {
                Vertex temp = q.Dequeue();
                foreach (Edge e in temp.getEdges())
                {
                    // If a vertex's value is infinity, it hasn't been visited, so 
                    // let's enqueue it as the something to check later, and then 
                    // set it's distance.
                    if (distance[e.getOtherVertex().getName()] == int.MaxValue)
                    {
                        q.Enqueue(e.getOtherVertex());
                        distance[e.getOtherVertex().getName()] = distance[temp.getName()] + 1;
                        int value = distance[e.getOtherVertex().getName()];

                        // To make sure we grab everything that has the same distance
                        // put it in a KeyValue Pair with the distance as the key
                        // and a SortedList of vertices.
                        if (!toSort.ContainsKey(value))
                        {
                            SortedSet<string> tempList = new SortedSet<string>();
                            tempList.Add(e.getOtherVertex().getName());
                            toSort.Add(value, tempList);
                        }
                        else
                        {
                            toSort[value].Add(e.getOtherVertex().getName());
                        }
                    }
                }
            }
            foreach (KeyValuePair<int, SortedSet<string>> kvp in toSort) 
            {
                foreach (string s in kvp.Value)
                {
                    sortedRows.Add(s);
                }
            }
            return sortedRows;
        }

        static void Main(string[] args)
        {
            string line;

            int numOfStudents = 0;
            int numOfPairs = 0;
            int numOfReports = 0;
            int lineCount = 0;

            bool studentsDone = false;
            bool pairsDone = false;
            bool reportsDone = false;

            Graph map = new Graph();
            List<string> students = new List<string>(2000);
            List<string> reports = new List<string>(8000);
            HashSet<string> finalReport = new HashSet<string>();

            while ((line = Console.ReadLine()) != null && line != "" && !reportsDone)
            {
                // Parse first single int, and following lines,
                // which represent number of students at the school (1 <= n <= 2000) [VERTEX]
                if (!studentsDone)
                {
                    if (numOfStudents == 0)
                    {
                        numOfStudents = int.Parse(line);
                    }
                    else
                    {
                        string temp = line;
                        students.Add(temp);

                        map.vertices.Add(temp, new Vertex(temp));
                    }

                    lineCount++;
                    if (lineCount == (numOfStudents + 1))
                    {
                        studentsDone = true;
                        students.Sort();
                    }
                }
                // Parse second single int, and following lines,
                // which represent pairs of students that are friends (0 <= h <= 10000) [EDGE]
                else if (studentsDone && !pairsDone)
                {
                    if (numOfPairs == 0)
                    {
                        numOfPairs = int.Parse(line);
                    }
                    else
                    {
                        string[] temp = line.Split();

                        string friend1 = temp[0];
                        string friend2 = temp[1];

                        // add that edge to the map
                        map.addEdge(friend1, friend2);
                        map.addEdge(friend2, friend1);

                    }
                    lineCount++;
                    if (lineCount == numOfStudents + numOfPairs + 2)
                    {
                        pairsDone = true;
                    }

                }
                // Parse third single int, and following lines, 
                // which represent number of reports principal needs to generate (1 <= t <= 2000) [PATH]
                else if (studentsDone && pairsDone)
                {
                    if (numOfReports == 0)
                    {
                        numOfReports = int.Parse(line);
                    }
                    else
                    {
                        string startStudent = line;
                        reports.Add(startStudent);
                    }
                    lineCount++;
                    if (lineCount == numOfStudents + numOfPairs + numOfReports + 3)
                    {
                        reportsDone = true;
                    }
                }
            }

            Vertex firstStudent;
            for(int i = 0; i < reports.Count; i++) 
            {
                map.getVertices().TryGetValue(reports[i], out firstStudent);
                finalReport = benFranklinSchool(map, firstStudent);
                StringBuilder builder = new StringBuilder(2000);

                // Student chosen to spread rumor has no friends to spread to
                // So we will just append the sorted student list to his name
                if (finalReport.Count == 0)
                {
                    builder.Append(reports[i] + " ");
                    foreach (string s in students)
                    {
                        builder.Append(s + " ");
                    }
                }
                else
                {            
                    foreach (string s in finalReport)
                    {
                        builder.Append(s + " ");
                    }
                    // once we've printed everyone that has friends
                    // go back over the student list and make sure there aren't
                    // any students (vertices) without friends (edges) and print them
                    foreach (string s in students)
                    {
                        if (!finalReport.Contains(s))
                        {
                            builder.Append(s + " ");
                        }
                    }
                }
                Console.Out.WriteLine(builder.ToString());
            }
            Console.ReadLine();
        }
    }
}
