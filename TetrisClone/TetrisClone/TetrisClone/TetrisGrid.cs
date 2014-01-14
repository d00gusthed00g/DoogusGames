namespace TetrisClone
{
    public class TetrisGrid
    {
        public int Rows { get; set; }
        public int Columns {get;set;}
        public byte[][] CellStateArray;
        private const int CellSize = 30;

        public TetrisGrid(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;

            // initialize block configuration
            InitializeGrid(rows, columns);
        }

        private void InitializeGrid(int rows, int columns)
        {
            CellStateArray = new byte[rows][];

            for (int row = 0; row < rows; row++)
            {
                CellStateArray[row] = new byte[columns];

                for (int col = 0; col < columns; col++)
                {
                    CellStateArray[row][col] = 0;
                }
            }
        }

        public int GetCellSize()
        {
            return CellSize;
        }

        public bool IsFilled(int row, int col)
        {
            if (col >= 0 && col < Columns && row >= 0 && row < Rows)
                return CellStateArray[row][col] == 1;
            
            return false;
        }

        public void FillCell(int row, int col)
        {
            if (col >= 0 && col < Columns && row >= 0 && row < Rows)
                CellStateArray[row][col] = 1;
        }

        public void ClearCell(int row, int col)
        {
            if (col >= 0 && col < Columns && row >= 0 && row < Rows)
                CellStateArray[row][col] = 0;
        }
    }
}