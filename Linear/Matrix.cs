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
    }
}