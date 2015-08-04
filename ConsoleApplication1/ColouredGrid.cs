using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public class ColouredGrid : Grid
    {
        private Distances _distances;
        private int _maximum;
        public Distances Distances
        {
            get { return _distances; }
            set
            {
                _distances = value;
                _maximum = _distances.Max().Value;
            }
        }


        public ColouredGrid(int rows, int columns) : base(rows, columns)
        {
        }


        protected override Color? Background_color_for(Cell cell)
        {

            var distance = Distances[cell];
            decimal intensity = (_maximum - distance) /(decimal)_maximum;
            int dark = (int)Math.Round(255 * intensity);
            int bright = (int)(128 + Math.Round(127 * intensity));
            return Color.FromArgb(dark, bright, dark);
        }


    }
}
