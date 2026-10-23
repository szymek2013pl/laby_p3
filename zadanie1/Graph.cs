using System.Text;

namespace Graphs;

public abstract class Graph
{

    public int VertexCount { get; }
    
    protected Graph(int vertexCount) {

        VertexCount = vertexCount;
    
    }

    public abstract List<int> GetOutVertices(int vertex);

    public abstract void AddEdge(int from, int to);

    public void Display()
    {

        StringBuilder s = new StringBuilder();

        for (int i = 0; i < VertexCount; i++)
        {

            s.Append($"{i} -> [");
            var neighbours = GetOutVertices(i);
            s.Append(string.Join(',', neighbours));
            s.Append("]\n");

        }

        Console.WriteLine(s.ToString());

    }

    public void DepthFirstSearch(int start, IVertexVisitor visitor)
    {

        bool[] visited = new bool[VertexCount];

        DepthFirstSearchRecursive(start, visited, visitor);

    }

    public void DepthFirstSearchRecursive(int vertex, bool[] visited, IVertexVisitor visitor)
    {

        visited[vertex] = true;
        visitor.PreVisit(vertex);
        List<int> neighbours = GetOutVertices(vertex);

        foreach (var n in neighbours)
        {

            if (visited[n] == false)
            {

                DepthFirstSearchRecursive(n, visited, visitor);

            }

        }

        visitor.PostVisit(vertex);

    }

}
