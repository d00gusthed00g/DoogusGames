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
    public enum TranslationDirection { Left, Right, Down}

    public abstract class Block : TetrisGrid
    {
        protected Texture2D BackgroundTexture;
        public int ColPosition { get; set; }
        public int RowPosition { get; set; }
        public int RotationIndex { get; set; }

        private const int ColumnCount = 4, RowCount =4;

        protected Block(int colPosition, int rowPosition) : base(RowCount, ColumnCount)
        {
            ColPosition = colPosition;
            RowPosition = rowPosition;
            RotationIndex = 0;
        }

        public virtual void Rotate()
        {
            if (RotationIndex >= 3)
                RotationIndex = 0;
            else
                RotationIndex++;
        }

        public virtual void Translate(TranslationDirection dir, int pfRows, int pfColumns)
        {
            if (IsCollision(dir, pfRows, pfColumns)) return;
            
            switch (dir)
            {
                case TranslationDirection.Right:
                    ColPosition++;
                    break;
                case TranslationDirection.Left:
                    ColPosition--;
                    break;
                case TranslationDirection.Down:
                    RowPosition++;
                    break;
            }
        }

        public bool IsCollision(TranslationDirection dir, int pfRows, int pfColumns)
        {
            var padding = 0;
            if (HasPadding((dir)))
                padding = 1;

            switch (dir)
            {
                case TranslationDirection.Right:
                    if (ColPosition + ColumnCount - padding >= pfColumns)
                        return true;
                    break;
                case TranslationDirection.Left:
                    if (ColPosition + padding <= 0)
                        return true;
                    break;
                case TranslationDirection.Down:
                    if (RowPosition + RowCount  >= pfRows)
                            return true;
                    break;
                default:
                    return false;
            }
            return false;
        }

        // returns padding before translation
        private bool HasPadding(TranslationDirection dir)
        {
            switch (dir)
            {
                case TranslationDirection.Right:
                    return CellStateArray.All(row => row[ColumnCount - 1] != 1);
                case TranslationDirection.Left:
                    return CellStateArray.All(row => row[0] != 1);
                case TranslationDirection.Down:
                    return !CellStateArray[RowCount-1].Any(col => col == 1);
                default:
                    return false;
            }
        }
        //// returns padding before translation
        //private bool HasPadding(TranslationDirection dir)
        //{
        //    switch (dir)
        //    {
        //        case TranslationDirection.Right:
        //            return CellStateArray.Any(row => row[ColumnCount - 1] == 1);
        //        case TranslationDirection.Left:
        //            return CellStateArray.Any(row => row[0] == 1);
        //        case TranslationDirection.Down:
        //            return CellStateArray[RowCount-1].Any(col => col == 1);
        //        default:
        //            return false;
        //    }
        //}
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
