using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    internal class RecursiveBackTracker
    {
        private Random rand = new Random();

        public IGrid<Cell> On(IGrid<Cell> grid, Cell startAt = null)
        {
            if (startAt == null)
            {
                startAt = grid.RandomCell();
            }

            var stack = new Stack<Cell>();
            stack.Push(startAt);

            while (stack.Count != 0)
            {
                var current = stack.Peek();
                var neighbours = current.Neighbours().Where(n => n.Links.Count == 0).ToList();
                if (neighbours.Count == 0)
                {
                    stack.Pop();
                }
                else
                {
                    var neighbour = neighbours[rand.Next(neighbours.Count)];
                    current.Link(neighbour);
                    stack.Push(neighbour);
                }


            }

            return grid;
        }
    }
}
