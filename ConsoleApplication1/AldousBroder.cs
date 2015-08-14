using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public class AldousBroder
    {
        private Random rand = new Random();

        public Grid<Cell> On(Grid<Cell> grid)
        {
            var cell = grid.RandomCell();
            var unvisited = grid.Size - 1;

            while (unvisited > 0)
            {
                var neighbour = cell.Neighbours()[rand.Next(cell.Neighbours().Count)];
                if (neighbour.Links.Count == 0)
                {
                    cell.Link(neighbour);
                    unvisited--;
                }

                cell = neighbour;
            }

            return grid;
        }

    }
}
