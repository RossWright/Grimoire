using Grimoire.Model;
using Grimoire.ViewModels;
using Syncfusion.ListView.XForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Grimoire.Views
{
    public class PickSpellListPage : ContentPageWithViewServices
    {
        public PickSpellListPage(ISpellList spellList)
        {
            Title = $"{spellList.Title} (L{spellList.Level})";

            var vm = new PickSpellListPageViewModel(this, spellList);
            BindingContext = vm;

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
                    View = new StackLayout
                    {
                        BackgroundColor = Color.Gray,
                        Orientation = StackOrientation.Horizontal,
                        Children =
                        {
                            new Label
                            {
                                FontAttributes = FontAttributes.Bold,
                                TextColor = Color.Black,
                                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                                Margin = new Thickness(0)
                            }.AndBind(Label.TextProperty, "Key.LevelText"),
                            new Label
                            {
                                FontAttributes = FontAttributes.Bold,
                                TextColor = Color.Black,
                                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                                Margin = new Thickness(0)
                            }.AndBind(Label.TextProperty, "Key.PointsRemaining")
                        }
                    }
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
                }.AndBind(Grid.IsVisibleProperty, nameof(SpellListEntryViewModel.IsVisible))
                .AddCells(
                    new GridCell(0, 0, new StackLayout
                    {
                        Orientation = StackOrientation.Horizontal,
                        Children =
                        {
                            new Label
                            {
                                Margin = new Thickness(0),
                                FontAttributes = FontAttributes.Bold,
                                VerticalTextAlignment = TextAlignment.Center,
                                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                                TextColor = Color.Black,
                            }.AndBind(Label.TextProperty, nameof(SpellListEntryViewModel.Name)),
                            new Label
                            {
                                Margin = new Thickness(10, 0, 0, 0),
                                VerticalTextAlignment = TextAlignment.Center,
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
                                Margin = new Thickness(30,0,0,0),
                                FontAttributes = FontAttributes.Bold,
                                VerticalTextAlignment = TextAlignment.Center,
                                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                                TextColor = Color.Black,
                                Text = "C:"
                            },

                            new Label
                            {
                                Margin = new Thickness(0),
                                VerticalTextAlignment = TextAlignment.Center,
                                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                                TextColor = Color.Black,
                            }.AndBind(Label.TextProperty, nameof(SpellListEntryViewModel.Cost)),

                            new Label
                            {
                                Margin = new Thickness(5,0,0,0),
                                FontAttributes = FontAttributes.Bold,
                                VerticalTextAlignment = TextAlignment.Center,
                                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                                TextColor = Color.Black,
                                Text = "M:"
                            },

                            new Label
                            {
                                VerticalTextAlignment = TextAlignment.Center,
                                Margin = new Thickness(0),
                                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                                TextColor = Color.Black,
                            }.AndBind(Label.TextProperty, nameof(SpellListEntryViewModel.MaxText)),

                            new Label
                            {
                                Margin = new Thickness(5,0,0,0),
                                FontAttributes = FontAttributes.Bold,
                                VerticalTextAlignment = TextAlignment.Center,
                                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                                TextColor = Color.Black,
                                Text = "P:"
                            },

                            new Label
                            {
                                Margin = new Thickness(0),
                                VerticalTextAlignment = TextAlignment.Center,
                                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                                TextColor = Color.Black,
                            }.AndBind(Label.TextProperty, nameof(SpellListEntryViewModel.Bought)),
                        }
                    }),

                    new GridCell(1, 0, 1, 2, new StackLayout
                    {
                        Orientation = StackOrientation.Horizontal,
                        Children =
                        {
                            new Button
                            {
                                Text = "-",
                                WidthRequest = 50
                            }
                            .AndBind(Button.IsVisibleProperty, nameof(SpellListEntryViewModel.CanSell))
                            .AndBind(Button.CommandProperty, nameof(PickSpellListPageViewModel.SellSpellCommand), BindingContext)
                            .AndBind(Button.CommandParameterProperty, "."),

                            new Button
                            {
                                Text = "+",
                                WidthRequest = 50
                            }
                            .AndBind(Button.IsVisibleProperty, nameof(SpellListEntryViewModel.CanBuy))
                            .AndBind(Button.CommandProperty, nameof(PickSpellListPageViewModel.BuySpellCommand), BindingContext)
                            .AndBind(Button.CommandParameterProperty, ".")
                        }
                    })
                ))
            }.AndBind(SfListView.ItemsSourceProperty, nameof(PickSpellListPageViewModel.Spells));

            list.DataSource.GroupDescriptors.Add(new Syncfusion.DataSource.GroupDescriptor()
            {
                PropertyName = "Level",
                KeySelector = _ => vm.GroupHeaders[((SpellListEntryViewModel)_).Level-1]
            });

            Content = new StackLayout { Children = { list } };
        }
    }
}