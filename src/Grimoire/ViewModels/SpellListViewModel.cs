using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grimoire.ViewModels
{
    public class SpellListViewModel : ObservableObject
    {
        public string Title => SpellList?.Title ?? "Untitled";
        public string Subtitle => SpellList != null ? $"{SpellList.Class} L{SpellList.Level}" : "";

        public Model.SpellList SpellList { get; private set; }

        public SpellListViewModel(Model.SpellList spellList)
        {
            SpellList = spellList;
        }
    }
}
