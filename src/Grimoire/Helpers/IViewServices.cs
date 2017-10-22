using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

public interface IViewServices
{
    INavigation Navigation { get; }
    Task<bool> DisplayAlert(string title, string message, string accept, string cancel);
    Task DisplayAlert(string title, string message, string cancel);
}

public class ContentPageWithViewServices : ContentPage, IViewServices
{

}
