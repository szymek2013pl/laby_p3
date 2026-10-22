namespace Matrices.Matrix;

public class DenseMatrix: Matrix
{

    private float[,] _matrix;

    public DenseMatrix(int rows, int columns) : base(rows, columns) {

        _matrix = new float[rows, columns];

    }

    public override float this[int row, int column]
    {

        get
        {

            if (row < 0 || column < 0 || row >= Rows || column >= Columns)
            {

                throw new ArgumentException("Index out of range");

            }

            return _matrix[row, column];

        }

        set
        {

            if (row < 0 || column < 0 || row >= Rows || column >= Columns)
            {

                throw new ArgumentException("Index out of range");

            }

            _matrix[row, column] = value;

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

                        _matrix[i, j] = 1;

                    }

                    else
                    {

                        _matrix[i, j] = 0;

                    }

                }

            }

        }

    }

    public override Matrix Transpose()
    {

        DenseMatrix result = new DenseMatrix(Columns, Rows);

        for (int i = 0; i < result.Rows; i++)
        {

            for (int j = 0; j < result.Columns; j++)
            {

                result[i,j] = _matrix[j,i];

            }

        }

        return result;

    }

    public override float Norm()
    {

        float norm = 0f;

        for (int i = 0; i < Rows; i++)
        {

            for (int j = 0; j < Columns; j++)
            {

                norm += _matrix[i,j] * _matrix[i,j];

            }

        }

        return MathF.Sqrt(norm);

    }

    protected override Matrix GetInstance(int rows, int columns)
    {

        DenseMatrix result = new DenseMatrix(rows, columns);
        return result;

    }

}