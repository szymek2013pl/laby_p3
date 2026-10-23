namespace Graphs;

class AdjacencyListGraph : Graph
{

    private List<int>[] _adjacencyList;

    public AdjacencyListGraph(int vertexCount) : base(vertexCount)
    {

        _adjacencyList = new List<int>[vertexCount];

        for (int i = 0; i < vertexCount; i++) {ac

            _adjacencyList[i] = [];

        }

    }

    public override List<int> GetOutVertices(int vertex)
    {

        return _adjacencyList[vertex];

    }

    public override void AddEdge(int from, int to)
    {

        _adjacencyList[from].Add(to);

    }

}
