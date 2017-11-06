using Grimoire.Model;
using Grimoire.ViewModels;
using Syncfusion.Data;
using Syncfusion.ListView.XForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Grimoire.Views
{
	public class ViewSpellListPage : ContentPageWithViewServices
	{
		public ViewSpellListPage(ISpellList spellList)
		{
            Title = spellList.Title;
            BindingContext = new ViewSpellListPageViewModel(this, spellList);

            ToolbarItems.Add(new ToolbarItem { Text = "Edit" }
                .AndBind(_ => _.SetBinding(ToolbarItem.CommandProperty,
                nameof(ViewSpellListPageViewModel.EditSpellListCommand))));

            var listView = new SfListView
            {
                AutoFitMode = AutoFitMode.Height,
                SelectionMode = SelectionMode.None,
                ItemSpacing = new Thickness(5),
                IsStickyGroupHeader = true,
                GroupHeaderSize = 100,
                GroupHeaderTemplate = new DataTemplate(() => new ViewCell
                {
                    View = new Label
                    {
                        FontAttributes = FontAttributes.Bold,
                        TextColor = Color.Black,
                        BackgroundColor = Color.Green,
                        FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                        Margin = new Thickness(0)
                    }.AndBind(Label.TextProperty, "Key")
                }),
                ItemTemplate = new DataTemplate(() =>
                    new Grid
                    {
                        Margin = new Thickness(0),
                        Padding = new Thickness(0),
                        RowDefinitions =
                        {
                            new RowDefinition { Height = GridLength.Auto },
                            new RowDefinition { Height = GridLength.Auto },
                            new RowDefinition { Height = GridLength.Star }
                        },
                        ColumnDefinitions =
                        {
                            new ColumnDefinition { Width = GridLength.Star },
                            new ColumnDefinition { Width = GridLength.Auto },
                        }
                    }.AddCells(
                        new GridCell(0, 0, new Label
                        {
                            FontAttributes = FontAttributes.Bold,
                            TextColor = Color.Black,
                            FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                            Margin = new Thickness(0)
                        }.AndBind(Label.TextProperty, nameof(SpellListEntryViewModel.Name))),
                        new GridCell(1, 0, new Label
                        {
                            FontAttributes = FontAttributes.Bold,
                            TextColor = Color.Black,
                            FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                            Margin = new Thickness(0)
                        }.AndBind(Label.TextProperty, nameof(SpellListEntryViewModel.Uses)))))
            }.AndBind(SfListView.ItemsSourceProperty, nameof(PickSpellListPageViewModel.Spells));

            listView.DataSource.GroupDescriptors.Add(new Syncfusion.DataSource.GroupDescriptor()
            {
                PropertyName = "Level"
            });

            Content = new StackLayout {
                Children = {
                    new Label().AndBind(_ => _.SetBinding(Label.TextProperty, nameof(SpellListViewModel.Title))),
                    new Label().AndBind(_ => _.SetBinding(Label.TextProperty, nameof(SpellListViewModel.Subtitle))),
                    listView
                }
            };
		}
	}
}