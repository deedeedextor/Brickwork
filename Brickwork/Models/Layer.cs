namespace Brickwork.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Describes class Layer.
    /// </summary>
    public class Layer : ILayer
    {
        private int rows;
        private int columns;

        public Layer(int rows, int columns)
        {
            this.Rows = rows;
            this.Columns = columns;

            this.Matrix = new int[Rows, Columns];
            this.Bricks = new Dictionary<Brick, int>();
        }

        public int Rows 
        {
            get => this.rows;
            set
            {
                if (value > 1 && value <= 100 && value % 2 == 0)
                {
                    this.rows = value;
                }
            }
        }

        public int Columns 
        {
            get => this.columns;
            set
            {
                if (value > 1 && value <= 100 && value % 2 == 0)
                {
                    this.columns = value;
                }
            }
        }

        public Dictionary<Brick, int> Bricks { get; set; }

        public int[,] Matrix { get; set; }

        /// <summary>
        /// Arranges the target layer
        /// </summary>
        /// <param name="outputLayer">The target layer</param>
        public void CalculateOutput(ILayer outputLayer)
        {
            Brick brick;
            var orderedBricks = this.Bricks.OrderBy(x => x.Key.Number).ToList();

            for (int row = 0; row < this.Matrix.GetLength(0); row++)
            {
                for (int col = 0; col < this.Matrix.GetLength(1); col++)
                {
                    if (IsInsideMatrix(this.Matrix, row, col + 1) && this.Matrix[row, col] != this.Matrix[row, col + 1] && (outputLayer.Matrix[row, col] == 0 && outputLayer.Matrix[row, col + 1] == 0))
                    {
                        brick = orderedBricks.First().Key;

                        outputLayer.Matrix[row, col] = brick.Number;
                        outputLayer.Matrix[row, col + 1] = brick.Number;
                        orderedBricks.Remove(orderedBricks[0]);
                    }
                    if (IsInsideMatrix(this.Matrix, row - 1, col) && this.Matrix[row, col] != this.Matrix[row - 1, col] && (outputLayer.Matrix[row, col] == 0 && outputLayer.Matrix[row - 1, col] == 0))
                    {
                        brick = orderedBricks.First().Key;

                        outputLayer.Matrix[row, col] = brick.Number;
                        outputLayer.Matrix[row - 1, col] = brick.Number;
                        orderedBricks.Remove(orderedBricks[0]);
                    }
                }
            }
        }

        /// <summary>
        /// Checks if bricks coordinates are in the matrix dimensions 
        /// </summary>
        ///<param name = "matrix" > The matrix to be used.</param>
        /// <param name="row">The specific row index.</param>
        /// <param name="col">The specific column index.</param>
        /// <returns>Returns true or false.</returns>
        public bool IsInsideMatrix(int[,] matrix, int row, int col)
        {
            return row >= 0 && row < matrix.GetLength(0) && col >= 0 && col < matrix.GetLength(1);
        }

        /// <summary>
        /// Checks if layer consists of valid bricks 
        /// </summary>
        public bool IsLayerValid()
        {
            for (int row = 0; row < this.Matrix.GetLength(0); row++)
            {
                for (int col = 0; col < this.Matrix.GetLength(1); col++)
                {
                    var currentBrick = new Brick(this.Matrix[row, col]);

                    if (!this.Bricks.Keys.Any(x => x.Number == currentBrick.Number))
                    {
                        this.Bricks.Add(currentBrick, 0);
                    }

                    if (IsInsideMatrix(this.Matrix, row, col + 1) && currentBrick.Number == this.Matrix[row, col + 1])
                    {
                        this.Bricks[currentBrick]++;
                    }
                    if (IsInsideMatrix(this.Matrix, row + 1, col) && currentBrick.Number == this.Matrix[row + 1, col])
                    {
                        this.Bricks[currentBrick]++;
                    }
                }
            }

            foreach (var item in this.Bricks)
            {
                if (item.Value != 1)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Prints the output matrix 
        /// </summary>
        public void PrintLayer()
        {
            for (int row = 0; row < this.Matrix.GetLength(0); row++)
            {
                for (int col = 0; col < this.Matrix.GetLength(1); col++)
                {
                    Console.Write(this.Matrix[row, col] + " ");
                }

                Console.WriteLine();
            }
        }

        /// <summary>
        /// Prints the output matrix with additional symbols for the bricks 
        /// </summary>
        public void PrintLayerWithAdditionalSymbols()
        {
            for (int row = 0; row < this.Matrix.GetLength(0); row++)
            {
                for (int col = 0; col < this.Matrix.GetLength(1); col++)
                {
                    if (col == 0)
                    {
                        Console.Write($"*{this.Matrix[row, col]}");
                    }

                    if (IsInsideMatrix(this.Matrix, row, col + 1) && this.Matrix[row, col] == this.Matrix[row, col + 1])
                    {
                        Console.Write($" {this.Matrix[row, col + 1]}");

                    }
                    else if (IsInsideMatrix(this.Matrix, row, col + 1) && this.Matrix[row, col] != this.Matrix[row, col + 1])
                    {
                        Console.Write($"*{this.Matrix[row, col + 1]}");
                    }

                    if (col + 1 == this.Matrix.GetLength(1) - 1)
                    {
                        Console.Write($"*");

                    }
                }
                Console.WriteLine();
            }
        }
    }
}
