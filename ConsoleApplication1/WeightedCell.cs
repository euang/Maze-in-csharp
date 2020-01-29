using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public class WeightedCell : Cell
    {
        private int Weight { get; set; }

        public WeightedCell(int row, int column) : base(row, column)
        {
            Weight = 1;
        }

        public override List<Cell> Neighbours()
        {
            throw new NotImplementedException();
        }

        //public override Distances CellDistances()
        //{
        //    var weights = new Distances(this);
        //    List<Cell> pending = new List<Cell>() { this };


        //    while (pending.Count > 0)
        //    {
        //        pending.Sort();
        //        var cell = pending[0];
        //        pending.RemoveAt(0);

        //        foreach (var neighbour in cell.Links)
        //        {
        //            var total_weight = weights[cell] + ((WeightedCell)neighbour).Weight;

        //            if (weights[neighbour] != null || total_weight < weights[neighbour])
        //            {
        //                pending.Add(neighbour);
        //                weights[neighbour] = total_weight;
        //            }
                    
        //        }
        //    }
        //}

    }
}
