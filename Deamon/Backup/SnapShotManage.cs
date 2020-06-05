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
    public static class SnapShotManage
    {
        public static async void Process(SnapShotModel model, string destination)
        {
            string nazevSlozky = "snaps";
            string temp = Path.Combine(destination, nazevSlozky);
            DirectoryInfo di = Directory.CreateDirectory(temp);
            di.Attributes = FileAttributes.Hidden;
            temp = Path.Combine(temp + "\\SnapShotek.json");
            //Existing(temp);
            //string strResultJson = JsonConvert.SerializeObject(model);
            using (StreamWriter file = File.CreateText(temp))
            {
                JsonSerializer serializer = new JsonSerializer();
                //serialize object directly into file stream
                serializer.Serialize(file, model);
            }
        }

        private static void Existing(string destination)
        {
            if (!File.Exists(destination))
            {
                File.Create(destination);
            }
        }

        public static SnapShotModel FindFull(string destination, int idjob)
        {
            string nazevSlozky = "snaps";
            List<SnapShotModel> models = new List<SnapShotModel>();
            string[] dirs = Directory.GetDirectories(destination);
            var filePaths = new List<string>();// Directory.GetFiles(Path.Combine(destination, nazevSlozky), "*.json");
            foreach (var item in dirs)
            {
                filePaths.Add(Path.Combine(destination,item, nazevSlozky, "SnapShotek.json"));
            }
            foreach (var item in filePaths)
            {
                using (StreamReader r = new StreamReader(item))
                {
                    string json = r.ReadToEnd();
                    models.Add(JsonConvert.DeserializeObject<SnapShotModel>(json));
                    r.Close();
                }
            }

            SnapShotModel tmp = new SnapShotModel();
            //for (int i = 0; i < models.Count; i++)
            //{
            //    for (int j = 1; j < models.Count; j++)
            //    {
            //        tmp = models[i];
            //        int compare = tmp.Time.CompareTo(models[j].Time);
            //        if (compare > 0 || compare == 0)
            //            tmp = models[j];
            //    }
            //}
            foreach (var item in models)
            {
                if (item.IsSec == false && item.JobId == idjob)
                    tmp = item;
            }
            //Console.WriteLine(tmp.Time);
            if (tmp.Dirs == null || models.Count == 0)
                tmp = CreateSnapShot(idjob, new List<string>(), new List<string>(), 5, false);

            return tmp;
        }
        //private static string PathToSnap = @"C:\Users\Polo\Documents\SnapShotData.json";
        //public static List<SnapShotModel> LoadSnapShots()
        //{
        //    Exists();
        //    using (StreamReader r = new StreamReader(PathToSnap))
        //    {
        //        string json = r.ReadToEnd();
        //        List<SnapShotModel> items = JsonConvert.DeserializeObject<List<SnapShotModel>>(json);
        //        return items;
        //    }
        //}
        //public static void SaveSnapShots(SnapShotModel model)
        //{
        //    Exists();
        //    var temp = new List<SnapShotModel>(LoadSnapShots());
        //    temp.Add(model);
        //    var json = JsonConvert.SerializeObject(temp);
        //    File.WriteAllText(PathToSnap, json);
        //}

        public static SnapShotModel CreateSnapShot(int jobid, List<string> dirs, List<string> files, int tillfullbackup, bool sec)
        {
            var snap = new SnapShotModel()
            {
                JobId = jobid,
                Dirs = dirs,
                Files = files,
                TillFull = tillfullbackup,
                IsSec = sec,
                Time = DateTime.UtcNow
            };
            //SaveSnapShots(snap);
            return snap;
        }
        //public static void DeleteSnap(int delete)
        //{
        //    Exists();
        //    var temp = new List<SnapShotModel>(LoadSnapShots());
        //    temp.RemoveAt(delete);
        //    var json = JsonConvert.SerializeObject(temp);
        //    File.WriteAllText(PathToSnap, json);
        //}
        //private static void Exists()
        //{
        //    if (File.Exists(PathToSnap) != true)
        //        File.Create(PathToSnap);
        //}
    }
}
