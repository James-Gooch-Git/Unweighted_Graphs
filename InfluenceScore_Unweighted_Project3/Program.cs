using System;
using System.Collections.Generic;

class Graph
{
    private int Nodes;
    private List<int>[] AdjacencyList;

    public Graph(int nodes)
    {
        Nodes = nodes;
        AdjacencyList = new List<int>[nodes];
        for (int i = 0; i < nodes; i++)
        {
            AdjacencyList[i] = new List<int>();
        }
    }

    public void AddEdge(int u, int v)
    {
        AdjacencyList[u].Add(v);
        AdjacencyList[v].Add(u); // Assuming an undirected graph
    }

    public double CalculateInfluenceScore(int source)
    {
        bool[] visited = new bool[Nodes];
        int[] distance = new int[Nodes];
        Queue<int> queue = new Queue<int>();

        // Initialize distances
        for (int i = 0; i < Nodes; i++)
        {
            distance[i] = int.MaxValue;
        }
        distance[source] = 0;

        // BFS
        queue.Enqueue(source);
        visited[source] = true;

        while (queue.Count > 0)
        {
            int current = queue.Dequeue();
            foreach (int neighbor in AdjacencyList[current])
            {
                if (!visited[neighbor])
                {
                    visited[neighbor] = true;
                    distance[neighbor] = distance[current] + 1;
                    queue.Enqueue(neighbor);
                }
            }
        }

        // Calculate total distance
        int totalDistance = 0;
        for (int i = 0; i < Nodes; i++)
        {
            if (i != source && distance[i] != int.MaxValue)
            {
                totalDistance += distance[i];
            }
        }

        // Calculate influence score
        return totalDistance > 0 ? (double)(Nodes - 1) / totalDistance : 0.0;
    }
}

// Example Usage
class Program
{
    static void Main()
    {
        //Graph graph = new Graph(5);
        //graph.AddEdge(0, 1);
        //graph.AddEdge(0, 2);
        //graph.AddEdge(1, 3);
        //graph.AddEdge(3, 4);

        //// Measure for Node 0
        //double score = graph.CalculateInfluenceScore(0);
        //Console.WriteLine("Influence Score for Node 0: " + score);

        //// Measure for all nodes
        //Console.WriteLine("Influence Scores for all nodes:");
        //for (int node = 0; node < 5; node++)
        //{
        //    double overallScore = graph.CalculateInfluenceScore(node);
        //    Console.WriteLine($"Node {node}: Influence Score = {overallScore}");
        //}

        //// Measure for a specific node (e.g., Node 3)
        //double xScore = graph.CalculateInfluenceScore(3); // 3 is placeholder, Put any node number here
        //Console.WriteLine("Influence Score for Node 3: " + xScore);

        // Create unweighted graph
        Graph unweightedGraph = new Graph(8); // 8 nodes for Alicia, Britney, Claire, etc.

        string[] nodeNames = { "Alicia", "Brittany", "Claire", "Diana", "Edward", "Harry", "Gloria", "Fred"};

        int[] nodeNumbers = { 0, 1, 2, 3, 4, 5, 6, 7 };
        // Add edges (using index-based mapping: Alicia=0, Britney=1, ...)
        unweightedGraph.AddEdge(0, 1); // Alicia -- Britney
        unweightedGraph.AddEdge(1, 2); // Britney -- Claire
        unweightedGraph.AddEdge(2, 3); // Claire -- Diana
        unweightedGraph.AddEdge(3, 4); // Diana -- Edward
        unweightedGraph.AddEdge(3, 5); // Diana -- Harry
        unweightedGraph.AddEdge(4, 5); // Edward -- Harry
        unweightedGraph.AddEdge(4, 6); // Edward -- Gloria
        unweightedGraph.AddEdge(4, 7); // Edward -- Fred
        unweightedGraph.AddEdge(6, 7); // Gloria -- Fred
        unweightedGraph.AddEdge(5, 6); // Harry -- Gloria

        // Measure for Node 0 - Alicia
        double score = unweightedGraph.CalculateInfluenceScore(0);
        Console.WriteLine($"Influence Score for Node 0 (Alicia): {score}");

        // Measure for all nodes
        Console.WriteLine("Influence Scores for all nodes:");
        for (int i = 0; i < nodeNumbers.Length; i++)
        {
            double overallScore = unweightedGraph.CalculateInfluenceScore(nodeNumbers[i]);
            Console.WriteLine($"Node {nodeNumbers[i]+ 1}, {nodeNames[i]}: Influence Score = {overallScore}");
        }

        // Measure for a specific node (e.g., Node 3)
        double xScore = unweightedGraph.CalculateInfluenceScore(3); // 3 is placeholder, Put any node number here
        Console.WriteLine("Influence Score for Node 3: " + xScore);
    }
}
