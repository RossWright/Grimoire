using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grimoire.Model
{
    public class SpellList
    {
        public string ID { get; set; }
        public string Title { get; set; }
        public string Class { get; set; }
        public int Level { get; set; }

        public SpellList Clone()
        {
            var clone = (SpellList)MemberwiseClone();
            clone.Title += " (Clone)";
            clone.ID = Guid.NewGuid().ToString();
            return clone;
        }
    }
}
