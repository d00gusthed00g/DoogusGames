using Microsoft.Xna.Framework;

namespace TetrisClone
{
    public class PlayField : TetrisGrid
    {
        private const int PlayfieldRows = 25;
        private const int PlayfieldColumns = 10;

        public PlayField() : base(PlayfieldRows, PlayfieldColumns)
        {
        }

        public void RenderBlock(Block block)
        {
            ClearCells();

            for(int row = 0; row < block.Rows;row++)
                for (int col = 0; col < block.Columns; col++)
                {
                    if (block.IsFilled(row, col))
                        this.FillCell(block.RowPosition + row, block.ColPosition + col);
                    else
                    {
                        this.ClearCell(block.RowPosition + row, block.ColPosition + col);
                    }
                }
        }


        private void ClearCells()
        {
             for(int row = 0; row< Rows;row++)
                for (int col = 0; col < Columns; col++)
                    ClearCell(row,col);
        }
    }
}