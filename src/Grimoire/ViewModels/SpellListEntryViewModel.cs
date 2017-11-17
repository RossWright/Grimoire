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
        public SpellListEntryViewModel(IClassSpell spell)
        {
            _spell = spell;
            if (spell.Cost == 0) _bought = 1;
        }
        private IClassSpell _spell;

        public string Name => _spell.Name;
        public SpellSchool? School => _spell.School;
        public SpellType? Type => _spell.Type;
        public SpellRange? Range => _spell.Range;
        public string Incant => _spell.Incant;
        public int IncantTimes => _spell.IncantTimes;
        public string Materials => _spell.Materials;
        public string Effects => _spell.Effects;
        public string Limits => _spell.Limits;
        public string Notes => _spell.Notes;

        public int Level => _spell.Level;
        public int Cost => _spell.Cost;
        public int Max => _spell.Max;
        public string MaxText => _spell.Max == 99 ? "-" : _spell.Max.ToString();

        public string Uses 
        { 
            get
            {
                if (_spell.UsesPer == null) return string.Empty;
                if (_spell.UsesPer == UsesPer.Unlimited) return "Unlimited";
                if (_spell.ChargeTimes > 0) return $"{_spell.Uses * _bought}/{_spell.UsesPer} (Charge x{_spell.ChargeTimes})";
                return $"{_spell.Uses * _bought}/{_spell.UsesPer}";
            }
        }

        public bool IsTooExpensive
        {
            get { return _isTooExpensive; }
            set { SetProperty(ref _isTooExpensive, value); }
        }
        private bool _isTooExpensive = false;

        public bool IsVisible => _bought > 0 || !IsTooExpensive;
        public bool HasBought =>  _bought > 0;
        public bool CanBuy => Cost > 0 && _bought < Max && !IsTooExpensive;
        public bool CanSell => Cost > 0 && _bought > 0;
        public int Bought
        {
            get { return _bought; }
            set
            {
                SetProperty(ref _bought, value);
                OnPropertyChanged(nameof(CanBuy));
                OnPropertyChanged(nameof(CanSell));
                OnPropertyChanged(nameof(HasBought));
                OnPropertyChanged(nameof(Uses));
            }
        }
        int _bought = 0;
    }
}
