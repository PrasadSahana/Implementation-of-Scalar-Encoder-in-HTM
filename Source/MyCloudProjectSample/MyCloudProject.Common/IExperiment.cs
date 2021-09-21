using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyCloudProject.Common
{
    public interface IExperiment
    {
        /// <summary>
        /// Runs experiment.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ExperimentResult> Run(ExerimentRequestMessage request);
    }
}
