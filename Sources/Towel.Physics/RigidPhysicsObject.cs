using Towel.Mathematics;
using Towel.Physics.Shapes;
using static Towel.Syntax;

namespace Towel.Physics
{
    public class RigidPhysicsObject<T>
    {
        private Shape<T> _shape;
        private Material<T> _material;
        private Vector<T> _velocity;
        private Vector<T> _angularVelocity;
        internal bool _isActive;
        internal bool _isStatic;
        internal bool _affectedByGravity;

        public RigidPhysicsObject(
            Shape<T> shape, 
            Material<T> material,
            bool isActive = true,
            bool isStatic = false,
            bool affectedByGravity = true)
        {
            this._shape = shape;
            this._material = material;
            this._isActive = isActive;
            this._isStatic = isStatic;
            this._affectedByGravity = affectedByGravity;
        }

        public Shape<T> Shape { get { return this._shape; } set { this._shape = value; } }
        public Material<T> Material { get { return this._material; } set { this._material = value; } }
        public Vector<T> Velocity { get { return this._velocity; } set { this._velocity = value; } }
        public Vector<T> AngularVelocity { get { return this._angularVelocity; } set { this._angularVelocity = value; } }
        public bool IsActive { get { return this._isActive; } set { this._isActive = value; } }
        public bool IsStatic { get { return this._isStatic; } set { this._isStatic = value; } }
        public bool AffectedByGravity { get { return this._affectedByGravity; } set { this._affectedByGravity = value; } }
        public T Mass { get { return Multiplication(this._material.Density, this._shape.Volume); } }
        public Vector<T> LinearMomentum { get { return this._velocity * this.Mass; } }
        public Vector<T> AngularMomentum { get { return this._angularVelocity * this.Mass; } }
    }
}
