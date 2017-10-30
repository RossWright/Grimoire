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

    public static Grid AddCells(this Grid grid, params GridCell[] cells)
    {
        foreach (var cell in cells)
            grid.Children.Add(cell.View, cell.Column, cell.Column + cell.ColumnSpan, cell.Row, cell.Row + cell.RowSpan);
        return grid;
    }
}

public class GridCell
{
    public GridCell(int column, int row, View view)
    {
        Column = column;
        Row = row;
        View = view;
    }

    public GridCell(int column, int row, int columnSpan, int rowSpan, View view)
    {
        Column = column;
        Row = row;
        ColumnSpan = columnSpan;
        RowSpan = rowSpan;
        View = view;
    }

    public int Column { get; set; }
    public int Row { get; set; }
    public int ColumnSpan { get; set; } = 1;
    public int RowSpan { get; set; } = 1;
    public View View { get; set; }
}
