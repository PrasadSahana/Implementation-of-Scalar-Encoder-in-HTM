﻿using MyCloudProject.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyExperiment
{
    public class AzureStorageProvider : IStorageProvider
    {
        public AzureStorageProvider(MyConfig config)
        { 
        
        }

        public Task<byte[]> DownloadInputFile(string fileName)
        {
            throw new NotImplementedException();
        }

        public Task UploadExperimentResult(ExperimentResult result)
        {
            throw new NotImplementedException();
        }

        public Task<byte[]> UploadResultFile(string fileName, byte[] data)
        {
            throw new NotImplementedException();
        }
    }
}