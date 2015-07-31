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
            var grid = new Grid(4, 4);
            //var bt = new BinaryTree();
            //grid = bt.On(grid);
            //Console.Write(grid);

            var swd = new Sidewinder();
            swd.On(grid);
            Console.WriteLine(grid);
            for (int i = 0; i < 10; i++)
            {
                grid = new Grid(4, 4);
                grid = swd.On(grid);
                Console.WriteLine();
                Console.Write(grid);
            }

            //grid[0,2].Link(grid[0,2].East);
            //grid[0, 2].East.Link(grid[0, 2].East.South);

            //Console.WriteLine(grid);

            //for (int i = 0x2500; i <= 0x2570; i += 0x10)
            //{
            //    for (int c = 0; c <= 0xF; ++c)
            //    {
            //        Console.WriteLine((char)(i + c));
            //        Console.WriteLine(c);
            //    }

            //    Console.WriteLine();
            //}


            Console.ReadLine();
            grid.SaveToPng();
        }
    }
}
