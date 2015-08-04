using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public class Distances : Dictionary<Cell, int>
    {
        public Cell Root { get; set; }

        public Distances(Cell root)
        {

            Root = root;
            this.Add(root, 0);
        }

        public Cell[] Cells()
        {
            return this.Keys.ToArray();

        }

        public Distances PathTo(Cell goal)
        {
            var current = goal;

            var breadcrumbs = new Distances(Root);
            breadcrumbs.Add(current, this[current]);

            while (current != Root)
            {
                foreach (var neighbour in current.Links)
                {
                    if (this[neighbour] < this[current])
                    {
                        if (!breadcrumbs.ContainsKey(neighbour))
                        {
                            breadcrumbs.Add(neighbour, this[neighbour]);
                        }
                        else
                        {
                            breadcrumbs[neighbour] = this[neighbour];
                        }
                        current = neighbour;
                        break;
                    }
                }
            }

            return breadcrumbs;
        }

        public KeyValuePair<Cell, int> Max()
        {
            var maxDistance = 0;
            var maxCell = Root;

            foreach (KeyValuePair<Cell, int> entry in this)
            {
                if (entry.Value > maxDistance)
                {
                    maxCell = entry.Key;
                    maxDistance = entry.Value;
                }
            }

            return new KeyValuePair<Cell, int>(maxCell, maxDistance);

        }
    }
}
