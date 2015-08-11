using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Mask
    {
        Random rand = new Random();

        public int Rows { get; set; }
        public int Columns { get; set; }

        private bool[,] bits;
        public Mask(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;
            bits = new bool[rows, columns];
            for (int x = 0; x < Rows; x++)
            {
                for (int y = 0; y < Columns; y++)
                {
                    bits[x, y] = true;
                }
            }
        }

        public bool this[int row, int column] // Indexer declaration
        {
            get
            {
                // get and set accessors
                if (row < 0 || row > Rows - 1)
                {
                    return false;
                }
                if (column < 0 || column > Columns - 1)
                {
                    return false;
                }

                return bits[row, column];
            }
            set { bits[row, column] = value; }
        }

        public int Count
        {
            get
            {
                int result = 0;
                for (int x = 0; x < Rows; x++)
                {
                    for (int y = 0; y < Columns; y++)
                    {
                        if (bits[x, y])
                        {
                            result++;
                        }
                    }
                }
                return result;
            }
        }

        public Point RandomLocation()
        {
            do
            {
                int row = rand.Next(Rows);
                int col = rand.Next(Columns);
                if (bits[row, col])
                {
                    return new Point(row, col);
                }
            } while (true);

        }

        public static Mask FromText(string file)
        {
            string[] lines = File.ReadAllLines(file);
            int rows = lines.Length;
            int columns = lines[0].Length;

            var mask = new Mask(rows, columns);
            for (int x = 0; x < rows; x++)
            {
                for (int y = 0; y < columns; y++)
                {
                    if (lines[x][y] == 'X')
                    {
                        mask[x, y] = false;
                    }
                    else
                    {
                        mask[x, y] = true;
                    }
                }
            }
            return mask;

        }

        public static Mask FromImage(string file)
        {
            Bitmap image = new Bitmap(System.Drawing.Image.FromFile(file));

            var mask = new Mask(image.Height, image.Width);
            for (int x = 0; x < mask.Rows; x++)
            {
                for (int y = 0; y < mask.Columns; y++)
                {
                    if (image.GetPixel(y, x).ToArgb() == Color.Black.ToArgb())
                    {
                        mask[x, y] = false;
                    }
                    else
                    {
                        mask[x, y] = true;
                    }
                }
            }
            return mask;

        }
    }
}
