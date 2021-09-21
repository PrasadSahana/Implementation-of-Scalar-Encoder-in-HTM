using System;
using System.Collections.Generic;
using System.Text;

namespace InterfacesSample
{
    public class SumAlgorithm : IMyAlgorithm
    {
        double m_Result = 0;

        public object GetResult()
        {
            return this.m_Result;
        }

        public void Train(double[] data)
        {
            double sum = 0;

            foreach (var item in data)
            {
                sum += item;
            }

            this.m_Result = sum;
        }
    }
}
