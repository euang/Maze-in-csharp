using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApplication1
{
    public class Grid
    {
        public int Rows;
        public int Columns;
        private Cell[,] grid;
        public Grid(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;

            PrepareGrid();
            ConfigureCells();
        }

        public void ConfigureCells()
        {
            foreach (var cell in Cells())
            {

                int row = cell.Row;
                int col = cell.Column;


                cell.North = this[row - 1, col];
                cell.South = this[row + 1, col];
                cell.West = this[row, col - 1];
                cell.East = this[row, col + 1];

            }
        }

        public Cell[] Cells()
        {
            return grid.Cast<Cell>().ToArray();
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


            for (int x = 0; x < Rows; x++)
            {
                string top = "|";
                string bottom = "+";

                for (int y = 0; y < Columns; y++)
                {
                    Cell cell;
                    if (grid[x, y] == null)
                    {
                        grid[x, y] = new Cell(-1, -1);
                    }
                    cell = grid[x, y];
                    string body = "   ";// <-- that's THREE (3) spaces!  

                    string east_boundary;
                    if (cell.IsLinked(cell.East))
                    {
                        east_boundary = " ";
                    }
                    else
                    {
                        east_boundary = "|";
                    }
                    top += body + east_boundary;


                    //// three spaces below, too >>-------------->> >...<
                    string south_boundary;
                    if (cell.IsLinked(cell.South))
                    {
                        south_boundary = "   ";
                    }
                    else
                    {
                        south_boundary = "---";
                    }

                    string corner = "+";
                    bottom += south_boundary + corner;
                }
                output += top + "\n";
                output += bottom + "\n";

            }

            return output;
        }

        public Cell this[int row, int column]    // Indexer declaration
        {
            get
            {
                // get and set accessors
                if (row < 0 || row > Rows - 1)
                {
                    return null;
                }
                if (column < 0 || column > Columns - 1)
                {
                    return null;
                }

                return grid[row, column];
            }
        }

        public Cell RandomCell()
        {
            var rnd = new Random();
            int row = rnd.Next(0, Rows);
            int column = rnd.Next(0, Columns);

            return grid[row, column];
        }

        public int Size()
        {
            return Rows * Columns;
        }
    }
}