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
            Console.ReadLine();
        }
    }
}
