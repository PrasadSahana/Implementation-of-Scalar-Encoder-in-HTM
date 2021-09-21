using LearningFoundation;
using LearningFoundation.DataProviders;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyAlgorithm;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MyAlgorithmUnitTests
{
    [TestClass]
    public class UnitTest1
    {
        private const string m_IrisData = @"Samples\iris.csv";

        [TestMethod]
        public void TestMyAlgorithm()
        {
            var path = System.IO.Path.Combine(Directory.GetCurrentDirectory(), m_IrisData);

            LearningApi api = new LearningApi(Helpers.GetDescriptor());

            api.UseCsvDataProvider(path, ',', 1);

            api.UseActionModule<object[][], double[][]>((object[][] data, IContext ctx) =>
            {
                List<double[]> newData = new List<double[]>();
                foreach (var item in data)
                {
                    List<double> row = new List<double>();
                    for (int i = 0; i < ctx.DataDescriptor.Features.Length; i++)
                    {
                        double converted;
                        if (double.TryParse((string)item[i], out converted))
                            row.Add(converted);
                        else
                            throw new System.Exception("Column is not convertable to double.");
                    }

                    switch (item[ctx.DataDescriptor.LabelIndex])
                    {
                        case "setosa":
                        case "Setosa":
                            row.Add(0);
                            break;
                        case "versicolor":
                            row.Add(1);
                            break;
                        case "virginica":
                            row.Add(2);
                            break;
                    }

                    newData.Add(row.ToArray());
                }
                return newData.ToArray();
            });
        
            api.UseMyPipelineModule();
          
            api.UseActionModule<double[][], double[][]>((double[][] data, IContext ctx) =>
            {
                return data;
            });

            api.UseMyAlgorithm(0.1);

            // Training

            MyAlgorithmScore score = api.Run() as MyAlgorithmScore;

            //api.Save("sas.json");
            //// Prediction


            double[][] predictingData = new double[3][];

            predictingData[0] = new double[] { 2 };
            predictingData[1] = new double[] { 5 };
            predictingData[2] = new double[] { 7 };


            //var api2 = LearningApi.Load("sas.json");
            MyAlgorithmResult result = api.Algorithm.Predict(predictingData, api.Context) as MyAlgorithmResult;

            //Assert.AreEqual(result.Results[0], 27.8);
        }
    }
}
