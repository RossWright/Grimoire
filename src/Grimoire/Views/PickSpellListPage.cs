using Grimoire.ViewModels;
using Syncfusion.ListView.XForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Grimoire.Views
{
    public class PickSpellListPage : ContentPageWithViewServices
    {
        public PickSpellListPage(Model.SpellList spellList)
        {
            Title = spellList.Title;
            BindingContext = new PickSpellListPageViewModel(this, spellList);

            ToolbarItems.Add(new ToolbarItem { Text = "Save" }
                .AndBind(_ => _.SetBinding(ToolbarItem.CommandProperty,
                nameof(PickSpellListPageViewModel.SaveSpellListCommand))));

            Content = new StackLayout
            {
                Children = {
                    new Label().AndBind(_ => _.SetBinding(Label.TextProperty, nameof(SpellListViewModel.Title))),
                    new Label().AndBind(_ => _.SetBinding(Label.TextProperty, nameof(SpellListViewModel.Subtitle))),

                    new SfListView
                    {
                        AutoFitMode = AutoFitMode.Height,
                        SelectionMode = SelectionMode.None,
                        ItemSpacing = new Thickness(5),
                        ItemTemplate = new DataTemplate(() =>
                        new Grid
                        {
                            Margin = new Thickness(0),
                            Padding = new Thickness(0),
                            RowDefinitions =
                            {
                                new RowDefinition { Height = GridLength.Star }
                            },
                            ColumnDefinitions =
                            {
                                new ColumnDefinition { Width = GridLength.Star },
                                new ColumnDefinition { Width = GridLength.Auto },
                                new ColumnDefinition { Width = GridLength.Auto },
                                new ColumnDefinition { Width = GridLength.Auto },
                            }
                        }.AddCells(
                            (0, 0, new Label
                            {
                                FontAttributes = FontAttributes.Bold,
                                TextColor = Color.Black,
                                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                                Margin = new Thickness(0)
                            }.AndBind(Label.TextProperty, nameof(SpellListEntryViewModel.Name))),
                            (1, 0, new Label
                            {
                                Margin = new Thickness(0)
                            }.AndBind(Label.TextProperty, nameof(SpellListEntryViewModel.Bought))),
                            (2, 0, new Button
                            {
                                Text = "-",
                                WidthRequest = 50
                            }
                            .AndBind(Button.IsVisibleProperty, nameof(SpellListEntryViewModel.CanSell))
                            .AndBind(Button.CommandProperty, nameof(PickSpellListPageViewModel.SellSpellCommand), BindingContext)
                            .AndBind(Button.CommandParameterProperty, ".")),
                            
                            (3, 0, new Button
                            {
                                Text = "+",
                                WidthRequest = 50
                            }
                            .AndBind(Button.IsVisibleProperty, nameof(SpellListEntryViewModel.CanBuy))
                            .AndBind(Button.CommandProperty, nameof(PickSpellListPageViewModel.BuySpellCommand), BindingContext)
                            .AndBind(Button.CommandParameterProperty, "."))
                        ))
                    }.AndBind(SfListView.ItemsSourceProperty, nameof(PickSpellListPageViewModel.Spells))
                }
            };
        }
    }
}