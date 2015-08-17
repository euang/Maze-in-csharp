using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;

namespace ConsoleApplication1
{
    public abstract class Grid<T>:IGrid<T> where T : Cell
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
    }
}
