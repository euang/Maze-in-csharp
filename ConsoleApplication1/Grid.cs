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


            for (int x = 0; x < grid.Rank; x++)
            {
                string top = "|";
                string bottom = "+";

                for (int y = 0; y < grid.GetLength(x); y++)
                {
                    Cell cell;
                    if(grid[x,y] == null){
                        grid[x, y] = new Cell(-1, -1);
                    }
                    cell = grid[x, y];
                    string body = "   ";// <-- that's THREE (3) spaces!  

                    string east_boundary = (cell.Linked ? (cell.east) ? " " : "|")
        top << body << east_boundary
                }
            }
           


          

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