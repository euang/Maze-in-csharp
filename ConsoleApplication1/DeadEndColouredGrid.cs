using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public class DeadEndColouredGrid : Grid
    {


        public DeadEndColouredGrid(int rows, int columns) : base(rows, columns)
        {
        }


        protected override Color? BackgroundColorFor(Cell cell)
        {

            if (cell.Links.Count == 1)
            {
                return Color.DarkSlateBlue;
            }

            if (cell.Links.Count == 2)
            {
                return Color.Gray;
            }
            if (cell.Links.Count == 3)
            {
                return Color.LightGray;
            }

            return base.BackgroundColorFor(cell);



        }


    }
}
