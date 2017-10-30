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
                    Class = AmtgardClass.Bard,
                    Level = random.Next(6) + 1
                };
            }));
        }
        static T PickOne<T>(params T[] choices)
        {
            random = random ?? new Random();
            var pick = random.Next(choices.Length);
            return choices[pick];
        }
        static Random random;

        List<SpellList> spellLists;

        public IEnumerable<SpellList> Load() => spellLists
            .OrderBy(_ => _.Class)
            .ThenByDescending(_ => _.Level)
            .ThenByDescending(_ => _.Title);
        
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

        public IEnumerable<ClassSpell> LoadSpellsForSpellList(SpellList spellList)
        {
            throw new NotImplementedException();
        }
    }
}
