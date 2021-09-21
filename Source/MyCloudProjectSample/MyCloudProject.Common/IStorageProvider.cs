using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyCloudProject.Common
{
    public interface IStorageProvider
    {
        Task<byte[]> DownloadInputFile(string fileName);

        Task<byte[]> UploadResultFile(string fileName, byte[] data);

        Task UploadExperimentResult(ExperimentResult result);
    }
}
