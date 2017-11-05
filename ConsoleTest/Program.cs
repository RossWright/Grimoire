using Grimoire.Services;
using System;
using System.Linq;

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var spells = Spells.LoadSpellsFromCsv()
                .ToDictionary(_ => _.Name, _ => _);
            var bard = Spells.LoadClassSpells("Bard", spells);
            var druid = Spells.LoadClassSpells("Druid", spells);
            var healer = Spells.LoadClassSpells("Healer", spells);
            var wizard = Spells.LoadClassSpells("Wizard", spells);
        }
    }
}
