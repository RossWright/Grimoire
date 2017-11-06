using Grimoire.Model;
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
        public PickSpellListPage(ISpellList spellList)
        {
            Title = spellList.Title;
            BindingContext = new PickSpellListPageViewModel(this, spellList);

            ToolbarItems.Add(new ToolbarItem { Text = "Save" }
                .AndBind(_ => _.SetBinding(ToolbarItem.CommandProperty,
                nameof(PickSpellListPageViewModel.SaveSpellListCommand))));

            var list = new SfListView
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
                        BackgroundColor = Color.Gray,
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
                        new RowDefinition { Height = GridLength.Star },
                        new RowDefinition { Height = GridLength.Auto }
                    },
                    ColumnDefinitions =
                    {
                        new ColumnDefinition { Width = GridLength.Star },
                        new ColumnDefinition { Width = GridLength.Auto },
                        new ColumnDefinition { Width = GridLength.Auto },
                    }
                }.AddCells(
                    new GridCell(0, 0, new StackLayout
                    {
                        Orientation = StackOrientation.Horizontal,
                        Children =
                        {
                            new Label
                            {
                                Margin = new Thickness(0),
                                FontAttributes = FontAttributes.Bold,
                                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                                TextColor = Color.Black,
                            }.AndBind(Label.TextProperty, nameof(SpellListEntryViewModel.Name)),
                            new Label
                            {
                                Margin = new Thickness(10, 0, 0, 0),
                                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                                TextColor = Color.Black,
                            }.AndBind(Label.TextProperty, nameof(SpellListEntryViewModel.Uses))
                        }
                    }),

                    new GridCell(0, 1, new StackLayout
                    {
                        Orientation = StackOrientation.Horizontal,
                        Children =
                        {
                            new Label
                            {
                                Margin = new Thickness(0),
                                FontAttributes = FontAttributes.Bold,
                                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                                TextColor = Color.Black,
                            }.AndBind(Label.TextProperty, nameof(SpellListEntryViewModel.Name)),

                            new Label
                            {
                                Margin = new Thickness(30,0,0,0),
                                FontAttributes = FontAttributes.Bold,
                                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                                TextColor = Color.Black,
                                Text = "Cost:"
                            },

                            new Label
                            {
                                Margin = new Thickness(0),
                                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                                TextColor = Color.Black,
                            }.AndBind(Label.TextProperty, nameof(SpellListEntryViewModel.Cost)),

                            new Label
                            {
                                Margin = new Thickness(10,0,0,0),
                                FontAttributes = FontAttributes.Bold,
                                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                                TextColor = Color.Black,
                                Text = "Max:"
                            },

                            new Label
                            {
                                Margin = new Thickness(0),
                                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                                TextColor = Color.Black,
                            }.AndBind(Label.TextProperty, nameof(SpellListEntryViewModel.Max)),

                            new Label
                            {
                                Margin = new Thickness(10,0,0,0),
                                FontAttributes = FontAttributes.Bold,
                                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                                TextColor = Color.Black,
                                Text = "Purchased:"
                            },

                            new Label
                            {
                                Margin = new Thickness(0),
                                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                                TextColor = Color.Black,
                            }.AndBind(Label.TextProperty, nameof(SpellListEntryViewModel.Bought)),
                        }
                    }),
                    
                    new GridCell(1, 0, 1, 2, new Button
                    {
                        Text = "-",
                        WidthRequest = 50
                    }
                    .AndBind(Button.IsVisibleProperty, nameof(SpellListEntryViewModel.CanSell))
                    .AndBind(Button.CommandProperty, nameof(PickSpellListPageViewModel.SellSpellCommand), BindingContext)
                    .AndBind(Button.CommandParameterProperty, ".")),
                    
                    new GridCell(2, 0, 1, 2, new Button
                    {
                        Text = "+",
                        WidthRequest = 50
                    }
                    .AndBind(Button.IsVisibleProperty, nameof(SpellListEntryViewModel.CanBuy))
                    .AndBind(Button.CommandProperty, nameof(PickSpellListPageViewModel.BuySpellCommand), BindingContext)
                    .AndBind(Button.CommandParameterProperty, "."))
                ))
            }.AndBind(SfListView.ItemsSourceProperty, nameof(PickSpellListPageViewModel.Spells));

            list.DataSource.GroupDescriptors.Add(new Syncfusion.DataSource.GroupDescriptor()
            {
                PropertyName = "Level"
            });

            Content = new StackLayout
            {
                Children = {
                    new Label().AndBind(_ => _.SetBinding(Label.TextProperty, nameof(SpellListViewModel.Title))),
                    new Label().AndBind(_ => _.SetBinding(Label.TextProperty, nameof(SpellListViewModel.Subtitle))),
                    list
                }
            };
        }
    }
}