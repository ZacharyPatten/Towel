using System;
using System.Linq.Expressions;
using System.Reflection;
using Towel.DataStructures;
using System.Linq;
using System.Text.RegularExpressions;

namespace Towel.Mathematics
{
    /// <summary>Contains definitions necessary for the generic Symbolics class.</summary>
    internal static class Symbolics
    {
        #region Attributes
        
        [AttributeUsage(AttributeTargets.Class)]
        internal abstract class RepresentationAttribute : Attribute
        {
            internal string[] _representations;

            internal RepresentationAttribute(string a, params string[] b)
            {
                if (string.IsNullOrWhiteSpace(a))
                {
                    throw new ArgumentException(
                        "There is a BUG in " + nameof(Towel) + ". A " +
                        nameof(Symbolics) + "." + nameof(RepresentationAttribute) + " representation is invalid.");
                }
                foreach (string @string in b)
                {
                    if (string.IsNullOrWhiteSpace(@string))
                    {
                        throw new ArgumentException(
                            "There is a BUG in " + nameof(Towel) + ". A " +
                            nameof(Symbolics) + "." + nameof(RepresentationAttribute) + " representation is invalid.");
                    }
                }
                this._representations = new string[b.Length + 1];
                this._representations[0] = a;
                for (int i = 1, j = 0; j < b.Length + 1; i++, j++)
                {
                    this._representations[i] = b[j];
                }
            }

            internal string[] Representations
            {
                get
                {
                    return this._representations;
                }
            }
        }

        [AttributeUsage(AttributeTargets.Class)]
        internal class OperationAttribute : RepresentationAttribute
        {
            internal OperationAttribute(string a, params string[] b) : base(a, b) { }
        }

        [AttributeUsage(AttributeTargets.Class)]
        internal class LeftUnaryOperatorAttribute : RepresentationAttribute
        {
            internal LeftUnaryOperatorAttribute(string a, params string[] b) : base(a, b) { }
        }

        [AttributeUsage(AttributeTargets.Class)]
        internal class RightUnaryOperatorAttribute : RepresentationAttribute
        {
            internal RightUnaryOperatorAttribute(string a, params string[] b) : base(a, b) { }
        }

        [AttributeUsage(AttributeTargets.Class)]
        internal class BinaryOperatorAttribute : RepresentationAttribute
        {
            internal BinaryOperatorAttribute(string a, params string[] b) : base(a, b) { }
        }

        [AttributeUsage(AttributeTargets.Class)]
        internal class KnownConstantAttribute : RepresentationAttribute
        {
            internal KnownConstantAttribute(string a, params string[] b) : base(a, b) { }
        }
        
        #endregion

        #region Expression + Inheriters

        #region Expression

        public abstract class Expression
        {
            public virtual Expression Simplify()
            {
                return this.Clone();
            }

            public virtual Expression Substitute(string variable, Expression value)
            {
                return this.Clone();
            }

            public virtual Expression Derive(string variable)
            {
                return this.Clone();
            }

            public virtual Expression Integrate(string variable)
            {
                return this.Clone();
            }

            public abstract Expression Clone();

            public override bool Equals(object obj)
            {
                return base.Equals(obj);
            }

            public override int GetHashCode()
            {
                return base.GetHashCode();
            }

            public static Expression operator +(Expression a, Expression b)
            {
                return new Add(a.Clone(), b.Clone());
            }

            public static Expression operator -(Expression a, Expression b)
            {
                return new Subtract(a.Clone(), b.Clone());
            }

            public static Expression operator *(Expression a, Expression b)
            {
                return new Multiply(a.Clone(), b.Clone());
            }

            public static Expression operator /(Expression a, Expression b)
            {
                return new Divide(a.Clone(), b.Clone());
            }

            public static Expression operator ==(Expression a, Expression b)
            {
                return new Equal(a.Clone(), b.Clone());
            }

            public static Expression operator !=(Expression a, Expression b)
            {
                return new NotEqual(a.Clone(), b.Clone());
            }

            public static Expression operator <(Expression a, Expression b)
            {
                return new LessThan(a.Clone(), b.Clone());
            }

            public static Expression operator >(Expression a, Expression b)
            {
                return new GreaterThan(a.Clone(), b.Clone());
            }

            public static Expression operator ^(Expression a, Expression b)
            {
                return new Power(a.Clone(), b.Clone());
            }
        }

        #endregion

        #region Variable

        public class Variable : Expression
        {
            public string _name;

            public string Name { get { return this._name; } }

            public Variable(string name)
            {
                this._name = name;
            }

            public override Expression Clone()
            {
                return new Variable(this.Name);
            }

            public override Expression Substitute(string variable, Expression value)
            {
                if (this.Name == variable)
                {
                    return value.Clone();
                }
                else
                {
                    return base.Substitute(variable, value);
                }
            }

            public override string ToString()
            {
                return "[" + this.Name + "]";
            }

            public static bool operator ==(Variable a, Variable b)
            {
                if (a == null)
                {
                    throw new ArgumentNullException(nameof(a));
                }
                if (b == null)
                {
                    throw new ArgumentNullException(nameof(b));
                }
                return a._name.Equals(b.Name);
            }

            public static bool operator !=(Variable a, Variable b)
            {
                return !(a == b);
            }

            public override bool Equals(object b)
            {
                if (b == null)
                {
                    throw new ArgumentNullException(nameof(b));
                }
                if (b is Variable)
                {
                    return this._name.Equals(b as Variable);
                }
                return false;
            }

            public override int GetHashCode()
            {
                return this._name.GetHashCode();
            }
        }

        #endregion
        
        #region Constant + Inheriters

        #region Constant

        public abstract class Constant : Expression
        {
            public virtual bool IsKnownConstant => false;

            public virtual bool IsZero => false;

            public virtual bool IsOne => false;

            public virtual bool IsTwo => false;

            public virtual bool IsThree => false;

            public virtual bool IsPi => false;

            public virtual Expression Simplify(Operation operation)
            {
                return this;
            }
        }

        #endregion

        #region KnownConstantOfUknownType + Inheriters

        #region KnownConstantOfUknownType

        public abstract class KnownConstantOfUknownType : Constant
        {
            public override bool IsKnownConstant => true;

            public abstract Constant<T> ApplyType<T>();
        }

        #endregion

        #region Pi

        [KnownConstant("π")]
        public class Pi : KnownConstantOfUknownType
        {
            public Pi() : base() { }

            public override bool IsPi => true;

            public override Constant<T> ApplyType<T>()
            {
                return new Constant<T>(Mathematics.Constant<T>.Pi);
            }

            public override Expression Clone()
            {
                return new Pi();
            }

            public override string ToString()
            {
                return "π";
            }
        }

        #endregion

        #region Zero

        public class Zero : KnownConstantOfUknownType
        {
            public Zero() : base() { }

            public override bool IsZero => true;

            public override Constant<T> ApplyType<T>()
            {
                return new Constant<T>(Mathematics.Constant<T>.Zero);
            }

            public override Expression Clone()
            {
                return new Zero();
            }

            public override string ToString()
            {
                return "0";
            }
        }

        #endregion

        #region One

        public class One : KnownConstantOfUknownType
        {
            public One() : base() { }

            public override bool IsOne => true;

            public override Constant<T> ApplyType<T>()
            {
                return new Constant<T>(Mathematics.Constant<T>.One);
            }

            public override Expression Clone()
            {
                return new One();
            }

            public override string ToString()
            {
                return "1";
            }
        }

        #endregion

        #region Two

        public class Two : KnownConstantOfUknownType
        {
            public Two() : base() { }

            public override bool IsTwo => true;

            public override Constant<T> ApplyType<T>()
            {
                return new Constant<T>(Mathematics.Constant<T>.Two);
            }

            public override Expression Clone()
            {
                return new Two();
            }

            public override string ToString()
            {
                return "2";
            }
        }

        #endregion

        #region Three

        public class Three : KnownConstantOfUknownType
        {
            public Three() : base() { }

            public override bool IsThree => true;

