using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Sidewinder
    {
        Random rand = new Random();

        public Grid On(Grid grid)
        {
            for (int x = 0; x < grid.Rows; x++)
            {
                List<Cell> run = new List<Cell>();

                for (int y = 0; y < grid.Columns; y++)
                {
                    var cell = grid[x, y];
                    run.Add(cell);


                    bool at_eastern_boundary = (cell.East == null);
                    bool at_northern_boundary = (cell.North == null);

                    bool should_close_out = at_eastern_boundary || (!at_northern_boundary && rand.Next(2) == 0);

                    if (should_close_out)
                    {
                        var member = run[rand.Next(run.Count)];
                        run.Remove(member);
                        if (member.North != null)
                        {
                            member.Link(member.North);
                        }
                        run.Clear();
                    }
                    else
                    {
                        cell.Link(cell.East);

                    }
                }
            }

            return grid;


        }
    }
}
