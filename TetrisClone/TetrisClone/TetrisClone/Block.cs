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
        private const int ColumnCount = 4, RowCount = 4;

        protected Texture2D BackgroundTexture;
        public int ColPosition { get; set; }
        public int RowPosition { get; set; }
        public int RotationIndex { get; set; }
        
        public int RightColumnsClipping { get; set; }
        public int LeftColumnsClipping { get; set; }

        protected Block(int colPosition, int rowPosition) : base(RowCount, ColumnCount)
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
            
            byte[] wall = { 1,1,1,1 };
            byte[] targetSegment;


            switch (dir)
            {
                case TranslationDirection.Right:
                    // GET TARGET
                    if (ColPosition + ColumnCount >= pf.Columns)
                    {
                        // collision target is wall
                        targetSegment = wall;
                    }
                    else
                    {
                        // collision target is the next array of 4 cells vertical
                        targetSegment = pf.GetColumnSegment(RowPosition, ColPosition + ColumnCount - this.RightColumnsClipping + 1);
                    }

                    // GET SOURCE

                    // which column of the bounding block are we testing? columns from right to left
                    for (int col = ColumnCount - 1; col >= 0; col--)
                    {
                        byte[] blockColumn = GetBlockColumnSegment(col);

                        // if side collision - no move
                        if (IsCollision(blockColumn, targetSegment))
                            return;
                    }


                    ColPosition++;
                    break;
                case TranslationDirection.Left:
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

        // set clipping here
        private bool IsCollision(byte[] sourceEdge, byte[] targetEdge)
        {
            for (int i = 0; i < sourceEdge.Length; i++)
            {
                if (sourceEdge[i] == 1)
                {
                    if (targetEdge[i] == 1)
                        return true;
                }
                // no collision, test for clipping
                if (sourceEdge[i] == 0)
                {
                    if (targetEdge[i] == 1)
                    {
                        this.RightColumnsClipping += 1;
                        break;
                    }
                }

            }
            return false;
        }

        public bool IsCollision(TranslationDirection dir, int sourceColumnIndex, byte[] collisionTestTarget)
        {
            switch (dir)
            {
                case TranslationDirection.Right:
                {
                    

                    break;
                }

                case TranslationDirection.Left:
                {
              

                    break;
                }
                case TranslationDirection.Down:
              
                    break;
                default:
                    return false;
            }
            return false;
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
