using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grimoire.Model;

namespace Grimoire.ViewModels
{
    public class SpellListEntryViewModel : ObservableObject
    {
        public int Level => _spell.Level;
        public string Name => _spell.Name;
        public int Cost => _spell.Cost;
        public int Max => _spell.Max;


        public int Bought
        {
            get { return _bought; }
            set
            {
                SetProperty(ref _bought, value);
                OnPropertyChanged(nameof(CanBuy));
                OnPropertyChanged(nameof(CanSell));
            }
        }
        int _bought = 0;


        public SpellListEntryViewModel(Spell spell)
        {
            _spell = spell;
            if (spell.Cost == 0) _bought = 1;
        }
        private Spell _spell;

        public bool CanBuy => Cost > 0 && _bought < Max;
        public bool CanSell => Cost > 0 && _bought > 0;
    }
}
