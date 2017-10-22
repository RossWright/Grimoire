using Grimoire.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Grimoire.Views
{
	public class ViewSpellListPage : ContentPageWithViewServices
	{
		public ViewSpellListPage(Model.SpellList spellList)
		{
            Title = spellList.Title;
            BindingContext = new ViewSpellListPageViewModel(this, spellList);

            ToolbarItems.Add(new ToolbarItem { Text = "Edit" }
                .AndBind(_ => _.SetBinding(ToolbarItem.CommandProperty,
                nameof(ViewSpellListPageViewModel.EditSpellListCommand))));

            Content = new StackLayout {
				Children = {
					new Label().AndBind(_ => _.SetBinding(Label.TextProperty, nameof(SpellListViewModel.Title))),
                    new Label().AndBind(_ => _.SetBinding(Label.TextProperty, nameof(SpellListViewModel.Subtitle))),
                }
			};
		}
	}
}