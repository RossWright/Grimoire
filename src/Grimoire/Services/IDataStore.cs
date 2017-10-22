using System.Collections.Generic;
using Grimoire.Model;

namespace Grimoire.Services
{
    public interface IDataStore
    {
        void Delete(SpellList spellList);
        IEnumerable<SpellList> Load();
        void Save(SpellList spellList);
        IEnumerable<Spell> LoadSpells(SpellList spellList);
    }
}