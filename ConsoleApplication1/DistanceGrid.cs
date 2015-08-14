using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public class DistanceGrid : OrthogonalGrid
    {
        public Distances Distances { get; set; }
        public DistanceGrid(int rows, int columns) : base(rows, columns)
        {
        }

        protected override string CellContents(Cell cell)
        {
            if (Distances.ContainsKey(cell))
            {
                return Base36.Encode(Distances[cell]);
            }
            else
            {
                return base.CellContents(cell);
            }
        }
    }
}
