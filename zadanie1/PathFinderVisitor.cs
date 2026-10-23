using System.Text;

namespace Graphs;

class PathFinderVisitor : IVertexVisitor
{

    private int _target;

    public List<int> Path { get; }
    public bool Found { get; private set; }
    
    public PathFinderVisitor(int target)
    {

        _target = target;
        Path = new List<int>();
        Found = false;

    }

    public void PreVisit(int vertex)
    {

        if (Found) return;

        Path.Add(vertex);

        if (vertex == _target)
        {

            Found = true;

        }

    }

    public void PostVisit(int vertex) {

        if (Found == false)
        {

            Path.RemoveAt(Path.Count - 1);

        }

    }

    public override string ToString()
    {

        if (Found == true)
        {

            StringBuilder s = new StringBuilder();

            for (int i = 0; i < Path.Count - 1; i++)
            {

                s.Append($"{Path[i]} --> ");

            }

            s.Append(Path[Path.Count - 1]);

            return s.ToString();

        }

        else
        {

            return "not found";

        }

    }

}
