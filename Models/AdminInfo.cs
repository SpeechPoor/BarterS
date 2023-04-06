using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public  class AdminInfo
    {
        public int AdminId { get; set; }
        public string AdminLogin { get; set; }
        public string AdminPass { get; set; }
        public int AdminStatus { get; set; }
    }
}
