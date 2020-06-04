using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;
using System.Web.UI.WebControls;

namespace Deamon.Models
{
    public class JobsClientModel
    {
        public int Id { get; set; }
        public string BackupType { get; set; }
        public bool ToZip { get; set; }
        public int MaxFullBackup { get; set; }
        public int MaxSecBackup { get; set; }
        public string[] CronTime { get; set; }
        public string Ends { get; set; }
        public List<string> Source { get; set; }
        public List<string> Destination { get; set; }
    }
}
