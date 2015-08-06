using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public class Wilsons
    {
        Random rand = new Random();

        public Grid On(Grid grid)
        {
            var unvisited = new List<Cell>();

            unvisited.AddRange(grid.Cells());

            var first = unvisited[rand.Next(unvisited.Count)];
            unvisited.Remove(first);

            Debug.Print($"{first.Row},{first.Column}");
            while (unvisited.Count > 0)
            {
                var cell = unvisited[rand.Next(unvisited.Count)];
                Debug.Print($"cell:{cell.Row},{cell.Column}");

                var path = new List<Cell>() { cell };
                while (unvisited.Contains(cell))
                {
                    cell = cell.Neighbours()[rand.Next(cell.Neighbours().Count)];
                    Debug.Print($"neighbour cell:{cell.Row},{cell.Column}");

                    var position = path.IndexOf(cell);
                    if (position > -1)
                    {
                        //add one to position as zero based.
                        path = path.GetRange(0, position + 1);
                    }
                    else
                    {
                        path.Add(cell);
                    }
                }
                for (int i = 0; i <= path.Count - 2; i++)
                {
                    path[i].Link(path[i + 1]);
                    unvisited.Remove(path[i]);
                }
            }
            return grid;
        }

    }
}
