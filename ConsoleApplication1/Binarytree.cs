using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public class BinaryTree
    {
        private Random rnd = new Random();


        public OrthogonalGrid On(OrthogonalGrid grid)
        {

            foreach (OrthogonalCell cell in grid.Cells())
            {
                List<Cell> neighbours = new List<Cell>();
                if (cell.North != null)
                {
                    neighbours.Add(cell.North);
                }
                if (cell.East != null)
                {
                    neighbours.Add(cell.East);
                }

                if (neighbours.Count > 0)
                {
                    var index = rnd.Next(neighbours.Count);
                    var neighbour = neighbours[index];
                    cell.Link(neighbour);
                }
            }

            return grid;


        }

    }
}
