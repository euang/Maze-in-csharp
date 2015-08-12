using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;

namespace ConsoleApplication1
{
    public abstract class Grid
    {
        public int Rows { get; }
        public int Columns { get; }
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


        public abstract Cell RandomCell();

        public abstract Cell[] Cells();

        public abstract void SaveToPng();



        protected virtual string CellContents(Cell cell)
        {
            return " ";
        }

        protected virtual Color? BackgroundColorFor(Cell cell)
        {
            return null;
        }

        public List<Cell> DeadEnds()
        {
            return Cells().Where(cell => cell.Links.Count == 1).ToList();
        }
    }
}
