using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Countries_WebClient
{
    public class Country
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Capital { get; set; }
        public float Area { get; set; }
        public int Population { get; set; }
        public string Region { get; set; }
    }
}
