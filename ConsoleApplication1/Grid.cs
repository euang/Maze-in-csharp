using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;

namespace ConsoleApplication1
{
    public abstract class Grid<T> : IGrid<T> where T : Cell
    {
        public int Rows { get; }
        public int Columns { get; }

        public int Size => Rows * Columns;

        protected Random rnd = new Random();

        public Grid(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;

            PrepareGrid();
            ConfigureCells();
        }

        protected Grid()
        {
            throw new NotImplementedException();
        }

        protected abstract void ConfigureCells();

        protected abstract void PrepareGrid();


        public abstract T RandomCell();

        public abstract T[] Cells();

        public abstract T this[int row, int column] { get; set; } // Indexer declaration

        public abstract void SaveToPng();



        protected virtual string CellContents(Cell cell)
        {
            return " ";
        }

        protected virtual Color? BackgroundColorFor(Cell cell)
        {
            return null;
        }

        public List<T> DeadEnds()
        {
            return Cells().Where(cell => cell.Links.Count == 1).ToList();
        }

        public void braid(double p = 1.0)
        {

            var list = DeadEnds();
            Shuffle(list);
            foreach (var cell in list)
            {
                if (cell.Links.Count != 1 || rnd.NextDouble() > p)
                {
                    continue;
                }
                var neighbors = cell.Neighbours().Where(n => !n.IsLinked(cell)).ToList();
                var best = neighbors.FirstOrDefault(n => n.Links.Count == 1);

                if (best == null)
                {
                    int r = rnd.Next(neighbors.Count);
                    best = neighbors[r];
                }

                cell.Link(best);
            }


        }

        private void Shuffle(IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rnd.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }


    }
}
