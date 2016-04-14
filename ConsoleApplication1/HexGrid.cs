using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    internal class HexGrid : Grid<HexCell>
    {

        protected HexCell[,] _grid;

        public HexGrid(int rows, int columns) : base(rows, columns)
        {

        }
        protected override void ConfigureCells()
        {
            foreach (var cell in Cells())
            {
                int row = cell.Row;
                int col = cell.Column;


                int north_diagonal;
                int south_diagonal;

                if (IsEven(col))
                {
                    north_diagonal = row - 1;
                    south_diagonal = row;
                }
                else
                {
                    north_diagonal = row;
                    south_diagonal = row + 1;
                }

                cell.NorthWest = this[north_diagonal, col - 1];
                cell.NorthEast = this[north_diagonal, col + 1];
                cell.SouthWest = this[south_diagonal, col - 1];
                cell.SouthEast = this[south_diagonal, col + 1];
                cell.North = this[row - 1, col];
                cell.South = this[row + 1, col];
                cell.East = this[row, col + 1];

            }
        }

        protected override void PrepareGrid()
        {
            _grid = new HexCell[Rows, Columns];

            for (int x = 0; x < Rows; x++)
            {
                for (int y = 0; y < Columns; y++)
                {
                    _grid[x, y] = new HexCell(x, y);
                }
            }
        }

        public override HexCell RandomCell()
        {
            int row = rnd.Next(0, Rows);
            int column = rnd.Next(0, Columns);

            return _grid[row, column];
        }

        public override HexCell[] Cells()
        {
            return _grid.Cast<HexCell>().Where(c => c != null).ToArray();
        }

        public override HexCell this[int row, int column]
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

        protected virtual void to_png_v1(int cellSize = 10)
        {

            var a_size = cellSize / 2.0;
            var b_size = cellSize * Math.Sqrt(3) / 2.0;
            var width = cellSize * 2;
            var height = b_size * 2;


            int img_width = (int)(3 * a_size * Columns + a_size + 0.5);
            int img_height = (int)(height * Rows + b_size + 0.5);

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
                        var cx = cellSize + 3 * cell.Column * a_size;
                        var cy = b_size + cell.Row * height;
                        if (!IsEven(cell.Column))
                        {
                            cy += b_size;
                        }


                        // f/n = far/near
                        // n/s/e/w = north/south/east/west
                        int x_fw = (int)(cx - cellSize);
                        var x_nw = (int)(cx - a_size);
                        var x_ne = (int)(cx + a_size);
                        var x_fe = (int)(cx + cellSize);

                        // m = middle
                        var y_n = (int)(cy - b_size);
                        var y_m = (int)cy;
                        var y_s = (int)(cy + b_size);

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
