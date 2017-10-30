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
    public class EditSpellListPageViewModel : SpellListViewModel
    {
        public EditSpellListPageViewModel(IViewServices viewSvc, Model.SpellList spellList)
            : base(spellList)
        {
            _title = spellList.Title;
            _class = spellList.Class;
            _level = spellList.Level;

            PickSpellListCommand = new Command(() =>
            {
                spellList.Title = _title;
                spellList.Class = _class;
                spellList.Level = _level;
                Services.MockDataStore.Instance.Save(spellList);
                viewSvc.Navigation.PushAsync(new Views.PickSpellListPage(spellList));
            });

            DeleteSpellListCommand = new Command(async () =>
            {
                if (await viewSvc.DisplayAlert("Delete Spell List",
                    $"Are you sure you want to delete \"{Title}\"?", "Yes", "No"))
                {
                    Services.MockDataStore.Instance.Delete(SpellList);
                    await viewSvc.Navigation.PopToRootAsync();
                }
            });

            CloneSpellListCommand = new Command(() =>
                viewSvc.Navigation.PushAsync(new Views.EditSpellListPage(SpellList.Clone())));
        }

        public ICommand PickSpellListCommand { get; private set; }
        public ICommand DeleteSpellListCommand { get; private set; }
        public ICommand CloneSpellListCommand { get; private set; }

        public new string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }
        string _title = "My Spell List";

        public string Class
        {
            get { return _class.ToString(); }
            set { SetProperty(ref _class, (AmtgardClass)Enum.Parse(typeof(AmtgardClass), value)); }
        }
        AmtgardClass _class = AmtgardClass.Bard;
        public static string[] Classes { get; } = { "Bard", "Druid", "Healer", "Wizard" };

        public string Level
        {
            get { return $"Level {_level}"; }
            set
            {
                if (int.TryParse(value[0].ToString(), out var level) &&
                    level >= 1 && level <= 6)
                    SetProperty(ref _level, level);
            }
        }
        int _level = 1;
        public static string[] Levels { get; } = Enumerable.Range(1, 6).Select(_ => $"Level {_}").ToArray();
    }
}
