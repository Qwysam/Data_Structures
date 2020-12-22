﻿using System;
using System.Collections.Generic;
using Priority_Queue;

namespace MOTree
{
    public class RandomTree
    {
        public int[,] randomP(int edge, int maxWeight)
        {
            Random secureRandom = new Random();
            int[,] randomArr = new int[edge - 2, edge - 2];
            for (int i = 0; i < edge - 2; i++)
            {
                for (int j = 0; j < edge - 2; j++)
                {
                    if (i < j)
                    {
                        randomArr[i, j] = secureRandom.Next(maxWeight);
                    }
                }
            }
            for (int i = 0; i < edge - 2; i++)
            {
                for (int j = 0; j < edge - 2; j++)
                {
                    randomArr[j, i] = randomArr[i, j];
                }
            }
            return randomArr;
        }
    }

    public class AlgoPrim
    {
        private static int infinite = 9999999; int[,] LinkCost;
        int NNodes;
        public AlgoPrim(int[,] mat)
        {
            int i, j;
            NNodes = mat.Length;
            LinkCost = new int[NNodes, NNodes];
            for (i = 0; i < NNodes; i++)
            {
                for (j = 0; j < NNodes-1; j++)
                {
                    LinkCost[i, j] = mat[i, j];
                    if (LinkCost[i, j] == 0)
                        LinkCost[i, j] = infinite;
                }
            }
            for (i = 0; i < NNodes; i++)
            {
                for (j = 0; j < NNodes; j++)
                    if (LinkCost[i, j] < infinite)
                        Console.WriteLine(" " + LinkCost[i, j] + " ");
                    else
                        Console.WriteLine(" * ");
                Console.WriteLine();
            }
        }

        public int unReached(bool[] r)
        {
            bool done = true;
            for (int i = 0; i < r.Length; i++)
                if (r[i] == false)
                    return i;
            return -1;
        }

        public void Prim()
        {
            int i, j, k, x, y;
            bool[] Reached = new bool[NNodes];
            int[] predNode = new int[NNodes];
            Reached[0] = true;
            for (k = 1; k < NNodes; k++)
            {
                Reached[k] = false;
            }
            predNode[0] = 0;
            printReachSet(Reached);
            for (k = 1; k < NNodes; k++)
            {
                x = y = 0;
                for (i = 0; i < NNodes; i++)
                    for (j = 0; j < NNodes; j++)
                    {
                        if (Reached[i] && !Reached[j] &&
                                LinkCost[i, j] < LinkCost[x, y])
                        {
                            x = i;
                            y = j;
                        }
                    }
                Console.WriteLine("Min cost edge: (" + x + "," + y + ")" + "cost = " + LinkCost[x, y]);
                predNode[y] = x;
                Reached[y] = true; printReachSet(Reached);
                Console.WriteLine();
            }
            int[] a = predNode;
            for (i = 0; i < NNodes; i++)
                Console.WriteLine(a[i] + " --> " + i);
        }
        void printReachSet(bool[] Reached)
        {
            Console.WriteLine("ReachSet = ");
            for (int i = 0; i < Reached.Length; i++)
                if (Reached[i])
                    Console.WriteLine(i + " ");
            //System.out.println();
        }
    }

