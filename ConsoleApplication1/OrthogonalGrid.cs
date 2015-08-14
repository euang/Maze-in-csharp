using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public class OrthogonalGrid : Grid<OrthogonalCell>
    {
        protected OrthogonalCell[,] _grid;

        protected override void ConfigureCells()
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

        protected override void PrepareGrid()
        {
            _grid = new OrthogonalCell[Rows, Columns];

            for (int x = 0; x < Rows; x++)
            {
                for (int y = 0; y < Columns; y++)
                {
                    _grid[x, y] = new OrthogonalCell(x, y);
                }
            }
        }

        public override OrthogonalCell RandomCell()
        {
            int row = rnd.Next(0, Rows);
            int column = rnd.Next(0, Columns);

            return _grid[row, column];
        }

        public OrthogonalGrid(int rows, int columns) : base(rows, columns)
        {

        }

        public override OrthogonalCell this[int row, int column] // Indexer declaration
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

                return _grid[row, column];
            }
            set
            {
                throw new NotImplementedException();

            }
        }

        public override OrthogonalCell[] Cells()
        {
            return _grid.Cast<OrthogonalCell>().Where(c => c != null).ToArray();
        }


        public override string ToString()
        {
            return UnicodeRepresentation();
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
                    if (_grid[x, y] == null)
                    {
                        _grid[x, y] = new OrthogonalCell(-1, -1);
                    }
                    var cell = _grid[x, y];
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
                var cell = _grid[0, y];
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
                OrthogonalCell cell;

                string top = '\u2502'.ToString();

                cell = _grid[x, 0];
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
                    if (_grid[x, y] == null)
                    {
                        _grid[x, y] = new OrthogonalCell(-1, -1);
                    }
                    cell = _grid[x, y];
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

        private void DrawCells(int cell_size, bool backgroundMode, Graphics g, Pen wallPen)
        {
            foreach (OrthogonalCell cell in Cells())
            {
                int x1 = cell.Column * cell_size;
                int y1 = cell.Row * cell_size;
                int x2 = (cell.Column + 1) * cell_size;
                int y2 = (cell.Row + 1) * cell_size;

                if (backgroundMode)
                {
                    var color = BackgroundColorFor(cell);
                    if (color.HasValue)
                    {
                        g.FillRectangle(new SolidBrush(color.Value), x1, y1, cell_size, cell_size);
                    }
                }
                else
                {
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
            }
        }

        public override void SaveToPng()
        {

            to_png_v1(20);
        }


        protected virtual void to_png_v1(int cellSize = 10)
        {
            int img_width = cellSize * Columns;
            int img_height = cellSize * Rows;

            Color background = Color.White;
            Color wall = Color.Black;
            // Create pen.
            Pen wallPen = new Pen(wall, 1);

            using (Bitmap img = new Bitmap(img_width + 1, img_height + 1))
            {
                using (Graphics g = Graphics.FromImage(img))
                {
                    g.Clear(background);

                    foreach (OrthogonalCell cell in Cells())
                    {
                        int x1 = cell.Column * cellSize;
                        int y1 = cell.Row * cellSize;
                        int x2 = (cell.Column + 1) * cellSize;
                        int y2 = (cell.Row + 1) * cellSize;

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

        private void to_png_v2(int cellSize = 10)
        {
            var img_width = cellSize * Columns;
            var img_height = cellSize * Rows;

            Color background = Color.White;
            Color wall = Color.Black;
            // Create pen.
            Pen wallPen = new Pen(wall, 1);

            using (Bitmap img = new Bitmap(img_width + 1, img_height + 1))
            {
                using (Graphics g = Graphics.FromImage(img))
                {
                    g.Clear(background);


                    DrawCells(cellSize, true, g, wallPen);
                    DrawCells(cellSize, false, g, wallPen);
                    img.Save(@"C:\code\maze.png", ImageFormat.Png);
                }
            }
        }

    }
}