            public override Constant<T> ApplyType<T>()
            {
                return new Constant<T>(Mathematics.Constant<T>.Three);
            }

            public override Expression Clone()
            {
                return new Three();
            }

            public override string ToString()
            {
                return "3";
            }
        }

        #endregion

        #endregion

        #region Constant<T> + Inheriters

        #region Constant<T>

        public class Constant<T> : Constant
        {
            public readonly T Value;

            public override bool IsZero => Compute.Equal(Value, Mathematics.Constant<T>.Zero);

            public override bool IsOne => Compute.Equal(Value, Mathematics.Constant<T>.One);

            public override bool IsTwo => Compute.Equal(Value, Mathematics.Constant<T>.Two);

            public override bool IsThree => Compute.Equal(Value, Mathematics.Constant<T>.Three);

            public Constant(T constant)
            {
                this.Value = constant;
            }

            public override Expression Simplify(Operation operation)
            {
                return operation.SimplifyHack<T>();
            }

            public override Expression Clone()
            {
                return new Constant<T>(this.Value);
            }
            
            public override string ToString()
            {
                return this.Value.ToString();
            }
        }

        #endregion

        #region Pi<T>

        public class Pi<T> : Constant<T>
        {
            public Pi() : base(Towel.Mathematics.Constant<T>.Pi) { }

            public override bool IsKnownConstant => true;

            public override Expression Clone()
            {
                return new Pi<T>();
            }

            public override string ToString()
            {
                return "π";
            }
        }

        #endregion

        #region Zero<T>

        public class Zero<T> : Constant<T>
        {
            public Zero() : base(Towel.Mathematics.Constant<T>.Zero) { }

            public override bool IsKnownConstant => true;

            public override Expression Clone()
            {
                return new Zero<T>();
            }

            public override string ToString()
            {
                return "0";
            }
        }

        #endregion

        #region One<T>

        public class One<T> : Constant<T>
        {
            public One() : base(Towel.Mathematics.Constant<T>.One) { }

            public override bool IsKnownConstant => true;

            public override Expression Clone()
            {
                return new One<T>();
            }

            public override string ToString()
            {
                return "1";
            }
        }

        #endregion

        #region Two<T>

        public class Two<T> : Constant<T>
        {
            public Two() : base(Towel.Mathematics.Constant<T>.Two) { }

            public override bool IsTwo => true;

            public override bool IsKnownConstant => true;

            public override Expression Clone()
            {
                return new Two<T>();
            }

            public override string ToString()
            {
                return "2";
            }
        }

        #endregion

        #region Three<T>

        public class Three<T> : Constant<T>
        {
            public Three() : base(Towel.Mathematics.Constant<T>.Three) { }

            public override bool IsKnownConstant => true;

            public override Expression Clone()
            {
                return new Three<T>();
            }

            public override string ToString()
            {
                return "3";
            }
        }

        #endregion

        #region True

        public class True : Constant<bool>
        {
            public True() : base(true) { }

            public override bool IsKnownConstant => true;

            public override Expression Clone()
            {
                return new True();
            }

            public override string ToString()
            {
                return "TRUE";
            }
        }

        #endregion

        #region False

        public class False : Constant<bool>
        {
            public False() : base(true) { }

            public override bool IsKnownConstant => true;

            public override Expression Clone()
            {
                return new False();
            }

            public override string ToString()
            {
                return "FALSE";
            }
        }

        #endregion

        #endregion

        #endregion

        #region Operation + Inheriters

        #region Operation

        public abstract class Operation : Expression
        {
            public interface Mathematical { }

            public interface Logical { }

            protected virtual Expression Simplify<T>()
            {
                return this;
            }

            internal Expression SimplifyHack<T>()
            {
                return this.Simplify<T>();
            }
        }

        #endregion

        #region Unary + Inheriters

        #region Unary

        public abstract class Unary : Operation
        {
            protected Expression _a;
            
            public Expression A
            {
                get { return this._a; }
                set { this._a = value; }
            }

            public Unary(Expression a) : base() { }
        }

        #endregion

        #region Negate

        [LeftUnaryOperator("-")]
        public class Negate : Unary, Operation.Mathematical
        {
            public Negate(Expression a) : base(a) { }

            public override Expression Simplify()
            {
                #region Computation
                // Rule: [-A] => [B] where A is constant and B is -A
                if (this.A is Constant constant)
                {
                    return constant.Simplify(this);
                }
                #endregion
                return base.Simplify();
            }

            protected override Expression Simplify<T>()
            {
                if (this.A is Constant<T> a)
                {
                    return new Constant<T>(Compute.Negate(a.Value));
                }
                return base.Simplify<T>();
            }

            public override Expression Clone()
            {
                return new Negate(this.A.Clone());
            }

            public override string ToString()
            {
                return "-" + this.A;
            }
        }

        #endregion

        #region NaturalLog

        public class NaturalLog : Unary, Operation.Mathematical
        {
            public NaturalLog(Expression operand) : base(operand) { }

            protected override Expression Simplify<T>()
            {
                if (this.A is Constant<T> a)
                {
                    return new Constant<T>(Compute.NaturalLogarithm(a.Value));
                }
                return base.Simplify<T>();
            }

            public override Expression Clone()
            {
                return new NaturalLog(this.A.Clone());
            }

            public override string ToString()
            {
                return "ln(" + this.A + ")";
            }
        }

        #endregion

        #region SquareRoot

        public class SquareRoot : Unary, Operation.Mathematical
        {
            public SquareRoot(Expression operand) : base(operand) { }

            protected override Expression Simplify<T>()
            {
                if (this.A is Constant<T> a)
                {
                    return new Constant<T>(Compute.SquareRoot(a.Value));
                }
                return base.Simplify<T>();
            }

            public override Expression Clone()
            {
                return new SquareRoot(this.A.Clone());
            }

            public override string ToString()
            {
                return "√(" + this.A + ")";
            }
        }

        #endregion

        #region Exponential

        public class Exponential : Unary, Operation.Mathematical
        {
            public Exponential(Expression a) : base(a) { }

            protected override Expression Simplify<T>()
            {
                if (this.A is Constant<T> a)
                {
                    return new Constant<T>(Compute.Exponential(a.Value));
                }
                return base.Simplify<T>();
            }

            public override Expression Clone()
            {
                return new Exponential(this.A.Clone());
            }

            public override string ToString()
            {
                return "e^(" + this.A + ")";
            }
        }

        #endregion

        #region Invert

        public class Invert : Unary, Operation.Mathematical
        {
            public Invert(Expression a) : base(a) { }

            protected override Expression Simplify<T>()
            {
                if (this.A is Constant<T> a)
                {
                    return new Constant<T>(Compute.Invert(a.Value));
                }
                return base.Simplify<T>();
            }

            public override Expression Clone()
            {
                return new Invert(this.A.Clone());
            }

            public override string ToString()
            {
                return "(1 / " + this.A + ")";
            }
        }

        #endregion

        #region Trigonometry _ Inheriters

        #region Trigonometry

        public abstract class Trigonometry : Unary, Operation.Mathematical
        {
            public Trigonometry(Expression a) : base(a) { }
        }

        #endregion

        #region Sine

        public class Sine : Trigonometry, Operation.Mathematical
        {
            public Sine(Expression a) : base(a) { }

            protected override Expression Simplify<T>()
            {
                if (this.A is Constant<T> a)
                {
                    //return new Constant<T>(Compute.Sine(a.Value));
                }
                return base.Simplify<T>();
            }

            public override Expression Clone()
            {
                return new Sine(this.A.Clone());
            }

            public override string ToString()
            {
                return nameof(Sine) + "(" + this.A + ")";
            }
        }

        #endregion

        #region Cosine

        public class Cosine : Trigonometry, Operation.Mathematical
        {
            public Cosine(Expression a) : base(a) { }

            protected override Expression Simplify<T>()
            {
                if (this.A is Constant<T> a)
                {
                    //return new Constant<T>(Compute.Cosine(a.Value));
                }
                return base.Simplify<T>();
            }

