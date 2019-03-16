using Towel.Mathematics;

namespace Towel.Physics
{
    public class Material<T>
    {
        private T _density = Constant<T>.One;
        private T _kineticFriction = Compute.Divide(Compute.FromInt32<T>(3), Compute.FromInt32<T>(10));
        private T _staticFriction = Compute.Divide(Compute.FromInt32<T>(6), Compute.FromInt32<T>(10));
        private T _restitution = Constant<T>.Zero;

        public Material(
            T density,
            T kineticFriction,
            T staticFriction,
            T restitution)
        {
            this._density = density;
            this._kineticFriction = kineticFriction;
            this._staticFriction = staticFriction;
            this._restitution = restitution;
        }

        public T Density { get { return _density; } set { _density = value; } }
        public T Restitution { get { return _restitution; } set { _restitution = value; } }
        public T StaticFriction { get { return _staticFriction; } set { _staticFriction = value; } }
        public T KineticFriction { get { return _kineticFriction; } set { _kineticFriction = value; } }
    }
}
