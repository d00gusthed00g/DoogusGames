using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TetrisClone
{
    public enum TranslationDirection { Left, Right, Down, Up}

    public abstract class Block : TetrisGrid
    {
        private const int ColumnSize = 4, RowSize = 4;

        protected Texture2D BackgroundTexture;
        public int ColPosition { get; set; }
        public int RowPosition { get; set; }
        public int RotationIndex { get; set; }
        public bool HasLanded { get; set; }
        
        protected Block(int rowPosition, int colPosition) : base(RowSize, ColumnSize)
        {
            ColPosition = colPosition;
            RowPosition = rowPosition;
            RotationIndex = 0;
        }

        public virtual void Rotate()
        {
            // TODO: perform wall kick

            if (RotationIndex >= 3)
                RotationIndex = 0;
            else
                RotationIndex++;
        }

        public virtual void Move(TranslationDirection dir, PlayField pf)
        {
            switch (dir)
            {
                case TranslationDirection.Right:
                    if (!IsRightCollision(pf.LandedBlocks, ColumnSize - 1))
                        ColPosition++;

                    break;
                case TranslationDirection.Left:
                    if (!IsLeftCollision(pf.LandedBlocks, 0))
                        ColPosition--;

                    break;
                case TranslationDirection.Down:
                    if (!IsBottomCollision(pf.LandedBlocks, RowSize - 1))
                    {
                        RowPosition++;
                    }
                    else
                    {
                        pf.LandedBlocks.AddBlock(this);
                        this.HasLanded = true;
                    }
                    break;
                case TranslationDirection.Up:
                    RowPosition--;
                    break;
            }
        }

        private bool IsRightCollision(LandedBlocks lb, int blockColumnIndex)
        {
            // always between 3 - 0 
            byte[] sourceSegment = this.GetColumnSegment(blockColumnIndex);

            // target is next playfield column over to current block column
            byte[] targetSegment = lb.GetColumnSegment(this.RowPosition, this.ColPosition + blockColumnIndex + 1, RowSize);

            if (IsCollision(sourceSegment, targetSegment))
                return true;

            return 
                blockColumnIndex > 0 && IsRightCollision(lb, --blockColumnIndex);

        }

        private bool IsLeftCollision(LandedBlocks lb, int blockColumnIndex)
        {
            // always between 3 - 0 
            byte[] sourceSegment = this.GetColumnSegment(blockColumnIndex);

            // target is next playfield column over to current block column
            int column = this.ColPosition + blockColumnIndex - 1;
            byte[] targetSegment = lb.GetColumnSegment(this.RowPosition, column, RowSize);

            if (IsCollision(sourceSegment, targetSegment))
                return true;

            return 
                blockColumnIndex < ColumnSize - 1 && IsLeftCollision(lb, ++blockColumnIndex);
        }

        private bool IsBottomCollision(LandedBlocks lb, int blockRowIndex)
        {
            // always between 3 - 0 
            byte[] sourceSegment = this.GetRowSegment(blockRowIndex);

            // target is next playfield row below  current block row
            int row = this.RowPosition + blockRowIndex + 1;
            byte[] targetSegment = lb.GetRowSegment(row, this.ColPosition, ColumnSize); 

            if (IsCollision(sourceSegment, targetSegment))
                return true;
            
            return 
                blockRowIndex > 0 && IsBottomCollision( lb, --blockRowIndex);

        }
        
        private bool IsCollision(byte[] sourceEdge, byte[] targetEdge)
        {
            // if any cells collide
            return sourceEdge.Where((sourceCell, i) => sourceCell == 1 && targetEdge[i] == 1).Any();
        }

        private byte[] GetColumnSegment(int columnIndex)
        {
            byte[] segment = new byte[4];

            // get column by applying column index on all rows[]
            for (int row = 0; row < CellStateArray.Length; row++)
            {
                segment[row] = CellStateArray[row][columnIndex];
            }

            return segment;
        }
        private byte[] GetRowSegment(int rowIndex)
        {
            return CellStateArray[rowIndex];
        }
    }
}
