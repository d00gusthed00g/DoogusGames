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

        public void ClearRow(PlayField landedField)
        {

        }


        private void ClearCells()
        {
            for (int row = 0; row < base.RowCount; row++)
                for (int col = 0; col < base.ColumnCount; col++)
                    ClearCell(row, col);
        }
    }
}