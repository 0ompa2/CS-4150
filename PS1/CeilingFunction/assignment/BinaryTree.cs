using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CeilingFunction
{
    class BinaryTree
    {
        private Node root;

        public BinaryTree()
        {
            root = null;
        }

        /// <summary>
        /// Add a node to the tree.
        /// New node goes to the left of a node if it's value is less
        /// New node goes to the right of a node if it's value is more
        /// </summary>
        /// <param name="num"></param>
        public void addNode(int num)
        {
            if (root == null)
            {
                root = new Node(num);
            }
            else
            {
                Node newNode = new Node(num);
                if (root == null) 
                {
                    root = newNode;
                }
                else
                {
                    addRecursive(num, root);
                }
            }
        }

        /// <summary>
        /// Recursive method for adding a node to the tree
        /// </summary>
        /// <param name="val"></param>
        /// <param name="node"></param>
        /// <returns></returns>
        private Node addRecursive(int val, Node node)
        {
            // Base case
            if (node == null)
            {
                node = new Node(val);
            }
            //If less than top, work left to find where to add
            else if (val < node.getValue())
            {
                node.setLHS(addRecursive(val, node.getLHS()));
            }
            //If more than top, work right to find where to add
            else if (val > node.getValue())
            {
                node.setRHS(addRecursive(val, node.getRHS()));
            }
            else if (val == node.getValue())
            {
                node.setRHS(addRecursive(val, node.getRHS()));
            }
            return node;
        }

        static void Main(string[] args)
        {
            string line; // incoming line from console
            bool firstInts = false;
            int numNodes = 0;

            List<BinaryTree> treeHash = new List<BinaryTree>();

            while ((line = Console.ReadLine()) != null && line != "")
            {
                // Discard the first line, which contains 2 ints, the first which
                // represents the number of lines to follow, the second which represents
                // the number of items to be read into the tree
                if (!firstInts)
                {
                    string[] temp = line.Split();
                    numNodes = int.Parse(temp[1]);
                    firstInts = true;
                }
                else
                {
                    int[] nodes = parseLine(line, numNodes);
                    BinaryTree tree = new BinaryTree();
                    foreach (int i in nodes)
                    {
                        tree.addNode(i);
                    }
                    treeHash.Add(tree);
                }
            }

            // Now we need to check all the BinaryTrees in treeHash
            // to see if their shapes are the same. If they are, 
            // remove them from the list and keep iterrating. 
            for (int i = 0, j; i < treeHash.Count - 1; ++i)
            {
                j = i + 1;

                while (j < treeHash.Count)
                {
                    if (treeShape(treeHash[i].root, treeHash[j].root))
                    {
                        treeHash.RemoveAt(j);
                    }
                    else
                    {
                        ++j;
                    }
                }
            }
            Console.Out.WriteLine(treeHash.Count);
            Console.ReadLine();
        }

        /// <summary>
        /// Helper method that takes a string and returns an int []
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        private static int[] parseLine(string s, int length)
        {
            string[] temp = s.Split();
            int[] arr = new int[length];
            for(int i = 0; i < length; i++) {
                arr[i] = int.Parse(temp[i]);
            }

            return arr;
        }

        /// <summary>
        /// Takes the root node of two trees, and checks their shape
        /// and returns true if they are equal
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        /// <returns></returns>
        private static bool treeShape(Node lhs, Node rhs)
        {
            // If the nodes at this point are equal
            // then the tree is equal
            if (lhs == null && rhs == null) 
            {
                return true;
            }

            // If a node is empty, but it's counterpart is not
            // then the tree can not be equal
            if ((lhs == null && rhs != null) || (lhs != null && rhs == null)) 
            {
                return false;
            }
                
            // Let's walk the whole tree, focusing on the left hand side
            // and the right hand side. Both must return true for the tree to be true.
            if (   treeShape(lhs.getLHS(), rhs.getLHS()) 
                && treeShape(lhs.getRHS(), rhs.getRHS()))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Class to define a node on a Binary Tree
        /// A Node is an object with a numerical value and the
        /// possibility of a left and right sub-node beneath it.
        /// 
        /// Methods: 
        /// add - adds node to tree as a leaf
        /// getLHS - returns lhs of node
        /// getRHS - returns rhs of node
        /// </summary>
        private class Node
        {
            private int value;
            private Node rhs;
            private Node lhs;

            public Node(int nodeVal)
            {
                value = nodeVal;
                lhs = null;
                rhs = null;
            }

            /// <summary>
            /// Getter for Left Hand Side
            /// </summary>
            /// <returns></returns>
            public Node getLHS()
            {
                return lhs;
            }

            /// <summary>
            /// Setter for Left Hand Side
            /// </summary>
            /// <param name="n"></param>
            public void setLHS(Node n)
            {
                lhs = n;
            }

            /// <summary>
            /// Getter for Right Hand Side
            /// </summary>
            /// <returns></returns>
            public Node getRHS()
            {
                return rhs;
            }

            /// <summary>
            /// Setter for Right Hand Side
            /// </summary>
            /// <param name="n"></param>
            public void setRHS(Node n)
            {
                rhs = n;
            }

            /// <summary>
            /// Getter for value of node
            /// </summary>
            /// <returns></returns>
            public int getValue()
            {
                return value;
            }
        }
    }
}