            public override Expression Clone()
            {
                return new Cosine(this.A.Clone());
            }

            public override string ToString()
            {
                return nameof(Cosine) + "(" + this.A + ")";
            }
        }

        #endregion

        #region Tangent

        public class Tangent : Trigonometry, Operation.Mathematical
        {
            public Tangent(Expression a) : base(a) { }

            protected override Expression Simplify<T>()
            {
                if (this.A is Constant<T> a)
                {
                    //return new Constant<T>(Compute.Tanget(a.Value));
                }
                return base.Simplify<T>();
            }

            public override Expression Clone()
            {
                return new Tangent(this.A.Clone());
            }

            public override string ToString()
            {
                return nameof(Tangent) + "(" + this.A + ")";
            }
        }

        #endregion

        #region Cosecant

        public class Cosecant : Trigonometry, Operation.Mathematical
        {
            public Cosecant(Expression a) : base(a) { }

            protected override Expression Simplify<T>()
            {
                if (this.A is Constant<T> a)
                {
                    //return new Constant<T>(Compute.Cosecant(a.Value));
                }
                return base.Simplify<T>();
            }

            public override Expression Clone()
            {
                return new Cosecant(this.A.Clone());
            }

            public override string ToString()
            {
                return nameof(Cosecant) + "(" + this.A + ")";
            }
        }

        #endregion

        #region Secant

        public class Secant : Trigonometry, Operation.Mathematical
        {
            public Secant(Expression a) : base(a) { }

            protected override Expression Simplify<T>()
            {
                if (this.A is Constant<T> a)
                {
                    //return new Constant<T>(Compute.Secant(a.Value));
                }
                return base.Simplify<T>();
            }

            public override Expression Clone()
            {
                return new Secant(this.A.Clone());
            }

            public override string ToString()
            {
                return nameof(Secant) + "(" + this.A + ")";
            }
        }

        #endregion

        #region Cotangent

        public class Cotangent : Trigonometry, Operation.Mathematical
        {
            public Cotangent(Expression a) : base(a) { }

            protected override Expression Simplify<T>()
            {
                if (this.A is Constant<T> a)
                {
                    //return new Constant<T>(Compute.Cotangent(a.Value));
                }
                return base.Simplify<T>();
            }

            public override Expression Clone()
            {
                return new Cotangent(this.A.Clone());
            }

            public override string ToString()
            {
                return nameof(Cotangent) + "(" + this.A + ")";
            }
        }

        #endregion

        #endregion

        #endregion

        #region Binary + Inheriters

        #region Binary

        public abstract class Binary : Operation
        {
            protected Expression _a;
            protected Expression _b;

            public Expression A
            {
                get { return this._a; }
                set { this._a = value; }
            }

            public Expression B
            {
                get { return this._b; }
                set { this._b = value; }
            }

            public Binary(Expression a, Expression b)
            {
                this._a = a;
                this._b = b;
            }
        }

        #endregion

        #region AddOrSubtract + Inheriters

        #region AddOrSubtract

        public abstract class AddOrSubtract : Binary, Operation.Mathematical
        {
            public AddOrSubtract(Expression a, Expression b) : base(a, b) { }
        }

        #endregion

        #region Add

        [BinaryOperator("+")]
        public class Add : AddOrSubtract
        {
            public Add(Expression a, Expression b) : base(a, b) { }

            public override Expression Simplify()
            {
                Expression LEFT = this.A.Simplify();
                Expression RIGHT = this.B.Simplify();
                #region Computation
                {   // Rule: [A + B] => [C] where A is constant, B is constant, and C is A + B
                    if (LEFT is Constant A && RIGHT is Constant B)
                    {
                        return A.Simplify(this);
                    }
                }
                #endregion
                #region Additive Identity Property
                {   // Rule: [X + 0] => [X]
                    if (RIGHT is Constant right && right.IsZero)
                    {
                        return LEFT;
                    }
                }
                {   // Rule: [0 + X] => [X]
                    if (LEFT is Constant left && left.IsZero)
                    {
                        return RIGHT;
                    }
                }
                #endregion
                #region Commutative/Associative Property
                {   // Rule: ['X + A' + B] => [X + C] where A is constant, B is constant, and C is A + B
                    if (LEFT is Add ADD && ADD.B is Constant A && RIGHT is Constant B)
                    {
                        var C = (A + B).Simplify();
                        var X = ADD.A;
                        return X + C;
                    }
                }
                {   // Rule: ['A + X' + B] => [X + C] where A is constant, B is constant, and C is A + B
                    if (LEFT is Add ADD && ADD.A is Constant A && RIGHT is Constant B)
                    {
                        var C = (A + B).Simplify();
                        var X = ADD.B;
                        return X + C;
                    }
                }
                {   // Rule: [B + 'X + A'] => [X + C] where A is constant, B is constant, and C is A + B
                    if (RIGHT is Add ADD && ADD.B is Constant A && LEFT is Constant B)
                    {
                        var C = (A + B).Simplify();
                        var X = ADD.A;
                        return X + C;
                    }
                }
                {   // Rule: [B + 'A + X'] => [X + C] where A is constant, B is constant, and C is A + B
                    if (RIGHT is Add ADD && ADD.A is Constant A && LEFT is Constant B)
                    {
                        var C = (A + B).Simplify();
                        var X = ADD.B;
                        return X + C;
                    }
                }
                {   // Rule: ['X - A' + B] => [X + C] where A is constant, B is constant, and C is B - A
                    if (LEFT is Subtract SUBTRACT && SUBTRACT.B is Constant A && RIGHT is Constant B)
                    {
                        var C = (B - A).Simplify();
                        var X = SUBTRACT.A;
                        return X + C;
                    }
                }
                {   // Rule: ['A - X' + B] => [C - X] where A is constant, B is constant, and C is A + B
                    if (LEFT is Subtract SUBTRACT && SUBTRACT.A is Constant A && RIGHT is Constant B)
                    {
                        var C = (A + B).Simplify();
                        var X = SUBTRACT.B;
                        return C - X;
                    }
                }
                {   // Rule: [B + 'X - A'] => [X + C] where A is constant, B is constant, and C is B - A
                    if (RIGHT is Subtract SUBTRACT && SUBTRACT.B is Constant A && LEFT is Constant B)
                    {
                        var C = (B - A).Simplify();
                        var X = SUBTRACT.A;
                        return C + X;
                    }
                }
                {   // Rule: [B + 'A - X'] => [C - X] where A is constant, B is constant, and C is A + B
                    if (RIGHT is Subtract SUBTRACT && SUBTRACT.A is Constant A && LEFT is Constant B)
                    {
                        var C = (A + B).Simplify();
                        var X = SUBTRACT.B;
                        return C - X;
                    }
                }
                #endregion
                return base.Simplify();
            }

            protected override Expression Simplify<T>()
            {
                if (this.A is Constant<T> a && this.B is Constant<T> b)
                {
                    return new Constant<T>(Compute.Add(a.Value, b.Value));
                }
                return base.Simplify<T>();
            }

            public override Expression Clone()
            {
                return new Add(this.A.Clone(), this.B.Clone());
            }

            public override string ToString()
            {
                string a = this.A.ToString();
                string b = this.B.ToString();
                if (this.A is Multiply || this.A is Divide && this.A is Constant && Compute.IsNegative(this.A as Constant))
                {
                    a = "(" + a + ")";
                }
                if (this.B is Add || this.B is Subtract || this.A is Multiply || this.A is Divide)
                {
                    b = "(" + b + ")";
                }
                if (this.B is Constant && Compute.IsNegative(this.B as Constant))
                {
                    return a + " - " + Compute.Negate(this.B as Constant);
                }
                return a + " + " + b;
            }
        }

        #endregion

        #region Subtract

        [BinaryOperator("-")]
        public class Subtract : AddOrSubtract
        {
            public Subtract(Expression a, Expression b) : base(a, b) { }

