using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoE_Leveling_Helper
{
    internal class Quest
    {
        public string name { get; set; }
        public string objective { get; set; }
        public string completion { get; set; }
        public string[] classes { get; set; }
    }
}
