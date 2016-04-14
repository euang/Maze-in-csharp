using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public class HexCell : Cell
    {
        public Cell NorthWest { get; set; }
        public Cell NorthEast { get; set; }

        public Cell SouthEast { get; set; }

        public Cell SouthWest { get; set; }

        public Cell North { get; set; }
        public Cell South { get; set; }

        public Cell East { get; set; }

        public HexCell(int row, int column) : base(row, column)
        {
        }

        public override List<Cell> Neighbours()
        {
            var list = new List<Cell>();

            if (NorthWest != null)
            {
                list.Add(NorthWest);
            }
            if (North != null)
            {
                list.Add(North);
            }

            if (NorthEast != null)
            {
                list.Add(NorthEast);
            }
            if (South != null)
            {
                list.Add(South);
            }

            if (SouthEast != null)
            {
                list.Add(SouthEast);
            }

            if (SouthWest != null)
            {
                list.Add(SouthWest);
            }

            return list;
        }
    }
}
