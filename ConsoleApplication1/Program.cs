using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            var grid = new DistanceGrid(5, 5);
            var swd = new Sidewinder();
            swd.On(grid);

            var start = grid[0, 0];

            var cellDistances = start.CellDistances();
            var max = cellDistances.Max();
            var new_start = max.Key;

            var new_distances = new_start.CellDistances();
            var goal = new_distances.Max().Key;

            grid.GridDistances = new_distances.PathTo(goal);
            Console.WriteLine(grid);


            Console.ReadLine();


        }
    }
}
