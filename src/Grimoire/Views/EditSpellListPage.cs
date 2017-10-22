using Grimoire.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Grimoire.Views
{
	public class EditSpellListPage : ContentPageWithViewServices
	{
        public EditSpellListPage(Model.SpellList spellList)
		{
            Title = spellList.Title;
            BindingContext = new EditSpellListPageViewModel(this, spellList);

            ToolbarItems.Add(new ToolbarItem { Text = "Next" }
                .AndBind(_ => _.SetBinding(ToolbarItem.CommandProperty, 
                nameof(EditSpellListPageViewModel.PickSpellListCommand))));

            Content = new StackLayout
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                Children =
                {
                    new Grid
                    {
                        RowDefinitions =
                        {
                            new RowDefinition {Height = GridLength.Star},
                            new RowDefinition {Height = GridLength.Auto},
                        } 
                    }
                    .AddCells(
                        (0, 0, new StackLayout
                        {
                            Spacing = 20,
                            Padding = 15,
                            VerticalOptions = LayoutOptions.FillAndExpand,
                            Children =
                            {
                                new Label {Text = "Title", FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)) },
                                new Entry {FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Entry))}
                                    .AndBind(Entry.TextProperty, nameof(EditSpellListPageViewModel.Title)),
                                new Label {Text = "Class", FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)) },
                                new Picker { ItemsSource = EditSpellListPageViewModel.Classes }
                                    .AndBind(Picker.SelectedItemProperty, nameof(EditSpellListPageViewModel.Class)),
                                new Label {Text = "Level", FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)) },
                                new Picker { ItemsSource = EditSpellListPageViewModel.Levels }
                                    .AndBind(Picker.SelectedItemProperty, nameof(EditSpellListPageViewModel.Level)),
                            }
                        }),
                        (0, 1, new StackLayout
                        {
                            Children =
                            {
                                MakeButton("Clone", Color.Blue, nameof(EditSpellListPageViewModel.CloneSpellListCommand)),
                                MakeButton("Delete", Color.Red, nameof(EditSpellListPageViewModel.DeleteSpellListCommand)),
                            }
                        })
                    )
                }
            };
        }

        Grid MakeButton(string caption, Color color, string commandName) =>
            new Grid
            {
                BackgroundColor = color,
                HorizontalOptions = LayoutOptions.Fill,
                HeightRequest = 50,
                GestureRecognizers =
                {
                    new TapGestureRecognizer()
                        .AndBind(_ => _.SetBinding(TapGestureRecognizer.CommandProperty, 
                            new Binding(commandName, source: BindingContext)))
                },
                Children =
                {
                    new Grid
                    {
                        VerticalOptions = LayoutOptions.Center,
                        HorizontalOptions = LayoutOptions.Center,
                        Children =
                        {
                            new Label
                            {
                                Text = caption,
                                TextColor = Color.White
                            }
                        }
                    }
                }
            };

    }
}