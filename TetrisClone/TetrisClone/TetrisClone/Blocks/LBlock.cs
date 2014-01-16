namespace TetrisClone.Blocks
{
    public class LBlock : Block
    {
        public LBlock(int row, int col)
            : base(row, col)
        {
            CellStateArray = new[]
            {
                new byte[] {0, 0, 0, 0},
                new byte[] {0, 0, 1, 0},
                new byte[] {0, 0, 1, 0},
                new byte[] {0, 0, 1, 1}
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
                        new byte[] {0, 0, 0, 0},
                        new byte[] {0, 0, 1, 0},
                        new byte[] {0, 0, 1, 0},
                        new byte[] {0, 0, 1, 1},
                    };
                    break;
                case 1:
                    CellStateArray = new[]
                    {
                        new byte[] {0, 0, 0, 0},
                        new byte[] {0, 0, 0, 0},
                        new byte[] {0, 1, 1, 1},
                        new byte[] {0, 1, 0, 0},
                    };
                    break;
                case 2:
                    CellStateArray = new[]
                    {
                        new byte[] {0, 0, 0, 0},
                        new byte[] {0, 0, 1, 1},
                        new byte[] {0, 0, 0, 1},
                        new byte[] {0, 0, 0, 1},
                    };
                    break;
                case 3:
                    CellStateArray = new[]
                    {
                        new byte[] {0, 0, 0, 0},
                        new byte[] {0, 0, 0, 0},
                        new byte[] {0, 0, 0, 1},
                        new byte[] {0, 1, 1, 1},
                    };
                    break;
            }
        }
    }
}