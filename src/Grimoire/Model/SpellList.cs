using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grimoire.Model
{
    public interface ISpellList
    {
        string ID { get; }
        string Title { get; set; }
        AmtgardClass Class { get; set; }
        int Level { get; set; }
        ISpellList Clone();
    }

    public class SpellList : ISpellList
    {
        public string ID { get; set; }
        public string Title { get; set; }
        public AmtgardClass Class { get; set; }
        public int Level { get; set; }

        public ISpellList Clone()
        {
            var clone = (SpellList)MemberwiseClone();
            clone.Title += " (Clone)";
            clone.ID = Guid.NewGuid().ToString();
            return clone;
        }
    }

    public enum AmtgardClass { Bard, Druid, Healer, Wizard }
}
