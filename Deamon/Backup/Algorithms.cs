using Deamon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Deamon.Services;

namespace Deamon.Backup
{
    public static class Algorithms
    {

        public static void FullBackup(JobsClientModel job)
        {
            SnapShotModel snap = SnapShotManage.CreateSnapShot(job.Id, new List<string>(), new List<string>(), job.MaxSecBackup);
            for (int i = 0; i < job.Source.Count; i++)
            {
                for (int e = 0; e < job.Destination.Count; e++)
                {
                    Helper.CopyCompared(job.Source[i],job.Destination[e],snap,job.ToZip);
                }
            }
            SnapShotManage.SaveSnapShots(snap);
            ReportService.RunAsync(ClientProcess.ClientIdGlobal, job.BackupType, true, new Exception("OK"));
        }
        public static void DiffBackup(SnapShotModel snap, JobsClientModel job)
        {

        }
        public static void IncrBackup(SnapShotModel snap, JobsClientModel job)
        {
            for (int i = 0; i < job.Source.Count; i++)
            {
                for (int e = 0; e < job.Destination.Count; e++)
                {
                    Helper.CopyCompared(job.Source[i], job.Destination[e], snap, job.ToZip);
                }
            }
            SnapShotManage.SaveSnapShots(snap);
            ReportService.RunAsync(ClientProcess.ClientIdGlobal, job.BackupType, true, new Exception("OK"));
        }


    }
}
