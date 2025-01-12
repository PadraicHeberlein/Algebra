namespace Algebra.Linear {
    public class SqMatrix : Matrix{
        private int n;
        public SqMatrix() : base() { }

        public SqMatrix(int size) : base(size, size) { n = size; }

        public SqMatrix Minor(int exRow, int exCol) {
            int minorRow = 0, minorCol = 0;
            SqMatrix minor = new SqMatrix(n - 1);

            for (int row = 0; row < n; row++) {
                if (row == exRow) continue;
                else {
                    for (int col = 0; col < n; col++) {
                        if (col == exCol) continue;
                        else {
                            minor.Set(
                                minorRow, 
                                minorCol, 
                                minor.Get(row, col)
                            );
                            minorCol++;
                        }
                    }
                    minorRow++;
                }
                
            }

            return minor;
        }
        
        public double Trace() {
            double trace = 0;

            for (int d = 0; d < n; d++) trace += elements[d, d];

            return trace;
        }
    }
}