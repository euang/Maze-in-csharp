using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class MaskedGrid : OrthogonalGrid
    {
        private Mask _mask;

        public MaskedGrid(int rows, int columns) : base(rows, columns)
        {
        }

        public MaskedGrid(Mask mask) : base(mask.Rows, mask.Columns)
        {
            _mask = mask;
            PrepareGrid();
            ConfigureCells();
        }

        protected override void PrepareGrid()
        {
            _grid = new OrthogonalCell[Rows, Columns];

            for (int x = 0; x < Rows; x++)
            {
                for (int y = 0; y < Columns; y++)
                {
                    if (_mask[x, y])
                    {
                        _grid[x, y] = new OrthogonalCell(x, y);
                    }
                    else
                    {
                        _grid[x, y] = null;
                    }
                }
            }
        }
        public Cell RandomCell()
        {
            var location = _mask.RandomLocation();
            return this[location.X, location.Y];
        }

        public int Size => _mask.Count;
    }
}
