namespace Algebra.Linear {
    public class Matrix {
        private int numRows, numCols;
        private double[,] elements;

        public Matrix() {
            numRows = 0; numCols = 0;
            elements = new double[,];
        }

        public Matrix(int rows, int cols) {
            numRows = rows; numCols = cols;
        }

        public bool IsSquare() {
            if (numRows == numCols) return true;
            
            return false;
        }

        public double Get(int row, int col) {
            return elements[row, col];
        }

        public void Set(int row, int col, double value) {
            elements[row, col] = value;
        }
    }
}