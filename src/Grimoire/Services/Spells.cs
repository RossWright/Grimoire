using Grimoire.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Grimoire.Services
{
    public static class Spells
    {
        static Spells()
        {
            var spells = LoadSpellsFromCsv()
                .ToDictionary(_ => _.Name, _ => _);
            Bard = LoadClassSpells("Bard", spells);
            Druid = LoadClassSpells("Druid", spells);
            Healer = LoadClassSpells("Healer", spells);
            Wizard = LoadClassSpells("Wizard", spells);
        }

        public static List<IClassSpell> Bard { get; private set; }
        public static List<IClassSpell> Druid { get; private set; }
        public static List<IClassSpell> Healer { get; private set; }
        public static List<IClassSpell> Wizard { get; private set; }
        public static List<IClassSpell> ByClass(AmtgardClass amtClass)
        {
            switch(amtClass)
            {
                case AmtgardClass.Bard: return Bard;
                case AmtgardClass.Druid: return Druid;
                case AmtgardClass.Healer: return Healer;
                case AmtgardClass.Wizard: return Wizard;
            }
            throw new KeyNotFoundException("Unknown Class");
        }
        
        public static List<IClassSpell> LoadClassSpells(string csvFile, Dictionary<string, ISpell> common)
        {
            var assembly = typeof(Spells).GetTypeInfo().Assembly;
            var stream = assembly.GetManifestResourceStream($"Grimoire.Data.{csvFile}.csv");
            var spells = new List<IClassSpell>();
            using (var reader = new System.IO.StreamReader(stream))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    Regex csvParser = new Regex(",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");
                    string[] fields = csvParser.Split(line);
                    spells.Add(new ClassSpell(common[fields[6].Trim('"')])
                    {
                        Level = int.Parse(fields[0]),
                        Cost = int.Parse(fields[1]),
                        Max = string.IsNullOrWhiteSpace(fields[2]) ? 99 : int.Parse(fields[2]),
                        Uses = string.IsNullOrWhiteSpace(fields[3]) ? 0 : int.Parse(fields[3]),
                        UsesPer = string.IsNullOrWhiteSpace(fields[4]) ? (UsesPer?)null : (UsesPer)Enum.Parse(typeof(UsesPer), fields[4]),
                        ChargeTimes = string.IsNullOrWhiteSpace(fields[5]) ? 0 : int.Parse(fields[5])
                    });
                }
            }
            return spells;
        }

        public static List<ISpell> LoadSpellsFromCsv()
        {
            var assembly = typeof(Spells).GetTypeInfo().Assembly;
            var stream = assembly.GetManifestResourceStream("Grimoire.Data.Spells.csv");
            var spells = new List<ISpell>();
            using (var reader = new System.IO.StreamReader(stream))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    Regex csvParser = new Regex(",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");
                    string[] fields = csvParser.Split(line);
                    spells.Add(new Spell
                    {
                        Name = fields[0]?.Trim('"'),
                        Type = (SpellType)Enum.Parse(typeof(SpellType), fields[1]),
                        School = (SpellSchool)Enum.Parse(typeof(SpellSchool), fields[2]),
                        Range = string.IsNullOrWhiteSpace(fields[3]) ? (SpellRange?)null :
                            (SpellRange)Enum.Parse(typeof(SpellRange), fields[3]),
                        Incant = fields[4]?.Trim('"'),
                        IncantTimes = string.IsNullOrWhiteSpace(fields[5]) ? (int)1 : int.Parse(fields[5]),
                        Materials = fields[6]?.Trim('"'),
                        Effects = fields[7]?.Trim('"'),
                        Limits = fields[8]?.Trim('"'),
                        Notes = fields[9]?.Trim('"')
                    });
                }
            }
            return spells;
        }
    }
}
