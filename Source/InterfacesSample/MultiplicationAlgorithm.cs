using System;
using System.Collections.Generic;
using System.Text;

namespace InterfacesSample
{
    public class MultiplicationAlgorithm : IMyAlgorithm
    {
        double m_Result = 0;

        public object GetResult()
        {
            return this.m_Result;
        }

        public void Train(double[] data)
        {
            double sum = 1;

            foreach (var item in data)
            {
                sum *= item; // sum = sum * item
            }

            this.m_Result = sum;
        }
    }
}
