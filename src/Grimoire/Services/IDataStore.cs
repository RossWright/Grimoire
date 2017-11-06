using System.Collections.Generic;
using Grimoire.Model;

namespace Grimoire.Services
{
    public interface IDataStore
    {
        IEnumerable<ISpellList> Load();
        void Save(ISpellList spellList);
        void Delete(ISpellList spellList);

        IEnumerable<IClassSpell> LoadSpellsForSpellList(ISpellList spellList);
        void SaveSpellsForSpellList(ISpellList spellList, IEnumerable<IClassSpell> classSpells);
    }
}