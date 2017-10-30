using System.Collections.Generic;
using Grimoire.Model;

namespace Grimoire.Services
{
    public interface IDataStore
    {
        IEnumerable<SpellList> Load();
        void Save(SpellList spellList);
        void Delete(SpellList spellList);

        IEnumerable<ClassSpell> LoadSpellsForSpellList(SpellList spellList);
    }
}