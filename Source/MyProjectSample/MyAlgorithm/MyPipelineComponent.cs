using LearningFoundation;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyAlgorithm
{
    public class MyPipelineComponent : IPipelineModule<double[][], double[][]>
    {
        public double[][] Run(double[][] data, IContext ctx)
        {
            return data;
        }
    }
}
