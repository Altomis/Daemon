using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deamon.Models
{
    public class Report
    {
        public int Id { get; set; }
        public int IdGroups { get; set; }
        public string BackupType { get; set; }
        public DateTime Time { get; set; }
        public bool IsError { get; set; }
        public string ErrorMsg { get; set; }
    }
}