            public override Expression Simplify()
            {
                Expression LEFT = this.A.Simplify();
                Expression RIGHT = this.B.Simplify();
                #region Computation
                {   // Rule: [A - B] => [C] where A is constant, B is constant, and C is A - B
                    if (LEFT is Constant left && RIGHT is Constant right)
                    {
                        return left.Simplify(this);
                    }
                }
                #endregion
                #region Identity Property
                {   // Rule: [X - 0] => [X]
                    if (RIGHT is Constant right && right.IsZero)
                    {
                        return LEFT;
                    }
                }
                {   // Rule: [0 - X] => [-X]
                    if (LEFT is Constant left && left.IsZero)
                    {
                        return new Negate(RIGHT);
                    }
                }
                #endregion
                #region Commutative/Associative Property
                {   // Rule: ['X - A' - B] => [X - C] where A is constant, B is constant, and C is A + B
                    if (LEFT is Subtract SUBTRACT && SUBTRACT.B is Constant A && RIGHT is Constant B)
                    {
                        var C = (A + B).Simplify();
                        var X = SUBTRACT.A;
                        return X - C;
                    }
                }
                {    // Rule: ['A - X' - B] => [C - X] where A is constant, B is constant, and C is A - B
                    if (LEFT is Subtract SUBTRACT && SUBTRACT.A is Constant A && RIGHT is Constant B)
                    {
                        var C = (A - B).Simplify();
                        var X = SUBTRACT.B;
                        return C - X;
                    }
                }
                {   // Rule: [B - 'X - A'] => [C - X] where A is constant, B is constant, and C is B - A
                    if (RIGHT is Subtract SUBTRACT && SUBTRACT.B is Constant A && LEFT is Constant B)
                    {
                        var C = (B - A).Simplify();
                        var X = SUBTRACT.A;
                        return C - X;
                    }
                }
                {   // Rule: [B - 'A - X'] => [C - X] where A is constant, B is constant, and C is B - A
                    if (RIGHT is Subtract SUBTRACT && SUBTRACT.A is Constant A && LEFT is Constant B)
                    {
                        var C = (B - A).Simplify();
                        var X = SUBTRACT.A;
                        return C - X;
                    }
                }
                {   // Rule: ['X + A' - B] => [X + C] where A is constant, B is constant, and C is A - B
                    if (LEFT is Add ADD && ADD.B is Constant A && RIGHT is Constant B)
                    {
                        var C = (A - B).Simplify();
                        var X = ADD.A;
                        return X + C;
                    }
                }
                {   // Rule: ['A + X' - B] => [C + X] where A is constant, B is constant, and C is A - B
                    if (LEFT is Add ADD && ADD.A is Constant A && RIGHT is Constant B)
                    {
                        var C = (A - B).Simplify();
                        var X = ADD.B;
                        return C + X;
                    }
                }
                {   // Rule: [B - 'X + A'] => [C - X] where A is constant, B is constant, and C is A + B
                    if (RIGHT is Add ADD && ADD.B is Constant A && LEFT is Constant B)
                    {
                        var C = (A + B).Simplify();
                        var X = ADD.A;
                        return C - X;
                    }
                }
                {   // Rule: [B - 'A + X'] => [C + X] where A is constant, B is constant, and C is B - A
                    if (RIGHT is Add ADD && ADD.A is Constant A && LEFT is Constant B)
                    {
                        var C = (B - A).Simplify();
                        var X = ADD.B;
                        return C + X;
                    }
                }
                #endregion
                return base.Simplify();
            }

            protected override Expression Simplify<T>()
            {
                if (this.A is Constant<T> a && this.B is Constant<T> b)
                {
                    return new Constant<T>(Compute.Subtract(a.Value, b.Value));
                }
                return base.Simplify<T>();
            }

            public override Expression Clone()
            {
                return new Subtract(this.A.Clone(), this.B.Clone());
            }

            public override string ToString()
            {
                string a = this.A.ToString();
                if (this.A is Multiply || this.A is Divide)
                {
                    a = "(" + a + ")";
                }
                string b = this.B.ToString();
                if (this.B is Add || this.B is Subtract || this.A is Multiply || this.A is Divide)
                {
                    b = "(" + b + ")";
                }
                return a + " - " + b;
            }
        }

        #endregion

        #endregion

        #region MultiplyOrDivide + Inheriters

        #region MultiplyOrDivide

        public abstract class MultiplyOrDivide : Binary, Operation.Mathematical
        {
            public MultiplyOrDivide(Expression a, Expression b) : base(a, b) { }
        }

        #endregion

        #region Multiply

        [BinaryOperator("*")]
        public class Multiply : MultiplyOrDivide
        {
            public Multiply(Expression a, Expression b) : base(a, b) { }

            public override Expression Clone()
            {
                return new Multiply(this.A.Clone(), this.B.Clone());
            }

            public override Expression Simplify()
            {
                Expression LEFT = this.A.Simplify();
                Expression RIGHT = this.B.Simplify();
                #region Computation
                {   // Rule: [A * B] => [C] where A is constant, B is constant, and C is A * B
                    if (LEFT is Constant A && RIGHT is Constant B)
                    {
                        return A.Simplify(this);
                    }
                }
                #endregion
                #region Zero Property
                {   // Rule: [X * 0] => [0]
                    if (RIGHT is Constant CONSTANT && CONSTANT.IsZero)
                    {
                        return CONSTANT;
                    }
                }
                {   // Rule: [0 * X] => [0]
                    if (LEFT is Constant CONSTANT && CONSTANT.IsZero)
                    {
                        return CONSTANT;
                    }
                }
                #endregion
                #region Identity Property
                {   // Rule: [X * 1] => [X]
                    if (RIGHT is Constant CONSTANT && CONSTANT.IsOne)
                    {
                        return LEFT;
                    }
                }
                {   // Rule: [1 * X] => [X]
                    if (LEFT is Constant CONSTANT && CONSTANT.IsOne)
                    {
                        return RIGHT;
                    }
                }
                #endregion
                #region Commutative/Associative Property
                {   // Rule: [(X * A) * B] => [X * C] where A is constant, B is constant, and C is A * B
                    if (LEFT is Multiply MULTIPLY && MULTIPLY.B is Constant A && RIGHT is Constant B)
                    {
                        var C = (A * B).Simplify();
                        var X = MULTIPLY.A;
                        return X * C;
                    }
                }
                {   // Rule: [(A * X) * B] => [X * C] where A is constant, B is constant, and C is A * B
                    if (LEFT is Multiply MULTIPLY && MULTIPLY.A is Constant A && RIGHT is Constant B)
                    {
                        var C = (A * B).Simplify();
                        var X = MULTIPLY.B;
                        return X * C;
                    }
                }
                {   // Rule: [B * (X * A)] => [X * C] where A is constant, B is constant, and C is A * B
                    if (RIGHT is Multiply MULTIPLY && MULTIPLY.B is Constant A && LEFT is Constant B)
                    {
                        var C = (A * B).Simplify();
                        var X = MULTIPLY.A;
                        return X * C;
                    }
                }
                {   // Rule: [B * (A * X)] => [X * C] where A is constant, B is constant, and C is A * B
                    if (RIGHT is Multiply MULTIPLY && MULTIPLY.A is Constant A && LEFT is Constant B)
                    {
                        var C = (A * B).Simplify();
                        var X = MULTIPLY.B;
                        return X * C;
                    }
                }
                {   // Rule: [(X / A) * B] => [X * C] where A is constant, B is constant, and C is B / A
                    if (LEFT is Divide DIVIDE && DIVIDE.B is Constant A && RIGHT is Constant B)
                    {
                        var C = (B / A).Simplify();
                        var X = DIVIDE.A;
                        return X * C;
                    }
                }
                {   // Rule: [(A / X) * B] => [C / X] where A is constant, B is constant, and C is A * B
                    if (LEFT is Divide DIVIDE && DIVIDE.A is Constant A && RIGHT is Constant B)
                    {
                        var C = (A * B).Simplify();
                        var X = DIVIDE.B;
                        return C / X;
                    }
                }
                {   // Rule: [B * (X / A)] => [X * C] where A is constant, B is constant, and C is B / A
                    if (RIGHT is Divide DIVIDE && DIVIDE.B is Constant A && LEFT is Constant B)
                    {
                        var C = (B / A).Simplify();
                        var X = DIVIDE.A;
                        return X * C;
                    }
                }
                {   // Rule: [B * (A / X)] => [C / X] where A is constant, B is constant, and C is A * B
                    if (RIGHT is Divide DIVIDE && DIVIDE.A is Constant A && LEFT is Constant B)
                    {
                        var C = (A * B).Simplify();
                        var X = DIVIDE.B;
                        return C / X;
                    }
                }
                #endregion
                #region Distributive Property
                {   // Rule: [X * (A +/- B)] => [X * A + X * B] where X is Variable
                    if ((LEFT is Variable VARIABLE && RIGHT is AddOrSubtract ADDORSUBTRACT))
                    {
                        // This might not be necessary
                    }
                }
                {   // Rule: [(A +/- B) * X] => [X * A + X * B] where X is Variable
                    if ((RIGHT is Variable VARIABLE && LEFT is AddOrSubtract ADDORSUBTRACT))
                    {
                        // This might not be necessary
                    }
                }
                #endregion
                #region Duplicate Variable Multiplications
                {   // Rule: [X * X] => [X ^ 2] where X is Variable
                    if (LEFT is Variable X1 && RIGHT is Variable X2 && X1.Name == X2.Name)
                    {
                        return X1 ^ new Two();
                    }
                }
                #endregion
                #region Multiplication With Powered Variables
                {   // Rule: [(V ^ A) * (V ^ B)] => [V ^ C] where A is constant, B is constant, V is a variable, and C is A + B
                    if (LEFT is Power POWER1 && RIGHT is Power POWER2 &&
                        POWER1.A is Variable V1 && POWER2.A is Variable V2 && V1.Name == V2.Name &&
                        POWER1.B is Constant A && POWER2.B is Constant B)
                    {
                        var C = (A + B).Simplify();
                        return V1 ^ C;
                    }
                }
                #endregion
                return base.Simplify();
            }

