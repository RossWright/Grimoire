using Grimoire.ViewModels;
using Syncfusion.ListView.XForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Grimoire.Views
{
    public class MainPage : ContentPageWithViewServices
    {
        public MainPage()
        {
            Title = "Amtgard Grimoire";
            BindingContext = viewModel = new MainPageViewModel(this);

            ToolbarItems.Add(new ToolbarItem { Text = "New" }
                .AndBind(_ => _.SetBinding(ToolbarItem.CommandProperty, nameof(MainPageViewModel.AddNewSpellListCommand))));

            Content = new SfListView
            {
                AllowSwiping = true,
                SwipeOffset = 150,
                AutoFitMode = AutoFitMode.Height,
                ItemSpacing = new Thickness(5),
                Behaviors =
                {
                    new EventToCommandBehavior
                    {
                        EventName = nameof(SfListView.SelectionChanged),
                        Command = ((MainPageViewModel)BindingContext).ViewSpellListCommand,
                        Converter = new ItemSelectionChangedEventArgsToSelectedItemConverter()
                    }
                },
                ItemTemplate = new DataTemplate(() =>
                    new StackLayout
                    {
                        Margin = new Thickness(0),
                        Padding = new Thickness(0),
                        Spacing = 0,
                        Children =
                        {
                            new Label
                            {
                                FontAttributes = FontAttributes.Bold,
                                TextColor = Color.Black,
                                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                                Margin = new Thickness(0)
                            }.AndBind(_ => _.SetBinding(Label.TextProperty, nameof(SpellListViewModel.Title))),
                            new Label
                            {
                                TextColor = Color.DarkGray,
                                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                                Margin = new Thickness(0)
                            }.AndBind(_ => _.SetBinding(Label.TextProperty, nameof(SpellListViewModel.Subtitle)))
                        }
                    }),
                LeftSwipeTemplate = new DataTemplate(() =>
                    new StackLayout
                    {
                        Orientation = StackOrientation.Horizontal,
                        Spacing = 0,
                        Children =
                        {
                            SlideButton("CLONE", Color.Blue, nameof(MainPageViewModel.CloneSpellListCommand)),
                            SlideButton("DELETE", Color.Red, nameof(MainPageViewModel.DeleteSpellListCommand)),
                        }
                    })
            }.AndBind(
                SfListView.ItemsSourceProperty,
                nameof(MainPageViewModel.SpellLists));
        }

        Grid SlideButton(string caption, Color color, string commandName) =>
            new Grid
            {
                BackgroundColor = color,
                VerticalOptions = LayoutOptions.Fill,
                WidthRequest = 75,
                GestureRecognizers =
                {
                    new TapGestureRecognizer()
                        .AndBind(TapGestureRecognizer.CommandProperty, commandName, BindingContext)
                        .AndBind(TapGestureRecognizer.CommandParameterProperty, ".")
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

        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModel.LoadSpellLists();
        }
        MainPageViewModel viewModel;
    }
}