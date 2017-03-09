using System;
using System.Collections.Generic;
using System.Text;

namespace autosink
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
        /// The edge is associated with the "weight".
        /// </summary>
        /// <param name="name1"></param>
        /// <param name="name2"></param>
        /// <param name="weight"></param>
        public void addEdgeWeighted(String name1, String name2, int weight)
        {
            Vertex vertex1;
            vertices.TryGetValue(name1, out vertex1);

            Vertex vertex2;
            vertices.TryGetValue(name2, out vertex2);

            vertex1.addEdgeWeighted(vertex2, weight);

        }

        public class Edge
        {
	        private Vertex otherEnd; 
	        private int weight;

	        /// <summary>
            /// Constructs an edge with one vertex at one end of it and associates a weight with it
	        /// </summary>
	        /// <param name="_other"></param>
	        /// <param name="_weight"></param>
	        public Edge(Vertex _other, int _weight)
	        {
		        this.weight = _weight;
		        this.otherEnd = _other;
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
            /// getter method for the cost associated with encountering this edge
	        /// </summary>
	        /// <returns></returns>
	        public int getWeight()
	        {
		        return weight;
	        }
        }

        public class Vertex
        {
            private String name;
            private Vertex cameFrom;
            private bool isVisited;
            private int costFromStart;
            private int tollWeight;
            private LinkedList<Edge> adj;

            /// <summary>
            /// Constructs a vertex object with an empty list of edges
            /// </summary>
            /// <param name="_name"></param>
            public Vertex(String _name)
            {
                name = _name;
                cameFrom = null;
                isVisited = false;
                costFromStart = 0;
                tollWeight = 0;
                adj = new LinkedList<Edge>();
            }

            /// <summary>
            /// Adds an edge to this vertex's list of edges
            /// Associates the other end of the edge to the otherVertex
            /// Associates a cost weight to this edge between these two vertices
            /// Since this edge enters otherVertex, it also increases its inDegree
            /// </summary>
            /// <param name="otherVertex"></param>
            /// <param name="weight"></param>
            public void addEdgeWeighted(Vertex otherVertex, int weight)
            {
                adj.AddLast(new Edge(otherVertex, weight));
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

            /// <summary>
            /// Setter method for the vertex's having-been-visited status
            /// </summary>
            /// <param name="status"></param>
            public void setVisited(bool status)
            {
                isVisited = status;
            }

            /// <summary>
            /// Getter method for the vertex's having-been-visited status
            /// </summary>
            /// <returns></returns>
            public bool getVisited()
            {
                return isVisited;
            }

            /// <summary>
            /// Setter method for this vertex's previous vertex in a path
            /// </summary>
            /// <param name="previous"></param>
            public void setCameFrom(Vertex previous)
            {
                cameFrom = previous;
            }

            /// <summary>
            /// Getter method for this vertex's previous vertex in a path
            /// </summary>
            /// <returns></returns>
            public Vertex getCameFrom()
            {
                return cameFrom;
            }

            /// <summary>
            /// Setter method for this vertex's total cost from a starting point in a path
            /// </summary>
            /// <param name="dist"></param>
            public void setCostFromStart(int dist)
            {
                costFromStart = dist;
            }

            /// <summary>
            /// Getter method for this vertex's total cost from a starting point in a path
            /// </summary>
            /// <returns></returns>
            public int getCostFromStart()
            {
                return costFromStart;
            }

            /// <summary>
            /// Setter for tollWeight
            /// </summary>
            /// <param name="toll"></param>
            public void setTollWeight(int toll)
            {
                tollWeight = toll;
            }

            /// <summary>
            /// Getter for tollWeight
            /// </summary>
            /// <returns></returns>
            public int getTollWeight()
            {
                return tollWeight;
            }

            ///// <summary>
            ///// Get all the children of this Vertex
            ///// </summary>
            ///// <returns></returns>
            //public HashSet<Vertex> getChildren()
            //{
            //    HashSet<Vertex> children = new HashSet<Vertex>();
            //    populateChildren(this, children);
            //    return children;
            //}

            //private void populateChildren(Vertex parent, HashSet<Vertex> children)
            //{
            //    if (parent.adj != null)
            //    {
            //        foreach (Edge e in parent.adj)
            //        {
            //            children.Add(e.getOtherVertex());
            //            populateChildren(e.getOtherVertex(), children);
            //        }
            //    }
            //}

            /// <summary>
            /// Compares this vertex with another for quality based on their name fields
            /// </summary>
            /// <param name="obj"></param>
            /// <returns></returns>
            public bool equals(Object obj)
            {
                if (!(obj is Vertex))
                    return false;

                Vertex other = (Vertex)obj;

                if (this.name.CompareTo(other.getName()) == 0)
                    return true;

                return false;
            }
        }

        /// <summary>
        /// Performs Dijkstra's routine on a weighted graph to determine the cheapest path from start vertex to a goal vertex.
        /// </summary>
        /// <param name="graph">
        /// The graph object to be traversed
        /// </param>
        /// <param name="startName">
        /// Name of the starting vertex in the path
        /// </param>
        /// <param name="goalName">
        /// Name of the ending vertex in the path
        /// </param>
        /// <returns>
        /// String List of the vertices that make up the cheapest path from the starting vertex (inclusive) to the
        /// ending vertex (inclusive) based on weight associated with the edges between the graphs vertices
        /// </returns>
        public static List<String> shortestPath(Graph graph, String startName, String goalName) 
        {
		    Queue<Vertex> queue = new Queue<Vertex>();
		    List<String> path = new List<String>();

		    Vertex start; 
            graph.getVertices().TryGetValue(startName, out start);
            Vertex goal;
            graph.getVertices().TryGetValue(goalName, out goal);

		    // if start and goal are the same, return one 
		    if(start.equals(goal)) 
            {
			    path.Add(startName);
			    return path;
		    }

		    // Initialize all nodes' cost to infinity
		    foreach (Vertex v in graph.getVertices().Values)
            {
			    v.setCostFromStart(int.MaxValue);
                v.setVisited(false);
                v.setCameFrom(null);
		    }

		    // Enqueue the start
		    queue.Enqueue(start);
		    start.setCostFromStart(0);

		    while(queue.Count != 0) {
			    Vertex curr = queue.Dequeue();

			    if(curr.equals(goal)) {
				    break;
			    }
			    curr.setVisited(true);
			
			    //get all of current's neighbors
			    foreach (Edge e in curr.getEdges()) 
                {
				    Vertex neighbor = e.getOtherVertex();
				    if(neighbor.getVisited() == false) 
                    {
					    LinkedList<Edge> edges = curr.getEdges();

					    foreach(Edge E in edges)
                        {
                            if (E.getOtherVertex().equals((neighbor)))
                            {
                                // if neighbor's cost from start is higher than current cost + edge weight, 
                                // enqueue and neighbor becomes current
                                if (neighbor.getCostFromStart() > (curr.getCostFromStart() + E.getWeight()))
                                {
                                    queue.Enqueue(neighbor);
                                    neighbor.setCameFrom(curr);
                                    neighbor.setCostFromStart(curr.getCostFromStart() + E.getWeight());
                                }
                            } 
                        }	    
				    }
			    }
		    }

		    if(goal.getCameFrom() == null){
			    return path;
		    }

		    //get the data from the cameFrom and add it to the list to be returned
		    for(Vertex v = goal; v != null; v = v.getCameFrom())
            {
                path.Add(v.getName());
            }
			    
		    //If no path was found, return an empty list
            if (!path.Contains(goalName) && !path.Contains(startName))
            {
                path.Clear();
                return path;
            }
            else
            {
                return path;
            }
	    }

        static void Main(string[] args)
        {
            string line;
            
            int numOfCities = 0;
            int numOfHighways = 0;
            int numOfTrips = 0;
            int lineCount = 0;

            bool citiesDone = false;
            bool highwaysDone = false;
            bool tripsDone = false;

            Graph map = new Graph();
            List<string> trips = new List<string>(8000);

            while ((line = Console.ReadLine()) != null && line != "" && !tripsDone)
            {
                // Parse first single int, and following lines,
                // which represent number of cities on the map (1 <= n <= 2000) [VERTEX]
                if (!citiesDone)
                {
                    if (numOfCities == 0)
                    {
                        numOfCities = int.Parse(line);
                    }
                    else
                    {
                        // get the city and the toll at that city, 
                        // which will become the edge weight from the previous city
                        //
                        // temp[0] = city name
                        // temp[1] = toll (weight to go here)
                        string[] temp = line.Split();

                        string cityName = temp[0];
                        int tollWeight = int.Parse(temp[1]);

                        Vertex city = new Vertex(cityName);
                        city.setTollWeight(tollWeight);

                        map.vertices.Add(cityName, city);
                    }
                    
                    lineCount++;
                    if(lineCount == (numOfCities + 1)) 
                    {
                        citiesDone = true;
                    }
                }
                // Parse second single int, and following lines,
                // which represent number of highways on the map (0 <= h <= 10000) [EDGE]
                else if(citiesDone && !highwaysDone) 
                {
                    if (numOfHighways == 0)
                    {
                        numOfHighways = int.Parse(line);
                    }
                    else
                    {                     
                        string[] temp = line.Split();

                        string city1 = temp[0];
                        string city2 = temp[1];

                        // Get the vertex object associated with city2 
                        // as it's tollWeight becomes the weight of the edge
                        Vertex secondCity;
                        map.vertices.TryGetValue(city2, out secondCity);

                        // Get the weight of city2
                        int weight = secondCity.getTollWeight();

                        // add that weighted edge to the map
                        map.addEdgeWeighted(city1, city2, weight);

                    }
                    lineCount++;
                    if (lineCount == numOfCities + numOfHighways + 2)
                    {
                        highwaysDone = true;
                    }
                    
                }
                // Parse third single int, and following lines, 
                // which represent number of trips between cities (1 <= t <= 8000) [PATH]
                else if(citiesDone && highwaysDone)
                {
                    if (numOfTrips == 0)
                    {
                        numOfTrips = int.Parse(line);
                    }
                    else
                    {
                        string[] temp = line.Split();
                        string city1 = temp[0];
                        string city2 = temp[1];

                        if (city1.Equals(city2))
                        {
                            trips.Add("0");
                        }
                        else
                        {
                            int totalWeight = 0;
                            List<string> path = shortestPath(map, city1, city2);
                            if (path.Count == 0)
                            {
                                trips.Add("NO");
                            }
                            else
                            {
                                for (int j = 0; j < path.Count; j++)
                                {
                                    if (path[j].Equals(city1))
                                    {
                                        continue;
                                    }
                                    else
                                    {
                                        Vertex tempV;
                                        map.getVertices().TryGetValue(path[j], out tempV);
                                        totalWeight += tempV.getTollWeight();
                                    }
                                }
                                trips.Add(totalWeight.ToString());
                            }
                        }
                    }
                    lineCount++;
                    if (lineCount == numOfCities + numOfHighways + numOfTrips + 3)
                    {
                        tripsDone = true;
                    }
                }
            }

            foreach (String s in trips)
            {
                Console.Out.WriteLine(s);
            }
            Console.ReadLine();
        }
    }
}
