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
    public class ViewSpellListPageViewModel : SpellListViewModel
    {
        public ViewSpellListPageViewModel(IViewServices viewSvc, ISpellList spellList)
            : base(spellList)
        {
            EditSpellListCommand = new Command(() =>
                viewSvc.Navigation.PushAsync(new Views.EditSpellListPage(spellList)));

            var spells = Services.MockDataStore.Instance.LoadSpellsForSpellList(spellList);
            Spells = spells.Select(_ => new SpellListEntryViewModel(_)).ToList();
        }
        public ICommand EditSpellListCommand { get; private set; }

        public List<SpellListEntryViewModel> Spells { get; } = new List<SpellListEntryViewModel>();
    }
}
