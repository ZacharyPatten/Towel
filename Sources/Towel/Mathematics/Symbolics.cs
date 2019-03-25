using System;
using System.Linq.Expressions;
using System.Reflection;
using Towel.DataStructures;
using System.Linq;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace Towel.Mathematics
{
    /// <summary>Contains definitions necessary for the generic Symbolics class.</summary>
    public static class Symbolics
    {
        #region OperatorPriority Enum

        [Serializable]
        internal enum OperatorPriority
        {
            Negation = 1,
            Factorial = 1,
            Addition = 2,
            Subtraction = 2,
            Multiplication = 3,
            Division = 3,
            Exponents = 4,
            Roots = 4,
            Logical = 5,
        }

        #endregion

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
                for (int i = 1, j = 0; j < b.Length; i++, j++)
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
        internal class LeftUnaryOperatorAttribute : Attribute
        {
            internal readonly string Representation;
            internal readonly OperatorPriority Priority;

            internal LeftUnaryOperatorAttribute(string representation, OperatorPriority operatorPriority) : base()
            {
                this.Representation = representation;
                this.Priority = operatorPriority;
            }
        }

        [AttributeUsage(AttributeTargets.Class)]
        internal class RightUnaryOperatorAttribute : Attribute
        {
            internal readonly string Representation;
            internal readonly OperatorPriority Priority;

            internal RightUnaryOperatorAttribute(string representation, OperatorPriority operatorPriority) : base()
            {
                this.Representation = representation;
                this.Priority = operatorPriority;
            }
        }

        [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
        internal class BinaryOperatorAttribute : Attribute
        {
            internal readonly string Representation;
            internal readonly OperatorPriority Priority;

            internal BinaryOperatorAttribute(string representation, OperatorPriority operatorPriority) : base()
            {
                this.Representation = representation;
                this.Priority = operatorPriority;
            }
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

            public Expression Substitute<T>(string variable, T value)
            {
                return SubstitutionHack(variable, new Constant<T>(value));
            }

            internal Expression SubstitutionHack(string variable, Expression value)
            {
                return Substitute(variable, value);
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

        [Serializable]
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

        [Serializable]
        public abstract class Constant : Expression
        {
            public virtual bool IsKnownConstant => false;

            public virtual bool IsZero => false;

            public virtual bool IsOne => false;

            public virtual bool IsTwo => false;

            public virtual bool IsThree => false;

            public virtual bool IsPi => false;

            public abstract bool IsNegative { get; }

            public virtual Expression Simplify(Operation operation, params Expression[] operands)
            {
                return this;
            }

            internal static System.Collections.Generic.Dictionary<Type, Func<object, Expression>> preCompiledConstructors =
                new System.Collections.Generic.Dictionary<Type, Func<object, Expression>>();

            internal static Expression BuildGeneric(object value)
            {
                Type valueType = value.GetType();
                Func<object, Expression> preCompiledConstructor;
                if (preCompiledConstructors.TryGetValue(valueType, out preCompiledConstructor))
                {
                    return preCompiledConstructor(value);
                }
                else
                {
                    Type constantType = typeof(Constant<>).MakeGenericType(valueType);
                    ConstructorInfo constructorInfo = constantType.GetConstructor(new Type[] { valueType });
                    ParameterExpression A = System.Linq.Expressions.Expression.Parameter(typeof(object));
                    NewExpression newExpression = System.Linq.Expressions.Expression.New(constructorInfo, System.Linq.Expressions.Expression.Convert(A, valueType));
                    Func<object, Expression> newFunction = System.Linq.Expressions.Expression.Lambda<Func<object, Expression>>(newExpression, A).Compile();
                    preCompiledConstructors.Add(valueType, newFunction);
                    return newFunction(value);
                }
            }
        }

        #endregion

        #region KnownConstantOfUknownType + Inheriters

        #region KnownConstantOfUknownType

        [Serializable]
        public abstract class KnownConstantOfUknownType : Constant
        {
            public override bool IsKnownConstant => true;

            public abstract Constant<T> ApplyType<T>();
        }

        #endregion

        #region Pi

        [Serializable]
        [KnownConstant("π")]
        public class Pi : KnownConstantOfUknownType
        {
            public Pi() : base() { }

            public override bool IsPi => true;

            public override bool IsNegative
            {
                get
                {
                    return false;
                }
            }

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

        [Serializable]
        public class Zero : KnownConstantOfUknownType
        {
            public Zero() : base() { }

            public override bool IsZero => true;

            public override bool IsNegative
            {
                get
                {
                    return false;
                }
            }

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

        [Serializable]
        public class One : KnownConstantOfUknownType
        {
            public One() : base() { }

            public override bool IsOne => true;

            public override bool IsNegative
            {
                get
                {
                    return false;
                }
            }

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

        [Serializable]
        public class Two : KnownConstantOfUknownType
        {
            public Two() : base() { }

            public override bool IsTwo => true;

            public override bool IsNegative
            {
                get
                {
                    return false;
                }
            }

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

        [Serializable]
        public class Three : KnownConstantOfUknownType
        {
            public Three() : base() { }

            public override bool IsThree => true;

            public override bool IsNegative
            {
                get
                {
                    return false;
                }
            }

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

        [Serializable]
        public class Constant<T> : Constant
        {
            public readonly T Value;

            public override bool IsZero => Compute.Equal(Value, Mathematics.Constant<T>.Zero);

            public override bool IsOne => Compute.Equal(Value, Mathematics.Constant<T>.One);

            public override bool IsTwo => Compute.Equal(Value, Mathematics.Constant<T>.Two);

            public override bool IsThree => Compute.Equal(Value, Mathematics.Constant<T>.Three);

            public override bool IsNegative
            {
                get
                {
                    return Compute.IsNegative(this.Value);
                }
            }

            public Constant(T constant)
            {
                this.Value = constant;
            }

            public override Expression Simplify(Operation operation, params Expression[] operands)
            {
                return operation.SimplifyHack<T>(operands);
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

        [Serializable]
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

        [Serializable]
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

        [Serializable]
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

        [Serializable]
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

        [Serializable]
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

        [Serializable]
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

        [Serializable]
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

            protected virtual Expression Simplify<T>(params Expression[] operands)
            {
                return this;
            }

            internal Expression SimplifyHack<T>(params Expression[] operands)
            {
                return this.Simplify<T>(operands);
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

            public Unary(Expression a) : base()
            {
                this._a = a;
            }
        }

        #endregion

        #region Simplification

        [Operation("Simplify")]
        [Serializable]
        public class Simplification : Unary
        {
            public Simplification(Expression a) : base(a) { }

            public override Expression Simplify()
            {
                return this.A.Simplify();
            }

            public override Expression Clone()
            {
                return new Simplification(this.A.Clone());
            }

            public override Expression Substitute(string variable, Expression value)
            {
                return new Simplification(this.A.Substitute(variable, value));
            }

            public override string ToString()
            {
                return "Simplify(" + this.A + ")";
            }
        }

        #endregion

        #region Negate

        [LeftUnaryOperator("-", OperatorPriority.Negation)]
        [Serializable]
        public class Negate : Unary, Operation.Mathematical
        {
            public Negate(Expression a) : base(a) { }

            public override Expression Simplify()
            {
                #region Computation
                // Rule: [-A] => [B] where A is constant and B is -A
                if (this.A is Constant constant)
                {
                    return constant.Simplify(this, this.A);
                }
                #endregion
                return base.Simplify();
            }

            protected override Expression Simplify<T>(params Expression[] operands)
            {
                if (operands[0] is Constant<T> a)
                {
                    return new Constant<T>(Compute.Negate(((Constant<T>)operands[0]).Value));
                }
                return base.Simplify<T>();
            }

            public override Expression Clone()
            {
                return new Negate(this.A.Clone());
            }

            public override Expression Substitute(string variable, Expression value)
            {
                return new Negate(this.A.Substitute(variable, value));
            }

            public override string ToString()
            {
                return "-" + this.A;
            }
        }

        #endregion

        #region NaturalLog

        [Serializable]
        public class NaturalLog : Unary, Operation.Mathematical
        {
            public NaturalLog(Expression operand) : base(operand) { }

            public override Expression Simplify()
            {
                #region Computation
                // Rule: [A] => [B] where A is constant and B is ln(A)
                if (this.A is Constant constant)
                {
                    return constant.Simplify(this, this.A);
                }
                #endregion
                return base.Simplify();
            }

            protected override Expression Simplify<T>(params Expression[] operands)
            {
                if (operands[0] is Constant<T> a)
                {
                    return new Constant<T>(Compute.NaturalLogarithm(((Constant<T>)operands[0]).Value));
                }
                return base.Simplify<T>();
            }

            public override Expression Clone()
            {
                return new NaturalLog(this.A.Clone());
            }

            public override Expression Substitute(string variable, Expression value)
            {
                return new NaturalLog(this.A.Substitute(variable, value));
            }

            public override string ToString()
            {
                return "ln(" + this.A + ")";
            }
        }

        #endregion

        #region SquareRoot

        [Serializable]
        public class SquareRoot : Unary, Operation.Mathematical
        {
            public SquareRoot(Expression operand) : base(operand) { }

            public override Expression Simplify()
            {
                #region Computation
                // Rule: [A] => [B] where A is constant and B is sqrt(A)
                if (this.A is Constant constant)
                {
                    return constant.Simplify(this, this.A);
                }
                #endregion
                return base.Simplify();
            }

            protected override Expression Simplify<T>(params Expression[] operands)
            {
                if (operands[0] is Constant<T> a)
                {
                    return new Constant<T>(Compute.SquareRoot(((Constant<T>)operands[0]).Value));
                }
                return base.Simplify<T>();
            }

            public override Expression Clone()
            {
                return new SquareRoot(this.A.Clone());
            }

            public override Expression Substitute(string variable, Expression value)
            {
                return new SquareRoot(this.A.Substitute(variable, value));
            }

            public override string ToString()
            {
                return "√(" + this.A + ")";
            }
        }

        #endregion

        #region Exponential

        [Serializable]
        public class Exponential : Unary, Operation.Mathematical
        {
            public Exponential(Expression a) : base(a) { }

            public override Expression Simplify()
            {
                #region Computation
                // Rule: [A] => [B] where A is constant and B is e ^ A
                if (this.A is Constant constant)
                {
                    return constant.Simplify(this, this.A);
                }
                #endregion
                return base.Simplify();
            }

            protected override Expression Simplify<T>(params Expression[] operands)
            {
                if (operands[0] is Constant<T> a)
                {
                    return new Constant<T>(Compute.Exponential(((Constant<T>)operands[0]).Value));
                }
                return base.Simplify<T>();
            }

            public override Expression Clone()
            {
                return new Exponential(this.A.Clone());
            }

            public override Expression Substitute(string variable, Expression value)
            {
                return new Exponential(this.A.Substitute(variable, value));
            }

            public override string ToString()
            {
                return "e^(" + this.A + ")";
            }
        }

        #endregion

        #region Invert

        [Serializable]
        public class Invert : Unary, Operation.Mathematical
        {
            public Invert(Expression a) : base(a) { }

            public override Expression Simplify()
            {
                #region Computation
                // Rule: [A] => [B] where A is constant and B is 1 / A
                if (this.A is Constant constant)
                {
                    return constant.Simplify(this, this.A);
                }
                #endregion
                return base.Simplify();
            }

            protected override Expression Simplify<T>(params Expression[] operands)
            {
                if (operands[0] is Constant<T> a)
                {
                    return new Constant<T>(Compute.Invert(((Constant<T>)operands[0]).Value));
                }
                return base.Simplify<T>();
            }

            public override Expression Clone()
            {
                return new Invert(this.A.Clone());
            }

            public override Expression Substitute(string variable, Expression value)
            {
                return new Invert(this.A.Substitute(variable, value));
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

        [Serializable]
        public class Sine : Trigonometry, Operation.Mathematical
        {
            public Sine(Expression a) : base(a) { }

            protected override Expression Simplify<T>(params Expression[] operands)
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

            public override Expression Substitute(string variable, Expression value)
            {
                return new Sine(this.A.Substitute(variable, value));
            }

            public override string ToString()
            {
                return nameof(Sine) + "(" + this.A + ")";
            }
        }

        #endregion

        #region Cosine

        [Serializable]
        public class Cosine : Trigonometry, Operation.Mathematical
        {
            public Cosine(Expression a) : base(a) { }

            protected override Expression Simplify<T>(params Expression[] operands)
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

            public override Expression Substitute(string variable, Expression value)
            {
                return new Cosine(this.A.Substitute(variable, value));
            }

            public override string ToString()
            {
                return nameof(Cosine) + "(" + this.A + ")";
            }
        }

        #endregion

        #region Tangent

        [Serializable]
        public class Tangent : Trigonometry, Operation.Mathematical
        {
            public Tangent(Expression a) : base(a) { }

            protected override Expression Simplify<T>(params Expression[] operands)
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

            public override Expression Substitute(string variable, Expression value)
            {
                return new Tangent(this.A.Substitute(variable, value));
            }

            public override string ToString()
            {
                return nameof(Tangent) + "(" + this.A + ")";
            }
        }

        #endregion

        #region Cosecant

        [Serializable]
        public class Cosecant : Trigonometry, Operation.Mathematical
        {
            public Cosecant(Expression a) : base(a) { }

            protected override Expression Simplify<T>(params Expression[] operands)
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

            public override Expression Substitute(string variable, Expression value)
            {
                return new Cosecant(this.A.Substitute(variable, value));
            }

            public override string ToString()
            {
                return nameof(Cosecant) + "(" + this.A + ")";
            }
        }

        #endregion

        #region Secant

        [Serializable]
        public class Secant : Trigonometry, Operation.Mathematical
        {
            public Secant(Expression a) : base(a) { }

            protected override Expression Simplify<T>(params Expression[] operands)
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

            public override Expression Substitute(string variable, Expression value)
            {
                return new Secant(this.A.Substitute(variable, value));
            }

            public override string ToString()
            {
                return nameof(Secant) + "(" + this.A + ")";
            }
        }

        #endregion

        #region Cotangent

        [Serializable]
        public class Cotangent : Trigonometry, Operation.Mathematical
        {
            public Cotangent(Expression a) : base(a) { }

            protected override Expression Simplify<T>(params Expression[] operands)
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

            public override Expression Substitute(string variable, Expression value)
            {
                return new Cotangent(this.A.Substitute(variable, value));
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

        [BinaryOperator("+", OperatorPriority.Addition)]
        [Serializable]
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
                        return A.Simplify(this, A, B);
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

            protected override Expression Simplify<T>(params Expression[] operands)
            {
                if (operands[0] is Constant<T> a && operands[1] is Constant<T> b)
                {
                    return new Constant<T>(Compute.Add(
                        ((Constant<T>)operands[0]).Value,
                        ((Constant<T>)operands[1]).Value));
                }
                return base.Simplify<T>();
            }

            public override Expression Clone()
            {
                return new Add(this.A.Clone(), this.B.Clone());
            }

            public override Expression Substitute(string variable, Expression value)
            {
                return new Add(this.A.Substitute(variable, value), this.B.Substitute(variable, value));
            }

            public override string ToString()
            {
                string a = this.A.ToString();
                string b = this.B.ToString();
                {
                    if ((this.A is Multiply MULTIPLY || this.A is Divide DIVIDE) && this.A is Constant CONSTANT && CONSTANT.IsNegative)
                    {
                        a = "(" + a + ")";
                    }
                }
                {
                    if (this.B is Add || this.B is Subtract || this.A is Multiply || this.A is Divide)
                    {
                        b = "(" + b + ")";
                    }
                }
                {
                    if (this.B is Constant CONSTANT && CONSTANT.IsNegative)
                    {
                        return a + " - " + Compute.Negate(this.B as Constant);
                    }
                }
                return a + " + " + b;
            }
        }

        #endregion

        #region Subtract

        [BinaryOperator("-", OperatorPriority.Subtraction)]
        [Serializable]
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
                        return left.Simplify(this, A, B);
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

            protected override Expression Simplify<T>(params Expression[] operands)
            {
                if (operands[0] is Constant<T> a && operands[1] is Constant<T> b)
                {
                    return new Constant<T>(Compute.Subtract(
                        ((Constant<T>)operands[0]).Value,
                        ((Constant<T>)operands[1]).Value));
                }
                return base.Simplify<T>();
            }

            public override Expression Clone()
            {
                return new Subtract(this.A.Clone(), this.B.Clone());
            }

            public override Expression Substitute(string variable, Expression value)
            {
                return new Subtract(this.A.Substitute(variable, value), this.B.Substitute(variable, value));
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

        [BinaryOperator("*", OperatorPriority.Multiplication)]
        [Serializable]
        public class Multiply : MultiplyOrDivide
        {
            public Multiply(Expression a, Expression b) : base(a, b) { }

            public override Expression Simplify()
            {
                Expression LEFT = this.A.Simplify();
                Expression RIGHT = this.B.Simplify();
                #region Computation
                {   // Rule: [A * B] => [C] where A is constant, B is constant, and C is A * B
                    if (LEFT is Constant A && RIGHT is Constant B)
                    {
                        return A.Simplify(this, A, B);
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

            protected override Expression Simplify<T>(params Expression[] operands)
            {
                if (operands[0] is Constant<T> a && operands[1] is Constant<T> b)
                {
                    return new Constant<T>(Compute.Multiply(
                        ((Constant<T>)operands[0]).Value,
                        ((Constant<T>)operands[1]).Value));
                }
                return base.Simplify<T>();
            }

            public override Expression Clone()
            {
                return new Multiply(this.A.Clone(), this.B.Clone());
            }

            public override Expression Substitute(string variable, Expression value)
            {
                return new Multiply(this.A.Substitute(variable, value), this.B.Substitute(variable, value));
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

        [BinaryOperator("/", OperatorPriority.Division)]
        [Serializable]
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
                        return A.Simplify(this, A, B);
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

            protected override Expression Simplify<T>(params Expression[] operands)
            {
                if (operands[0] is Constant<T> a && operands[1] is Constant<T> b)
                {
                    return new Constant<T>(Compute.Divide(
                        ((Constant<T>)operands[0]).Value,
                        ((Constant<T>)operands[1]).Value));
                }
                return base.Simplify<T>();
            }

            public override Expression Clone()
            {
                return new Divide(this.A.Clone(), this.B.Clone());
            }

            public override Expression Substitute(string variable, Expression value)
            {
                return new Divide(this.A.Substitute(variable, value), this.B.Substitute(variable, value));
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

        [BinaryOperator("^", OperatorPriority.Exponents)]
        [Serializable]
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
                        return A.Simplify(this, A, B);
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

            protected override Expression Simplify<T>(params Expression[] operands)
            {
                if (operands[0] is Constant<T> a && operands[1] is Constant<T> b)
                {
                    return new Constant<T>(Compute.Power(
                        ((Constant<T>)operands[0]).Value,
                        ((Constant<T>)operands[1]).Value));
                }
                return base.Simplify<T>();
            }

            public override Expression Clone()
            {
                return new Power(this.A.Clone(), this.B.Clone());
            }

            public override Expression Substitute(string variable, Expression value)
            {
                return new Power(this.A.Substitute(variable, value), this.B.Substitute(variable, value));
            }

            public override string ToString()
            {
                return this.A + " ^ " + this.B;
            }
        }

        #endregion

        #region Root

        [Serializable]
        public class Root : Binary, Operation.Mathematical
        {
            public Root(Expression a, Expression b) : base(a, b) { }

            public override Expression Simplify()
            {
                Expression LEFT = this.A.Simplify();
                Expression RIGHT = this.B.Simplify();
                #region Computation
                {   // Rule: [A ^ (1 / B)] => [C] where A is constant, B is constant, and C is A ^ (1 / B)
                    if (LEFT is Constant A && RIGHT is Constant B)
                    {
                        return A.Simplify(this, A, B);
                    }
                }
                #endregion
                return base.Simplify();
            }

            protected override Expression Simplify<T>(params Expression[] operands)
            {
                if (operands[0] is Constant<T> a && operands[1] is Constant<T> b)
                {
                    return new Constant<T>(Compute.Root(
                        ((Constant<T>)operands[0]).Value,
                        ((Constant<T>)operands[1]).Value));
                }
                return base.Simplify<T>();
            }

            public override Expression Clone()
            {
                return new Root(this.A.Clone(), this.B.Clone());
            }

            public override Expression Substitute(string variable, Expression value)
            {
                return new Root(this.A.Substitute(variable, value), this.B.Substitute(variable, value));
            }

            public override string ToString()
            {
                return this.A + " ^ (1 / " + this.B + ")";
            }
        }

        #endregion

        #region Equal

        [BinaryOperator("=", OperatorPriority.Logical)]
        [Serializable]
        public class Equal : Binary, Operation.Logical
        {
            public Equal(Expression a, Expression b) : base(a, b) { }

            public override Expression Simplify()
            {
                Expression LEFT = this.A.Simplify();
                Expression RIGHT = this.B.Simplify();
                #region Computation
                {   // Rule: [A == B] => [C] where A is constant, B is constant, and C is A == B
                    if (LEFT is Constant A && RIGHT is Constant B)
                    {
                        return A.Simplify(this, A, B);
                    }
                }
                #endregion
                return base.Simplify();
            }

            protected override Expression Simplify<T>(params Expression[] operands)
            {
                if (operands[0] is Constant<T> a && operands[1] is Constant<T> b)
                {
                    return new Constant<bool>(Compute.Equal(
                        ((Constant<T>)operands[0]).Value,
                        ((Constant<T>)operands[1]).Value));
                }
                return base.Simplify<T>();
            }

            public override Expression Clone()
            {
                return new Equal(this.A.Clone(), this.B.Clone());
            }

            public override Expression Substitute(string variable, Expression value)
            {
                return new Equal(this.A.Substitute(variable, value), this.B.Substitute(variable, value));
            }

            public override string ToString()
            {
                return this.A + " = " + this.B;
            }
        }

        #endregion

        #region NotEqual

        [BinaryOperator("≠", OperatorPriority.Logical)]
        [Serializable]
        public class NotEqual : Binary, Operation.Logical
        {
            public NotEqual(Expression a, Expression b) : base(a, b) { }

            public override Expression Simplify()
            {
                Expression LEFT = this.A.Simplify();
                Expression RIGHT = this.B.Simplify();
                #region Computation
                {   // Rule: [A == B] => [C] where A is constant, B is constant, and C is A != B
                    if (LEFT is Constant A && RIGHT is Constant B)
                    {
                        return A.Simplify(this, A, B);
                    }
                }
                #endregion
                return base.Simplify();
            }

            protected override Expression Simplify<T>(params Expression[] operands)
            {
                if (operands[0] is Constant<T> a && operands[1] is Constant<T> b)
                {
                    return new Constant<bool>(Compute.NotEqual(
                        ((Constant<T>)operands[0]).Value,
                        ((Constant<T>)operands[1]).Value));
                }
                return base.Simplify<T>();
            }

            public override Expression Clone()
            {
                return new NotEqual(this.A.Clone(), this.B.Clone());
            }

            public override Expression Substitute(string variable, Expression value)
            {
                return new NotEqual(this.A.Substitute(variable, value), this.B.Substitute(variable, value));
            }

            public override string ToString()
            {
                return this.A + " ≠ " + this.B;
            }
        }

        #endregion

        #region LessThan

        [BinaryOperator("<", OperatorPriority.Logical)]
        [Serializable]
        public class LessThan : Binary, Operation.Logical
        {
            public LessThan(Expression a, Expression b) : base(a, b) { }

            public override Expression Simplify()
            {
                Expression LEFT = this.A.Simplify();
                Expression RIGHT = this.B.Simplify();
                #region Computation
                {   // Rule: [A == B] => [C] where A is constant, B is constant, and C is A < B
                    if (LEFT is Constant A && RIGHT is Constant B)
                    {
                        return A.Simplify(this, A, B);
                    }
                }
                #endregion
                return base.Simplify();
            }

            protected override Expression Simplify<T>(params Expression[] operands)
            {
                if (operands[0] is Constant<T> a && operands[1] is Constant<T> b)
                {
                    return new Constant<bool>(Compute.LessThan(
                        ((Constant<T>)operands[0]).Value,
                        ((Constant<T>)operands[1]).Value));
                }
                return base.Simplify<T>();
            }

            public override Expression Clone()
            {
                return new LessThan(this.A.Clone(), this.B.Clone());
            }

            public override Expression Substitute(string variable, Expression value)
            {
                return new LessThan(this.A.Substitute(variable, value), this.B.Substitute(variable, value));
            }

            public override string ToString() { return this.A + " < " + this.B; }
        }

        #endregion

        #region GreaterThan

        [BinaryOperator(">", OperatorPriority.Logical)]
        [Serializable]
        public class GreaterThan : Binary, Operation.Logical
        {
            public GreaterThan(Expression left, Expression right) : base(left, right) { }

            public override Expression Simplify()
            {
                Expression LEFT = this.A.Simplify();
                Expression RIGHT = this.B.Simplify();
                #region Computation
                {   // Rule: [A == B] => [C] where A is constant, B is constant, and C is A > B
                    if (LEFT is Constant A && RIGHT is Constant B)
                    {
                        return A.Simplify(this, A, B);
                    }
                }
                #endregion
                return base.Simplify();
            }

            protected override Expression Simplify<T>(params Expression[] operands)
            {
                if (operands[0] is Constant<T> a && operands[1] is Constant<T> b)
                {
                    return new Constant<bool>(Compute.GreaterThan(
                        ((Constant<T>)operands[0]).Value,
                        ((Constant<T>)operands[1]).Value));
                }
                return base.Simplify<T>();
            }

            public override Expression Clone()
            {
                return new GreaterThan(this.A.Clone(), this.B.Clone());
            }

            public override Expression Substitute(string variable, Expression value)
            {
                return new GreaterThan(this.A.Substitute(variable, value), this.B.Substitute(variable, value));
            }

            public override string ToString() { return this.A + " < " + this.B; }
        }

        #endregion
        
        #region LessThanOrEqual

        [BinaryOperator("<=", OperatorPriority.Logical)]
        [Serializable]
        public class LessThanOrEqual : Binary, Operation.Logical
        {
            public LessThanOrEqual(Expression left, Expression right) : base(left, right) { }

            public override Expression Simplify()
            {
                Expression LEFT = this.A.Simplify();
                Expression RIGHT = this.B.Simplify();
                #region Computation
                {   // Rule: [A == B] => [C] where A is constant, B is constant, and C is A <= B
                    if (LEFT is Constant A && RIGHT is Constant B)
                    {
                        return A.Simplify(this, A, B);
                    }
                }
                #endregion
                return base.Simplify();
            }

            protected override Expression Simplify<T>(params Expression[] operands)
            {
                if (operands[0] is Constant<T> a && operands[1] is Constant<T> b)
                {
                    return new Constant<bool>(Compute.LessThanOrEqual(
                        ((Constant<T>)operands[0]).Value,
                        ((Constant<T>)operands[1]).Value));
                }
                return base.Simplify<T>();
            }

            public override Expression Clone()
            {
                return new LessThanOrEqual(this.A.Clone(), this.B.Clone());
            }

            public override Expression Substitute(string variable, Expression value)
            {
                return new LessThanOrEqual(this.A.Substitute(variable, value), this.B.Substitute(variable, value));
            }

            public override string ToString() { return this.A + " < " + this.B; }
        }

        #endregion

        #region GreaterThanOrEqual

        [BinaryOperator(">=", OperatorPriority.Logical)]
        [Serializable]
        public class GreaterThanOrEqual : Binary, Operation.Logical
        {
            public GreaterThanOrEqual(Expression left, Expression right) : base(left, right) { }

            public override Expression Simplify()
            {
                Expression LEFT = this.A.Simplify();
                Expression RIGHT = this.B.Simplify();
                #region Computation
                {   // Rule: [A == B] => [C] where A is constant, B is constant, and C is A >= B
                    if (LEFT is Constant A && RIGHT is Constant B)
                    {
                        return A.Simplify(this, A, B);
                    }
                }
                #endregion
                return base.Simplify();
            }

            protected override Expression Simplify<T>(params Expression[] operands)
            {
                if (operands[0] is Constant<T> a && operands[1] is Constant<T> b)
                {
                    return new Constant<bool>(Compute.GreaterThanOrEqual(
                        ((Constant<T>)operands[0]).Value,
                        ((Constant<T>)operands[1]).Value));
                }
                return base.Simplify<T>();
            }

            public override Expression Clone()
            {
                return new GreaterThanOrEqual(this.A.Clone(), this.B.Clone());
            }

            public override Expression Substitute(string variable, Expression value)
            {
                return new GreaterThanOrEqual(this.A.Substitute(variable, value), this.B.Substitute(variable, value));
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

        // Notes:
        // Parsing uses the Expression class hierarchy with the class atributes to build
        // a library of parsing constants via reflection. Once built, the parsing library
        // is used as a reference to contruct the proper Expression type via a contruction
        // delegate.

        #region Runtime Built Parsing Libary

        // Library Building Fields
        private static bool ParseableLibraryBuilt = false;
        private static object ParseableLibraryLock = new object();
        // Regex Expressions
        internal const string ParenthesisPattern = @"\(.*\)";
        private static string ParsableOperationsRegexPattern;
        private static string ParsableOperatorsRegexPattern;
        private static string ParsableKnownConstantsRegexPattern;
        // Operation Refrences
        private static System.Collections.Generic.Dictionary<string, Func<Expression, Unary>> ParsableUnaryOperations;
        private static System.Collections.Generic.Dictionary<string, Func<Expression, Expression, Binary>> ParsableBinaryOperations;
        private static System.Collections.Generic.Dictionary<string, Func<Expression, Expression, Expression, Ternary>> ParsableTernaryOperations;
        private static System.Collections.Generic.Dictionary<string, Func<Expression[], Multinary>> ParsableMultinaryOperations;
        // Operator References
        private static System.Collections.Generic.Dictionary<string, (OperatorPriority, Func<Expression, Unary>)> ParsableLeftUnaryOperators;
        private static System.Collections.Generic.Dictionary<string, (OperatorPriority, Func<Expression, Unary>)> ParsableRightUnaryOperators;
        private static System.Collections.Generic.Dictionary<string, (OperatorPriority, Func<Expression, Expression, Binary>)> ParsableBinaryOperators;
        // Known Constant References
        private static System.Collections.Generic.Dictionary<string, Func<KnownConstantOfUknownType>> ParsableKnownConstants;

        #region Reflection Code (Actually Building the Parsing Library)

        internal static void BuildParsableOperationLibrary()
        {
            lock (ParseableLibraryLock)
            {
                if (ParseableLibraryBuilt)
                {
                    return;
                }

                // Unary Operations
                ParsableUnaryOperations = new System.Collections.Generic.Dictionary<string, Func<Expression, Unary>>();
                ParsableLeftUnaryOperators = new System.Collections.Generic.Dictionary<string, (OperatorPriority, Func<Expression, Unary>)>();
                ParsableRightUnaryOperators = new System.Collections.Generic.Dictionary<string, (OperatorPriority, Func<Expression, Unary>)>();
                foreach (Type type in Assembly.GetExecutingAssembly().GetDerivedTypes<Unary>().Where(x => !x.IsAbstract))
                {
                    ConstructorInfo constructorInfo = type.GetConstructor(new Type[] { typeof(Expression) });
                    ParameterExpression A = System.Linq.Expressions.Expression.Parameter(typeof(Expression));
                    NewExpression newExpression = System.Linq.Expressions.Expression.New(constructorInfo, A);
                    Func<Expression, Unary> newFunction = System.Linq.Expressions.Expression.Lambda<Func<Expression, Unary>>(newExpression, A).Compile();
                    string operationName = type.ConvertToCsharpSource();
                    if (operationName.Contains("+"))
                    {
                        int index = operationName.LastIndexOf("+");
                        operationName = operationName.Substring(index + 1);
                    }
                    ParsableUnaryOperations.Add(operationName.ToLower(), newFunction);
                    OperationAttribute operationAttribute = type.GetCustomAttribute<OperationAttribute>();
                    if (!(operationAttribute is null))
                    {
                        foreach (string representation in operationAttribute.Representations)
                        {
                            ParsableUnaryOperations.Add(representation.ToLower(), newFunction);
                        }
                    }

                    // Left Unary Operators
                    foreach (LeftUnaryOperatorAttribute @operator in type.GetCustomAttributes<LeftUnaryOperatorAttribute>())
                    {
                        ParsableLeftUnaryOperators.Add(@operator.Representation.ToLower(), (@operator.Priority, newFunction));
                    }

                    // Right Unary Operators
                    foreach (RightUnaryOperatorAttribute @operator in type.GetCustomAttributes<RightUnaryOperatorAttribute>())
                    {
                        ParsableRightUnaryOperators.Add(@operator.Representation.ToLower(), (@operator.Priority, newFunction));
                    }
                }

                // Binary Operations
                ParsableBinaryOperations = new System.Collections.Generic.Dictionary<string, Func<Expression, Expression, Binary>>();
                ParsableBinaryOperators = new System.Collections.Generic.Dictionary<string, (OperatorPriority, Func<Expression, Expression, Binary>)>();
                foreach (Type type in Assembly.GetExecutingAssembly().GetDerivedTypes<Binary>().Where(x => !x.IsAbstract))
                {
                    ConstructorInfo constructorInfo = type.GetConstructor(new Type[] { typeof(Expression), typeof(Expression) });
                    ParameterExpression A = System.Linq.Expressions.Expression.Parameter(typeof(Expression));
                    ParameterExpression B = System.Linq.Expressions.Expression.Parameter(typeof(Expression));
                    NewExpression newExpression = System.Linq.Expressions.Expression.New(constructorInfo, A, B);
                    Func<Expression, Expression, Binary> newFunction = System.Linq.Expressions.Expression.Lambda<Func<Expression, Expression, Binary>>(newExpression, A, B).Compile();
                    string operationName = type.ConvertToCsharpSource();
                    if (operationName.Contains("+"))
                    {
                        int index = operationName.LastIndexOf("+");
                        operationName = operationName.Substring(index + 1);
                    }
                    ParsableBinaryOperations.Add(operationName.ToLower(), newFunction);
                    OperationAttribute operationAttribute = type.GetCustomAttribute<OperationAttribute>();
                    if (!(operationAttribute is null))
                    {
                        foreach (string representation in operationAttribute.Representations)
                        {
                            ParsableBinaryOperations.Add(representation.ToLower(), newFunction);
                        }
                    }

                    // Binary Operators
                    foreach (BinaryOperatorAttribute @operator in type.GetCustomAttributes<BinaryOperatorAttribute>())
                    {
                        ParsableBinaryOperators.Add(@operator.Representation.ToLower(), (@operator.Priority, newFunction));
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
                    Func<Expression, Expression, Expression, Ternary> newFunction = System.Linq.Expressions.Expression.Lambda<Func<Expression, Expression, Expression, Ternary>>(newExpression, A, B, C).Compile();
                    string operationName = type.ConvertToCsharpSource();
                    if (operationName.Contains("+"))
                    {
                        int index = operationName.LastIndexOf("+");
                        operationName = operationName.Substring(index + 1);
                    }
                    ParsableTernaryOperations.Add(operationName.ToLower(), newFunction);
                    OperationAttribute operationAttribute = type.GetCustomAttribute<OperationAttribute>();
                    if (!(operationAttribute is null))
                    {
                        foreach (string representation in operationAttribute.Representations)
                        {
                            ParsableTernaryOperations.Add(representation.ToLower(), newFunction);
                        }
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
                    if (operationName.Contains("+"))
                    {
                        int index = operationName.LastIndexOf("+");
                        operationName = operationName.Substring(index + 1);
                    }
                    ParsableMultinaryOperations.Add(operationName.ToLower(), newFunction);
                    OperationAttribute operationAttribute = type.GetCustomAttribute<OperationAttribute>();
                    if (!(operationAttribute is null))
                    {
                        foreach (string representation in operationAttribute.Representations)
                        {
                            ParsableMultinaryOperations.Add(representation.ToLower(), newFunction);
                        }
                    }
                }

                // Known Constants
                ParsableKnownConstants = new System.Collections.Generic.Dictionary<string, Func<KnownConstantOfUknownType>>();
                foreach (Type type in Assembly.GetExecutingAssembly().GetDerivedTypes<KnownConstantOfUknownType>().Where(x => !x.IsAbstract))
                {
                    ConstructorInfo constructorInfo = type.GetConstructor(Type.EmptyTypes);
                    NewExpression newExpression = System.Linq.Expressions.Expression.New(constructorInfo);
                    Func<KnownConstantOfUknownType> newFunction = System.Linq.Expressions.Expression.Lambda<Func<KnownConstantOfUknownType>>(newExpression).Compile();
                    //string knownConstant = type.ConvertToCsharpSource();
                    //if (knownConstant.Contains("+"))
                    //{
                    //    int index = knownConstant.LastIndexOf("+");
                    //    knownConstant = knownConstant.Substring(index + 1);
                    //}
                    //ParsableKnownConstants.Add(knownConstant.ToLower(), newFunction);
                    KnownConstantAttribute knownConstantAttribute = type.GetCustomAttribute<KnownConstantAttribute>();
                    if (!(knownConstantAttribute is null))
                    {
                        foreach (string representation in knownConstantAttribute.Representations)
                        {
                            ParsableKnownConstants.Add(representation.ToLower(), newFunction);
                        }
                    }
                }

                // Build a regex to match any operation
                System.Collections.Generic.IEnumerable<string> operations =
                    ParsableUnaryOperations.Keys.Concat(
                        ParsableBinaryOperations.Keys.Concat(
                            ParsableTernaryOperations.Keys.Concat(
                                ParsableMultinaryOperations.Keys))).Select(x => Regex.Escape(x));
                ParsableOperationsRegexPattern = string.Join(@"\s*\(.*\)|", operations) + @"\s *\(.*\)";

                // Build a regex to match any operator
                System.Collections.Generic.IEnumerable<string> operators = 
                    ParsableLeftUnaryOperators.Keys.Concat(
                        ParsableRightUnaryOperators.Keys.Concat(
                            ParsableBinaryOperators.Keys)).Select(x => Regex.Escape(x));
                ParsableOperatorsRegexPattern = string.Join("|", operators);

                System.Collections.Generic.IEnumerable<string> knownConstants =
                    ParsableKnownConstants.Keys.Select(x => Regex.Escape(x));
                ParsableKnownConstantsRegexPattern = string.Join("|", knownConstants);

                ParseableLibraryBuilt = true;
            }
        }

        #endregion

        #endregion

        #region System.Linq.Expression

        public static Expression Parse(System.Linq.Expressions.Expression e)
        {
            try
            {
                Func<System.Linq.Expressions.Expression, Expression> recursive = null;
                Func<MethodCallExpression, Expression> methodCallExpression_to_node = null;

                recursive =
                    (System.Linq.Expressions.Expression expression) =>
                    {
                        UnaryExpression unary_expression = expression as UnaryExpression;
                        BinaryExpression binary_expression = expression as BinaryExpression;

                        switch (expression.NodeType)
                        {
                            case ExpressionType.Lambda:
                                return recursive((expression as LambdaExpression).Body);
                            case ExpressionType.Constant:
                                return Constant.BuildGeneric((expression as ConstantExpression).Value);
                            case ExpressionType.Parameter:
                                return new Variable((expression as ParameterExpression).Name);
                            case ExpressionType.Negate:
                                return new Negate(recursive(unary_expression.Operand));
                            case ExpressionType.UnaryPlus:
                                return recursive(unary_expression.Operand);
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
                            case ExpressionType.Call:
                                return methodCallExpression_to_node(expression as MethodCallExpression);
                        }
                        throw new ArgumentException("The expression could not be parsed.", nameof(e));
                    };

                methodCallExpression_to_node =
                    (MethodCallExpression methodCallExpression) =>
                    {
                        MethodInfo methodInfo = methodCallExpression.Method;
                        if (methodInfo == null)
                        {
                            throw new ArgumentException("The expression could not be parsed.", nameof(e));
                        }

                        Expression[] arguments = null;
                        if (methodCallExpression.Arguments != null)
                        {
                            arguments = new Expression[methodCallExpression.Arguments.Count];
                            for (int i = 0; i < arguments.Length; i++)
                                arguments[i] = recursive(methodCallExpression.Arguments[i]);
                        }

                        if (!ParseableLibraryBuilt)
                        {
                            BuildParsableOperationLibrary();
                        }

                        string operation = methodInfo.Name.ToLower();

                        switch (arguments.Length)
                        {
                            case 1:
                                Func<Expression, Unary> newUnaryFunction;
                                if (ParsableUnaryOperations.TryGetValue(operation, out newUnaryFunction))
                                {
                                    return newUnaryFunction(arguments[0]);
                                }
                                break;
                            case 2:
                                Func<Expression, Expression, Binary> newBinaryFunction;
                                if (ParsableBinaryOperations.TryGetValue(operation, out newBinaryFunction))
                                {
                                    return newBinaryFunction(arguments[0], arguments[1]);
                                }
                                break;
                            case 3:
                                Func<Expression, Expression, Expression, Ternary> newTernaryFunction;
                                if (ParsableTernaryOperations.TryGetValue(operation, out newTernaryFunction))
                                {
                                    return newTernaryFunction(arguments[0], arguments[1], arguments[2]);
                                }
                                break;
                        }

                        Func<Expression[], Multinary> newMultinaryFunction;
                        if (ParsableMultinaryOperations.TryGetValue(operation, out newMultinaryFunction))
                        {
                            return newMultinaryFunction(arguments);
                        }

                        throw new ArgumentException("The expression could not be parsed.", nameof(e));
                    };

                return recursive(e);
            }
            catch (ArithmeticException arithmeticException)
            {
                throw new ArgumentException("The expression could not be parsed.", nameof(e), arithmeticException);
            }
        }

        #endregion

        #region string

        /// <summary>Parses a symbolic methematics expression with the assumption that it will simplify to a constant.</summary>
        /// <typeparam name="T">The generic numerical type to recieve as the outputted type.</typeparam>
        /// <param name="string">The string to be parse.</param>
        /// <param name="tryParsingFunction">A function for parsing numerical values into the provided generic type.</param>
        /// <returns>The parsed expression simplified down to a constant value.</returns>
        public static T ParseAndSimplifyToConstant<T>(string @string, TryParseNumeric<T> tryParsingFunction = null)
        {
            return (Parse(@string, tryParsingFunction).Simplify() as Constant<T>).Value;
        }

        /// <summary>A try-parsing function to parse a string that represents a numerical value.</summary>
        /// <typeparam name="T">The type that the numeric value will be parsed into.</typeparam>
        /// <param name="string">The string to parse.</param>
        /// <param name="parsedValue">The parsed numeric value if successful.</param>
        /// <returns>Whether or not the parsing attempt was successful or not.</returns>
        public delegate bool TryParseNumeric<T>(string @string, out T parsedValue);

        /// <summary>Parses a string into a Towel.Mathematics.Symbolics expression tree.</summary>
        /// <typeparam name="T">The type to convert any constants into (ex: float, double, etc).</typeparam>
        /// <param name="string">The expression string to parse.</param>
        /// <param name="parsingFunction">A parsing function for the provided generic type. This is optional, but highly recommended.</param>
        /// <returns>The parsed Towel.Mathematics.Symbolics expression tree.</returns>
        public static Expression Parse<T>(string @string, TryParseNumeric<T> tryParsingFunction = null)
        {
            // Build The Parsing Library
            if (!ParseableLibraryBuilt)
            {
                BuildParsableOperationLibrary();
            }
            // Error Handling
            if (string.IsNullOrWhiteSpace(@string))
            {
                throw new ArgumentException("The expression could not be parsed. { " + @string + " }", nameof(@string));
            }
            // Trim
            @string = @string.Trim();
            // Parse The Next Non-Nested Operator If One Exist
            if (TryParseNonNestedOperatorExpression<T>(@string, tryParsingFunction, out Expression ParsedNonNestedOperatorExpression))
            {
                return ParsedNonNestedOperatorExpression;
            }
            // Parse The Next Parenthesis If One Exists
            if (TryParseParenthesisExpression<T>(@string, tryParsingFunction, out Expression ParsedParenthesisExpression))
            {
                return ParsedParenthesisExpression;
            }
            // Parse The Next Operation If One Exists
            if (TryParseOperationExpression<T>(@string, tryParsingFunction, out Expression ParsedOperationExpression))
            {
                return ParsedOperationExpression;
            }
            // Parse The Next Set Of Variables If Any Exist
            if (TryParseVariablesExpression<T>(@string, tryParsingFunction, out Expression ParsedVeriablesExpression))
            {
                return ParsedVeriablesExpression;
            }
            // Parse The Next Known Constant Expression If Any Exist
            if (TryParseKnownConstantExpression<T>(@string, tryParsingFunction, out Expression ParsedKnownConstantExpression))
            {
                return ParsedKnownConstantExpression;
            }
            // Parse The Next Constant Expression If Any Exist
            if (TryParseConstantExpression<T>(@string, tryParsingFunction, out Expression ParsedConstantExpression))
            {
                return ParsedConstantExpression;
            }
            // Invalid Or Non-Supported Expression
            throw new ArgumentException("The expression could not be parsed. { " + @string + " }", nameof(@string));
        }

        internal static bool TryParseNonNestedOperatorExpression<T>(string @string, TryParseNumeric<T> tryParsingFunction, out Expression expression)
        {
            // Try to match the operators pattern built at runtime based on the symbolic tree hierarchy
            MatchCollection operatorMatches = Regex.Matches(@string, ParsableOperatorsRegexPattern);
            if (operatorMatches.Count > 0)
            {
                // Find the first operator with the highest available priority
                Match @operator = null;
                OperatorPriority priority = default(OperatorPriority);
                int currentOperatorMatch = 0;
                int scope = 0;
                bool isUnaryLeftOperator = false;
                bool isUnaryRightOperator = false;
                bool isBinaryOperator = false;
                for (int i = 0; i < @string.Length; i++)
                {
                    switch (@string[i])
                    {
                        case '(': scope++; break;
                        case ')': scope--; break;
                    }

                    // Handle Input Errors
                    if (scope < 0)
                    {
                        throw new ArgumentException("The expression could not be parsed. { " + @string + " }", nameof(@string));
                    }
                    
                    Match currentMatch = operatorMatches[currentOperatorMatch];
                    if (currentMatch.Index == i)
                    {
                        if (scope == 0)
                        {
                            Match previousMatch = currentOperatorMatch != 0 ? operatorMatches[currentOperatorMatch - 1] : null;
                            Match nextMatch = currentOperatorMatch != operatorMatches.Count - 1 ? operatorMatches[currentOperatorMatch + 1] : null;

                            // We found an operator in the current scope
                            // Now we need to determine if it is a unary-left, unary-right, or binary operator

                            if (// first character in the expression
                                currentMatch.Index == 0 ||
                                // nothing but white space since the previous operator
                                (previousMatch != null &&
                                string.IsNullOrWhiteSpace(
                                    @string.Substring(
                                        previousMatch.Index + previousMatch.Length,
                                        currentMatch.Index - (previousMatch.Index + previousMatch.Length) - 1)) &&
                                        ParsableLeftUnaryOperators.ContainsKey(currentMatch.Value)))

                            {
                                // Unary-Left Operator
                                if (@operator == null || priority < ParsableLeftUnaryOperators[currentMatch.Value].Item1)
                                {
                                    @operator = currentMatch;
                                    isUnaryLeftOperator = true;
                                    isUnaryRightOperator = false;
                                    isBinaryOperator = false;
                                    priority = ParsableLeftUnaryOperators[currentMatch.Value].Item1;
                                }
                            }
                            else if (
                                // last character(s) in the expression
                                (currentMatch.Index + currentMatch.Length - 1) == @string.Length - 1 ||
                                // nothing but white space until the next operator
                                (nextMatch != null &&
                                string.IsNullOrWhiteSpace(
                                    @string.Substring(
                                        currentMatch.Index + currentMatch.Length,
                                        nextMatch.Index - (currentMatch.Index + currentMatch.Length) - 1)) &&
                                        ParsableRightUnaryOperators.ContainsKey(currentMatch.Value)))
                            {
                                // Unary Right Operator
                                if (@operator == null || priority < ParsableRightUnaryOperators[currentMatch.Value].Item1)
                                {
                                    @operator = currentMatch;
                                    isUnaryLeftOperator = false;
                                    isUnaryRightOperator = true;
                                    isBinaryOperator = false;
                                    priority = ParsableRightUnaryOperators[currentMatch.Value].Item1;
                                }
                            }
                            else
                            {
                                if (ParsableBinaryOperators.ContainsKey(currentMatch.Value))
                                {
                                    // Binary Operator
                                    if (@operator == null || priority < ParsableBinaryOperators[currentMatch.Value].Item1)
                                    {
                                        @operator = currentMatch;
                                        isUnaryLeftOperator = false;
                                        isUnaryRightOperator = false;
                                        isBinaryOperator = true;
                                        priority = ParsableBinaryOperators[currentMatch.Value].Item1;
                                    }
                                }
                            }
                        }
                        currentOperatorMatch++;

                        if (currentOperatorMatch >= operatorMatches.Count)
                        {
                            break;
                        }
                    }
                }

                // if an operator was found, parse the expression
                if (@operator != null)
                {
                    if (isUnaryLeftOperator)
                    {
                        string a = @string.Substring(@operator.Index + @operator.Length);
                        Expression A = Parse(a, tryParsingFunction);
                        expression = ParsableLeftUnaryOperators[@operator.Value].Item2(A);
                        return true;
                    }
                    else if (isUnaryRightOperator)
                    {
                        string a = @string.Substring(0, @operator.Index);
                        Expression A = Parse(a, tryParsingFunction);
                        expression = ParsableRightUnaryOperators[@operator.Value].Item2(A);
                        return true;
                    }
                    else if (isBinaryOperator)
                    {
                        string a = @string.Substring(0, @operator.Index);
                        Expression A = Parse(a, tryParsingFunction);
                        string b = @string.Substring(@operator.Index + @operator.Length);
                        Expression B = Parse(b, tryParsingFunction);
                        expression = ParsableBinaryOperators[@operator.Value].Item2(A, B);
                        return true;
                    }
                }
            }

            // No non-nested operator patterns found. Fall back.
            expression = null;
            return false;
        }

        internal static bool TryParseParenthesisExpression<T>(string @string, TryParseNumeric<T> tryParsingFunction, out Expression expression)
        {
            // Try to match a parenthesis pattern.
            Match parenthesisMatch = Regex.Match(@string, ParenthesisPattern);
            Match operationMatch = Regex.Match(@string, ParsableOperationsRegexPattern);
            if (parenthesisMatch.Success)
            {
                if (operationMatch.Success && parenthesisMatch.Index > operationMatch.Index)
                {
                    // The next set of parenthesis are part of an operation. Fall back and
                    // let the TryParseOperationExpression handle it.
                    expression = null;
                    return false;
                }

                // Parse the nested expression
                string nestedExpression = parenthesisMatch.Value.Substring(1, parenthesisMatch.Length - 2);
                expression = Parse(nestedExpression, tryParsingFunction);

                // Check for implicit multiplications to the left of the parenthesis pattern
                if (parenthesisMatch.Index > 0)
                {
                    string leftExpression = @string.Substring(0, parenthesisMatch.Index);
                    expression *= Parse(leftExpression, tryParsingFunction);
                }

                // Check for implicit multiplications to the right of the parenthesis pattern
                int right_start = parenthesisMatch.Index + parenthesisMatch.Length;
                if (right_start != @string.Length)
                {
                    string rightExpression = @string.Substring(right_start, @string.Length - right_start);
                    expression *= Parse(rightExpression, tryParsingFunction);
                }

                // Parsing was successful
                return true;
            }

            // No parenthesis pattern found. Fall back.
            expression = null;
            return false;
        }

        internal static bool TryParseOperationExpression<T>(string @string, TryParseNumeric<T> tryParsingFunction, out Expression expression)
        {
            expression = null;
            Match operationMatch = Regex.Match(@string, ParsableOperationsRegexPattern);

            if (operationMatch.Success)
            {
                string operationMatch_Value = operationMatch.Value;
                string operation = operationMatch_Value.Substring(0, operationMatch_Value.IndexOf('(') - 1);
                Match parenthesisMatch = Regex.Match(@string, ParenthesisPattern);
                string parenthesisMatch_Value = parenthesisMatch.Value;
                AddableArray<string> operandSplits = SplitOperands(parenthesisMatch_Value.Substring(1, parenthesisMatch_Value.Length - 2));

                switch (operandSplits.Count)
                {
                    case 1:
                        Func<Expression, Unary> newUnaryFunction;
                        if (ParsableUnaryOperations.TryGetValue(operation, out newUnaryFunction))
                        {
                            expression = newUnaryFunction(Parse<T>(operandSplits[0]));
                        }
                        break;
                    case 2:
                        Func<Expression, Expression, Binary> newBinaryFunction;
                        if (ParsableBinaryOperations.TryGetValue(operation, out newBinaryFunction))
                        {
                            expression = newBinaryFunction(Parse<T>(operandSplits[0]), Parse<T>(operandSplits[1]));
                        }
                        break;
                    case 3:
                        Func<Expression, Expression, Expression, Ternary> newTernaryFunction;
                        if (ParsableTernaryOperations.TryGetValue(operation, out newTernaryFunction))
                        {
                            expression = newTernaryFunction(Parse<T>(operandSplits[0]), Parse<T>(operandSplits[2]), Parse<T>(operandSplits[2]));
                        }
                        break;
                }
                Func<Expression[], Multinary> newMultinaryFunction;
                if (ParsableMultinaryOperations.TryGetValue(operation, out newMultinaryFunction))
                {
                    expression = newMultinaryFunction(operandSplits.Select(x => Parse<T>(x)).ToArray());
                }

                // handle implicit multiplications if any exist
                if (operationMatch.Index != 0) // Left
                {
                    Expression A = Parse(@string.Substring(0, operationMatch.Index), tryParsingFunction);
                    expression *= A;
                }
                if (operationMatch.Length + operationMatch.Index < @string.Length) // Right
                {
                    Expression A = Parse(@string.Substring(operationMatch.Length + operationMatch.Index), tryParsingFunction);
                    expression *= A;
                }

                if (!(expression is null))
                {
                    return true;
                }
                else
                {
                    throw new ArgumentException("The expression could not be parsed. { " + @string + " }", nameof(@string));
                }
            }

            // No operation pattern found. Fall back.
            return false;
        }

        internal static AddableArray<string> SplitOperands(string @string)
        {
            AddableArray<string> operands = new AddableArray<string>();
            int scope = 0;
            int operandStart = 0;
            for (int i = 0; i < @string.Length; i++)
            {
                switch (@string[i])
                {
                    case '(': scope++; break;
                    case ')': scope--; break;
                    case ',':
                        if (scope == 0)
                        {
                            operands.Add(@string.Substring(operandStart, i - operandStart));
                        }
                        break;
                }
            }
            if (scope != 0)
            {
                throw new ArgumentException("The expression could not be parsed. { " + @string + " }", nameof(@string));
            }
            operands.Add(@string.Substring(operandStart, @string.Length - operandStart));
            return operands;
        }

        internal static bool TryParseVariablesExpression<T>(string @string, TryParseNumeric<T> tryParsingFunction, out Expression parsedExpression)
        {
            string variablePattern = @"\[.*\]";

            // extract and parse variables
            System.Collections.Generic.IEnumerable<Expression> variables =
                Regex.Matches(@string, variablePattern)
                .Cast<Match>()
                .Select(x => new Variable(x.Value.Substring(1, x.Value.Length - 2)));

            // if no variables, fall back
            if (!variables.Any())
            {
                parsedExpression = null;
                return false;
            }

            // assume the remaining string splits are constants and try to parse them
            System.Collections.Generic.IEnumerable<Expression> constants =
                Regex.Split(@string, variablePattern)
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Select(x =>
                {
                    Expression exp;
                    TryParseConstantExpression(x, tryParsingFunction, out exp);
                    return exp;
                });

            // multiply all the expressions together, starting with the constants because 
            // it will look better if converted to a string
            bool set = false;
            parsedExpression = null;
            foreach (Expression constant in constants.Concat(variables))
            {
                if (!set)
                {
                    parsedExpression = constant;
                    set = true;
                }
                else
                {
                    parsedExpression *= constant;
                }
            }
            return true;
        }

        internal static bool TryParseKnownConstantExpression<T>(string @string, TryParseNumeric<T> tryParsingFunction, out Expression parsedExpression)
        {
            Match knownConstantMatch = Regex.Match(@string, ParsableKnownConstantsRegexPattern);

            if (knownConstantMatch.Success)
            {
                parsedExpression = ParsableKnownConstants[knownConstantMatch.Value]().ApplyType<T>();

                // implied multiplications to the left and right
                if (knownConstantMatch.Index != 0)
                {
                    Expression A = Parse<T>(@string.Substring(0, knownConstantMatch.Index));
                    parsedExpression *= A;
                }
                if (knownConstantMatch.Index < @string.Length - 1)
                {
                    Expression B = Parse<T>(@string.Substring(knownConstantMatch.Index + 1));
                    parsedExpression *= B;
                }
                return true;
            }

            parsedExpression = null;
            return false;
        }

        internal static bool TryParseConstantExpression<T>(string @string, TryParseNumeric<T> tryParsingFunction, out Expression parsedExpression)
        {
            if (tryParsingFunction != null && tryParsingFunction(@string, out T parsedValue))
            {
                parsedExpression = new Constant<T>(parsedValue);
                return true;
            }

            int decimalIndex = -1;
            for (int i = 0; i < @string.Length; i++)
            {
                char character = @string[i];
                if (character == '.')
                {
                    if (decimalIndex >= 0 || i == @string.Length - 1)
                    {
                        parsedExpression = null;
                        return false;
                    }
                    decimalIndex = i;
                }
                if ('0' > character && character > '9')
                {
                    parsedExpression = null;
                    return false;
                }
            }

            if (decimalIndex >= 0)
            {
                string wholeNumberString;
                if (decimalIndex == 0)
                {
                    wholeNumberString = "0";
                }
                else
                {
                    wholeNumberString = @string.Substring(0, decimalIndex);
                }
                string decimalPlacesString = @string.Substring(decimalIndex + 1);

                if (int.TryParse(wholeNumberString, out int wholeNumberInt) &&
                    int.TryParse(decimalPlacesString, out int decimalPlacesInt))
                {
                    T wholeNumber = Compute.FromInt32<T>(wholeNumberInt);
                    T decimalPlaces = Compute.FromInt32<T>(decimalPlacesInt);
                    while (Compute.GreaterThanOrEqual(decimalPlaces, Mathematics.Constant<T>.One))
                    {
                        decimalPlaces = Compute.Divide(decimalPlaces, Mathematics.Constant<T>.Ten);
                    }
                    parsedExpression = new Constant<T>(Compute.Add(wholeNumber, decimalPlaces));
                    return true;
                }
            }
            else
            {
                if (int.TryParse(@string, out int parsedInt))
                {
                    parsedExpression = new Constant<T>(Compute.FromInt32<T>(parsedInt));
                    return true;
                }
            }

            parsedExpression = null;
            return false;
        }

        #endregion

        #endregion
    }
}