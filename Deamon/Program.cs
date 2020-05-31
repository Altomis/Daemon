using Deamon.Backup;
using Deamon.Models;
using Deamon.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Deamon
{
    class Program
    {
        public static int CheckAPI { get; set; } = 0;

        static void Main(string[] args)
        {
            //ClientProcess.RunAsync().GetAwaiter().GetResult();
            //Console.ReadLine();


            Timer aTimer = new Timer();
            aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            aTimer.Interval = 5000;
            aTimer.Enabled = true;

            while (Console.Read() != 'q') ;
        }
        public static void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            CheckAPI++;
            Console.WriteLine("just elapsed");
            if (CheckAPI == 10)
            {
                //Console.WriteLine("five elapsed");
                CheckAPI = 0;
            }
            //SnapShotManage.CreateSnapShot(1, new List<string>() { "adsfajsdf", "asdfajsdfj" }, new List<string>() { "231656651", "3216551" }, 5);
            //SnapShotManage.DeleteSnap(1);

            //checks for getmyjobs


            var jobs = new List<JobsClientModel>(JobsManage.LoadJobs());
            //foreach (JobsClientModel item in jobs)
            //{
            //    Console.WriteLine(item.Id);
            //}


            for (int i = 0; i < jobs.Count; i++)
            {
                foreach (DateTime item in jobs[i].CronTime)
                {
                    if (Helper.IsTime(item))
                        Backup.BackupLogic.DoBackup(jobs[i]);
                }
            }
        }
    }
}
