using Grimoire.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grimoire.Services
{
    public class MockDataStore : IDataStore
    {
        public static IDataStore Instance { get; } = new MockDataStore();
        private MockDataStore()
        {
            spellLists = new List<Model.SpellList>(Enumerable.Range(0, 10).Select(_ =>
            {
                var charClass = PickOne("Wizard", "Healer", "Bard", "Druid");
                var adjective = PickOne("My ", "Battle ", "Support ", "Tank ", "Mega-", "Death ", "Holy ", "Cheesy ", "New ", "Old ", "Best ");
                return new SpellList
                {
                    ID = Guid.NewGuid().ToString(),
                    Title = $"{adjective}{charClass}",
                    Class = charClass,
                    Level = random.Next(6) + 1
                };
            }));
        }
        static string PickOne(params string[] choices)
        {
            random = random ?? new Random();
            var pick = random.Next(choices.Length);
            return choices[pick];
        }
        static Random random;

        List<SpellList> spellLists;

        public IEnumerable<SpellList> Load() => spellLists.OrderBy(_ => _.Title).ThenByDescending(_ => _.Level);
        
        public void Save(SpellList spellList)
        {
            Delete(spellList);
            spellLists.Add(spellList);
        }

        public void Delete(SpellList spellList)
        {
            var index = spellLists.FindIndex(_ => _.ID == spellList.ID);
            if (index != -1)
                spellLists.RemoveAt(index);
        }

        public IEnumerable<Spell> LoadSpells(SpellList spellList)
        {
            return new Spell[]
            {
                new Spell
                {
                    Level = 1,
                    Name = "Banish",
                    Cost = 1,
                    Uses = 1,
                    UsesPer = "Life",
                    Type = "Verbal",
                    School ="Spirit",
                    Range ="20’",
                    Incant = "The spirits banish thee from this place",
                    IncantTimes = 3,
                    Effects = "Target Insubstantial player must return to their respawn location where their Insubstantial State immediately ends.",
                    Notes = "A player bearing Undead Minion or Greater Undead Minion who is currently Insubstantial has their Enchantment removed."
                },
                new Spell
                {
                    Level = 1,
                    Name = "Cancel",
                    UsesPer = "Unlimited",
                    Type = "Neutral",
                    School ="Neutral",
                    Range ="Touch",
                    Incant = "My work shall be undone",
                    IncantTimes = 3,
                    Effects = "Remove an Enchantment cast by the caster"
                },
                new Spell
                {
                    Level = 1,
                    Name = "Equipment: Weapon, Short",
                    Cost = 2,
                    Max = 1,
                    Type = "Neutral",
                    School ="Neutral",
                    Effects ="May use any Short weapon. May use one such weapon at a time for each instance purchased."
                },
                new Spell
                {
                    Level = 1,
                    Name = "Experienced",
                    Cost = 2,
                    Max = 2,
                    Type = "Neutral",
                    School ="Neutral",
                    Effects ="A single per-life Verbal purchased becomes Charge x5 in addition to the normal frequency. OR a single per-refresh Verbal purchased becomes Charge x10 in addition to the normal frequency. This Verbal must be determined before the game begins and cannot be changed.",
                    Limits = "Verbal must be 4th level or lower."
                },
                new Spell
                {
                    Level = 1,
                    Name = "Force Barrier",
                    Cost = 1,
                    Max = 1,
                    Uses = 1,
                    UsesPer = "Refresh",
                    Type = "Verbal",
                    School ="Sorcery",
                    Range = "Self",
                    Incant = "I shall not be harmed",
                    IncantTimes = 1,
                    Effects ="Player is Frozen for 30 seconds."
                },
                new Spell
                {
                    Level = 1,
                    Name = "Force Bolt",
                    Cost = 1,
                    Max = 4,
                    Uses = 3,
                    UsesPer = "Balls",
                    Type = "Magic Ball",
                    School ="Sorcery",
                    Range = "Ball",
                    Incant = "Forecbolt",
                    IncantTimes = 3,
                    Effects ="Force Bolt will have one of the following effects on the object first struck: A weapon hit is destroyed.Armor hit with Armor Points remaining is subject to Armor Breaking.A player hit receives a Wound to that hit location.",
                    Materials = "Blue Magic Ball"
                },
                new Spell
                {
                    Level = 1,
                    Name = "Heat Weapon",
                    Cost = 1,
                    Uses = 1,
                    UsesPer = "Life",
                    Type = "Verbal",
                    School ="Flame",
                    Range = "20'",
                    Incant = "I call upon flame to heat that[type of weapon]",
                    IncantTimes = 3,
                    Effects ="Target weapon may not be wielded for 30 seconds.Players who are Immune to Flame may continue to wield the weapon.",
                },
                new Spell
                {
                    Level = 1,
                    Name = "Mend",
                    Cost = 1,
                    Uses = 1,
                    UsesPer = "Life",
                    Type = "Verbal",
                    School ="Sorcery",
                    Range = "Touch",
                    Incant = "I make this item whole again",
                    IncantTimes = 5,
                    Effects ="Destroyed item is repaired.One point of armor in one location is repaired.",
                },
                new Spell
                {
                    Level = 1,
                    Name = "Shove",
                    Cost = 1,
                    Uses = 1,
                    UsesPer = "Life",
                    ChargeTimes = 3,
                    Type = "Verbal",
                    School ="Sorcery",
                    Range = "Touch",
                    Incant = "My power shoves thee",
                    IncantTimes = 3,
                    Effects ="Target player is moved back 20’. Works on Stopped players.",
                },
            };
        }
    }
}
