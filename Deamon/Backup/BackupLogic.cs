using Deamon.Models;
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
        public static void DoBackup(JobsClientModel fromjob)
        {
            var snaps = new List<SnapShotModel>(SnapShotManage.LoadSnapShots());
            SnapShotModel snap;
            foreach (SnapShotModel item in snaps)
            {
                if (item.JobId == fromjob.Id)
                {
                    snap = item;
                    break;
                }
                else 
                {
                    List<string> getdirs = new List<string>();// = Directory.GetDirectories(jobs.Source);
                    List<string> getfiles = new List<string>();// = Directory.GetFiles(@"C:\Users\Polo\Desktop\testprg");
                    foreach (string sourc in fromjob.Source)
                    {
                        foreach (string itt in Directory.GetDirectories(sourc))
                            getdirs.Add(itt);
                        foreach (string itt in Directory.GetFiles(sourc))
                            getfiles.Add(itt);
                    }
                    SnapShotManage.CreateSnapShot(fromjob.Id, getdirs, getfiles, fromjob.MaxSecBackup);
                }
            }
            try
            {
                if (fromjob.BackupType == "full")
                    Algorithms.FullBackup();
                else if (fromjob.BackupType == "incr")
                    Algorithms.IncrBackup();
                else if (fromjob.BackupType == "diff")
                    Algorithms.DiffBackup();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
