using LearningFoundation;
using System;
using System.Collections.Generic;

namespace MyAlgorithm
{
    /// <summary>
    /// MyAlgorithm is implementation of best algorithm in the world for prdiction of my student scores.
    /// </summary>
    public class MyAlgorithm : IAlgorithm
    {
        private double m_LearningRate;

        public Dictionary<int, float> AverageSpeed { get; set; } = new Dictionary<int, float>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="learningRate">The learning rate of algorithm. Typically 0.2 - 1.7.</param>
        /// <param name="anotherArg">Bla</param>
        public MyAlgorithm(double learningRate, double anotherArg = 2.5)
        {
            this.m_LearningRate = learningRate;
        }

   
        /// <summary>
        /// 1
        /// </summary>
        /// <param name="data"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public IResult Predict(double[][] data, IContext ctx)
        {
            var results= new MyAlgorithmResult()
            {
                Results = new float[data.Length],
            };

            int i = 0;
            foreach (var item in data)
            {
                if (this.AverageSpeed.ContainsKey((int)item[0]))
                {
                    results.Results[i] = this.AverageSpeed[(int)item[0]];
                }
                else
                    results.Results[i] = -1;

                i++;
            }

            return results;
        }

        public IScore Run(double[][] data, IContext ctx)
        {
            foreach (var row in data)
            {
                foreach (var item in row)
                {
                    if (this.AverageSpeed.ContainsKey((int)item))
                    {
                        //todo
                    }
                    else
                    {
                        //todo
                    }
                }
            }
            // Here is th elace for my work. Most of work.
            return new MyAlgorithmScore();
        }

        public IScore Train(double[][] data, IContext ctx)
        {
            return Run(data, ctx);
        }
    }
}
