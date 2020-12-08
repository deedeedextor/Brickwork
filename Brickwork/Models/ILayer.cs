namespace Brickwork.Models
{
    public interface ILayer
    {
        public int Rows { get; set; }

        public int Columns { get; set; }

        public int[,] Matrix { get; set; }

        bool IsLayerValid();

        bool IsInsideMatrix(int[,] matrix, int row, int col);

        void CalculateOutput(ILayer outputLayer);

        void PrintLayer();

        void PrintLayerWithAdditionalSymbols();
    }
}