            protected override Expression Simplify<T>()
            {
                if (this.A is Constant<T> a && this.B is Constant<T> b)
                {
                    return new Constant<T>(Compute.Multiply(a.Value, b.Value));
                }
                return base.Simplify<T>();
            }

            public override string ToString()
            {
                string a = this.A.ToString();
                string b = this.B.ToString();
                if (this.B is Multiply || this.B is Divide)
                {
                    b = "(" + b + ")";
                }
                else if (this.A is Constant a_const && a_const.IsKnownConstant && this.B is Constant)
                {
                    return b + a;
                }
                else if (this.A is Constant && this.B is Constant b_const && b_const.IsKnownConstant)
                {
                    return a + b;
                }
                return a + " * " + b;
            }
        }

        #endregion

        #region Divide

        [BinaryOperator("/")]
        public class Divide : MultiplyOrDivide
        {
            public Divide(Expression a, Expression b) : base(a, b) { }

            public override Expression Simplify()
            {
                Expression LEFT = this.A.Simplify();
                Expression RIGHT = this.B.Simplify();
                #region Error Handling
                {   // Rule: [X / 0] => Error
                    if (RIGHT is Constant CONSTANT && CONSTANT.IsZero)
                    {
                        throw new DivideByZeroException();
                    }
                }
                #endregion
                #region Computation
                {   // Rule: [A / B] => [C] where A is constant, B is constant, and C is A / B
                    if (LEFT is Constant A && RIGHT is Constant B)
                    {
                        return A.Simplify(this);
                    }
                }
                #endregion
                #region Zero Property
                {   // Rule: [0 / X] => [0]
                    if (LEFT is Constant CONSTANT && CONSTANT.IsZero)
                    {
                        return CONSTANT;
                    }
                }
                #endregion
                #region Identity Property
                {   // Rule: [X / 1] => [X]
                    if (RIGHT is Constant CONSTANT && CONSTANT.IsOne)
                    {
                        return LEFT;
                    }
                }
                #endregion
                #region Commutative/Associative Property
                {   // Rule: [(X / A) / B] => [X / C] where A is constant, B is constant, and C is A * B
                    if (LEFT is Divide DIVIDE && DIVIDE.B is Constant A && RIGHT is Constant B)
                    {
                        var C = (A * B).Simplify();
                        var X = DIVIDE.A;
                        return X / C;
                    }
                }
                {   // Rule: [(A / X) / B] => [C / X] where A is constant, B is constant, and C is A / B
                    if (LEFT is Divide DIVIDE && DIVIDE.A is Constant A && RIGHT is Constant B)
                    {
                        var C = (A / B).Simplify();
                        var X = DIVIDE.B;
                        return C / X;
                    }
                }
                {   // Rule: [B / (X / A)] => [C / X] where A is constant, B is constant, and C is B / A
                    if (RIGHT is Divide DIVIDE && DIVIDE.B is Constant A && LEFT is Constant B)
                    {
                        var C = (B / A).Simplify();
                        var X = DIVIDE.A;
                        return C / X;
                    }
                }
                {   // Rule: [B / (A / X)] => [C / X] where A is constant, B is constant, and C is B / A
                    if (RIGHT is Divide DIVIDE && DIVIDE.A is Constant A && LEFT is Constant B)
                    {
                        var C = (B / A).Simplify();
                        var X = DIVIDE.B;
                        return C / X;
                    }
                }
                {   // Rule: [(X * A) / B] => [X * C] where A is constant, B is constant, and C is A / B
                    if (LEFT is Multiply MULTIPLY && MULTIPLY.B is Constant A && RIGHT is Constant B)
                    {
                        var C = (A / B).Simplify();
                        var X = MULTIPLY.A;
                        return X * C;
                    }
                }
                {   // Rule: [(A * X) / B] => [X * C] where A is constant, B is constant, and C is A / B
                    if (LEFT is Multiply MULTIPLY && MULTIPLY.A is Constant A && RIGHT is Constant B)
                    {
                        var C = (A / B).Simplify();
                        var X = MULTIPLY.B;
                        return X * C;
                    }
                }
                {   // Rule: [B / (X * A)] => [C / X] where A is constant, B is constant, and C is A * B
                    if (RIGHT is Multiply MULTIPLY && MULTIPLY.B is Constant A && LEFT is Constant B)
                    {
                        var C = (A * B).Simplify();
                        var X = MULTIPLY.A;
                        return C / X;
                    }
                }
                {   // Rule: [B / (A * X)] => [X * C] where A is constant, B is constant, and C is B / A
                    if (RIGHT is Multiply MULTIPLY && MULTIPLY.A is Constant A && LEFT is Constant B)
                    {
                        var C = (B / A).Simplify();
                        var X = MULTIPLY.B;
                        return X * C;
                    }
                }
                #endregion
                #region Distributive Property
                {   // Rule: [X / (A +/- B)] => [X / A + X / B] where where A is constant, B is constant, and X is Variable
                    if ((LEFT is Variable VARIABLE && RIGHT is AddOrSubtract ADDORSUBTRACT))
                    {
                        // This might not be necessary
                    }
                }
                {   // Rule: [(A +/- B) / X] => [(A / X) + (B / X)] where where A is constant, B is constant, and X is Variable
                    if ((RIGHT is Variable VARIABLE && LEFT is AddOrSubtract ADDORSUBTRACT))
                    {
                        // This might not be necessary
                    }
                }
                #endregion
                #region Division With Powered Variables
                {   // Rule: [(V ^ A) / (V ^ B)] => [V ^ C] where A is constant, B is constant, V is a variable, and C is A - B
                    if (LEFT is Power POWER1 && RIGHT is Power POWER2 &&
                        POWER1.A is Variable V1 && POWER2.A is Variable V2 && V1.Name == V2.Name &&
                        POWER1.B is Constant A && POWER2.B is Constant B)
                    {
                        var C = (A - B).Simplify();
                        return V1 ^ C;
                    }
                }
                #endregion
                return base.Simplify();
            }

