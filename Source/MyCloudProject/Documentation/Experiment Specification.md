## What is our experiment about
#### What experiment is doing and reference to our SE project documentation (PDF):
Scalar encoder is a part of Hierarchical Temporal Memory (HTM) systems used in solving problems like classification, prediction, and anomaly detection for a wide range of datatypes. HTM provides a very flexible and biologically precise framework. The input for HTM systems should be in the form of Sparse Distributed Representations (SDRs). SDR consists of a large array of bits in which most of the bits are zeros and few bits are ones. The encoders are used to covert the data input into SDR in HTM systems. The encoders also decide which bits should be ones and which bits should be zeros in the output for any given input value in such a way that it captures the important semantic characteristics of data. Hence if the inputs are alike, then output is more likely to have highly overlapping SDRs. 
Azure storage acts as a platform for modern data storage which is a cloud storage by Microsoft. The azure storage is highly reliable, and feasible, secure, adaptable and it can be accessed from any part of the world over HTTP or HTTPS. The core azure storage services include blobs, files, queues, tables, and disks. In this project we mainly deal with three types of storage which are blobs, queues, and tables.

#### Cloud Project
The steps involved while deploying the Scalar encoder to cloud is are as follows:
1) The docker image is produced for the scalar encoder code. This docker image is stored in azure container registry and using the azure container instances the container is deployed.
2) The input data is uploaded as a file by the user to the blob storage.
3) To trigger the process a message is sent by the user.
4) After receiving the message in the queue, the code is triggered, as a result it downloads and extracts the file.
5) Scalar encoder test cases will use the data from the downloaded file and then it starts executing.
6) After the execution of scalar encoder test cases the obtained results will be uploaded to the Blob and Table.

#### Code Description:
This project is based on the class Expriment.cs. When this program is run then it will be called by the main Program.cs file. The main aim of this Experiment.cs is to create a local folder in desktop path where the files will be first downloaded.
The Objects that are being taken in this Experiment.cs code is unchanged and needs to be used throughout. They are:
•	StorageProvider
•	Logger(Log)
•	configSection

Here Inputdata.json file is uploaded as a file to blob and message is sent to the queue by user to trigger the process. Here configSection is used with IConfigurationSection configuration and InitHelpers class to retrieve the details from appconfig.Json file. Once the Constructor is created then RunQueueListener method is created after that. This basically aims at listening the queue messages from the program that is pipelined next.
##### Step I: 
The input that is being triggered by queue is executed and read. The input is then deserialized or converted from byte stream to object memory. The log is then logged on the console which shows queue messages from the input file that was provided such as name, description etc.
##### Step II: 
The name of the input file that is present in queue (in this case 2,4 and 8) is first downloaded from blob and then the local file that is returned, is stored in local path. The method DownloadInputFile in class AzureBlobOperations.cs is used to download input file from path.
##### Step III: 
The program is run with input file that is present in local repository, for further processing. The path of the file that is stored needs to be mentioned here. The result that is obtained here from Run after program is debugged, is serialized or again converted into byte stream. The code of the method ‘Run’ is given below:
public async Task<ExperimentResult> Run(string localFileName)
        {
            var seProjectInputDataList =
                JsonConvert.DeserializeObject<List<SeProjectInputDataModel>>(FileUtilities.ReadFile(localFileName));

            var startTime = DateTime.UtcNow;

            // running until the input ends
            string uploadedDataURI = await RunSoftwareEngineeringExperiment(seProjectInputDataList);

            // Added delay
            Thread.Sleep(5000);
            var endTime = DateTime.UtcNow;
            
            logger?.LogInformation(
                $"Ran all test cases as per input from the blob storage");
            
            long duration = endTime.Subtract(startTime).Seconds;

            var res = new ExperimentResult(this.config.GroupId, Guid.NewGuid().ToString());
            UpdateExperimentResult(res, startTime, endTime, duration, localFileName, uploadedDataURI);
            return res;
        }

