using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class TradeConduct
    {
        public int Uid { get; set; }
        public string Uname { get; set; }
        public int Gid { get; set; }
        public string Gname { get; set; }
        public double Gprice { get; set; }
        public string Gdescription{ get; set; }
        public string  Gpicture { get; set; }
        public int Gseller { get; set; }   
        public char Tfinish { get; set; }
        public char TAbnormal { get; set; }
    }
}
