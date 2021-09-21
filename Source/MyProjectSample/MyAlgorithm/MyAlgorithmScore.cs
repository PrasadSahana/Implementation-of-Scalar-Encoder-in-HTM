using LearningFoundation;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyAlgorithm
{
    public class MyAlgorithmScore  : IScore
    {
        public float HowGoodIsMyResult { get; set; }

        public Dictionary<int, float>  AverageSpeed { get; set; }

        //public Dictionary<int, bool> AverageSpeedSuperv { get; set; }

    }
}
