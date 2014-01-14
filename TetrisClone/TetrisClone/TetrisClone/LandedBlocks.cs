namespace TetrisClone
{
    public class LandedBlocks : TetrisGrid
    {
        private const int RowCount = 25;
        private const int ColumnCount = 10;

        public LandedBlocks()
            : base(RowCount, ColumnCount)
        {
            ClearCells();
        }

        public void AddBlock(Block block)
        {

            for (int row = 0; row < block.Rows; row++)
            {
                for (int col = 0; col < block.Columns; col++)
                {
                    if (block.IsFilled(row, col))
                    {
                        this.FillCell(block.RowPosition + row, block.ColPosition + col);
                    }
                    else
                    {
                        this.ClearCell(block.RowPosition + row, block.ColPosition + col);
                    }
                }
            }
        }

        public void ClearRow(PlayField landedField)
        {
            //ClearCells();

            //for (int row = 0; row < landedField.Rows; row++)
            //    for (int col = 0; col < landedField.Columns; col++)
            //    {
            //        if (landedField.IsFilled(row, col))
            //            this.FillCell(block.RowPosition + row, block.ColPosition + col);
            //        else
            //        {
            //            this.ClearCell(block.RowPosition + row, block.ColPosition + col);
            //        }
            //    }
        }


        private void ClearCells()
        {
            for (int row = 0; row < Rows; row++)
                for (int col = 0; col < Columns; col++)
                    ClearCell(row, col);
        }
    }
}