            protected override Expression Simplify<T>()
            {
                if (this.A is Constant<T> a && this.B is Constant<T> b)
                {
                    return new Constant<T>(Compute.Divide(a.Value, b.Value));
                }
                return base.Simplify<T>();
            }

            public override Expression Clone()
            {
                return new Divide(this.A.Clone(), this.B.Clone());
            }

            public override string ToString()
            {
                string a = this.A.ToString();
                string b = this.B.ToString();
                if (this.B is Multiply || this.B is Divide)
                {
                    b = "(" + b + ")";
                }
                return a + " / " + b;
            }
        }

        #endregion

        #endregion

        #region Power

        [BinaryOperator("^")]
        public class Power : Binary, Operation.Mathematical
        {
            public Power(Expression a, Expression b) : base(a, b) { }

            public override Expression Simplify()
            {
                Expression LEFT = this.A.Simplify();
                Expression RIGHT = this.B.Simplify();
                #region Computation
                {   // Rule: [A ^ B] => [C] where A is constant, B is constant, and C is A ^ B
                    if (LEFT is Constant A && RIGHT is Constant B)
                    {
                        return A.Simplify(this);
                    }
                }
                #endregion
                #region Zero Base
                {   // Rule: [0 ^ X] => [0]
                    if (LEFT is Constant CONSTANT && CONSTANT.IsZero)
                    {
                        return CONSTANT;
                    }
                }
                #endregion
                #region One Power
                {   // Rule: [X ^ 1] => [X]
                    if (RIGHT is Constant CONSTANT && CONSTANT.IsOne)
                    {
                        return LEFT;
                    }
                }
                #endregion
                #region Zero Power
                {   // Rule: [X ^ 0] => [1]
                    if (RIGHT is Constant CONSTANT && CONSTANT.IsZero)
                    {
                        return new One();
                    }
                }
                #endregion
                return base.Simplify();
            }

            protected override Expression Simplify<T>()
            {
                if (this.A is Constant<T> a && this.B is Constant<T> b)
                {
                    return new Constant<T>(Compute.Power(a.Value, b.Value));
                }
                return base.Simplify<T>();
            }

            public override Expression Clone()
            {
                return new Power(this.A.Clone(), this.B.Clone());
            }

            public override string ToString()
            {
                return this.A + " ^ " + this.B;
            }
        }

        #endregion

        #region Root

        public class Root : Binary, Operation.Mathematical
        {
            public Root(Expression a, Expression b) : base(a, b) { }

            protected override Expression Simplify<T>()
            {
                if (this.A is Constant<T> a && this.B is Constant<T> b)
                {
                    return new Constant<T>(Compute.Root(a.Value, b.Value));
                }
                return base.Simplify<T>();
            }

            public override Expression Clone()
            {
                return new Root(this.A.Clone(), this.B.Clone());
            }

            public override string ToString()
            {
                return this.A + " ^ (1 / " + this.B + ")";
            }
        }

        #endregion

        #region Equal

        [BinaryOperator("=")]
        public class Equal : Binary, Operation.Logical
        {
            public Equal(Expression a, Expression b) : base(a, b) { }

            protected override Expression Simplify<T>()
            {
                if (this.A is Constant<T> a && this.B is Constant<T> b)
                {
                    return new Constant<bool>(Compute.Equal(a.Value, b.Value));
                }
                return base.Simplify<T>();
            }

            public override Expression Clone()
            {
                return new Equal(this.A.Clone(), this.B.Clone());
            }

            public override string ToString()
            {
                return this.A + " = " + this.B;
            }
        }

        #endregion

        #region NotEqual

        [BinaryOperator("≠")]
        public class NotEqual : Binary, Operation.Logical
        {
            public NotEqual(Expression a, Expression b) : base(a, b) { }

            protected override Expression Simplify<T>()
            {
                if (this.A is Constant<T> a && this.B is Constant<T> b)
                {
                    return new Constant<bool>(Compute.NotEqual(a.Value, b.Value));
                }
                return base.Simplify<T>();
            }

            public override Expression Clone()
            {
                return new NotEqual(this.A.Clone(), this.B.Clone());
            }

            public override string ToString()
            {
                return this.A + " ≠ " + this.B;
            }
        }

        #endregion

        #region LessThan

        [BinaryOperator("<")]
        public class LessThan : Binary, Operation.Logical
        {
            public LessThan(Expression a, Expression b) : base(a, b) { }

            protected override Expression Simplify<T>()
            {
                if (this.A is Constant<T> a && this.B is Constant<T> b)
                {
                    return new Constant<bool>(Compute.LessThan(a.Value, b.Value));
                }
                return base.Simplify<T>();
            }

            public override Expression Clone()
            {
                return new LessThan(this.A.Clone(), this.B.Clone());
            }

            public override string ToString() { return this.A + " < " + this.B; }
        }

        #endregion

        #region GreaterThan

        [BinaryOperator(">")]
        public class GreaterThan : Binary, Operation.Logical
        {
            public GreaterThan(Expression left, Expression right) : base(left, right) { }

            protected override Expression Simplify<T>()
            {
                if (this.A is Constant<T> a && this.B is Constant<T> b)
                {
                    return new Constant<bool>(Compute.GreaterThan(a.Value, b.Value));
                }
                return base.Simplify<T>();
            }

            public override Expression Clone()
            {
                return new GreaterThan(this.A.Clone(), this.B.Clone());
            }

            public override string ToString() { return this.A + " < " + this.B; }
        }

        #endregion
        
        #region LessThanOrEqual

        [BinaryOperator("<=")]
        public class LessThanOrEqual : Binary, Operation.Logical
        {
            public LessThanOrEqual(Expression left, Expression right) : base(left, right) { }

            protected override Expression Simplify<T>()
            {
                if (this.A is Constant<T> a && this.B is Constant<T> b)
                {
                    return new Constant<bool>(Compute.LessThanOrEqual(a.Value, b.Value));
                }
                return base.Simplify<T>();
            }

            public override Expression Clone()
            {
                return new LessThanOrEqual(this.A.Clone(), this.B.Clone());
            }

            public override string ToString() { return this.A + " < " + this.B; }
        }

        #endregion

        #region GreaterThanOrEqual

        [BinaryOperator(">=")]
        public class GreaterThanOrEqual : Binary, Operation.Logical
        {
            public GreaterThanOrEqual(Expression left, Expression right) : base(left, right) { }

            protected override Expression Simplify<T>()
            {
                if (this.A is Constant<T> a && this.B is Constant<T> b)
                {
                    return new Constant<bool>(Compute.GreaterThanOrEqual(a.Value, b.Value));
                }
                return base.Simplify<T>();
            }

            public override Expression Clone()
            {
                return new GreaterThanOrEqual(this.A.Clone(), this.B.Clone());
            }

            public override string ToString() { return this.A + " < " + this.B; }
        }

        #endregion

        #endregion

        #region Ternary + Inheriters

        #region Ternary

        public abstract class Ternary : Operation
        {
            protected Expression _a;
            protected Expression _b;
            protected Expression _c;

            public Expression A
            {
                get { return this._a; }
                set { this._a = value; }
            }

            public Expression B
            {
                get { return this._b; }
                set { this._b = value; }
            }

            public Expression C
            {
                get { return this._c; }
                set { this._c = value; }
            }

            public Ternary() { }

            public Ternary(Expression a, Expression b, Expression c)
            {
                this._a = a;
                this._b = b;
                this._c = c;
            }
        }

        #endregion

        #endregion

        #region Multinary + Inheriters

        #region Multinary

        public abstract class Multinary : Operation
        {
            protected Expression[] _operands;

            public Expression[] Operands
            {
                get { return this._operands; }
                set { this._operands = value; }
            }

            public Multinary() { }

            public Multinary(Expression[] operands)
            {
                this._operands = operands;
            }
        }

        #endregion

        #endregion

        #endregion

        #endregion

        #region Parsers

