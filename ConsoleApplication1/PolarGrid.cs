using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class PolarGrid : Grid
    {
        public PolarGrid(int rows, int columns) : base(rows, columns)
        {
        }

        protected override void to_png_v1(int cellSize = 10)
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
                        var theta = 2 * System.Math.PI / _grid.GetLength(1);
                        var inner_radius = cell.Row * cellSize;
                        var outer_radius = (cell.Row + 1) * cellSize;
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

                        if (!cell.IsLinked(cell.East))
                        {
                            g.DrawLine(wallPen, cx, cy, dx, dy);
                        }

                        if (!cell.IsLinked(cell.South))
                        {
                            g.DrawLine(wallPen, ax, ay, cx, cy);
                        }

                    }
                    g.DrawEllipse(wallPen,0,0,img_size,img_size);
                    img.Save(@"C:\code\maze.png", ImageFormat.Png);
                }

            }
        }

        public static double ConvertRadiansToDegrees(double radians)
        {
            double degrees = (180 / Math.PI) * radians;
            return (degrees);
        }
    }
}
