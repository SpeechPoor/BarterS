using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class GoodsInfo
    {
        public int Gid { get; set; }
        public string Gname { get; set; }
        public int Gtype { get; set; }
        public double Gprice { get; set; }
        public int Gseller { get; set; }
        public Boolean  Gstatus{ get; set; }
        public string Gdescription { get; set; }
        public DateTime GDateTime { get; set; }
        public string Gpicture { get; set; }
    }
}
