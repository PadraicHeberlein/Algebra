namespace Algebra.Linear.R3 {
    public class SquareMatrixR3 {
        // constants and common matrices
        public static SquareMatrixR3 ZERO = new SquareMatrixR3();
        public static SquareMatrixR3 I = new SquareMatrixR3(
            VectorR3.E1, VectorR3.E2, VectorR3.E3, false
        );
        // private members
        private double[,] e;
        // default constructor
        public SquareMatrixR3() {
            e = new double[n, n];
        }
        // row / column constructor
        public SquareMatrixR3(
            VectorR3 v0, VectorR3 v1, VectorR3 v2, 
            bool givenRows = false
        ) {
            e = new double[n, n];

            if (givenRows) {
                for (int col = 0; col < n; col++) {
                    e[0, col] = v0.Get(col);
                    e[1, col] = v1.Get(col);
                    e[2, col] = v2.Get(col);
                }
            } else {
                for (int row = 0; row < n; row++) {
                    e[row, 0] = v0.Get(row);
                    e[row, 1] = v1.Get(row);
                    e[row, 2] = v2.Get(row);
                }
            }
        }

        // copy constructor
        public SquareMatrixR3(SquareMatrixR3 m) {
            e = new double[n, n]; e = m.e;
        }

        // public methods:

        // check if two matrices are equal
        public bool Equals(SquareMatrixR3 m) {
            for (int row = 0; row < n; row++) {
                for (int col = 0; col < n; col++) {
                    double diff = Math.Abs(
                        e[row, col] - m.e[row, col]
                    );
                    if (diff > ConstR3.ZERO) 
                        return false;
                }
            }

            return true;
        }

        // add two matrices
        public SquareMatrixR3 Add(SquareMatrixR3 m)
        {
            SquareMatrixR3 sum = new SquareMatrixR3();

            for (int row = 0; row < n; row++) {
                for (int col = 0; col < n; col++)
                    sum.e[row, col] = 
                        e[row, col] + m.e[row, col];
            }

            return sum;
        }

        // subtract two matrices
        public SquareMatrixR3 Sub(SquareMatrixR3 m) {
            return new SquareMatrixR3(Add(m.Sx(-1)));
        }

        // multiply two matrices
        public SquareMatrixR3 Xm(SquareMatrixR3 m) {
            SquareMatrixR3 product = new SquareMatrixR3();
            VectorR3[] rows = GetRows();
            VectorR3[] otherCols = m.GetCols();

            for (int i = 0; i < n; i++) {
                for (int j = 0; j < n; j++)
                    product.e[i, j] = rows[i].Dot(otherCols[j]);
            }

            return product;
        }

        // multiply a matrix by a vector
        public VectorR3 Xv(VectorR3 v)
        {
            VectorR3 product = new VectorR3();
            VectorR3[] rows = GetRows();

            for (int i = 0; i < n; i++)
                product.Set(i, rows[i].Dot(v));

            return product;
        }

        // multiply a matrix by a scalar
        public SquareMatrixR3 Xs(double s) {
            SquareMatrixR3 product = new SquareMatrixR3();
            
            for (int i = 0; i < n; i++) {
                for (int j = 0; j < n; j++)
                    product.e[i, j] = s * e[i, j];
            }

            return product;
        }

        // calculate the determinant
        public double Det() {
            double first = 
                e[0, 0] * (e[1, 1] * e[2, 2] - e[1, 2] * e[2, 1]);
            double second = 
                e[0, 1] * (e[1, 0] * e[2, 2] - e[1, 2] * e[2, 0]);
            double third = 
                e[0, 2] * (e[1, 0] * e[2, 1] - e[1, 1] * e[2, 0]);

            return first - second + third;
        }

        // check if the determinant is zero
        public bool DetIsZero() {
            if (Det() < Const.Zero) return true;

            return false;
        }

        // calculate the inverse
        public SquareMatrixR3 Inv() {
            double det = Det();
            SquareMatrixR3 theInverse = new SquareMatrixR3();

            theInverse.e[0, 0] = e[1, 1] * e[2, 2] - e[1, 2] * e[2, 1];
            theInverse.e[0, 1] = -(e[1, 0] * e[2, 2] - e[1, 2] * e[2, 0]);
            theInverse.e[0, 2] = e[1, 0] * e[2, 1] - e[1, 1] * e[2, 0];

            theInverse.e[1, 0] = -(e[0, 1] * e[2, 2] - e[0, 2] * e[2, 1]);
            theInverse.e[1, 1] = e[0, 0] * e[2, 2] - e[0, 2] * e[2, 0];
            theInverse.e[1, 2] = -(e[0, 0] * e[2, 1] - e[0, 1] * e[2, 0]);

            theInverse.e[2, 0] = e[0, 1] * e[1, 2] - e[0, 2] * e[1, 1];
            theInverse.e[2, 1] = -(e[0, 0] * e[1, 2] - e[0, 2] * e[1, 0]);
            theInverse.e[2, 2] = e[0, 0] * e[1, 1] - e[0, 1] * e[1, 0];

            if (Det() > Const.Zero) {
                theInverse = theInverse.Sx(1 / det);
            } else {
                throw new IllegalArgumentException();
            }

            return theInverse.T();
        }

        // get methods:

        // get a single element of the matrix
        public double Get(int row, int col) {
            return e[row, col];
        }

        // get a column in the matrix
        public VectorR3 GetCol(int col) {
            VectorR3 theCol = new VectorR3();
            for (int row = 0; row < n; row++)
                theCol.Set(row, e[row, col]);
            return theCol;
        }

        // get columns in the matrix
        public VectorR3[] GetCols() {
            VectorR3[] cols = new VectorR3[n];
            for (int col = 0; col < n; col++)
            {
                VectorR3 theCol = GetCol(col);
                cols[col] = theCol;
            }
            return cols;
        }

        // get row of the matrix
        public VectorR3 GetRow(int row) {
            VectorR3 theRow = new VectorR3();
            for (int col = 0; col < n; col++)
                theRow.Set(col, e[row, col]);
            return theRow;
        }

        // get all rows in the matrix
        public VectorR3[] GetRows() {
            VectorR3[] rows = new VectorR3[n];
            for (int row = 0; row < n; row++)
            {
                VectorR3 theRow = GetRow(row);
                rows[row] = theRow;
            }
            return rows;
        }

        // set methods:

        // set a single element value
        public void Set(double value, int row, int col) {
            e[row, col] = value;
        }

        // set a single column
        public void SetCol(int colNum, VectorR3 theCol) {
            e[0, colNum] = theCol.Get(0);
            e[1, colNum] = theCol.Get(1);
            e[2, colNum] = theCol.Get(2);
        }

        // set a single row
        public void SetRow(int rowNum, VectorR3 theRow) {
            e[rowNum, 0] = theRow.Get(0);
            e[rowNum, 1] = theRow.Get(1);
            e[rowNum, 2] = theRow.Get(3);
        }

        // calculate the transpose
        public SquareMatrixR3 T() {
            VectorR3[] rows = GetRows();
            return new MatrixR3(rows[0], rows[1], rows[2], false);
        }

        public override String ToString() {
            return  
                $" | {e[0, 0]:F2} {e[0, 1]:F2} {e[0, 2]:F2} |\n" +
                $" | {e[1, 0]:F2} {e[1, 1]:F2} {e[1, 2]:F2} |\n" +
                $" | {e[2, 0]:F2} {e[2, 1]:F2} {e[2, 2]:F2} |\n";
        }

        // rotation matrices for each axis by phi radians
        public static SquareMatrixR3 Rx(double phi) {
            SquareMatrixR3 rx = new SquareMatrixR3();

            rx.SetCol(0, new VectorR3(1, 0, 0));
            rx.SetCol(1, new VectorR3(0, Math.Cos(phi), Math.Sin(phi)));
            rx.SetCol(2, new VectorR3(0, -1 * Math.Sin(phi), Math.Cos(phi)));

            return rx;
        }

        public static SquareMatrixR3 Ry(double phi) {
            SquareMatrixR3 rx = new SquareMatrixR3();

            rx.SetCol(0, new VectorR3(1, 0, 0));
            rx.SetCol(1, new VectorR3(0, Math.Cos(phi), Math.Sin(phi)));
            rx.SetCol(2, new VectorR3(0, -1 * Math.Sin(phi), Math.Cos(phi)));

            return rx;
        }

        public static SquareMatrixR3 Rz(double phi) {
            SquareMatrixR3 ry = new SquareMatrixR3();

            ry.SetCol(0, new VectorR3(Math.Cos(phi), 0, -1 * Math.Sin(phi)));
            ry.SetCol(1, new VectorR3(0, 1, 0));
            ry.SetCol(2, new VectorR3(Math.Sin(phi), 0, Math.Cos(phi)));

            return ry;
        }
    }
}