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
                var charClass = PickOne(AmtgardClass.Bard, AmtgardClass.Druid, AmtgardClass.Healer, AmtgardClass.Wizard);
                var adjective = PickOne("My ", "Battle ", "Support ", "Tank ", "Mega-", "Death ", "Holy ", "Cheesy ", "New ", "Old ", "Best ");
                return new SpellList
                {
                    ID = Guid.NewGuid().ToString(),
                    Title = $"{adjective}{charClass}",
                    Class = charClass,
                    Level = random.Next(6) + 1
                };
            }));
            spellListSpells = spellLists.ToDictionary(_ => _.ID, _ => LoadSpellsForSpellList(_).ToList());
        }

        static T PickOne<T>(params T[] choices)
        {
            random = random ?? new Random();
            var pick = random.Next(choices.Length);
            return choices[pick];
        }
        static Random random;

        private readonly List<SpellList> spellLists;
        private readonly Dictionary<string, List<IClassSpell>> spellListSpells;

        public IEnumerable<ISpellList> Load() => spellLists
            .OrderBy(_ => _.Class)
            .ThenByDescending(_ => _.Level)
            .ThenByDescending(_ => _.Title);
        
        public void Save(ISpellList spellList)
        {
            Delete(spellList);
            spellLists.Add((SpellList)spellList);
        }

        public void Delete(ISpellList spellList)
        {
            var index = spellLists.FindIndex(_ => _.ID == spellList.ID);
            if (index != -1)
                spellLists.RemoveAt(index);
        }

        public IEnumerable<IClassSpell> LoadSpellsForSpellList(ISpellList spellList)
        {
            var classSpells = Spells.ByClass(spellList.Class);
            return classSpells
                .Where(_ => _.Level <= spellList.Level)
                .OrderBy(_ => _.Level)
                .ThenBy(_ => _.Name);
        }
        
        public void SaveSpellsForSpellList(ISpellList spellList, IEnumerable<IClassSpell> classSpells)
        {
            throw new NotImplementedException();
        }
    }
}
