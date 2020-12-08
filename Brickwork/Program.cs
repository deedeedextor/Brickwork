namespace Brickwork
{
    using Brickwork.Models;
    using System;
    using System.Linq;

    public class Program
    {
        private static ILayer inputLayer;
        private static ILayer outputLayer;

        public static void Main(string[] args)
        {
            GetInput();

            if (inputLayer.IsLayerValid())
            {
                inputLayer.CalculateOutput(outputLayer);
                outputLayer.PrintLayer();

                //outputLayer.PrintLayerWithAdditionalSymbols();
            }
            else
            {
                ValidationError("Layer contains invalid blocks!");
            }
        }

        private static void GetInput()
        {
            int[] dimentions = Console.ReadLine()
                .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            int rows = dimentions[0];
            int columns = dimentions[1];
            inputLayer = new Layer(rows, columns);

            if (inputLayer.Rows != 0 && inputLayer.Columns != 0)
            {
                outputLayer = new Layer(rows, columns);

                for (int row = 0; row < inputLayer.Matrix.GetLength(0); row++)
                {
                    int[] symbols = Console.ReadLine()
                    .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray();

                    if (symbols.Count() < columns)
                    {
                        ValidationError("The input is not complete!");
                    }

                    for (int col = 0; col < inputLayer.Matrix.GetLength(1); col++)
                    {
                        inputLayer.Matrix[row, col] = symbols[col];
                    }
                }
                Console.WriteLine();
            }
            else
            {
                ValidationError("A valid area should not exceed 100 rows and columns!");
            }
        }

        private static void ValidationError(string message)
        {
            Console.WriteLine("-1");
            Console.WriteLine($"{message}");
            System.Environment.Exit(1);
        }
    }
}
