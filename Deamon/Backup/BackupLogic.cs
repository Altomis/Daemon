using Deamon.Models;
using Deamon.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deamon.Backup
{
    public static class BackupLogic
    {
        public static int ClientIdGlobal { get; private set; }

        public static void DoBackup(JobsClientModel fromjob)
        {
            //var snaps = new List<SnapShotModel>(SnapShotManage.LoadSnapShots());
            //SnapShotModel snap = new SnapShotModel();
            //foreach (SnapShotModel item in snaps)
            //{
            //    if (item.JobId == fromjob.Id)
            //    {
            //        snap = item;
            //        break;
            //    }
            //    else 
            //    {
            //        //List<string> getdirs = new List<string>();// = Directory.GetDirectories(jobs.Source);
            //        //List<string> getfiles = new List<string>();// = Directory.GetFiles(@"C:\Users\Polo\Desktop\testprg");
            //        //foreach (string sourc in fromjob.Source)
            //        //{
            //        //    foreach (string itt in Directory.GetDirectories(sourc))
            //        //        getdirs.Add(itt);
            //        //    foreach (string itt in Directory.GetFiles(sourc))
            //        //        getfiles.Add(itt);
            //        //}
            //        SnapShotManage.CreateSnapShot(fromjob.Id, new List<string>(), new List<string>(), fromjob.MaxSecBackup,false);
            //    }
            //}
            try
            {
                if (fromjob.BackupType == "full")
                    Algorithms.FullBackup(fromjob);
                else if (fromjob.BackupType == "incr")
                    Algorithms.IncrBackup(fromjob);
                else if (fromjob.BackupType == "diff")
                    Algorithms.DiffBackup(fromjob);
            }
            catch (Exception ex)
            {
                ReportService.RunAsync(ClientIdGlobal, fromjob.BackupType, true, ex).GetAwaiter();
            }
        }
    }
}
