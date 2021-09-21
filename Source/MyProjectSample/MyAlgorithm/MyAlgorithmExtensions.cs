using LearningFoundation;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyAlgorithm
{
    public static class MyAlgorithmExtensions
    {
        public static LearningApi UseMyAlgorithm(this LearningApi api, double learningRate) {

            MyAlgorithm alg = new MyAlgorithm(learningRate, 2.3);
            api.AddModule(alg, "MyAlgorithm");
            return api;
        }
    }
}
