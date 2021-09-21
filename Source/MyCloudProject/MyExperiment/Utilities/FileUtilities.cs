using System.IO;

namespace MyExperiment.Utilities
{
    public static class FileUtilities
    {
        /// <summary>
        /// Reading the text 
        /// </summary>
        /// <param name="localfilePath"></param>
        /// <returns></returns>
        public static string ReadFile(string localFilePath)
        {
            string jsonString = File.ReadAllText(localFilePath);
            return jsonString;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static void WriteDataInFile(string fileName, int data, int[] output)
        {
            // Create a local file in the ./data/ directory for uploading and downloading
            string localfilePath = Path.Combine(Experiment.DataFolder, fileName);

            if (!File.Exists(localfilePath))
            {
                File.Create(localfilePath);
            }

            StreamWriter sw = File.AppendText(localfilePath);
            
            try
            {
                sw.WriteLine();
                sw.WriteLine("*************--ScalarEncoding Process Started--************");
                sw.Write("The Encoded Value for : " + data + " -> ");
                foreach (var d in output)
                {
                    sw.Write(d + "  ");
                }
            }
            finally
            {
                sw.WriteLine();
                sw.WriteLine("*************--ScalarEncoding Process Ended--************");
                sw.WriteLine();
                sw.Flush();
                sw.Close(); 
            }

           
        }
    }
}