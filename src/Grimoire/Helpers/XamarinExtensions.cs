using System;
using Xamarin.Forms;

public static class XamarinExtensions
{
    public static T AndBind<T>(this T obj, Action<BindableObject> _) where T : BindableObject
    {
        _(obj);
        return obj;
    }
    public static T AndBind<T>(this T obj, BindableProperty prop, string path) where T : BindableObject
    {
        obj.SetBinding(prop, path);
        return obj;
    }
    public static T AndBind<T>(this T obj, BindableProperty prop, string path, object source) where T : BindableObject
    {
        obj.SetBinding(prop, new Binding(path, source: source));
        return obj;
    }

    public static Grid AddCells(this Grid grid, params (int column, int row, View view)[] cells)
    {
        foreach(var cell in cells)
            grid.Children.Add(cell.view, cell.column, cell.row);
        return grid;
    }
}