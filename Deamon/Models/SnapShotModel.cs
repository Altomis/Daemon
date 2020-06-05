using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deamon.Models
{
    public class SnapShotModel
    {
        public int JobId { get; set; }
        public List<string> Dirs { get; set; }
        public List<string> Files { get; set; }
        public int TillFull { get; set; }
        public bool IsSec { get; set; }
        public DateTime Time { get; set; }
    }
}
