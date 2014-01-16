using System;
using TetrisClone.Blocks;

namespace TetrisClone
{
    public class BlockFactory
    {
        private readonly Random _rand;

        public BlockFactory()
        {
            _rand = new Random();
        }

        public enum BlockType { I, O, L, T, S, Z, J }

        public Block GenerateBlock()
        {
            int block = _rand.Next(7);

            switch (block)
            {
                case (int) BlockType.I: return new IBlock(-1, 3);
                case (int) BlockType.O: return new OBlock(-1, 3);
                case (int) BlockType.L: return new LBlock(-1, 2);
                case (int) BlockType.T: return new TBlock(-1, 2);
                case (int) BlockType.S: return new SBlock(-1, 2);
                case (int) BlockType.Z: return new ZBlock(-1, 2);
                case (int) BlockType.J: return new JBlock(-1, 2);
                default: return null;
            }
        }
    }
}