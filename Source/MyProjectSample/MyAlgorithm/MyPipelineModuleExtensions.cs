using LearningFoundation;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyAlgorithm
{
    public static class MyPipelineModuleExtensions
    {
        public static LearningApi UseMyPipelineModule(this LearningApi api)
        {

            MyPipelineComponent alg = new MyPipelineComponent();
            api.AddModule(alg, $"MyPipelineComponent-{Guid.NewGuid()}");
            return api;
        }
       
    }
}