using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    internal class PolarGrid : Grid<PolarCell>
    {
        public PolarGrid(int rows, int columns) : base(rows, columns)
        {
        }

        public PolarGrid(int rows) : base(rows, 1)
        {
            ConfigureCells();
        }

        private PolarCell[][] _rows;

        protected override void PrepareGrid()
        {
            _rows = new PolarCell[Rows][];


            var row_height = 1.0 / Rows;
            _rows[0] = new[] { new PolarCell(0, 0) };

            for (int i = 1; i < Rows; i++)
            {
                var radius = i / (double)Rows;
                var circumference = 2 * Math.PI * radius;
                var previous_count = _rows[i - 1].Length;

                var estimated_cell_width = circumference / previous_count;
                int ratio = (int)Math.Round(estimated_cell_width / row_height);
                int cells = previous_count * ratio;
                _rows[i] = new PolarCell[cells];
                for (int j = 0; j < cells; j++)
                {
                    _rows[i][j] = new PolarCell(i, j);
                }
            }
        }

        public override PolarCell[] Cells()
        {
            return _rows.SelectMany(cells => cells).ToArray();
        }

        public override PolarCell this[int row, int column] // Indexer declaration
        {
            get
            {
                // get and set accessors
                if (row < 0 || row > Rows - 1)
                {
                    return null;
                }

                //   return _rows[row][column % _rows[row].Length];
                //if (column < 0 || column > _rows[row].Length - 1)
                //{
                //    return null;
                //}

                var mod = column - _rows[row].Length * Math.Floor((decimal)column / _rows[row].Length);

                return _rows[row][(int)mod];
            }
            set { }
        }


        protected override void ConfigureCells()
        {
            foreach (var cell in Cells())
            {
                if (cell.Row > 0)
                {
                    cell.Cw = this[cell.Row, cell.Column + 1];
                    cell.Ccw = this[cell.Row, cell.Column - 1];

                    var ratio = _rows[cell.Row].Length / _rows[cell.Row - 1].Length;
                    var parent = _rows[cell.Row - 1][cell.Column / ratio];
                    parent.Outward.Add(cell);
                    cell.Inward = parent;
                }
            }
        }


        public override PolarCell RandomCell()
        {
            var row = rnd.Next(Rows);
            var col = rnd.Next(_rows[row].Length);
            return _rows[row][col];
        }


        public override void SaveToPng()
        {
            to_png_v1();
        }


        protected void to_png_v1(int cellSize = 10)
        {
            int img_size = 2 * cellSize * Rows;

            Color background = Color.White;
            Color wall = Color.Black;
            // Create pen.
            Pen wallPen = new Pen(wall, 1);

            using (Bitmap img = new Bitmap(img_size + 1, img_size + 1))
            {
                using (Graphics g = Graphics.FromImage(img))
                {
                    g.Clear(background);

                    var center = img_size / 2;

                    foreach (var cell in Cells())
                    {
                        var outer_radius = (cell.Row + 1) * cellSize;

                        if (cell.Row == 0)
                        {
                            continue;
                        }

                        var theta = 2 * System.Math.PI / _rows[cell.Row].Length;
                        var inner_radius = cell.Row * cellSize;
                        var theta_ccw = cell.Column * theta;
                        var theta_cw = (cell.Column + 1) * theta;

                        int ax = (int)(center + (inner_radius * Math.Cos(theta_ccw)));
                        int ay = (int)(center + (inner_radius * Math.Sin(theta_ccw)));
                        int bx = (int)(center + (outer_radius * Math.Cos(theta_ccw)));
                        int by = (int)(center + (outer_radius * Math.Sin(theta_ccw)));
                        int cx = (int)(center + (inner_radius * Math.Cos(theta_cw)));
                        int cy = (int)(center + (inner_radius * Math.Sin(theta_cw)));
                        int dx = (int)(center + (outer_radius * Math.Cos(theta_cw)));
                        int dy = (int)(center + (outer_radius * Math.Sin(theta_cw)));

                        if (!cell.IsLinked(cell.Inward))
                        {
                            g.DrawArc(wallPen, center - inner_radius, center - inner_radius, inner_radius * 2, inner_radius * 2, (int)ConvertRadiansToDegrees(theta_ccw), (int)ConvertRadiansToDegrees(theta));
                        }

                        if (!cell.IsLinked(cell.Cw))
                        {
                            g.DrawLine(wallPen, cx, cy, dx, dy);
                        }

                    }
                    g.DrawEllipse(wallPen, 0, 0, img_size, img_size);
                    img.Save(@"C:\code\maze.png", ImageFormat.Png);
                }

            }
        }

        private static double ConvertRadiansToDegrees(double radians)
        {
            double degrees = (180 / Math.PI) * radians;
            return (degrees);
        }
    }
}
