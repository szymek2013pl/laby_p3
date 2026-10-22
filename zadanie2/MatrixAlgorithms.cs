namespace Matrices.Matrix;

public class MatrixAlgorithms
{

    public static Matrix PseudoInverse(Matrix A, int iterations = 1000)
    {

        float alfa = 1 / (A.Norm() * A.Norm());
        Matrix A1 = alfa * A.Transpose();

        for (int i = 0; i < iterations; i++)
        {

            Matrix I = A * 0f;
            I.Identity();

            Matrix A2 = A1 + A1 * (I - A * A1);

            if ((A2 - A1).Norm() < Math.Pow(10, -7))
            {

                break;

            }

            A1 = A2;

        }

        return A1;
    }

}