using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class PolarCell : Cell
    {

        public Cell Cw { get; set; }
        public Cell Ccw { get; set; }
        public Cell Inward { get; set; }

        public List<Cell> Outward { get; set; }

        public PolarCell(int row, int column) : base(row, column)
        {
            Outward = new List<Cell>();
        }

        public override List<Cell> Neighbours()
        {
            var list = new List<Cell>();

            if (Cw != null)
            {
                list.Add(Cw);
            }

            if (Ccw != null)
            {
                list.Add(Ccw);
            }

            if (Inward != null)
            {
                list.Add(Inward);
            }


            list.AddRange(Outward);


            return list;
        }


    }
}
