﻿using System;
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
            //var s = new Stopwatch();
            //s.Start();
            //var grid = new Grid(50, 50);
            //var swd = new HuntAndKill();
            //swd.On(grid);
            //s.Stop();
            //Console.WriteLine(s.Elapsed);
            //Console.WriteLine($"Deadends {grid.DeadEnds().Count}");
            ////  var start = grid[grid.Rows / 2, grid.Columns / 2];

            ////   grid.Distances = start.CellDistances();
            //// Console.WriteLine(grid);
            //grid.SaveToPng();

            DeadEndCounts();
            Console.ReadLine();


        }

        private static void DeadEndCounts()
        {
            string Namespace = typeof(BinaryTree).Namespace;

            var algorithms = new[] {
            "BinaryTree",
            "Sidewinder",
            "AldousBroder",
            "Wilsons",
            "HuntAndKill"}
            ;

            const int tries = 100;
            const int size = 20;

            var averages = new Dictionary<string, int>();
            foreach (var algorithm in algorithms)
            {
                Console.WriteLine($"running {algorithm}");

                var deadEndCounts = new List<int>();
                for (int i = 0; i < tries; i++)
                {
                    var grid = new Grid(size, size);
                    dynamic alg = Activator.CreateInstance(Type.GetType(Namespace + "." + algorithm));
                    alg.On(grid);
                    deadEndCounts.Add(grid.DeadEnds().Count);
                }

                var totalDeadEnds = deadEndCounts.Sum();
                averages[algorithm] = totalDeadEnds / deadEndCounts.Count;
            }

            const int totalCells = size * size;

            Console.WriteLine();
            Console.WriteLine($"Average dead-ends per {size}x{size} maze ({totalCells} cells):");
            Console.WriteLine();

            foreach (var alg in averages.OrderByDescending(a => a.Value))
            {
                var percentage = alg.Value / (decimal)totalCells;

                Console.WriteLine($"{alg.Key,14} : {alg.Value,3}/{totalCells} ({percentage:p})");

            }

        }
    }
}