    public class AlgoK
    {
        public class Edge
        {
            public int source;
            public int destination;
            public int weight;
            public Edge(int source, int destination, int weight)
            {
                this.source = source;
                this.destination = destination;
                this.weight = weight;
            }
        }
        public class Graph
        {
            int vertices;
            List<Edge> allEdges = new List<Edge>();
            public Graph(int vertices)
            {
                this.vertices = vertices;
            }
            public void addEgde(int source, int destination, int weight)
            {
                Edge edge = new Edge(source, destination, weight); allEdges.Add(edge); //add to total edges
            }
            public void kruskalMST()
            {
                SimplePriorityQueue<Edge> pq = new SimplePriorityQueue<Edge>();
                //add all the edges to priority queue, //sort the edges on weights
                for (int i = 0; i < allEdges.Count; i++)
                {
                    pq.Enqueue(allEdges[i], allEdges[i].weight);
                }
                //create a parent []
                int[] parent = new int[vertices];
                //makeset
                makeSet(parent);
                List<Edge> mst = new List<Edge>();
                //process vertices - 1 edges
                int index = 0;
                while (index < vertices - 1)
                {
                    Edge edge = pq.Dequeue();
                    //check if adding this edge creates a cycle
                    int x_set = find(parent, edge.source);
                    int y_set = find(parent, edge.destination);
                    if (x_set == y_set)
                    {
                        //ignore, will create cycle
                    }
                    else
                    {
                        //add it to our final result
                        mst.Add(edge);
                        index++;
                        union(parent, x_set, y_set);
                    }
                }
                //print MST
                Console.WriteLine("Minimum Spanning Tree: ");
                printGraph(mst);
            }
            public void makeSet(int[] parent)
            {
                //Make set- creating a new element with a parent pointer to itself.
                for (int i = 0; i < vertices; i++)
                {
                    parent[i] = i;
                }
            }
            public int find(int[] parent, int vertex)
            {
                //chain of parent pointers from x upwards through the tree // until an element is reached whose parent is itself if(parent[vertex]!=vertex)
                return find(parent, parent[vertex]); ;
                return vertex;
            }
            public void union(int[] parent, int x, int y)
            {
                int x_set_parent = find(parent, x);
                int y_set_parent = find(parent, y);
                //make x as parent of y parent[y_set_parent] = x_set_parent;
            }
            public void printGraph(List<Edge> edgeList)
            {
                for (int i = 0; i < edgeList.Count; i++)
                {
                    Edge edge = edgeList[i];
                    Console.WriteLine("Edge-" + i + " source: " + edge.source +
                            " destination: " + edge.destination +
                            " weight: " + edge.weight);
                }
            }
            public Graph randomK(int edge, int maxWeight)
            {
                Random random = new Random();
                AlgoK.Graph graph = new Graph(edge + 1);
                for (int i = 0; i <= edge; i++)
                {
                    graph.addEgde(random.Next(edge), random.Next(edge), random.Next(maxWeight));
                }
                return graph;
            }
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Input number of edge: ");
            int numberOfEdge = int.Parse(Console.ReadLine());
            Console.WriteLine("Input max weight: ");
            int weight = int.Parse(Console.ReadLine());
            Console.WriteLine("Input Algorithm: \n1.Kruskal\n2.Prim");
            int operation = int.Parse(Console.ReadLine());
            if (operation == 1)
            {
                Console.WriteLine("Input manual or random input: ");
                Console.WriteLine("1.Manual\n2.Random");
                int op = int.Parse(Console.ReadLine());
                if (op == 1)
                {
                    AlgoK.Graph graph = new AlgoK.Graph(6);
                    graph.addEgde(0, 1, 4);
                    graph.addEgde(0, 2, 3);
                    graph.addEgde(1, 2, 1);
                    graph.addEgde(1, 3, 2);
                    graph.addEgde(2, 3, 4);
                    graph.addEgde(3, 4, 2);
                    graph.addEgde(4, 5, 6);
                    graph.kruskalMST();
                }
                else if (op == 2)
                {
                    AlgoK.Graph graph = new AlgoK.Graph(numberOfEdge + 1); graph = graph.randomK(numberOfEdge, weight); graph.kruskalMST();
                }
            }
            else if (operation == 2)
            {
                Console.WriteLine("Input manual or random input: ");
                Console.WriteLine("1.Manual\n2.Random");
                int op = int.Parse(Console.ReadLine());
                if (op == 1)
                {
                    int[,] conn = {
                        {0,3,0,2,0,0,0,0,4}, //0
                        {3,0,0,0,0,0,0,4,0}, //1
                        {0,0,0,6,0,1,0,2,0}, //2
                        {2,0,6,0,1,0,0,0,0}, //3
                        {0,0,0,1,0,0,0,0,8}, //4
                        {0,0,1,0,0,0,8,0,0}, //5
                        {0,0,0,0,0,8,0,0,0}, //6
                        {0,4,2,0,0,0,0,0,0}, //7
                        {4, 0, 0, 0, 8, 0, 0, 0, 0}
                        };
                    AlgoPrim G = new AlgoPrim(conn);
                    G.Prim();
                }
                else if (op == 2)
                {
                    int[,] conn;
                    RandomTree rt = new RandomTree();
                    conn = rt.randomP(10, 100);
                    AlgoPrim G = new AlgoPrim(conn);
                    G.Prim();
                }
            }
        }
    }
}
