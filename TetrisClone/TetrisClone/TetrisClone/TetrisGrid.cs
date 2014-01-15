namespace TetrisClone
{
    public class TetrisGrid
    {
        public int RowCount { get; set; }
        public int ColumnCount {get;set;}
        public byte[][] CellStateArray;
        public int CellPixelSize = 30;

        public TetrisGrid(int rowCount, int columnCount)
        {
            RowCount = rowCount;
            ColumnCount = columnCount;

            // initialize block configuration
            InitializeGrid(rowCount, columnCount);
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

        public byte[] GetColumnSegment(int row, int column, int rowSize)
        {
            byte[] columnSegment = new byte[4];

            // wall
            if (column >= ColumnCount || column < 0)
            {
                return new byte[] { 1, 1, 1, 1 };
            }

            for (int r = 0; r < rowSize; r++)
            {
                columnSegment[r] = this.IsFilled(row + r, column) ? (byte)1 : (byte)0;
            }

            return columnSegment;
        }

        public byte[] GetRowSegment(int row, int column, int columnSize)
        {
            byte[] rowSegment = new byte[4];

            // floor
            if (row >= RowCount || row < 0)
            {
                return new byte[] { 1, 1, 1, 1 };
            }

            for (int c = 0; c < columnSize; c++)
            {
                rowSegment[c] = this.IsFilled(row, column+c) ? (byte)1 : (byte)0;
            }

            return rowSegment;
        }

        public bool IsFilled(int row, int col)
        {
            if (col >= 0 && col < ColumnCount && row >= 0 && row < RowCount)
                return CellStateArray[row][col] == 1;
            
            return false;
        }

        public void FillCell(int row, int col)
        {
            if (col >= 0 && col < ColumnCount && row >= 0 && row < RowCount)
                CellStateArray[row][col] = 1;
        }

        public void ClearCell(int row, int col)
        {
            if (col >= 0 && col < ColumnCount && row >= 0 && row < RowCount)
                CellStateArray[row][col] = 0;
        }
    }
}