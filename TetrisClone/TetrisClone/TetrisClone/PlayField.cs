using Microsoft.Xna.Framework;

namespace TetrisClone
{
    public class PlayField : TetrisGrid
    {
        public LandedBlocks LandedBlocks;
        private Block _currentBlock;
        
        public PlayField(int rowCount, int columnCount) : base(rowCount, columnCount)
        {
            // assumption, LandedBlocks = same size as PlayField
            LandedBlocks = new LandedBlocks(rowCount, columnCount);

            //TODO CLEAR - test blocks
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

            for (int row = 0; row < block.RowCount; row++)
                for (int col = 0; col < block.ColumnCount; col++)
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

            for (int row = 0; row < landedBlocksGrid.RowCount; row++)
                for (int col = 0; col < landedBlocksGrid.ColumnCount; col++)
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
            for (int row = 0; row < base.RowCount; row++)
                for (int col = 0; col < base.ColumnCount; col++)
                    ClearCell(row, col);
        }


    }
}