using CalculatorLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace mtst
{
    [TestClass]
    public class UnitTest1
    {

        private BetterCalculator getApi()
        {
            return new BetterCalculator();
        }

        [TestMethod()]
        public void TestMethod()
        {
            Assert.AreEqual(1, 1);
        }

        [TestCategory("MyApi"), TestCategory("Initialization"), TestMethod()]
        [Priority(2)]
        public void TestMethod1()
        {
            var api = getApi();
            var res = api.Add(21, 10);
            Assert.AreEqual(res, 31);
        }

        [Priority(1)]
        [TestCategory("MyApi"), TestCategory("Addition"), TestMethod()]
        [DataTestMethod]
        [DataRow("1", "2")]
        [DataRow(" ", "a")]
        public void TestWorkWithInputData(string value1, string expectedValue)
        {
            Assert.AreEqual(value1 + expectedValue, string.Concat(value1, expectedValue));
        }


        [TestCategory("MyApi"), TestCategory("Addition"), TestMethod()]
        [DataTestMethod]
        [DataRow(1, 2, 3)]
        [DataRow(10, 20, 30)]
        [DataRow(1000, 20, 1020)]
        public void TestAdd1(int value1, int value2, int expectedValue)
        {
            var api = getApi();
            Assert.AreEqual(api.Add(value1, value2), expectedValue);
        }

        [TestCategory("MyApi"), TestCategory("Addition"), TestMethod()]
        [DataTestMethod]
        [DataRow(1, 2, 7)]
        [DataRow(10, 20, 31)]
        public void TestAdd2(int value1, int value2, int expectedValue)
        {
            var api = getApi();
            Assert.AreNotEqual(api.Add(value1, value2), expectedValue);
        }

        [TestCategory("MyApi"), TestCategory("Substraction"), TestMethod()]
        [DataTestMethod]
        [DataRow(7, 5, 2)]
        [DataRow(10, 20, -10)]
        public void TestSubstract1(int value1, int value2, int expectedValue)
        {
            var api = getApi();
            Assert.AreEqual(api.Sub(value1, value2), expectedValue);
        }


        [TestCategory("MyApi"), TestCategory("Substraction"), TestMethod()]
        [DataTestMethod]
        [DataRow(7, 5, 20)]
        [DataRow(10, 20, 10)]
        public void TestSubstract2(int value1, int value2, int expectedValue)
        {
            var api = getApi();
            Assert.AreNotEqual(api.Sub(value1, value2), expectedValue);
        }


        [TestCategory("MyApi"), TestCategory("Division"), TestMethod()]
        [DataTestMethod]
        [DataRow(8, 4, 2)]
        [DataRow(10, 5, 2)]
        public void TestSubstract(int value1, int value2, int expectedValue)
        {
            var api = getApi();
            Assert.AreEqual(api.Div(value1, value2), expectedValue);
        }


        [TestCategory("MyApi"), TestCategory("Division"), TestMethod()]
        [ExpectedException(typeof(DivideByZeroException))]
        [DataTestMethod]
        [DataRow(8, 0)]
        [DataRow(10, 0)]
        public void TestDivideByZero(int value1, int value2)
        {
            var api = getApi();
            api.Div(value1, value2);
        }

        [TestCategory("MyApi"), TestCategory("Multiplication"), TestMethod()]
        [DataTestMethod]
        [DataRow(8, 0, 0)]
        [DataRow(5, 8, 40)]
        public void TestMultiply(int value1, int value2, int expectedValue)
        {
            var api = getApi();

            var res = api.Mul(value1, value2);

            Assert.AreEqual(res, expectedValue);
        }
    }
}
