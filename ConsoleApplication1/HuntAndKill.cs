using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    internal class HuntAndKill
    {
        Random rand = new Random();

        public Grid<Cell> On(Grid<Cell> grid)
        {
            var current = grid.RandomCell();

            while (current != null)
            {
                var unvisitedNeighbours = current.Neighbours().Where(n => n.Links.Count == 0).ToList();
                if (unvisitedNeighbours.Any())
                {
                    var neighbour = unvisitedNeighbours[rand.Next(unvisitedNeighbours.Count())];
                    current.Link(neighbour);
                    current = neighbour;
                }
                else
                {
                    current = null;

                    foreach (var cell in grid.Cells())
                    {
                        var visitedNeighbours = cell.Neighbours().Where(n => n.Links.Any()).ToList();
                        if (cell.Links.Count == 0 && visitedNeighbours.Any())
                        {
                            current = cell;
                            var neighbour = visitedNeighbours[rand.Next(visitedNeighbours.Count())];
                            current.Link(neighbour);
                            break;
                        }

                    }
                }

            }

            return grid;
        }
    }
}
