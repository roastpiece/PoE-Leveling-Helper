using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoE_Leveling_Helper
{
    internal class Gem
    {

        public string name { get; set; }
        public int reqLevel { get; set; }
        public string vendorPrice { get; set; }
        public Quest quest { get; set; }
        public Vendor vendor { get; set; }
    }
}
