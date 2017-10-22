using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grimoire.Model
{
    public class Spell
    {
        public string ID { get; set; }
        public int Level { get; set; }
        public string Name { get; set; }
        public string School { get; set; }
        public string Type { get; set; }
        public string ShortDescription { get; set; }
        public string Materials { get; set; }
        public string Effects { get; set; }
        public string Limits { get; set; }
        public string Notes { get; set; }
        public int IncantTimes { get; internal set; }
        public string Incant { get; internal set; }
        public int ChargeTimes { get; internal set; }
        public string UsesPer { get; internal set; }
        public int Max { get; internal set; } = int.MaxValue;
        public int Cost { get; internal set; }
        public int Uses { get; internal set; }
        public string Range { get; internal set; }
    }
}
