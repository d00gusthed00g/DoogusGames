namespace TetrisClone
{
    public class OBlock : Block
    {
        public OBlock(int row, int col)
            : base(row, col)
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
}