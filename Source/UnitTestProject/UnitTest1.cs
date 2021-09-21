using CalculatorLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        [DataRow(1,2,3)]
        [DataRow(1, 2, 3)]
        [DataRow(123, 2, 125)]
        [DataRow(3, 5, 8)]
        [TestCategory("CoreTests")]
        public void AddTest(int a, int b, int expectedRes)
        {
            BetterCalculator bCalc = new BetterCalculator();

            int res = bCalc.Add(a, b);

            Assert.IsTrue(res == expectedRes); 

        }

        public static void multiplicationTest()
        { 
        }

        [TestMethod]
        public void MultiplicationTest()
        {
            BetterCalculator bCalc = new BetterCalculator();

            int res = bCalc.Mul(2, 2);

            Assert.IsTrue(res == 4);

        }
    }
}
