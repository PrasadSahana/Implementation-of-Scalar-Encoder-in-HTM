using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTestsSamples
{
    [TestClass]
    public class MyUnitTestSampleClass
    {
        [ExpectedException(typeof(Exception))]
        [TestMethod]
        public void MyTestMethod1()
        {
            throw new Exception("uuupsss! :(");
        }

    }
}
