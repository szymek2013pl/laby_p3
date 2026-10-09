namespace PrimeChecker.Lib;

public static class NumberUtils
{

    public static bool isPrime(int number)
    {

        if (number == 0 || number == 1 || number < 0)
        {

            return false;

        }

        for (int i = 2; i * i <= number; i++)
        {

            if (number % i == 0)
            {

                return false;

            }


        }

        return true;

    }

}