        // Parsing is easy :) we just use reflection to convert the Expression class hierarchy (with attributes)
        // into a parsing library of strings to match against.

        #region Runtime Built Parsing Libary

        private static bool PARSABLELIBRARYBUILT = false; // we only ever need to build the library with reflection once

        private static object PARSABLELIBRARYLOCK = new object();

        // operations
        private static System.Collections.Generic.Dictionary<string, Func<Expression, Unary>> ParsableUnaryOperations;
        private static System.Collections.Generic.Dictionary<string, Func<Expression, Expression, Binary>> ParsableBinaryOperations;
        private static System.Collections.Generic.Dictionary<string, Func<Expression, Expression, Expression, Ternary>> ParsableTernaryOperations;
        private static System.Collections.Generic.Dictionary<string, Func<Expression[], Multinary>> ParsableMultinaryOperations;

        // operators
        private static System.Collections.Generic.Dictionary<string, Func<Expression, Unary>> ParsableLeftUnaryOperators;
        private static System.Collections.Generic.Dictionary<string, Func<Expression, Unary>> ParsableRightUnaryOperators;
        private static System.Collections.Generic.Dictionary<string, Func<Expression, Expression, Binary>> ParsableBinaryOperators;
        private static string[] ParsableOperators;

        // known constants
        private static System.Collections.Generic.Dictionary<string, Func<Constant>> PARSABLEKNOWNCONSTANTS;

        #region Reflection Code (Actually Building the Parsing Library)

        internal static void BuildParsableOperationLibrary()
        {
            lock (PARSABLELIBRARYLOCK)
            {
                if (PARSABLELIBRARYBUILT)
                {
                    return;
                }

                // Unary Operations
                ParsableUnaryOperations = new System.Collections.Generic.Dictionary<string, Func<Expression, Unary>>();
                foreach (Type type in Assembly.GetExecutingAssembly().GetDerivedTypes<Unary>().Where(x => !x.IsAbstract))
                {
                    ConstructorInfo constructorInfo = type.GetConstructor(new Type[] { typeof(Expression) });
                    ParameterExpression A = System.Linq.Expressions.Expression.Parameter(typeof(Expression));
                    NewExpression newExpression = System.Linq.Expressions.Expression.New(constructorInfo, A);
                    Func<Expression, Unary> newFunction = System.Linq.Expressions.Expression.Lambda<Func<Expression, Unary>>(newExpression, A).Compile();
                    string operationName = type.ConvertToCsharpSource();
                    ParsableUnaryOperations.Add(operationName, newFunction);
                    foreach (string representation in type.GetCustomAttribute<OperationAttribute>().Representations)
                    {
                        ParsableUnaryOperations.Add(representation, newFunction);
                    }

                    // Left Unary Operators
                    ParsableLeftUnaryOperators = new System.Collections.Generic.Dictionary<string, Func<Expression, Unary>>();
                    foreach (string representation in type.GetCustomAttribute<LeftUnaryOperatorAttribute>().Representations)
                    {
                        ParsableLeftUnaryOperators.Add(representation, newFunction);
                    }

                    // Right Unary Operators
                    ParsableRightUnaryOperators = new System.Collections.Generic.Dictionary<string, Func<Expression, Unary>>();
                    foreach (string representation in type.GetCustomAttribute<RightUnaryOperatorAttribute>().Representations)
                    {
                        ParsableRightUnaryOperators.Add(representation, newFunction);
                    }
                }

                // Binary Operations
                ParsableBinaryOperations = new System.Collections.Generic.Dictionary<string, Func<Expression, Expression, Binary>>();
                foreach (Type type in Assembly.GetExecutingAssembly().GetDerivedTypes<Binary>().Where(x => !x.IsAbstract))
                {
                    ConstructorInfo constructorInfo = type.GetConstructor(new Type[] { typeof(Expression), typeof(Expression) });
                    ParameterExpression A = System.Linq.Expressions.Expression.Parameter(typeof(Expression));
                    ParameterExpression B = System.Linq.Expressions.Expression.Parameter(typeof(Expression));
                    NewExpression newExpression = System.Linq.Expressions.Expression.New(constructorInfo, A, B);
                    Func<Expression, Expression, Binary> newFunction = System.Linq.Expressions.Expression.Lambda<Func<Expression, Expression, Binary>>(newExpression, A).Compile();
                    string operationName = type.ConvertToCsharpSource();
                    ParsableBinaryOperations.Add(operationName, newFunction);
                    foreach (string representation in type.GetCustomAttribute<OperationAttribute>().Representations)
                    {
                        ParsableBinaryOperations.Add(representation, newFunction);
                    }

                    // Binary Operators
                    ParsableBinaryOperators = new System.Collections.Generic.Dictionary<string, Func<Expression, Expression, Binary>>();
                    foreach (string representation in type.GetCustomAttribute<BinaryOperatorAttribute>().Representations)
                    {
                        ParsableBinaryOperators.Add(representation, newFunction);
                    }
                }

                // Ternary Operations
                ParsableTernaryOperations = new System.Collections.Generic.Dictionary<string, Func<Expression, Expression, Expression, Ternary>>();
                foreach (Type type in Assembly.GetExecutingAssembly().GetDerivedTypes<Ternary>().Where(x => !x.IsAbstract))
                {
                    ConstructorInfo constructorInfo = type.GetConstructor(new Type[] { typeof(Expression), typeof(Expression), typeof(Expression) });
                    ParameterExpression A = System.Linq.Expressions.Expression.Parameter(typeof(Expression));
                    ParameterExpression B = System.Linq.Expressions.Expression.Parameter(typeof(Expression));
                    ParameterExpression C = System.Linq.Expressions.Expression.Parameter(typeof(Expression));
                    NewExpression newExpression = System.Linq.Expressions.Expression.New(constructorInfo, A, B, C);
                    Func<Expression, Expression, Expression, Ternary> newFunction = System.Linq.Expressions.Expression.Lambda<Func<Expression, Expression, Expression, Ternary>>(newExpression, A).Compile();
                    string operationName = type.ConvertToCsharpSource();
                    ParsableTernaryOperations.Add(operationName, newFunction);
                    foreach (string representation in type.GetCustomAttribute<OperationAttribute>().Representations)
                    {
                        ParsableTernaryOperations.Add(representation, newFunction);
                    }
                }

                // Multinary Operations
                ParsableMultinaryOperations = new System.Collections.Generic.Dictionary<string, Func<Expression[], Multinary>>();
                foreach (Type type in Assembly.GetExecutingAssembly().GetDerivedTypes<Multinary>().Where(x => !x.IsAbstract))
                {
                    ConstructorInfo constructorInfo = type.GetConstructor(new Type[] { typeof(Expression[]) });
                    ParameterExpression A = System.Linq.Expressions.Expression.Parameter(typeof(Expression[]));
                    NewExpression newExpression = System.Linq.Expressions.Expression.New(constructorInfo, A);
                    Func<Expression[], Multinary> newFunction = System.Linq.Expressions.Expression.Lambda<Func<Expression[], Multinary>>(newExpression, A).Compile();
                    string operationName = type.ConvertToCsharpSource();
                    ParsableMultinaryOperations.Add(operationName, newFunction);
                    foreach (string representation in type.GetCustomAttribute<OperationAttribute>().Representations)
                    {
                        ParsableMultinaryOperations.Add(representation, newFunction);
                    }
                }

                // Known Constants
                PARSABLEKNOWNCONSTANTS = new System.Collections.Generic.Dictionary<string, Func<Constant>>();
                foreach (Type type in Assembly.GetExecutingAssembly().GetDerivedTypes<KnownConstantAttribute>().Where(x => !x.IsAbstract))
                {
                    ConstructorInfo constructorInfo = type.GetConstructor(Type.EmptyTypes);
                    NewExpression newExpression = System.Linq.Expressions.Expression.New(constructorInfo);
                    Func<Constant> newFunction = System.Linq.Expressions.Expression.Lambda<Func<Constant>>(newExpression).Compile();
                    string operationName = type.ConvertToCsharpSource();
                    PARSABLEKNOWNCONSTANTS.Add(operationName, newFunction);
                    foreach (string representation in type.GetCustomAttribute<KnownConstantAttribute>().Representations)
                    {
                        PARSABLEKNOWNCONSTANTS.Add(representation, newFunction);
                    }
                }

                PARSABLELIBRARYBUILT = true;
            }
        }

