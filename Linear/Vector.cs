using Algebra;

namespace Algebra.Linear {
    public class Vector {
        int dim; double[] elements;
        public Vector() { dim = 0; }
        public Vector(int dimension) { Initialize(dimension); }
        public Vector(double[] tuple) {
            dim = elements.Length;
            for (int i = 0; i < dim; i++) elements[i] = tuple[i];
        }
        void Initialize(int dimension) {
            dim = dimension; elements = new double[dim];
            for (int i = 0; i < dim; i++) elements[i] = 0.0;
        }
        public Vector Add(Vector other) {
            Vector sum = new Vector(dim);
            for (int i = 0; i < dim; i++)
                sum.elements[i] = elements[i] + other.elements[i];

            return sum;
        }
        public int Dim() { return elements.Length; }
        public Vector Neg() {
            Vector neg = new Vector(dim);
            for (int i = 0; i < dim; i++)
                neg.elements[i] = -1 * elements[i];

            return neg;
        }
        public Vector Sub(Vector other) { return Add(other.Neg); }
        public Vector Sx(double scalar) {
            Vector prod = new Vector(dim);
            for (int i = 0; i < dim; i++) 
                prod.elements[i] = scalar * elements[i];

            return prod;
        }
    }
}