using System.Collections.Generic;
namespace ConsoleApplication1
{

    public class Cell
    {
        public int Row { get; private set; }
        public int Column { get; private set; }

        public object North { get; }
        public object South { get; }
        public object East { get; }
        public object West { get; }

        public HashSet<Cell> Links;

        public Cell(int row, int column)
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

        public bool Linked(Cell cell)
        {
            return Links.Contains(cell);
        }


    }
}