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
            var grid = new ColouredGrid(25, 25);
            var swd = new Sidewinder();
            swd.On(grid);

            var start = grid[grid.Rows/2, grid.Columns/2];

            grid.Distances = start.CellDistances();
//            Console.WriteLine(grid);
grid.SaveToPng();

            Console.ReadLine();


        }
    }
}
