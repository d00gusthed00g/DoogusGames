using Microsoft.Xna.Framework;

namespace TetrisClone
{
    public class PlayField : TetrisGrid
    {
        private const int RowCount = 25;
        private const int ColumnCount = 10;

        // assumption, LandedBlocks = same size as PlayField
        public LandedBlocks LandedBlocks;
        private Block _currentBlock;

        public PlayField() : base(RowCount, ColumnCount)
        {
           LandedBlocks = new LandedBlocks();
           LandedBlocks.AddBlock(new OBlock(6, 5));
        }

        public void AddBlock(Block block)
        {
            _currentBlock = block;
        }

        public void Update()
        {
            ClearCells();
            RenderBlock();
            RenderLandedBlocks();
        }

        private void RenderBlock()
        {
            var block = _currentBlock;

            for (int row = 0; row < block.Rows; row++)
                for (int col = 0; col < block.Columns; col++)
                {
                    if (block.IsFilled(row, col))
                        this.FillCell(block.RowPosition + row, block.ColPosition + col);
                    else
                    {
                        //this.ClearCell(block.RowPosition + row, block.ColPosition + col);
                    }
                }
        }

        private void RenderLandedBlocks()
        {
            var landedBlocksGrid = this.LandedBlocks;

            for (int row = 0; row < landedBlocksGrid.Rows; row++)
                for (int col = 0; col < landedBlocksGrid.Columns; col++)
                {
                    if (landedBlocksGrid.IsFilled(row, col))
                        this.FillCell(row, col);
                    else
                    {
                        //this.ClearCell(row, col);
                    }
                }
        }


        private void ClearCells()
        {
             for(int row = 0; row< Rows;row++)
                for (int col = 0; col < Columns; col++)
                    ClearCell(row,col);
        }

        public byte[] GetColumnSegment(int row, int column)
        {
            byte[] targetColumn = new byte[4];

            for (int r = row; r < targetColumn.Length; r++)
            {
                targetColumn[r] = this.IsFilled(r, column) ? (byte)1 : (byte)0;
            }
            return targetColumn;
        }
    }
}