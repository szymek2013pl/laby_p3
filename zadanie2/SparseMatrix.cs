namespace Matrices.Matrix;

public class SparseMatrix : Matrix
{

    private Dictionary<(int, int), float> _matrix;

    public SparseMatrix(int rows, int columns) : base(rows, columns)
    {

        _matrix = new Dictionary<(int, int), float>();

    }

    public override float this[int row, int column]
    {

        get
        {

            if (row < 0 || column < 0 || row >= Rows || column >= Columns)
            {

                throw new ArgumentException("Index out of range");

            }

            if (_matrix.ContainsKey((row, column)))
            {

                return _matrix[(row, column)];

            }

            return 0f;

        }

        set
        {

            if (row < 0 || column < 0 || row >= Rows || column >= Columns)
            {

                throw new ArgumentException("Index out of range");

            }

            if (value == 0f) { 
            
                _matrix.Remove((row, column));

            }

            else
            {

                _matrix[(row, column)] = value;

            }

        }

    }

    public override void Identity()
    {

        if (Rows != Columns)
        {

            throw new InvalidOperationException("Matrix is not squared");

        }

        else
        {

            for (int i = 0; i < Rows; i++)
            {

                for (int j = 0; j < Columns; j++)
                {

                    if (i == j)
                    {

                        _matrix[(i, j)] = 1f;

                    }

                    else
                    {

                        _matrix[(i, j)] = 0f;

                    }


                }

            }

        }

    }

    public override Matrix Transpose()
    {

        SparseMatrix result = new SparseMatrix(Columns, Rows);

        foreach (var element in _matrix)
        {

            int oldRow = element.Key.Item1;
            int oldColumn = element.Key.Item2;

            float value = element.Value;

            result[oldColumn,oldRow] = value;

        }

        return result;

    }

    public override float Norm()
    {

        float norm = 0f;

        foreach (var element in _matrix)
        {

            norm += element.Value;

        }

        return MathF.Sqrt(norm);

    }

    protected override Matrix GetInstance(int rows, int columns)
    {

        SparseMatrix result = new SparseMatrix(rows, columns);
        return result;

    }

}