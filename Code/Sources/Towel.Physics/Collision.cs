using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Towel.Mathematics;

namespace Towel.Physics
{
    public class Collision<T>
    {
        public class Contact
        {
            private T _penetration = Compute.Constant<T>.Zero;
            private T _initialPenetration = Compute.Constant<T>.Zero;
        }

        private RigidPhysicsObject<T> _a;
        private RigidPhysicsObject<T> _b;
        private List<Contact> _contacts;
    }
}
