using System.Collections.Generic;

namespace TetrisClone
{
    public class LandedBlocks : TetrisGrid
    {
        public LandedBlocks(int rowCount, int columnCount)
            : base(rowCount, columnCount)
        {
            ClearCells();
        }

        public void AddBlock(Block block)
        {

            for (int row = 0; row < block.RowCount; row++)
            {
                for (int col = 0; col < block.ColumnCount; col++)
                {
                    if (block.IsFilled(row, col))
                    {
                        this.FillCell(block.RowPosition + row, block.ColPosition + col);
                    }
                }
            }
        }

        public List<int> GetFullRows()
        {
            List<int> rows = new List<int>();

            for (int row = 0; row < base.RowCount; row++)
            {
                bool hasTetris = true;
                for (int col = 0; col < base.ColumnCount; col++)
                {
                    if (this.IsFilled(row, col))
                        continue;

                    hasTetris = false;
                    break;
                }

                if (hasTetris)
                {
                    rows.Add(row);
                }

            }

            return rows;

        }

        private bool IsEmptyRow(int row)
        {
            for (int col = 0; col < base.ColumnCount; col++)
            {
                if (this.IsFilled(row, col))
                    return false;

                
            }
            return true;
        }

        public void ClearRow(int row)
        {
            for (int col = 0; col < CellStateArray[row].Length; col++)
            {
               // byte cell = CellStateArray[row][col];
                ClearCell(row, col);
            }

            PushRowsDown(row);
        }

        private void PushRowsDown(int deletedRow)
        {
            for (int row = deletedRow; row > 0; row--)
            {
                //if (IsEmptyRow(row))
                CellStateArray[row] = CellStateArray[row - 1];
            }
        }

        private void ClearCells()
        {
            for (int row = 0; row < base.RowCount; row++)
                for (int col = 0; col < base.ColumnCount; col++)
                    ClearCell(row, col);
        }
    }
}