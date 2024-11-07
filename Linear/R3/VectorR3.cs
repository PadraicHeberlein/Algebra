using System;
using System.Collections.Generic;

using Algebra.Linear;

namespace Algebra.Linear.R3
{
    public class VectorR3 : Vector
    {
        // public readonly vectors 
        public static readonly VectorR3 ZERO_VECTOR = new VectorR3();
        public static readonly VectorR3 E1 = new VectorR3({ 1.0, 0.0, 0.0 });
        public static readonly VectorR3 E2 = new VectorR3({ 0.0, 1.0, 0.0 });
        public static readonly VectorR3 E3 = new VectorR3({ 0.0, 0.0, 1.0 });

        // private constants
        private const int X = 0, Y = 1, Z = 2;

        // default constructor
        public VectorR3() : base(3) { }

        // point-coordinate constructor
        public VectorR3(double xp, double yp, double zp) : base({ xp, yp, zp }) { }

        // copy constructor
        public VectorR3(VectorR3 v) : base({ v.Get(X), v.Get(Y), v.Get(Z) }) { }

        // public methods:

        // dot product (scalar product, inner product)
        public double Dot(VectorR3 v) {
            return Get(X) * v.Get(X) + Get(Y) * v.Get(Y) + Get(Z) * v.Get(Z);
        }

        // cross product (vector product)
        public VectorR3 Cross(VectorR3 v) {
            double iHat = Get(Y) * v.Get(Z) - Get(Z) * v.Get(Y);
            double jHat = Get(Z) * v.Get(X) - Get(X) * v.Get(Z);
            double kHat = Get(X) * v.Get(Y) - Get(Y) * v.Get(X);

            return new VectorR3(iHat, jHat, kHat);
        }

        // equals comparator
        public bool Equals(VectorR3 v) {
            for (int component = 0; component < N; component++)
            {
                double diff = Math.Abs(Get(component) - v.Get(component));
                if (diff < Constants.ZERO)
                    return true;
            }
            return true;
        }    

        // check if two parallel vectors are in the same direction\
        public bool IsInSameDirection(VectorR3 v) {
            if (!IsParallelTo(v))
                throw new Exception("Vectors aren't parallel!");

            VectorR3 thisUnit = v.Unit();
            VectorR3 thatUnit = v.Unit();

            return thisUnit.Equals(thatUnit);
        }

        // check if two vectors are parallel
        public bool IsParallelTo(VectorR3 v) {return Cross(v).IsZero(); }

        // check if two vectors are perpendicular
        public bool IsPerpendicularTo(VectorR3 v) {
            return Math.Abs(Dot(v)) < Constants.ZERO;
        }

        // check if vector is zero vector
        public bool IsZero() {
            bool xIsZero = Math.Abs(Get(X)) < Constants.ZERO;
            bool yIsZero = Math.Abs(Get(Y)) < Constants.ZERO;
            bool zIsZero = Math.Abs(Get(Z)) < Constants.ZERO;

            return (xIsZero && yIsZero && zIsZero);
        }

        // returns magnitude of the vector
        public double Mag() { return Math.Sqrt(MagSquared()); }

        public double MagSquared() { return Dot(this); }

        //TODO: implement outer product

        // ToString method for printing in tests
        public override String ToString() { 
            return $"<{Get(X)}, {Get(Y)}, {Get(Z)}>"; 
        }

        // returns the normalized vector
        public VectorR3 Unit() { return Sx(1 / Mag()); }

        // public set method
        public void Set(VectorR3 v) {
            Set(X, v.Get(X));
            Set(Y, v.Get(Y));
            Set(Z, v.Get(Z));
        }

        // public interfaces:

        // equality comparator interface
        public class VectorR3EqualityComparer : IEqualityComparer<VectorR3> {
            bool IEqualityComparer<VectorR3>
                .Equals(VectorR3 v1, VectorR3 v2) { return v1.Equals(v2); }

            int IEqualityComparer<VectorR3>
                .GetHashCode(VectorR3 obj) {
                throw new NotImplementedException();
            }
        }
    }
}