using System;
using System.Linq;

namespace ConsoleApplication1
{
    public class Grid
    {
        public int Rows;
        public int Columns;
        private Cell[,] grid;
        public void Initialise(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;

            ConfigureCells();
        }

        public void ConfigureCells()
        {

        }

        public void PrepareGrid()
        {
            grid = new Cell[Rows, Columns];

            for (int x = 0; x < Rows; x++)
            {
                for (int y = 0; y < Columns; y++)
                {
                    grid[x, y] = new Cell(x, y);
                }
            }
        }

        public override string ToString()
        {
            string output = "+" + String.Concat(Enumerable.Repeat("---+", Columns)) + "\n";

            // each_row do | row |
            string top = "|";
            string bottom = "+";

            for (int i = 0; i < grid.Rank; i++)
            {

            }
            row.each do | cell |
              cell = Cell.new(-1, -1) unless cell

       string body = "   ";// <-- that's THREE (3) spaces!
            east_boundary = (cell.linked ? (cell.east) ? " " : "|")
        top << body << east_boundary

        // three spaces below, too >>-------------->> >...<
            string south_boundary = (cell.linked ? (cell.south) ? "   " : "---");
       string corner = "+";
            bottom += south_boundary + corner;

            output += top + "\n";
            output += bottom + "\n";


            return output;
        }
    }
}