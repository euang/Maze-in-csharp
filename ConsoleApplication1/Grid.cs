using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;

namespace ConsoleApplication1
{
    public class Grid
    {
        public int Rows { get; }
        public int Columns { get; }
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
            return UnicodeRepresentation();
        }

        public Cell this[int row, int column] // Indexer declaration
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

        private string AsciiRepresentation()
        {
            string output = "+" + String.Concat(Enumerable.Repeat("---+", Columns)) + "\n";


            for (int x = 0; x < Rows; x++)
            {
                string top = "|";
                string bottom = "+";

                for (int y = 0; y < Columns; y++)
                {
                    if (grid[x, y] == null)
                    {
                        grid[x, y] = new Cell(-1, -1);
                    }
                    var cell = grid[x, y];
                    string body = $" {CellContents(cell)} ";

                    string eastBoundary;
                    if (cell.IsLinked(cell.East))
                    {
                        eastBoundary = " ";
                    }
                    else
                    {
                        eastBoundary = "|";
                    }
                    top += body + eastBoundary;


                    //// three spaces below, too >>-------------->> >...<
                    string southBoundary;
                    if (cell.IsLinked(cell.South))
                    {
                        southBoundary = "   ";
                    }
                    else
                    {
                        southBoundary = "---";
                    }

                    string corner = "+";
                    bottom += southBoundary + corner;
                }
                output += top + "\n";
                output += bottom + "\n";

            }

            return output;
        }

        private string UnicodeRepresentation()
        {

            string output = "\u250C";

            //do top row
            for (int y = 0; y < Columns; y++)
            {
                Cell cell = grid[0, y];
                if (cell.East == null)
                {
                    output += "\u2500\u2500\u2500\u2510";
                }
                else
                {
                    if (cell.IsLinked(cell.East))
                    {
                        output += "\u2500\u2500\u2500\u2500";
                    }
                    else
                    {
                        output += "\u2500\u2500\u2500\u252C";
                    }
                }
            }
            output += "\n";


            for (int x = 0; x < Rows; x++)
            {
                Cell cell;

                string top = '\u2502'.ToString();

                cell = grid[x, 0];
                string row;
                if (cell.IsLinked(cell.South))
                {
                    row = "\u2502"; //│
                }
                else
                {
                    if (cell.South == null)
                    {
                        row = "\u2514"; //└
                    }
                    else
                    {
                        row = "\u251C"; //├
                    }
                }

                for (int y = 0; y < Columns; y++)
                {
                    if (grid[x, y] == null)
                    {
                        grid[x, y] = new Cell(-1, -1);
                    }
                    cell = grid[x, y];
                    string body = $" {CellContents(cell)} ";

                    string eastBoundary;
                    if (cell.IsLinked(cell.East))
                    {
                        eastBoundary = " ";
                    }
                    else
                    {
                        eastBoundary = "\u2502"; //│
                    }
                    top += body + eastBoundary;


                    //// three spaces below, too >>-------------->> >...<
                    string southBoundary;
                    if (cell.IsLinked(cell.South))
                    {
                        southBoundary = "   ";
                    }
                    else
                    {
                        southBoundary = "\u2500\u2500\u2500";
                    }


                    bool up, down = true, left, right;

                    if (cell.South == null)
                    {
                        left = true;
                        down = false;
                    }
                    else
                    {
                        left = !cell.IsLinked(cell.South);
                    }

                    if (cell.East == null)
                    {
                        up = true;
                        right = false;
                    }
                    else
                    {
                        up = !cell.IsLinked(cell.East);
                        if (cell.East.South == null)
                        {
                            right = true;
                            down = false;
                        }
                        else
                        {
                            right = !cell.East.IsLinked(cell.East.South);
                            down = !cell.South.IsLinked(cell.East.South);
                        }
                    }
                    string corner = " ";
                    if (left & right & up & down)
                    {
                        corner = "\u253C"; //┼
                    }

                    if (left & right & up & !down)
                    {
                        corner = "\u2534"; //┴
                    }

                    if (left & right & !up & down)
                    {
                        corner = "\u252C"; //┬
                    }

                    if (left & right & !up & !down)
                    {
                        corner = "\u2500"; //─
                    }

                    if (!left & right & up & down)
                    {
                        corner = "\u251C"; //├
                    }

                    if (!left & right & up & !down)
                    {
                        corner = "\u2514"; //└
                    }

                    if (!left & right & !up & down)
                    {
                        corner = "\u250C"; //└
                    }

                    if (left & !right & up & down)
                    {
                        corner = "\u2524"; //┤
                    }

                    if (!left & !right & up & down)
                    {
                        corner = "\u2502"; //│ 
                    }

                    if (left & !right & up & !down)
                    {
                        corner = "\u2518"; //┘
                    }

                    if (left & right & !up & !down)
                    {
                        corner = "\u2500";
                    }

                    if (left & !right & !up & !down)
                    {
                        corner = "\u2500";
                    }

                    if (left & !right & !up & down)
                    {
                        corner = "\u2510";
                    }

                    row += southBoundary + corner;
                }
                output += top + "\n";
                output += row + "\n";

            }

            return output;
        }

        public void SaveToPng()
        {
            to_png_v1(20);
        }

        private void to_png_v1(int cell_size = 10)
        {
            int img_width = cell_size * Columns;
            int img_height = cell_size * Rows;

            Color background = Color.White;
            Color wall = Color.Black;
            // Create pen.
            Pen wallPen = new Pen(wall, 1);

            using (Bitmap img = new Bitmap(img_width + 1, img_height + 1))
            {
                using (Graphics g = Graphics.FromImage(img))
                {
                    g.Clear(background);

                    foreach (var cell in Cells())
                    {
                        int x1 = cell.Column * cell_size;
                        int y1 = cell.Row * cell_size;
                        int x2 = (cell.Column + 1) * cell_size;
                        int y2 = (cell.Row + 1) * cell_size;

                        if (cell.North == null)
                        {
                            g.DrawLine(wallPen, x1, y1, x2, y1);
                        }
                        if (cell.West == null)
                        {
                            g.DrawLine(wallPen, x1, y1, x1, y2);
                        }
                        if (!cell.IsLinked(cell.East))
                        {
                            g.DrawLine(wallPen, x2, y1, x2, y2);
                        }

                        if (!cell.IsLinked(cell.South))
                        {
                            g.DrawLine(wallPen, x1, y2, x2, y2);
                        }

                    }
                    img.Save(@"C:\code\maze.png", ImageFormat.Png);
                }

            }

        }

        protected virtual string CellContents(Cell cell)
        {
            return " ";
        }
    }
}