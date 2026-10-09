using PrimeChecker.Lib;

namespace PrimeChecker.Tests;

[TestClass]
public sealed class NumberUtilsTests
{
    [TestMethod]
    public void TestMethod1()
    {


        int num1 = -10;
        int num2 = 0;
        int num3 = 2;
        int num4 = 9;
        int num5 = 13;

        bool s1 = NumberUtils.isPrime(num1);
        bool s2 = NumberUtils.isPrime(num2);
        bool s3 = NumberUtils.isPrime(num3);
        bool s4 = NumberUtils.isPrime(num4);
        bool s5 = NumberUtils.isPrime(num5);

        Assert.IsFalse(s1);
        Assert.IsFalse(s2);
        Assert.IsTrue(s3);
        Assert.IsFalse(s4);
        Assert.IsTrue(s5);



    }
}
