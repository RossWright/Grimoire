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
        public PickSpellListPageViewModel(IViewServices viewSvc, Model.SpellList spellList)
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

            Spells = Services.MockDataStore.Instance.LoadSpells(spellList)
                .Select(_ => new SpellListEntryViewModel(_))
                .ToList();
        }
        public ICommand SaveSpellListCommand { get; private set; }

        public List<SpellListEntryViewModel> Spells { get; } = new List<SpellListEntryViewModel>();
        public ICommand BuySpellCommand { get; private set; }
        public ICommand SellSpellCommand { get; private set; }
    }
}
