using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public class DistanceGrid : Grid
    {
        public Distances GridDistances { get; set; }
        public DistanceGrid(int rows, int columns) : base(rows, columns)
        {
        }

        protected override string CellContents(Cell cell)
        {
            if (GridDistances.ContainsKey(cell))
            {
                return Base36.Encode(GridDistances[cell]);
            }
            else
            {
                return base.CellContents(cell);
            }
        }
    }
}