        #endregion

        #endregion

        #region System.Linq.Expression

        public static Expression Parse(System.Linq.Expressions.Expression e)
        {
            try
            {
                System.Func<System.Linq.Expressions.Expression, Expression> recursive = null;
                System.Func<MethodCallExpression, Expression> methodCallExpression_to_node = null;

                recursive =
                    (System.Linq.Expressions.Expression expression) =>
                    {
                        UnaryExpression unary_expression = expression as UnaryExpression;
                        BinaryExpression binary_expression = expression as BinaryExpression;

                        switch (expression.NodeType)
                        {
                            // Lambda
                            case ExpressionType.Lambda:
                                //labmda_expression.Parameters
                                return recursive((expression as LambdaExpression).Body);
                            // constant
                            case ExpressionType.Constant:
                                return new Constant<T>((T)(expression as ConstantExpression).Value);
                            // variable
                            case ExpressionType.Parameter:
                                return new Variable((expression as ParameterExpression).Name);
                            // unary
                            case ExpressionType.Negate:
                                return new Negate(recursive(unary_expression.Operand));
                            case ExpressionType.UnaryPlus:
                                return recursive(unary_expression.Operand);
                            // binary
                            case ExpressionType.Add:
                                return new Add(recursive(binary_expression.Left), recursive(binary_expression.Right));
                            case ExpressionType.Subtract:
                                return new Subtract(recursive(binary_expression.Left), recursive(binary_expression.Right));
                            case ExpressionType.Multiply:
                                return new Multiply(recursive(binary_expression.Left), recursive(binary_expression.Right));
                            case ExpressionType.Divide:
                                return new Divide(recursive(binary_expression.Left), recursive(binary_expression.Right));
                            case ExpressionType.Power:
                                return new Power(recursive(binary_expression.Left), recursive(binary_expression.Right));
                            // call
                            case ExpressionType.Call:
                                return methodCallExpression_to_node(expression as MethodCallExpression);
                                // Invocation
                                //case ExpressionType.Invoke:
                                //	return invocationExpression_to_node(expression as InvocationExpression);
                        }
                        throw new System.ArithmeticException("Invalid syntax parse (unexpected expression node type): " + expression);
                    };

                methodCallExpression_to_node =
                    (MethodCallExpression methodCallExpression) =>
                    {
                        MethodInfo method = methodCallExpression.Method;
                        if (method == null || method.DeclaringType != typeof(Compute))
                            throw new System.ArithmeticException("Invalid syntax parse (only members of Towel.MathematicsCompute allowed): " + methodCallExpression);

                        Expression[] nodes = null;
                        if (methodCallExpression.Arguments != null)
                        {
                            nodes = new Expression[methodCallExpression.Arguments.Count];
                            for (int i = 0; i < nodes.Length; i++)
                                nodes[i] = recursive(methodCallExpression.Arguments[i]);
                        }

                        throw new System.ArithmeticException("Invalid syntax parse (only members of Towel.MathematicsCompute allowed): " + methodCallExpression);
                    };

                return recursive(e);
            }
            catch (System.ArithmeticException exception_specific)
            {
                throw new System.ArithmeticException("failed to parse expression into Towel Framework mathematical syntax: " + e, exception_specific);
            }
        }

        #endregion

        #region string

        public static Expression Parse<T>(string expression)
        {
            // build the parsing library if not already built
            if (!PARSABLELIBRARYBUILT)
            {
                BuildParsableOperationLibrary();
            }
            // error handling
            if (string.IsNullOrWhiteSpace(expression))
            {
                throw new ArgumentException("The expression could not be parsed.", nameof(expression));
            }
            expression = expression.Trim();
            // operations
            if (expression[expression.Length - 1] == ')')
            {
                if (expression[0] == '(')
                {
                    return Parse<T>(expression.Substring(1, expression.Length - 2));
                }
                int parenthasisIndex = expression.IndexOf('(');
                if (parenthasisIndex > -1)
                {
                    string operation = expression.Substring(0, parenthasisIndex);
                    operation.ToLower();
                    ListArray<string> operandSplits = SplitOperands(expression.Substring(parenthasisIndex + 1, expression.Length - parenthasisIndex - 1));
                    switch (operandSplits.Count)
                    {
                        case 1:
                            Func<Expression, Unary> newUnaryFunction;
                            if (ParsableUnaryOperations.TryGetValue(operation, out newUnaryFunction))
                            {
                                return newUnaryFunction(Parse<T>(operandSplits[0]));
                            }
                            break;
                        case 2:
                            Func<Expression, Expression, Binary> newBinaryFunction;
                            if (ParsableBinaryOperations.TryGetValue(operation, out newBinaryFunction))
                            {
                                return newBinaryFunction(Parse<T>(operandSplits[0]), Parse<T>(operandSplits[1]));
                            }
                            break;
                        case 3:
                            Func<Expression, Expression, Expression, Ternary> newTernaryFunction;
                            if (ParsableTernaryOperations.TryGetValue(operation, out newTernaryFunction))
                            {
                                return newTernaryFunction(Parse<T>(operandSplits[0]), Parse<T>(operandSplits[2]), Parse<T>(operandSplits[2]));
                            }
                            break;
                    }


                }
                throw new ArgumentException("The expression could not be parsed.", nameof(expression));
            }
            // variables
            if (expression[expression.Length - 1] == ']')
            {
                if (expression[0] != '[')
                {
                    throw new ArgumentException("The expression could not be parsed.", nameof(expression));
                }
                return new Variable(expression.Substring(1, expression.Length - 2));
            }
            // operators
            Expression operatorParsedNode;
            if (TryParseOperators(expression, out operatorParsedNode))
            {
                return operatorParsedNode;
            }
            // known constants
            Expression knownConstantsParsedNode;
            if (TryParseKnownConstants(expression, out knownConstantsParsedNode))
            {
                return knownConstantsParsedNode;
            }

            int operatorIndex = expression;

            if (expression.Equals("PI", StringComparison.InvariantCultureIgnoreCase) || expression == "π")
            {
                return new Constant<T>(Towel.Mathematics.Constant<T>.Pi);
            }
        }

        public static ListArray<string> SplitOperands(string expression)
        {
            ListArray<string> operands = new ListArray<string>();
            int scope = 0;
            int operandStart = 0;
            for (int i = 0; i < expression.Length; i++)
            {
                switch (expression[i])
                {
                    case '(': scope++; break;
                    case ')': scope--; break;
                    case ',':
                        if (scope == 0)
                        {
                            operands.Add(expression.Substring(operandStart, i - operandStart));
                        }
                        break;
                }
            }
            if (scope != 0)
            {
                throw new ArgumentException("The expression could not be parsed.", nameof(expression));
            }
            operands.Add(expression.Substring(operandStart, expression.Length - operandStart));
            return operands;
        }


        public static bool TryParseOperators(string expression, out Expression node)
        {
            bool foundOperator = false;
            int operatorIndex;
            Symbolics.String.StringParsableOperators @operator;
            int scope = 0;
            for (int i = 0; i < expression.Length; i++)
            {
                if (expression[i] == '(')
                {
                    scope++;
                }
                else if (expression[i] == ')')
                {
                    scope--;
                }
                else if (scope == 0)
                {
                    
                }
            }
        }

        public static bool TryParseKnownConstants(string expression, out Expression node)
        {
            foreach ((string, Symbolics.String.ParsableKnownConstants) knownConstant in Symbolics.String.ParsableKnownConstantsStrings)
            {
                if (expression.Equals(knownConstant.Item1, StringComparison.InvariantCultureIgnoreCase))
                {
                    
                }
            }
            node = null;
            return false;
        }

        #endregion

        #endregion
    }
}