using PrimeChecker.Lib;

for (int i = -5; i < 20; i++)
{

    bool score = NumberUtils.isPrime(i);
    
    if (score == false)
    {

        Console.WriteLine($"{i} nie jest liczba pierwsa");

    }

    else
    {

        Console.WriteLine($"{i} jest liczba pierwsza");

    }

}