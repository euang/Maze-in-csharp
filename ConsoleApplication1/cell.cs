using System.Collections.Generic;
namespace ConsoleApplication1
{

    public abstract class Cell
    {
        public int Row { get; private set; }
        public int Column { get; private set; }


        public readonly HashSet<Cell> Links;

        protected Cell(int row, int column)
        {
            Row = row;
            Column = column;

            Links = new HashSet<Cell>();
        }

        public void Link(Cell cell, bool bidi = true)
        {
            Links.Add(cell);
            if (bidi)
            {
                cell.Link(this, false);
            }
        }

        public void Unlink(Cell cell, bool bidi = true)
        {
            Links.Remove(cell);

            if (bidi)
            {
                cell.Unlink(this, false);
            }

        }

        public bool IsLinked(Cell cell)
        {
            return Links.Contains(cell);
        }

        public abstract List<Cell> Neighbours();

        public Distances CellDistances()
        {
            var distances = new Distances(this);
            var frontier = new List<Cell>() { this };

            while (frontier.Count > 0)
            {
                var newFrontier = new List<Cell>();
                foreach (var cell in frontier)
                {
                    foreach (var linked in cell.Links)
                    {
                        if (!distances.ContainsKey(linked))
                        {
                            distances.Add(linked, distances[cell] + 1);
                            newFrontier.Add(linked);
                        }
                    }
                }
                frontier = newFrontier;
            }
            return distances;
        }
    }
}