Once this code is run then data is deserialized and data from the output file is downloaded. After that it runs software engineering experiment using the method RunSoftwareEngineeringExperiment. This method shows the steps where the test cases are processed by pointing to ScalarEncoderTests. Then a file is created to output the results into another file.

{
// Step 2: Uploading output to blob storage
              var uploadedUri =
                  await storageProvider.UploadResultFile("EncodedOutput.txt",
                      null);
              logger?.LogInformation(
                  $"Test cases output file uploaded successful. Blob URL: {Encoding.ASCII.GetString(uploadedUri)}");

            // return a string and delete the combined file if possible
            return "Project completed";
}

The output file is uploaded to blob in azure account using the method UploadResultFile.

#### Inputs and Resultant Outputs in our Cloud Project: 

InputData.json file is uploaded as a file to blob/table and Message is sent to the queue by user to trigger the process. The name of the input file that is present in queue (in this case 2,4 and 8) is first downloaded from blob/table and then the local file that is returned, is stored in local path.
##### Step I: 
When RunSoftwareEngineeringExperiment is executed then the result is uploaded to blob storage having a method of UploadResultFile in AzureBlobOperations.cs class where URI of the file (in blob) is returned.
##### Step II: 
The obtained result that is uploaded to blob storage by the means of UploadExperimentResult method is inserted (if not already exists) into the Table created by the same method CreateTableAsync. Once the resultant file is obtained for blob then it is being named as EncodedOutput.
##### Step III: 
The result is inserted (if not already exists) into the Table created by the method CreateTableAsync. Similar to blob storage, the resultant file that is obtained is being named as EncodedOutput.
The numbers 2,4 and 8 that is present in our input file is being referred to test cases from our Software Engineering Project. Reference to our Software Engineering project documentation can be found below:
SoftwareEngineeringProjectDocumentation

#### How to run our experiment
##### Step1:
Create one queue, blob and table containers on azure. Upload the below InputData.json file in the blob container:

{
  {
    "inputData": 2
  },
  {
    "inputData": 4
  },
  {
    "inputData": 8
  }
}

##### Step2: 
Update the connection strings in the file appsettings.json.

"MyConfig": {
    "GroupId": "Group3",
    "StorageConnectionString": "DefaultEndpointsProtocol=https;AccountName=group3storageaccount;AccountKey=wy2aN6T7ynuQxwoOmleHppf6ti3Y5qYnOl4FB/n28MfgW2Eko0IKwFNH8H+aF9d0O1cLfgFC0abVOzWSjqpWjA==;EndpointSuffix=core.windows.net",
    "TrainingContainer": "training-files",
    "ResultContainer": "result-files",
    "ResultTable": "results",
    "Queue": "trigger-queue",
    "StorageConnectionStringCosmosTable": "DefaultEndpointsProtocol=https;AccountName=group3cosmos;AccountKey=v2v5dcP1v41q5z6lgfRcmGsOY3fYaYgOOQlLjHlxZ6uSMCXrGuIe3CxlLhE8xab3JbhwoC1yfWUCVVmGYttN3g==;TableEndpoint=https://group3cosmos.table.cosmos.azure.com:443/;",
    "LocalPath": "./data/"
  }

##### Step3: 
Run the experiment with InputData.json  and while running you will notice a queue that being triggered in azure portal. We have to copy and paste the below data in the queue message on the azure portal.

{
     "ExperimentId" : "123",
     "InputFile" : "https://group3storageaccount.blob.core.windows.net/input-files/InputData.json", 
     "Name" : "Testing input1.json",
     "Description" : "project review"
 }
  
##### Step4:
Once you run the experiment, you will observe an output file that is being downloaded and the resultant file is being uploaded to azure blob and table container.

#### References
[1]	Implementation of Scalar Encoder in HTM
https://github.com/UniversityOfAppliedSciencesFrankfurt/se-cloud-2019-2020/blob/Group3/Source/HTM/UnitTestsProject/EncoderTests/Documentation/Project Report.pdf	
	






