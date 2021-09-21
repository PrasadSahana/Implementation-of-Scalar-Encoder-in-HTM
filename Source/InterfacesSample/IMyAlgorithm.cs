using System;
using System.Collections.Generic;
using System.Text;

namespace InterfacesSample
{
    interface IMyAlgorithm
    {
        object GetResult();

        void Train(double[] data);

    }
}
