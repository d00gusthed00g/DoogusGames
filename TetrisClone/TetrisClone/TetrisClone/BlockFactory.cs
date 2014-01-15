using System;

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
            int block = _rand.Next(3);

            switch (block)
            {
                case (int) BlockType.I: return new IBlock(0, 5);
                case (int) BlockType.O: return new OBlock(0, 5);
                case (int) BlockType.L: return new LBlock(0, 5);
                default: return null;
            }
        }
    }
}