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
        private static string PathToSnap = @"C:\Users\Polo\Documents\SnapShotData.json";
        public static List<SnapShotModel> LoadSnapShots()
        {
            Exists();
            using (StreamReader r = new StreamReader(PathToSnap))
            {
                string json = r.ReadToEnd();
                List<SnapShotModel> items = JsonConvert.DeserializeObject<List<SnapShotModel>>(json);
                return items;
            }
        }
        public static void SaveSnapShots(SnapShotModel model)
        {
            Exists();
            var temp = new List<SnapShotModel>(LoadSnapShots());
            temp.Add(model);
            var json = JsonConvert.SerializeObject(temp);
            File.WriteAllText(PathToSnap, json);
        }

        public static SnapShotModel CreateSnapShot(int jobid, List<string> dirs, List<string> files, int tillfullbackup)
        {
            var snap = new SnapShotModel()
            {
                JobId = jobid,
                Dirs = dirs,
                Files = files,
                TillFull = tillfullbackup
            };
            SaveSnapShots(snap);
            return snap;
        }
        public static void DeleteSnap(int delete)
        {
            Exists();
            var temp = new List<SnapShotModel>(LoadSnapShots());
            temp.RemoveAt(delete);
            var json = JsonConvert.SerializeObject(temp);
            File.WriteAllText(PathToSnap, json);
        }
        private static void Exists()
        {
            if (File.Exists(PathToSnap) != true)
                File.Create(PathToSnap);
        }
    }
}
