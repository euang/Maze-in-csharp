using System;
using System.Collections.Generic;
using ConsoleApplication1;

public class TriangleCell : OrthogonalCell
{
    public TriangleCell(int row, int column) : base(row, column)
    {
    }

    public bool Upright
    {
        get { return ((Row + Column) % 2 != 0); }
    }

    public override List<Cell> Neighbours()
    {
        var list = new List<Cell>();

        if (West != null)
        {
            list.Add(West);
        }

        if (East != null)
        {
            list.Add(East);
        }

        if (North != null && !Upright)
        {
            list.Add(North);
        }

        if (South != null && Upright)
        {
            list.Add(South);
        }

        return list;
    }
}