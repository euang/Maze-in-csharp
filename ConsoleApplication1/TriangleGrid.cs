using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    internal class TriangleGrid : Grid<TriangleCell>
    {

        protected TriangleCell[,] _grid;
        private int apex_y;
        private int base_y;

        public TriangleGrid(int rows, int columns) : base(rows, columns)
        {

        }
        protected override void ConfigureCells()
        {
            foreach (var cell in Cells())
            {
                int row = cell.Row;
                int col = cell.Column;

                if (cell.Upright)
                {
                    cell.South = this[row + 1, col];
                }
                else
                {
                    cell.North = this[row - 1, col];
                }

                cell.North = this[row - 1, col];
                cell.South = this[row + 1, col];
                cell.East = this[row, col + 1];
                cell.West = this[row, col - 1];
            }
        }

        protected override void PrepareGrid()
        {
            _grid = new TriangleCell[Rows, Columns];

            for (int x = 0; x < Rows; x++)
            {
                for (int y = 0; y < Columns; y++)
                {
                    _grid[x, y] = new TriangleCell(x, y);
                }
            }
        }

        public override TriangleCell RandomCell()
        {
            int row = rnd.Next(0, Rows);
            int column = rnd.Next(0, Columns);

            return _grid[row, column];
        }

        public override TriangleCell[] Cells()
        {
            return _grid.Cast<TriangleCell>().Where(c => c != null).ToArray();
        }

        public override TriangleCell this[int row, int column]
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

        public override void SaveToPng()
        {
            to_png_v1();
        }

        private bool IsEven(int number)
        {
            return number % 2 == 0;
        }

        protected virtual void to_png_v1(int cellSize = 16)
        {
            var halfWidth = cellSize / 2;
            var height = cellSize * Math.Sqrt(3) / 2.0;
            var width = cellSize * 2;
            var halfHeight = height / 2;


            int img_width = (int)(cellSize * (Columns + 1) / 2.0);
            int img_height = (int)(height * Rows);

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
                        var cx = halfWidth + cell.Column * halfWidth;
                        var cy = halfHeight + cell.Row * height;

                        // f/n = far/near
                        // n/s/e/w = north/south/east/west
                        int x_w = (int)(cx - halfWidth);
                        var x_m = (int)(cx);
                        var x_e = (int)(cx + halfWidth);

                        if (cell.Upright)
                        {
                            apex_y = (int)(cy - halfHeight);
                            base_y = (int)(cy + halfHeight);
                        }
                        else
                        {
                            apex_y = (int)(cy + halfHeight);
                            base_y = (int)(cy - halfHeight);
                        }

                        var noSouth = cell.Upright && cell.South == null;
                        var not_linked = !cell.Upright && !cell.Links(cell.North);

                        if (noSouth || not_linked)
                        {
                            g.DrawLine(wallPen, x_e, base_y, x_w, base_y);
                        }
                        if (cell.North == null)
                        {
                            g.DrawLine(wallPen, x_nw, y_n, x_ne, y_n);
                        }

                        if (cell.NorthWest == null)
                        {
                            g.DrawLine(wallPen, x_fw, y_m, x_nw, y_n);
                        }

                        if (cell.SouthWest == null)
                        {
                            g.DrawLine(wallPen, x_fw, y_m, x_nw, y_s);
                        }
                        if (!cell.IsLinked(cell.NorthEast))
                        {
                            g.DrawLine(wallPen, x_ne, y_n, x_fe, y_m);
                        }

                        if (!cell.IsLinked(cell.SouthEast))
                        {
                            g.DrawLine(wallPen, x_fe, y_m, x_ne, y_s);
                        }

                        if (!cell.IsLinked(cell.South))
                        {
                            g.DrawLine(wallPen, x_ne, y_s, x_nw, y_s);
                        }

                    }
                    img.Save(@"C:\code\maze.png", ImageFormat.Png);
                }

            }

        }

    }
}
