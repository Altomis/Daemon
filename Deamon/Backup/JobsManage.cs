using Deamon.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deamon.Backup
{
    public static class JobsManage
    {
        private static string PathToJobs = @"C:\Users\Polo\Documents\JobsData.json";
        public static List<JobsClientModel> LoadJobs()
        {
            Exists();
            using (StreamReader r = new StreamReader(PathToJobs))
            {
                string json = r.ReadToEnd();
                List<JobsClientModel> items = JsonConvert.DeserializeObject<List<JobsClientModel>>(json);
                //foreach (JobsClientModel item in items)
                //{
                //    Console.WriteLine(item.Id);
                //}
                return items;
            }
        }
        public static void SaveJobs(JobsClientModel model)
        {
            Exists();
            var temp = new List<JobsClientModel>(LoadJobs());
            bool r = true;
            for (int i = 0; i < temp.Count; i++)
            {
                if (temp[i].Id == model.Id)
                {
                    temp[i] = model;
                }
            }
            if (r)
                temp.Add(model);
            var json = JsonConvert.SerializeObject(temp);
            File.WriteAllText(PathToJobs, json);
        }
        public static void DeleteJobs(JobsClientModel model)
        {
            Exists();
            var temp = new List<JobsClientModel>(LoadJobs());
            temp.Remove(model);
            var json = JsonConvert.SerializeObject(temp);
            File.WriteAllText(PathToJobs, json);
        }
        private static void Exists()
        {
            if (File.Exists(PathToJobs) != true)
                File.Create(PathToJobs);
        }

    }
}
