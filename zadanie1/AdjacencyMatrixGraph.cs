namespace Graphs;

class AdjacencyMatrixGraph : Graph
{

    private bool[,] _adjacencyMatrix;
    
    public AdjacencyMatrixGraph(int vertexCount) : base(vertexCount) { 
    
        _adjacencyMatrix = new bool[vertexCount, vertexCount];
    
    }

    public override List<int> GetOutVertices(int vertex)
    {

        List<int> result = new List<int>();

        for (int i = 0; i < VertexCount; i++)
        {

            if (_adjacencyMatrix[vertex, i])
            {

                result.Add(i);

            } 

        }

        return result;

    }

    public override void AddEdge(int from, int to)
    {

        _adjacencyMatrix[from, to] = true;

    }

}
