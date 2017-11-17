using Grimoire.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Grimoire.ViewModels
{
    public class PickSpellListPageViewModel : SpellListViewModel
    {
        public PickSpellListPageViewModel(IViewServices viewSvc, ISpellList spellList)
            : base(spellList)
        {
            SaveSpellListCommand = new Command(() =>
            {
                Services.MockDataStore.Instance.Save(spellList);
                viewSvc.Navigation.PopToRootAsync(false);
                viewSvc.Navigation.PushAsync(new Views.ViewSpellListPage(spellList));
            });

            BuySpellCommand = new Command(obj =>
            {
                var vm = (SpellListEntryViewModel)obj;
                vm.Bought = Math.Min(vm.Max, vm.Bought + 1);
            });

            SellSpellCommand = new Command(obj =>
            {
                var vm = (SpellListEntryViewModel)obj;
                vm.Bought = Math.Max(0, vm.Bought - 1);
            });

            Spells = Services.MockDataStore.Instance.LoadSpellsForSpellList(spellList)
                .Select(_ => new SpellListEntryViewModel(_))
                .OrderByDescending(_ => _.Level)
                .ThenBy(_ => _.Name)
                .ToList();

            foreach (var spells in Spells)
                spells.PropertyChanged += Spells_PropertyChanged;

            GroupHeaders = new PickSpellGroupHeaderViewModel[]
            {
                new PickSpellGroupHeaderViewModel(1),
                new PickSpellGroupHeaderViewModel(2),
                new PickSpellGroupHeaderViewModel(3),
                new PickSpellGroupHeaderViewModel(4),
                new PickSpellGroupHeaderViewModel(5),
                new PickSpellGroupHeaderViewModel(6)
            };
        }

        private void Spells_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            int points = 6;
            for (var level = SpellList.Level; level > 0; level--)
            {
                var levelSpells = Spells.Where(_ => _.Level == level).ToArray();
                foreach (var spellEntry in levelSpells)
                    points -= spellEntry.Bought * spellEntry.Cost;
                GroupHeaders[level - 1].PointsRemaining = points;
                foreach (var spellEntry in levelSpells)
                    spellEntry.IsTooExpensive = spellEntry.Cost > points;
                points += 5;
            }
        }

        public List<SpellListEntryViewModel> Spells { get; } = new List<SpellListEntryViewModel>();
        public PickSpellGroupHeaderViewModel[] GroupHeaders { get; private set; }

        public ICommand BuySpellCommand { get; private set; }
        public ICommand SellSpellCommand { get; private set; }
        public ICommand SaveSpellListCommand { get; private set; }

        public class PickSpellGroupHeaderViewModel : ObservableObject
        {
            public PickSpellGroupHeaderViewModel(int level) { _level = level; }

            public string LevelText => $"Level {_level}";
            private readonly int _level;

            public int PointsRemaining
            {
                get { return _pointsRemaining; }
                set { SetProperty(ref _pointsRemaining, value); }
            }
            int _pointsRemaining = 5;
        }
    }
}
