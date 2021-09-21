using LearningFoundation;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyAlgorithm
{
    public class MyAlgorithmResult : IResult
    {
        /// <summary>
        /// List of results. -1 means no result for slot.
        /// </summary>
        public float[] Results { get; internal set; }
    }
}
