using Deamon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Deamon.Services;
using System.IO;
using System.IO.Compression;

namespace Deamon.Backup
{
    public static class Algorithms
    {

        public static void FullBackup(JobsClientModel job)
        {
            SnapShotModel snap = SnapShotManage.CreateSnapShot(job.Id, new List<string>(), new List<string>(), job.MaxSecBackup,false);
            string finaltime = DateTime.UtcNow.ToString().Replace(':', ';').Replace(' ', '_');
            for (int i = 0; i < job.Source.Count; i++)
            {
                for (int e = 0; e < job.Destination.Count; e++)
                {
                    if(job.ToZip)
                    {
                        Helper.CopyCompared(job.Source[i],Path.Combine("C:\\TEMPBACKUP",finaltime), snap);
                        ZipFile.CreateFromDirectory("C:\\TEMPBACKUP", Path.Combine(job.Destination[e], finaltime) + ".zip");
                    }
                    else Helper.CopyCompared(job.Source[i], Path.Combine(job.Destination[e], finaltime), snap);
                
                }
            }
            foreach (var item in job.Destination)
            {
                SnapShotManage.Process(snap, Path.Combine(item, finaltime));
            }
            ReportService.RunAsync(ClientProcess.ClientIdGlobal, "full", false, new Exception("OK")).GetAwaiter();
        }
        public static void DiffBackup(JobsClientModel job)
        {
            SnapShotModel snap = SnapShotManage.FindFull(job.Destination[0],job.Id);
            //snap.IsSec = true;
            string finaltime = DateTime.UtcNow.ToString().Replace(':', ';').Replace(' ', '_');
            for (int i = 0; i < job.Source.Count; i++)
            {
                for (int e = 0; e < job.Destination.Count; e++)
                {
                    if (job.ToZip)
                    {
                        Helper.CopyCompared(job.Source[i], Path.Combine("C:\\TEMPBACKUP", finaltime), snap);
                        ZipFile.CreateFromDirectory("C:\\TEMPBACKUP", Path.Combine(job.Destination[e], finaltime) + ".zip");
                    }
                    else Helper.CopyCompared(job.Source[i], Path.Combine(job.Destination[e], finaltime), snap);

                }
            }
            foreach (var item in job.Destination)
            {
                SnapShotManage.Process(snap, Path.Combine(item, finaltime));
            }
            ReportService.RunAsync(ClientProcess.ClientIdGlobal, "diff", false, new Exception("OK")).GetAwaiter();
        }
        public static void IncrBackup(JobsClientModel job)
        {
            //for (int i = 0; i < job.Source.Count; i++)
            //{
            //    for (int e = 0; e < job.Destination.Count; e++)
            //    {
            //        Helper.CopyCompared(job.Source[i], job.Destination[e], snap, job.ToZip);
            //    }
            //}
            //SnapShotManage.SaveSnapShots(snap);
            //ReportService.RunAsync(ClientProcess.ClientIdGlobal, job.BackupType, false, new Exception("OK")).GetAwaiter();
        }


    }
}
