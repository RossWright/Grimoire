using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Grimoire.ViewModels
{
    public class MainPageViewModel
    {
        public MainPageViewModel(IViewServices viewSvc)
        {
            dataStore = Services.MockDataStore.Instance;

            SpellLists = new ObservableCollection<SpellListViewModel>();

            ViewSpellListCommand = new Command(vm =>
                viewSvc.Navigation.PushAsync(new Views.ViewSpellListPage(((SpellListViewModel)vm).SpellList)));

            AddNewSpellListCommand = new Command(() =>
                viewSvc.Navigation.PushAsync(new Views.EditSpellListPage(new Model.SpellList())));

            DeleteSpellListCommand = new Command(vm =>
            {
                viewSvc.DisplayAlert("Delete Spell List",
                    $"Are you sure you want to delete \"{((SpellListViewModel)vm).Title}\"?", "Yes", "No")
                    .ContinueWith(_ =>
                    {
                        if (_.Result)
                        {
                            dataStore.Delete(((SpellListViewModel)vm).SpellList);
                            SpellLists.Remove(((SpellListViewModel)vm));
                        }
                    });
            });

            CloneSpellListCommand = new Command(vm =>
                viewSvc.Navigation.PushAsync(new Views.EditSpellListPage(((SpellListViewModel)vm).SpellList.Clone())));
        }
        Services.IDataStore dataStore;

        public void LoadSpellLists()
        {
            SpellLists.Clear();
            foreach (var spellList in dataStore.Load())
                SpellLists.Add(new SpellListViewModel(spellList));
        }
        public ObservableCollection<SpellListViewModel> SpellLists { get; set; }

        public ICommand ViewSpellListCommand { get; private set; }
        public ICommand AddNewSpellListCommand { get; private set; }
        public ICommand DeleteSpellListCommand { get; private set; }
        public ICommand CloneSpellListCommand { get; private set; }
    }


}
