using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Deamon.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using Deamon.Backup;

namespace Deamon.Services
{
    public static class JobsService
    {
        static HttpClient cclient = new HttpClient();

        public static async Task<List<JobsClientModel>> GetJobsClientAsync(string Path)
        {
            List<JobsClientModel> job = null;
            HttpResponseMessage response = await cclient.GetAsync(Path);
            if (response.IsSuccessStatusCode)
            {
                job = await response.Content.ReadAsAsync<List<JobsClientModel>>();
            }
            return job;
        }

        public static async Task RunJobsClient()
        {
            cclient.BaseAddress = new Uri("http://localhost:49497/");
            cclient.DefaultRequestHeaders.Accept.Clear();
            cclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await cclient.GetAsync("api/jobs");

            //List<JobsClientModel> tempjobs = await response.Content.ReadAsAsync<List<JobsClientModel>>();
            
            List<JobsClientModel> jobsClient = await GetJobsClientAsync("http://localhost:49497/api/jobs/getmyjobs/30");
            foreach (var item in jobsClient)
            {
                JobsManage.SaveJobs(item);
            }
            cclient.Dispose();
            //Console.WriteLine("ID: {0}\t BackupType: {1}", jobsClient.Id, jobsClient.BackupType);
        }
    }
}

//[
//  {
//    "Id": 1,
//    "BackupType": "full",
//    "MaxFullBackup": 3,
//    "MaxSecBackup": 5,
//    "CronTime": [ "2020-05-16T00:00:00Z", "2020-05-17T00:00:00Z", "2020-05-18T00:00:00Z", "2020-05-19T00:00:00Z", "2020-05-20T00:00:00Z", "2020-05-21T00:00:00Z", "2020-05-22T00:00:00Z", "2020-05-23T00:00:00Z", "2020-05-24T00:00:00Z", "2020-05-25T00:00:00Z" ],
//    "Ends": "7/6/2020/",
//    "Source": [ "C:\\backuptest", "C:\\backuptest1" ],
//    "Destination": [ "D:\\backuptest" ]
//  },
//  {
//    "Id": 2,
//    "BackupType": "dif",
//    "MaxFullBackup": 2,
//    "MaxSecBackup": 2,
//    "CronTime": [ "2021-01-03T00:00:00Z", "2021-01-03T00:01:00Z", "2021-01-03T00:02:00Z", "2021-01-03T00:03:00Z", "2021-01-03T00:04:00Z", "2021-01-03T00:05:00Z", "2021-01-03T00:06:00Z", "2021-01-03T00:07:00Z", "2021-01-03T00:08:00Z", "2021-01-03T00:09:00Z" ],
//    "Ends": "9/9/2000/",
//    "Source": [ "C:\backuptest2" ],
//    "Destination": [ "D:\backuptest2" ]
//  }
//]