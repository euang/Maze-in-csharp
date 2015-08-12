using System;
using System.Collections.Generic;

namespace ConsoleApplication1
{
    public class OrthogonalCell : Cell
    {
        public OrthogonalCell North { get; set; }
        public OrthogonalCell South { get; set; }
        public OrthogonalCell East { get; set; }
        public OrthogonalCell West { get; set; }


        public OrthogonalCell(int row, int column) : base(row, column)
        {
        }

        public override List<Cell> Neighbours()
        {

            var list = new List<Cell>();

            if (North != null)
            {
                list.Add(North);
            }

            if (South != null)
            {
                list.Add(South);
            }

            if (East != null)
            {
                list.Add(East);
            }

            if (West != null)
            {
                list.Add(West);
            }

            return list;

        }
    }
}
