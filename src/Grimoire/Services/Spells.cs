﻿using Grimoire.Model;
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

        static List<IClassSpell> LoadClassSpells(string csvFile, Dictionary<string, ISpell> common)
        {
            var assembly = typeof(Spells).GetTypeInfo().Assembly;
            var stream = assembly.GetManifestResourceStream($"Grimoire.{csvFile}.csv");
            var spells = new List<IClassSpell>();
            using (var reader = new System.IO.StreamReader(stream))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    Regex csvParser = new Regex(",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");
                    string[] fields = csvParser.Split(line);
                    spells.Add(new ClassSpell(common[fields[6]])
                    {
                        Level = int.Parse(fields[0]),
                        Cost = int.Parse(fields[1]),
                        Max = int.Parse(fields[2]),
                        Uses = int.Parse(fields[3]),
                        UsesPer = (UsesPer)Enum.Parse(typeof(UsesPer), fields[4]),
                        ChargeTimes = int.Parse(fields[5])
                    });
                }
            }
            return spells;
        }

        static List<ISpell> LoadSpellsFromCsv()
        {
            var assembly = typeof(Spells).GetTypeInfo().Assembly;
            var stream = assembly.GetManifestResourceStream("Grimoire.Spells.csv");
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
                        Name = fields[0],
                        Type = (SpellType)Enum.Parse(typeof(SpellType), fields[1]),
                        School = (SpellSchool)Enum.Parse(typeof(SpellSchool), fields[2]),
                        Incant = fields[3],
                        IncantTimes = int.Parse(fields[4]),
                        Materials = fields[5],
                        Effects = fields[6],
                        Limits = fields[7],
                        Notes = fields[8]
                    });
                }
            }
            return spells;
        }
    }
}