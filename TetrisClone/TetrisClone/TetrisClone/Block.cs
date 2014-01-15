using System;
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
        private const int ColumnCount = 4, RowSize = 4;

        protected Texture2D BackgroundTexture;
        public int ColPosition { get; set; }
        public int RowPosition { get; set; }
        public int RotationIndex { get; set; }
        
        protected Block(int colPosition, int rowPosition) : base(RowSize, ColumnCount)
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
                    if (!IsCollision(dir, pf.LandedBlocks, ColumnCount - 1))
                        ColPosition++;

                    break;
                case TranslationDirection.Left:
                    if (!IsCollision(dir, pf.LandedBlocks, 0))
                        ColPosition--;

                    break;


                case TranslationDirection.Down:
                    RowPosition++;
                    break;
                case TranslationDirection.Up:
                    RowPosition--;
                    break;
            }
        }

        private bool IsCollision(TranslationDirection dir, LandedBlocks lb, int blockColumnIndex)
        {
            // always between 3 - 0 
            byte[] sourceSegment = this.GetBlockColumnSegment(blockColumnIndex);

            int column = 0;
            
            // target is next playfield column over to current block column
            if(dir == TranslationDirection.Right)
                column = this.ColPosition + blockColumnIndex + 1;

            if(dir == TranslationDirection.Left)
                column = this.ColPosition + blockColumnIndex - 1;

            byte[] targetSegment = lb.GetColumnSegment(RowPosition, column, RowSize); 

            if (IsCollision(sourceSegment, targetSegment))
                return true;

            if (dir == TranslationDirection.Right)
                return blockColumnIndex > 0 &&
                   IsCollision(dir, lb, --blockColumnIndex);

            if (dir == TranslationDirection.Left)
                return blockColumnIndex < ColumnCount-1 &&
                   IsCollision(dir, lb, ++blockColumnIndex);

            //else
            return false;
        }
        
        private bool IsCollision(byte[] sourceEdge, byte[] targetEdge)
        {
            // if any cells collide
            return sourceEdge.Where((sourceCell, i) => sourceCell == 1 && targetEdge[i] == 1).Any();
        }

        private byte[] GetBlockColumnSegment(int columnIndex)
        {
            byte[] segment = new byte[4];

            // get column by applying column index on all rows[]
            for (int row = 0; row < CellStateArray.Length; row++)
            {
                segment[row] = CellStateArray[row][columnIndex];
            }

            return segment;
        }
    }



    public class IBlock : Block
    {
        public IBlock(int col, int row) : base (col, row)
        {
            CellStateArray = new[]
            {
                new byte[] {0, 1, 0, 0},
                new byte[] {0, 1, 0, 0},
                new byte[] {0, 1, 0, 0},
                new byte[] {0, 1, 0, 0},
            };
        }

        public override void Rotate()
        {
            base.Rotate();

            switch (RotationIndex)
            {
                case 0:
                    CellStateArray = new[]
                    {
                        new byte[] {0, 1, 0, 0},
                        new byte[] {0, 1, 0, 0},
                        new byte[] {0, 1, 0, 0},
                        new byte[] {0, 1, 0, 0},
                    };
                    break;
                case 1:
                    CellStateArray = new[]
                    {
                        new byte[] {0, 0, 0, 0},
                        new byte[] {1, 1, 1, 1},
                        new byte[] {0, 0, 0, 0},
                        new byte[] {0, 0, 0, 0},
                    };
                    break;
                case 2:
                    CellStateArray = new[]
                    {
                        new byte[] {0, 0, 1, 0},
                        new byte[] {0, 0, 1, 0},
                        new byte[] {0, 0, 1, 0},
                        new byte[] {0, 0, 1, 0},
                    };
                    break;
                case 3:
                    CellStateArray = new[]
                    {
                        new byte[] {0, 0, 0, 0},
                        new byte[] {0, 0, 0, 0},
                        new byte[] {1, 1, 1, 1},
                        new byte[] {0, 0, 0, 0},
                    };
                    break;
            }
        }
    }

    public class OBlock : Block
    {
        public OBlock(int col, int row)
            : base(col, row)
        {
            CellStateArray = new[]
            {
                new byte[] {0, 0, 0, 0},
                new byte[] {0, 0, 0, 0},
                new byte[] {0, 1, 1, 0},
                new byte[] {0, 1, 1, 0}
            };
        }
    }

    public class BlockFactory
    {
        private readonly Random _rand;

        public BlockFactory()
        {
            _rand = new Random();
        }

        public enum BlockType { I, O, T, S, Z, J, L }

        public Block GetBlock()
        {
            int block = _rand.Next(7);

            switch (block)
            {
                case (int) BlockType.I: return new IBlock(5,5);
                case (int) BlockType.O: return new OBlock(5,5);
                default: return null;
            }
        }
    }
}
