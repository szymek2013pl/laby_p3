namespace Matrices.Matrix;

using System.Text;

public abstract class Matrix
{

    public int Rows { get; }
    public int Columns { get; }

    protected Matrix(int rows, int columns)
    {

        if (rows < 1 || columns < 1)
        {

            throw new ArgumentException("Rows and columna cannot be lower than 1");

        }

        Rows = rows;
        Columns = columns;

    }

    public abstract float this[int row, int column] { get; set; }

    public override string ToString()
    {

        StringBuilder str = new StringBuilder();

        for (int i = 0; i < Rows; i++)
        {

            for (int j = 0; j < Columns; j++)
            {

                str.Append(this[i, j]);
                str.Append(' ');

            }

            str.Append('\n');

        }

        return str.ToString();

    }

    public abstract void Identity();

    public abstract Matrix Transpose();

    public abstract float Norm();

    protected abstract Matrix GetInstance(int rows, int columns);

    public static Matrix operator +(Matrix m1, Matrix m2)
    {

        Matrix result = m1.GetInstance(m1.Rows, m1.Columns);

        for (int i = 0; i < m1.Rows; i++)
        {

            for (int j = 0; j < m1.Columns; j++)
            {

                result[i,j] = m1[i,j] + m2[i,j];

            }

        }

        return result;

    }

    public static Matrix operator -(Matrix m1, Matrix m2)
    {

        Matrix result = m1.GetInstance(m1.Rows, m1.Columns);

        for (int i = 0; i < m1.Rows; i++)
        {

            for (int j = 0; j < m1.Columns; j++)
            {

                result[i, j] = m1[i, j] - m2[i, j];

            }

        }

        return result;

    }

    
    public static Matrix operator *(Matrix m1, Matrix m2)
    {

        Matrix result = m1.GetInstance(m1.Rows, m2.Columns);

        for (int i = 0; i < m1.Rows; i++)
        {

            for (int j = 0; j < m2.Columns; j++)
            {
                
                result[i, j] = m1[i, j] * m2[i, j];

            }

        }

        return result;

    }
    

    public static Matrix operator *(Matrix m, float scalar)
    {

        Matrix result = m.GetInstance(m.Rows, m.Columns);

        for (int i = 0; i < m.Rows; i++)
        {

            for (int j = 0; j < m.Columns; j++)
            {

                result[i, j] = m[i, j] * scalar;

            }

        }

        return result;

    }

    public static Matrix operator *(float scalar, Matrix m)
    {

        Matrix result = m.GetInstance(m.Rows, m.Columns);

        for (int i = 0; i < m.Rows; i++)
        {

            for (int j = 0; j < m.Columns; j++)
            {

                result[i, j] = m[i, j] * scalar;

            }

        }

        return result;

    }

}