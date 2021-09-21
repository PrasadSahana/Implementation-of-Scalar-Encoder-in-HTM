using MyCloudProject.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyExperiment
{
    public class Experiment : IExperiment
    {
        public Task<ExperimentResult> Run(ExerimentRequestMessage request)
        {
            ExperimentResult res = new ExperimentResult();

            res.StartTimeUtc = DateTime.UtcNow;

            
            return Task.FromResult< ExperimentResult>(new ExperimentResult()); // TODO...
        }
    }
}
