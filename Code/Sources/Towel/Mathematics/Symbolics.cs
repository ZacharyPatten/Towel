using System;
using System.Linq.Expressions;
using System.Reflection;
using Towel.Structures;
using System.Linq;

namespace Towel.Mathematics
{
	/// <summary>Base implementations for symbolic mathematics.</summary>
	/// <typeparam name="T">The generic numeric type. (double, int, decimal, etc.)</typeparam>
	public static class Symbolics<T>
	{
        // types of nodes in a symbolic node tree
		#region Definition

		public abstract class Node
		{
			public static implicit operator Node(T constant) { return new Constant(constant); }

			public Node Simplify() { return Symbolics<T>.Simplify(this); }
			public Node Assign(string variable, T value) { return Symbolics<T>.Substitute(this, variable, value); }
			public Node Derive(string variable) { return Symbolics<T>.Derive(this, variable); }
			public Node Integrate(string variable) { return Symbolics<T>.Integrate(this, variable); }

            public abstract void Stepper(Step<Node> step);

            public static Node operator +(Node a, Node b) { return new Addition(Clone(a), Clone(b)); }
            public static Node operator -(Node a, Node b) { return new Subtraction(Clone(a), Clone(b)); }
            public static Node operator *(Node a, Node b) { return new Multiplication(Clone(a), Clone(b)); }
            public static Node operator /(Node a, Node b) { return new Division(Clone(a), Clone(b)); }
            /// <summary>
            /// WARNING: NOT A NODE EQUALITY CHECK. THIS IS A SYMBOLIC MATH WRAPPER. See "Symbolics.AreEqual" for node comparison.
            /// </summary>
            //public static Node operator ==(Node a, Node b) { return new Equate(Clone(a), Clone(b)); }
            //public static Node operator !=(Node a, Node b) { return new EquateNot(Clone(a), Clone(b)); }
            //public static Node operator <(Node a, Node b) { return new LessThan(Clone(a), Clone(b)); }
            //public static Node operator >(Node a, Node b) { return new GreaterThan(Clone(a), Clone(b)); }
		}

		public class Constant : Node
		{
			T _constant;

			public T Value { get { return this._constant; } }
			
			public Constant(T constant)
			{
				this._constant = constant;
			}

			public static implicit operator T(Constant constant) { return constant._constant; }
			
			public override string ToString() { return this._constant.ToString(); }

            public static bool operator ==(Constant a, Constant b)
            {
                if (a == null)
                {
                    throw new ArgumentNullException(nameof(a));
                }
                if (b == null)
                {
                    throw new ArgumentNullException(nameof(b));
                }
                return Compute.Equal(a, b);
            }

            public static bool operator !=(Constant a, Constant b)
            {
                return !(a == b);
            }

            public override void Stepper(Step<Node> step) { }
		}

		public class Variable : Node
		{
			public string _name;

			public string Name { get { return this._name; } }
			
			public Variable(string name)
			{
				this._name = name;
			}
			
			public override string ToString() { return this._name; }

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

            public static implicit operator string(Variable variable) { return variable._name; }

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

            public override void Stepper(Step<Node> step) { }
		}

		public abstract class Operation : Node
		{
		}

		public abstract class Unary : Operation
		{
			protected Node _operand;

			public Node Operand
			{
				get { return this._operand; }
				set { this._operand = value; }
			}

			public Unary() : base() { }

			public Unary(Node operand)
			{
				this._operand = operand;
			}

            public override void Stepper(Step<Node> step)
            {
                step(this._operand);
            }
		}

		public class Negate : Unary
		{
			public Negate() : base() { }

			public Negate(Node operand) : base(operand) { }

			public override string ToString() { return string.Concat("-", this._operand); }
		}

        public abstract class Trigonometry : Unary
        {
            public Trigonometry() : base() { }

            public Trigonometry(Node operand)
			{
				this._operand = operand;
			}
        }

		public class Sine : Trigonometry
		{
			public Sine() : base() { }

			public Sine(Node operand) : base(operand) { }

			public override string ToString() { return string.Concat("Sine(", this._operand, ")"); }
		}

		public class Cosine : Trigonometry
		{
			public Cosine() : base() { }

			public Cosine(Node operand) : base(operand) { }

			public override string ToString() { return string.Concat("Cosine(", this._operand, ")"); }
		}

		public class Tangent : Trigonometry
		{
			public Tangent() : base() { }

			public Tangent(Node operand) : base(operand) { }

			public override string ToString() { return string.Concat("Tangent(", this._operand, ")"); }
		}

        public class Cosecant : Trigonometry
        {
            public Cosecant() : base() { }

			public Cosecant(Node operand) : base(operand) { }

			public override string ToString() { return string.Concat("Cosecant(", this._operand, ")"); }
        }

        public class Secant : Trigonometry
        {
            public Secant() : base() { }

			public Secant(Node operand) : base(operand) { }

			public override string ToString() { return string.Concat("Secant(", this._operand, ")"); }
        }

        public class Cotangent : Trigonometry
        {
            public Cotangent() : base() { }

			public Cotangent(Node operand) : base(operand) { }

			public override string ToString() { return string.Concat("Cotangent(", this._operand, ")"); }
        }

		public class NaturalLog : Unary
		{
			public NaturalLog() : base() { }

			public NaturalLog(Node operand) : base(operand) { }

			public override string ToString() { return string.Concat("ln(", this._operand, ")"); }
		}

		public class SquareRoot : Unary
		{
			public SquareRoot() : base() { }

			public SquareRoot(Node operand) : base(operand) { }

			public override string ToString() { return string.Concat("sqrt(", this._operand, ")"); }
		}

		public class Exponential : Unary
		{
			public Exponential() : base() { }

			public Exponential(Node operand) : base(operand) { }

			public override string ToString() { return string.Concat("e^(", this._operand, ")"); }
		}

		public class Invert : Unary
		{
			public Invert() : base() { }

			public Invert(Node operand) : base(operand) { }
		}

		public class Determinent : Unary
		{
			public Determinent() : base() { }

			public Determinent(Node operand) : base(operand) { }
		}

		public abstract class Binary : Operation
		{
			protected Node _left;
			protected Node _right;

			public Node Left
			{
				get { return this._left; }
				set { this._left = value; }
			}

			public Node Right
			{
				get { return this._right; }
				set { this._right = value; }
			}

			public Binary() { }

			public Binary(Node left, Node right)
			{
				this._left = left;
				this._right = right;
			}

            public override void Stepper(Step<Node> step)
            {
                step(this._left);
                step(this._right);
            }
		}

        public abstract class AdditionOrSubtraction : Binary
        {
            public AdditionOrSubtraction() : base() { }

            public AdditionOrSubtraction(Node left, Node right)  : base(left, right) { }
        }

        public class Addition : AdditionOrSubtraction
		{
			public Addition() : base() { }

			public Addition(Node left, Node right) : base(left, right) { }

			public override string ToString()
			{
				string left = this._left.ToString();
				if (this._left is Multiplication || this._left is Division && left is Constant && Compute.Compare(left as Constant, Compute.Constant<T>.Zero) == Comparison.Less)
					left = string.Concat("(", left, ")");
				string right = this._right.ToString();
				if (this._right is Addition || this._right is Subtraction || this._left is Multiplication || this._left is Division)
					right = string.Concat("(", right, ")");
				if (this._right is Constant && Compute.Compare(this._right as Constant, Compute.Constant<T>.Zero) == Comparison.Less)
					return string.Concat(left, " - ", Compute.Multiply(this._right as Constant, Compute.FromInt32<T>(-1)));
				return string.Concat(left, " + ", right);
			}
		}

        public class Subtraction : AdditionOrSubtraction
		{
			public Subtraction() : base() { }

			public Subtraction(Node left, Node right) : base(left, right) { }

			public override string ToString()
			{
				string left = this._left.ToString();
				if (this._left is Multiplication || this._left is Division)
					left = string.Concat("(", left, ")");
				string right = this._right.ToString();
				if (this._right is Addition || this._right is Subtraction || this._left is Multiplication || this._left is Division)
					right = string.Concat("(", right, ")");
				return string.Concat(left, " - ", right);
			}
		}

        public abstract class MultiplicationOrDivision : Binary
        {
            public MultiplicationOrDivision() : base() { }

            public MultiplicationOrDivision(Node left, Node right) : base(left, right) { }
        }

        public class Multiplication : MultiplicationOrDivision
		{
			public Multiplication() : base() { }

			public Multiplication(Node left, Node right) : base(left, right) { }

			public override string ToString()
			{
				string left = this._left.ToString();
				//if (this._left is Multiplication || this._left is Division)
				//	left = string.Concat("(", left, ")");
				string right = this._right.ToString();
				if (this._right is Multiplication || this._right is Division)
					right = string.Concat("(", right, ")");
				return string.Concat(left, " * ", right);
			}
		}

        public class Division : MultiplicationOrDivision
		{
			public Division() : base() { }

			public Division(Node left, Node right) : base(left, right) { }

			public override string ToString()
			{
				string left = this._left.ToString();
				//if (this._left is Multiplication || this._left is Division)
				//	left = string.Concat("(", left, ")");
				string right = this._right.ToString();
				if (this._right is Multiplication || this._right is Division)
					right = string.Concat("(", right, ")");
				return string.Concat(left, " / ", right);
			}
		}

		public class Power : Binary
		{
			public Power() : base() { }

			public Power(Node left, Node right) : base(left, right) { }

			public override string ToString() { return string.Concat(this._left, " ^ ", this._right); }
		}

		public class Root : Binary
		{
			public Root() : base() { }

			public Root(Node left, Node right) : base(left, right) { }

			public override string ToString() { return string.Concat(this._left, " ^ (1 / ", this._right, ")"); }
		}

		public class Equate : Multinary
		{
			public Equate() : base() { }

            public Equate(params Node[] nodes) : base(nodes) { }

			public override string ToString()
            {
                if (this._operands.Length > 0)
                {
                    System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
                    stringBuilder.Append(this._operands[0].ToString());
                    foreach (Node node in this._operands)
                    {
                        stringBuilder.Append(" = ");
                        stringBuilder.Append(node);
                    }
                    return stringBuilder.ToString();
                }
                else
                {
                    return string.Empty;
                }
            }
		}

        public class LessThan : Binary
        {
            public LessThan() : base() { }

            public LessThan(Node left, Node right) : base(left, right) { }

			public override string ToString() { return string.Concat(this._left, " < ", this._right); }
        }

        public class GreaterThan : Binary
        {
            public GreaterThan() : base() { }

            public GreaterThan(Node left, Node right) : base(left, right) { }

            public override string ToString() { return string.Concat(this._left, " > ", this._right); }
        }

		public abstract class Ternary : Operation
		{
			protected Node _one;
			protected Node _two;
			protected Node _three;

			public Node One
			{
				get { return this._one; }
				set { this._one = value; }
			}

			public Node Two
			{
				get { return this._two; }
				set { this._two = value; }
			}

			public Node Three
			{
				get { return this._three; }
				set { this._three = value; }
			}

			public Ternary() { }

			public Ternary(Node one, Node two, Node three)
			{
				this._one = one;
				this._two = two;
				this._three = three;
			}

            public override void Stepper(Step<Node> step)
            {
                step(this._one);
                step(this._two);
                step(this._three);
            }
		}

		public abstract class Multinary : Operation
		{
			protected Node[] _operands;

			public Node[] Operands
			{
				get { return this._operands; }
				set { this._operands = value; }
			}

			public Multinary() { }

			public Multinary(Node[] operands)
			{
				this._operands = operands;
			}

            public override void Stepper(Step<Node> step)
            {
                foreach (Node node in this._operands)
                    step(node);
            }
		}

		public class Summation : Multinary
		{
			public Summation() : base() { }

			public Summation(Node[] array) : base(array) { }
		}

		#endregion

        // parsing input into a symbolic definition node tree
		#region Interpretation

		public static Node Parse(Expression e)
		{
			try
			{
				System.Func<Expression, Node> recursive = null;
                System.Func<MethodCallExpression, Node> methodCallExpression_to_node = null;

				recursive =
					(Expression expression) =>
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
								return new Constant((T)(expression as ConstantExpression).Value);
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
								return new Addition(recursive(binary_expression.Left), recursive(binary_expression.Right));
							case ExpressionType.Subtract:
								return new Subtraction(recursive(binary_expression.Left), recursive(binary_expression.Right));
							case ExpressionType.Multiply:
								return new Multiplication(recursive(binary_expression.Left), recursive(binary_expression.Right));
							case ExpressionType.Divide:
								return new Division(recursive(binary_expression.Left), recursive(binary_expression.Right));
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
                            throw new System.ArithmeticException("Invalid syntax parse (only members of Towel.MathematicsCompute<T> allowed): " + methodCallExpression);

						Node[] nodes = null;
                        if (methodCallExpression.Arguments != null)
						{
                            nodes = new Node[methodCallExpression.Arguments.Count];
							for (int i = 0; i < nodes.Length; i++)
                                nodes[i] = recursive(methodCallExpression.Arguments[i]);
						}

                        switch (method.Name)
						{
							// constants
							case "Pi": break;
							// arithmetic
							case "Negate": return new Negate(nodes[0]);
							case "Add": return new Addition(nodes[0], nodes[1]);
							case "Summation": return new Summation(nodes);
							case "Subtract": return new Subtraction(nodes[0], nodes[1]);
							case "Multiply": return new Multiplication(nodes[0], nodes[1]);
							case "Divide": return new Division(nodes[0], nodes[1]);
							case "Power": return new Power(nodes[0], nodes[1]);
							case "SquareRoot": return new SquareRoot(nodes[0]);
							case "Root": return new Root(nodes[0], nodes[1]);
							// logic
							case "AbsoluteValue": break;
							case "max": break;
							case "min": break;
							case "clamp": break;
							case "equ_len": break;
							case "Compare": break;
							case "Equate": return new Equate(nodes[0], nodes[1]);
							// factoring
							case "GreatestCommonFactor": break;
							case "LeastCommonMultiple": break;
							case "factorPrimes": break;
							// eponentials
							case "exp": break;
							case "ln": break;
							case "log": break;
							// angle
							case "DegreesToRadians": break;
							case "TurnsToRadians": break;
							case "GradiansToRadians": break;
							case "RadiansToDegrees": break;
							case "RadiansToTurns": break;
							case "RadiansToGradians": break;
							// miscelaneous
							case "IsPrime": break;
							case "invert": break;
							// interpolation
							case "LinearInterpolation": break;
							// Vector
							case "Vector_Add": break;
							case "Vector_Negate": break;
							case "Vector_Subtract": break;
							case "Vector_Multiply": break;
							case "Vector_Divide": break;
							case "Vector_DotProduct": break;
							case "Vector_CrossProduct": break;
							case "Vector_Normalize": break;
							case "Vector_Magnitude": break;
							case "Vector_MagnitudeSquared": break;
							case "Vector_Angle": break;
							case "Vector_RotateBy": break;
							case "Vector_Lerp": break;
							case "Vector_Slerp": break;
							case "Vector_Blerp": break;
							case "Vector_EqualsValue": break;
							case "Vector_EqualsValue_leniency": break;
							case "Vector_RotateBy_quaternion": break;
							// Matrix
							case "Matrix_FactoryZero": break;
							case "Matrix_FactoryOne": break;
							case "Matrix_FactoryIdentity": break;
							case "Matrix_IsSymetric": break;
							case "Matrix_Negate": break;
							case "Matrix_Add": break;
							case "Matrix_Subtract": break;
							case "Matrix_Multiply": break;
							case "Matrix_Multiply_vector": break;
							case "Matrix_Multiply_scalar": break;
							case "Matrix_Divide": break;
							case "Matrix_Power": break;
							case "Matrix_Minor": break;
							case "Matrix_ConcatenateRowWise": break;
							case "Matrix_Determinent": break;
							case "Matrix_Echelon": break;
							case "Matrix_ReducedEchelon": break;
							case "Matrix_Inverse": break;
							case "Matrix_Adjoint": break;
							case "Matrix_Transpose": break;
							case "Matrix_DecomposeLU": break;
							case "Matrix_EqualsByValue": break;
							case "Matrix_EqualsByValue_leniency": break;
							case "Matrix_RowMultiplication": break;
							case "Matrix_RowAddition": break;
							case "Matrix_SwapRows": break;
							// Quaternion
							case "Quaternion_Magnitude": break;
							case "Quaternion_MagnitudeSquared": break;
							case "Quaternion_Conjugate": break;
							case "Quaternion_Add": break;
							case "Quaternion_Subtract": break;
							case "Quaternion_Multiply": break;
							case "Quaternion_Multiply_scalar": break;
							case "Quaternion_Multiply_Vector": break;
							case "Quaternion_Normalize": break;
							case "Quaternion_Invert": break;
							case "Quaternion_Lerp": break;
							case "Quaternion_Slerp": break;
							case "Quaternion_Rotate": break;
							case "Quaternion_EqualsValue": break;
							case "Quaternion_EqualsValue_leniency": break;
							// combinatorics
							case "Factorial": break;
							case "Combinations": break;
							case "Choose": break;
							// statistics
							case "Mode": break;
							case "Mean": break;
							case "Median": break;
							case "GeometricMean": break;
							case "Variance": break;
							case "StandardDeviation": break;
							case "MeanDeviation": break;
							case "Range": break;
							case "Quantiles": break;
							case "Correlation": break;
							// trigonometry
							case "Sine": return new Sine(nodes[0]);
							case "Cosine": return new Cosine(nodes[0]);
							case "Tangent": return new Tangent(nodes[0]);
							case "Cosecant": break;
							case "Secant": break;
							case "Cotangent": break;
							case "InverseSine": break;
							case "InverseCosine": break;
							case "InverseTangent": break;
							case "InverseCosecant": break;
							case "InverseSecant": break;
							case "InverseCotangent": break;
							case "HyperbolicSine": break;
							case "HyperbolicCosine": break;
							case "HyperbolicTangent": break;
							case "HyperbolicSecant": break;
							case "HyperbolicCosecant": break;
							case "HyperbolicCotangent": break;
							case "InverseHyperbolicSine": break;
							case "InverseHyperbolicCosine": break;
							case "InverseHyperbolicTangent": break;
							case "InverseHyperbolicCosecant": break;
							case "InverseHyperbolicSecant": break;
							case "InverseHyperbolicCotangent": break;
						}
                        throw new System.ArithmeticException("Invalid syntax parse (only members of Towel.MathematicsCompute<T> allowed): " + methodCallExpression);
					};

				return recursive(e);
			}
			catch (System.ArithmeticException exception_specific)
			{
				throw new System.ArithmeticException("failed to parse expression into Towel Framework mathematical syntax: " + e, exception_specific);
			}
		}

		public static Node Parse(string tree)
		{
            // constant node: "7"
            if (tree.All(x => char.IsDigit(x)))
            {
                return new Constant(Compute.FromInt32<T>(int.Parse(tree)));
            }
            // constant node (rational): "7.7"
            if (tree.All(x => char.IsDigit(x) || x == '.'))
            {
                string whole = tree.Substring(0, tree.IndexOf('.') - 1);
                string partial = tree.Substring(tree.IndexOf('.') + 1, (tree.Length - 1) - (tree.IndexOf('.') + 1));

                int dividend = (int)System.Math.Pow(10, partial.Length);
                T wholeValue = Compute.FromInt32<T>(int.Parse(whole));
                T partialValue = Compute.Divide(Compute.FromInt32<T>(int.Parse(partial)), Compute.FromInt32<T>(dividend));

                return new Constant(Compute.Add(wholeValue, partialValue));
            }
            // variable node: "[variable]"
            else if (tree[0] == '[')
            {
                string variable = tree.Substring(1, tree.Length - 2);
                return new Variable(variable);
            }
            // operation node: "token(argument[0], argument[1], ...)"
            else
            {
                // get the token
                string token = tree.Substring(0, tree.IndexOf('('));

                // get the substring of arguments
                int arguments_start = token.Length + 1;
                int arguments_length = (tree.Length - 1) - arguments_start;
                string list = tree.Substring(arguments_start, arguments_length);

                // count the number of arguments
                int scope = 0;
                int argument = 0;
                if (list.Length > 0 && list[0] != '(')
                {
                    argument++;
                }
                for (int b = 0; b < list.Length; b++)
                    switch (list[b])
                    {
                        case '(':
                            scope++;
                            break;
                        case ')':
                            scope--;
                            break;
                        case ',':
                            if (scope == 0)
                                argument++;
                            break;
                    }

                // get the arguments
                string[] arguments = new string[argument];
                argument = 0;
                scope = 0;
                int a = 0;
                for (int b = 0; b < list.Length; b++)
                    switch (list[b])
                    {
                        case '(':
                            scope++;
                            break;
                        case ')':
                            scope--;
                            break;
                        case ',':
                            if (scope == 0)
                            {
                                arguments[argument] = list.Substring(a, b - a).Trim();
                                a = b + 1;
                                argument++;
                            }
                            break;
                    }

                arguments[argument] = list.Substring(a).Trim();

                // recursive calls
                Node[] nodes = new Node[arguments.Length];
                for (int i = 0; i < nodes.Length; i++)
                    nodes[i] = Parse(arguments[i]);

                // node creation
                switch (token)
                {
                    case "add":
                        return new Addition(nodes[0], nodes[1]);

                    case "subtract":
                        return new Subtraction(nodes[0], nodes[1]);

                    case "multiply":
                        return new Multiplication(nodes[0], nodes[1]);

                    case "divide":
                        return new Division(nodes[0], nodes[1]);

                    case "power":
                        return new Power(nodes[0], nodes[1]);

                    case "negate":
                        return new Negate(nodes[0]);

                    case "sin":
                        return new Sine(nodes[0]);

                    case "cos":
                        return new Cosine(nodes[0]);

                    case "tan":
                        return new Tangent(nodes[0]);

                    case "csc":
                        return new Cosecant(nodes[0]);

                    case "sec":
                        return new Secant(nodes[0]);

                    case "cot":
                        return new Cotangent(nodes[0]);

                    //case "arcsin":
                    //    return new Arcsin(nodes[0]);

                    //case "arccos":
                    //    return new Arccos(nodes[0]);

                    //case "arctan":
                    //    return new Arctan(nodes[0]);

                    //case "arccsc":
                    //    return new Arccsc(nodes[0]);

                    //case "arcsec":
                    //    return new ArcSec(nodes[0]);

                    //case "arccot":
                    //    return new Arccot(nodes[0]);

                    case "equate":
                        return new Equate(nodes);

                    case "less":
                        return new LessThan(nodes[0], nodes[1]);

                    case "greater":
                        return new GreaterThan(nodes[0], nodes[1]);

                    //case "derive":
                    //	return;
                    //case "integrate":
                    //	return;
                    //case "integrate":
                    //	return;

                    default:
                        throw new System.Exception("mathematics parsing error");

                }
            }
		}

		#endregion

        // properties and pattern matching for node trees
        #region Evaluation

        // pattern matching for specific formats (such as polynomials, terms, quadratics, lines, etc.)
        #region Pattern Matching

        /// <summary>
        /// Algebraic Expression: a mathematical expression (non logic, set theory, etc.) that could appear
        /// on either side of an equals sign.
        /// </summary>
        public static bool IsValidAlgebraicExpression(Node node)
        {
            throw new System.NotImplementedException();
            //if ()
        }

        public static bool IsSimplifiableToPolynomial(Node node)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Simplified Polynomial: a simplified expression consisting of variables and coefficients,
        /// that involves only the operations of addition, subtraction, multiplication,
        /// and non-negative integer exponents. NOTE: simplify nodes before calling
        /// this function; nodes like "x ^ (2 / 1)" and "(2 ^ 1)x" will return false.
        /// </summary>
        /// <param name="node">The node to determine if it is a polynomial or not.</param>
        /// <returns>True if the node is a polynmial. False if not.</returns>
        public static bool IsSimplifiedPolynomial(Node node)
            {
                bool containsTerm = false;
                return IsSimplifiedPolynomial(node, ref containsTerm, false, new SetHashArray<string>(), new MapHashLinked<SetHashArray<T>, string>()) && containsTerm;
            }
        private static bool IsSimplifiedPolynomial(Node node,
            // is there at least one term
            ref bool containsTerm,
            // validate simplification. Examples: "2 * x * x" and "2 * (x + y)" not simplified
            bool withinMultiplication,
            SetHashArray<string> existingVariables,
            // validate simplification. Examples: "x ^ 2 + x ^ 2" not simplified
            MapHashLinked<SetHashArray<T>, string> existingExponents)
            {
                if (node is Multiplication)
                {
                    Binary binary = node as Binary;
                    if (!IsSimplifiedPolynomial(binary.Left, ref containsTerm, true, existingVariables.Clone(), existingExponents))
                        return false;
                    if (!IsSimplifiedPolynomial(binary.Right, ref containsTerm, true, existingVariables.Clone(), existingExponents))
                        return false;
                }
                else if (node is Addition || node is Subtraction)
                {
                    if (withinMultiplication)
                        return false;
                    Binary binary = node as Binary;
                    if (!IsSimplifiedPolynomial(binary.Left, ref containsTerm, withinMultiplication, existingVariables, existingExponents))
                        return false;
                    if (!IsSimplifiedPolynomial(binary.Right, ref containsTerm, withinMultiplication, existingVariables, existingExponents))
                        return false;
                }
                else if (node is Power) // Only valid if: Power(Variable, Constant)
                {
                    Binary binary = node as Binary;
                    if (!(binary.Left is Variable))
                        return false;
                    if (!(binary.Right is Constant))
                        return false;
                    Variable variable = binary.Left as Variable;
                    Constant exponent = binary.Right as Constant;
                    if (withinMultiplication)
                    {
                        if (existingVariables.Contains((binary.Left as Variable).Name))
                        {
                            return false;
                        }
                        else
                        {
                            existingVariables.Add((node as Variable).Name);
                        }
                    }
                    containsTerm = true; // we know there is at least one term now
                    if (!Compute.IsInteger(exponent) || !Compute.IsNonNegative(exponent))
                        return false;
                    if (existingExponents.Contains(variable))
                    {
                        if (existingExponents[variable].Contains(exponent))
                        {
                            return false;
                        }
                        else
                        {
                            existingExponents[variable].Add(exponent);
                        }
                    }
                    else
                    {
                        existingExponents.Add(variable, new SetHashArray<T>());
                        existingExponents[variable].Add(exponent);
                    }

                }
                else if (node is Variable) // Only enters if there is no exponent (would have been caught by Power)
                {
                    if (withinMultiplication)
                    {
                        if (existingVariables.Contains((node as Variable).Name))
                        {
                            return false;
                        }
                        else
                        {
                            existingVariables.Add((node as Variable).Name);
                        }
                    }
                    containsTerm = true; // we know there is at least one term now
                }
                else if (node is Constant)
                {
                    containsTerm = true; // we know there is at least one term now
                }
                else if (node is Negate)
                {
                    // pass through
                }
                else
                {
                    return false;
                }
                return true;
            }

        /// <summary>
        /// Simplified Term: either a single number or variable, or the product of several numbers or variables that is simplified.
        /// </summary>
        /// <param name="node">The node to determine if it is a simplified term.</param>
        /// <returns>Whether or not the node is a simplified term.</returns>
        public static bool IsSimplifiedTerm(Node node)
            {
                bool containsTerm = false;
                bool coefficientExists = false;
                SetHashArray<Variable> existingVariables = new SetHashArray<Variable>();
                return IsSimplifiedTerm(node, ref containsTerm, ref coefficientExists, existingVariables);
            }
        private static bool IsSimplifiedTerm(Node node, ref bool containsTerm, ref bool coefficientExists, SetHashArray<Variable> existingVariables)
            {
                if (node is Multiplication)
                {
                    Multiplication multiplication = node as Multiplication;
                    IsSimplifiedTerm(multiplication.Left, ref containsTerm, ref coefficientExists, existingVariables);
                    IsSimplifiedTerm(multiplication.Right, ref containsTerm, ref coefficientExists, existingVariables);
                }
                else if (node is Power)
                {
                    Power power = node as Power;
                    if (!(power.Left is Variable))
                        return false;
                    if (!(power.Right is Constant))
                        return false;
                    Variable variable = power.Left as Variable;
                    Constant exponent = power.Right as Constant;
                    if (!Compute.IsInteger(exponent) || !Compute.IsNonNegative(exponent))
                        return false;
                    if (existingVariables.Contains(variable))
                        return false;
                    existingVariables.Add(variable);
                }
                else if (node is Variable)
                {
                    Variable variable = node as Variable;
                    if (existingVariables.Contains(variable))
                        return false;
                    existingVariables.Add(variable);
                    containsTerm = true;
                }
                else if (node is Constant)
                {
                    if (coefficientExists)
                    {
                        return false;
                    }
                    coefficientExists = true;
                    containsTerm = true;
                }
                else if (node is Negate)
                {
                    if (coefficientExists)
                    {
                        return false;
                    }
                    IsSimplifiedTerm((node as Negate).Operand);
                }
                else
                {
                    return false;
                }
                return true;
            }

        /// <summary>
        /// Simplified Linear Function: a simplified function of a line often in y = "mx + b" format (where m is the slope and b is the intercept).
        /// </summary>
        /// <param name="node">The node to check for a simplified linear equation.</param>
        /// <returns>True if the node is a simplified linear equation.</returns>
        public static bool IsSimplifiedLinearFunction(Node node)
        {
            return 
                IsSimplifiedPolynomial(node) && // must be a simplified polynomial
                Compute.Equal(Degree(node), Compute.Constant<T>.One) && // must have a degree of one
                Variables(node).Count == 1; // must only include one variable
        }

        /// <summary>
        /// Simplified Quadratic Function: a simplified polynomial with a degree of 2 and only one variable. Example: y = "ax ^ 2 + bx + c".
        /// </summary>
        /// <param name="node">The node to check if it is a simplified quadratic function.</param>
        /// <returns>True if the node is a simplified quadratic equation.</returns>
        public static bool IsSimplifiedQuadraticFunction(Node node)
        {
            return
                IsSimplifiedPolynomial(node) && // must be a simplified polynomial
                Compute.Equal(Degree(node), Compute.FromInt32<T>(2)) && // must have a degree of one
                Variables(node).Count == 1; // must only include one variable
        }

        /// <summary>
        /// Simplified Cubic Function: a simplified polynomial with a degree of 3 and only one variable.
        /// </summary>
        /// <param name="node">The node to check if it is a simplified cubic function.</param>
        /// <returns>True if the node is a simplified cubic equation.</returns>
        public static bool IsSimplifiedCubicFunction(Node node)
        {
            return
                IsSimplifiedPolynomial(node) && // must be a simplified polynomial
                Compute.Equal(Degree(node), Compute.FromInt32<T>(3)) && // must have a degree of one
                Variables(node).Count == 1; // must only include one variable
        }

        /// <summary>
        /// Simplified Quartic Function: a simplified polynomial with a degree of 4 and only one variable.
        /// </summary>
        /// <param name="node">The node to check if it is a simplified quartic function.</param>
        /// <returns>True if the node is a simplified quartic equation.</returns>
        public static bool IsSimplifiedQuarticFunction(Node node)
        {
            return
                IsSimplifiedPolynomial(node) && // must be a simplified polynomial
                Compute.Equal(Degree(node), Compute.FromInt32<T>(4)) && // must have a degree of one
                Variables(node).Count == 1; // must only include one variable
        }
        
        /// <summary>
        /// Simplified Quintic Function: a simplified polynomial with a degree of 5 and only one variable.
        /// </summary>
        /// <param name="node">The node to check if it is a simplified quintic function.</param>
        /// <returns>True if the node is a simplified quintic equation.</returns>
        public static bool IsSimplifiedQuinticFunction(Node node)
        {
            return
                IsSimplifiedPolynomial(node) && // must be a simplified polynomial
                Compute.Equal(Degree(node), Compute.FromInt32<T>(5)) && // must have a degree of one
                Variables(node).Count == 1; // must only include one variable
        }

        /// <summary>
        /// Simplified Sextic Function: a simplified polynomial with a degree of 6 and only one variable.
        /// </summary>
        /// <param name="node">The node to check if it is a simplified sextic function.</param>
        /// <returns>True if the node is a simplified sextic equation.</returns>
        public static bool IsSimplifiedSexticFunction(Node node)
        {
            return
                IsSimplifiedPolynomial(node) && // must be a simplified polynomial
                Compute.Equal(Degree(node), Compute.FromInt32<T>(6)) && // must have a degree of one
                Variables(node).Count == 1; // must only include one variable
        }
        
        /// <summary>
        /// Simplified Power Function: a simplified single term function in the form y = "ax ^ b" where a and b are constants.
        /// </summary>
        /// <param name="node">The node to determine if it is a simplified power function or not.</param>
        /// <returns>True if the node is a simplified power function. False if not.</returns>
        public static bool IsSimplifiedPowerFunction(Node node)
        {
            return
                IsSimplifiedPolynomial(node) && // must be a polynomial (have integer exponents)
                IsSimplifiedTerm(node) && // must be a single term
                Variables(node).Count == 1; // must contain a single variable
        }

        /// <summary>
        /// Simplified Rational Function: ratio of two polynomials. Example: y = "(x ^ 2) / x" and y = "(5x ^ 2 + 2x + 7) / (x ^ 2 - 5x - 14)".
        /// </summary>
        /// <param name="node">The node to determine if it is a simplified rational function.</param>
        /// <returns>True if node is a simplified rational function. False if not.</returns>
        public static bool IsSimplifiedRationalFunction(Node node)
        {
            return
                (node is Division) &&
                IsSimplifiedPolynomial((node as Division).Left) &&
                IsSimplifiedPolynomial((node as Division).Right);
        }

        /// <summary>
        /// Simplified Exponential Function: simplified, single-term equation where the variable is an exponent of a constant. Example: y = "ab ^ x"
        /// where a and b are constants.
        /// </summary>
        /// <param name="node">The node to determine if it is a simplified exponential function.</param>
        /// <returns>True if the node is a simplified exponential function; False if not.</returns>
        public static bool IsSimplifiedExponentialFunction(Node node)
        {
            throw new System.NotImplementedException();
        }

        public static bool IsSimplifiedLogarithmicFunction(Node node)
        {
            throw new System.NotImplementedException();
        }

        public static bool IsSimplifiedSinusoidalFunction(Node node)
        {
            throw new System.NotImplementedException();
        }

        #endregion

        // general logic functions for any node type
        #region General Logic

        /// <summary>
        /// Checks for equality between two nodes. NOTE: does not simplify or check for mathematical equality.
        /// For example, "Negate(Constant(1))" and "Constant(-1)" are not equal.
        /// </summary>
        /// <param name="a">The first node of the comparison.</param>
        /// <param name="b">The second node of the comparison.</param>
        /// <returns>True if equal. False if not.</returns>
        public static bool AreEqual(Node a, Node b)
        {
            if (a is Variable && b is Variable)
            {
                return a as Variable == b as Variable;
            }
            else if (a is Constant && b is Constant)
            {
                return a as Constant == b as Constant;
            }
            if (a.GetType() == b.GetType())
            {
                if (a is Unary && b is Unary)
                {
                    return AreEqual((a as Unary).Operand, (b as Unary).Operand);
                }
                else if (a is Binary && b is Binary)
                {
                    if (!AreEqual((a as Binary).Left, (b as Binary).Right))
                    {
                        return false;
                    }
                    return AreEqual((a as Binary).Left, (b as Binary).Right);
                }
                else if (a is Ternary && b is Ternary)
                {
                    if (!AreEqual((a as Ternary).One, (b as Ternary).One))
                    {
                        return false;
                    }
                    else if (!AreEqual((a as Ternary).Two, (b as Ternary).Two))
                    {
                        return false;
                    }
                    return AreEqual((a as Ternary).Three, (b as Ternary).Three);
                }
                else if (a is Multinary && b is Multinary)
                {
                    Multinary _a = a as Multinary;
                    Multinary _b = b as Multinary;
                    if (_a.Operands.Length != _b.Operands.Length)
                    {
                        return false;
                    }
                    int operands = (a as Multinary).Operands.Length;
                    for (int i = 0; i < operands; i++)
                    {
                        if (!AreEqual((a as Multinary).Operands[i], (b as Multinary).Operands[i]))
                        {
                            return false;
                        }
                    }
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Checks for node types within a given node.
        /// </summary>
        /// <returns>True if the node contains a given node type.</returns>
        public static bool Contains<NodeType>(Node node)
            where NodeType : Node
        {
            if (node is NodeType)
            {
                return true;
            }
            else
            {
                bool containsTrigFunction = false;
                node.Stepper((Node child) =>
                {
                    if (Contains<NodeType>(child))
                    {
                        containsTrigFunction = true;
                    }
                });
                if (containsTrigFunction)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Checks for node types within a given node.
        /// </summary>
        /// <returns>True if the node contains a given node type.</returns>
        public static bool Contains<NodeType>(Node node, Predicate<NodeType> where)
            where NodeType : Node
        {
            if (node is NodeType && where(node as NodeType))
            {
                return true;
            }
            else
            {
                bool containsTrigFunction = false;
                node.Stepper((Node child) =>
                {
                    if (Contains<NodeType>(child))
                    {
                        containsTrigFunction = true;
                    }
                });
                if (containsTrigFunction)
                {
                    return true;
                }
            }
            return false;
        }

        #endregion

        // pulls out properties and components such as degree, variables, and coefficients
        #region Extraction

        /// <summary>
        /// Degree: the highest exponent on any variable of a polynomial. NOTE: must meet the criteria
        /// of "IsPolynomial".
        /// </summary>
        /// <param name="node">The polynomial node to get the degree of.</param>
        /// <returns>The degree of the polynomial.</returns>
        public static T Degree(Node node)
            {
                if (!IsSimplifiedPolynomial(node))
                    throw new System.ArithmeticException("Attempting to compute the degree of a non-polynomial node.");
                bool assigned = false;
                T degree = Degree(node, ref assigned);
                if (!assigned)
                {
                    degree = Compute.Constant<T>.Zero;
                }
                return degree;
            }
        private static T Degree(Node node, ref bool assigned)
            {
                T degree = default(T);
                if (node is Power)
                {
                    Constant exponent = (node as Power).Right as Constant;
                    if (!assigned || Compute.GreaterThan(exponent, degree))
                    {
                        degree = exponent;
                    }
                }
                else if (node is Binary)
                {
                    Binary binary = node as Binary;
                    T degree_left = Degree(binary.Left, ref assigned);
                    if (!assigned || Compute.GreaterThan(degree_left, degree))
                    {
                        degree = degree_left;
                    }
                    T degree_right = Degree(binary.Right, ref assigned);
                    if (!assigned || Compute.GreaterThan(degree_right, degree))
                    {
                        degree = degree_right;
                    }
                }
                else if (node is Negate)
                {
                    T operand_degree = Degree((node as Negate).Operand, ref assigned);
                    if (!assigned || Compute.GreaterThan(operand_degree, degree))
                    {
                        degree = operand_degree;
                    }
                }
                return degree;
            }

        /// <summary>
            /// Term: either a single number or variable, or the product of several numbers or variables.
            /// Note: will not simplify; "x ^ 2 + x ^ 2" will return "x ^ 2" and "x ^ 2".
            /// </summary>
            /// <param name="node">The expression node to get the terms of.</param>
            /// <returns>The terms in the provided expression node.</returns>
        public static List<Node> Terms(Node node)
            {
                ListLinked<Node> terms = new ListLinked<Node>();
                Terms(node, false, false, terms);
                if (terms.Count == 0)
                    return null;
                else
                    return terms;
            }
        private static void Terms(Node node, bool withinTerm, bool negate, ListLinked<Node> terms)
            {
                if (node is Multiplication)
                {
                    Multiplication binary = node as Multiplication;
                    Terms(binary.Left, true, false, terms);
                    Terms(binary.Right, true, false, terms);
                    goto AddTerm;
                }
                else if (node is Addition)
                {
                    if (withinTerm)
                        throw new System.ArithmeticException("Attempting to get the Terms of an invalid expression (distributive simplification not applied).");
                    Addition addition = node as Addition;
                    Terms(addition.Left, withinTerm, false, terms);
                    Terms(addition.Right, withinTerm, false, terms);
                }
                else if (node is Subtraction)
                {
                    if (withinTerm)
                        throw new System.ArithmeticException("Attempting to get the Terms of an invalid expression (distributive simplification not applied).");
                    Subtraction subtraction = node as Subtraction;
                    Terms(subtraction.Left, withinTerm, false, terms);
                    Terms(subtraction.Right, withinTerm, true, terms);
                }
                else if (node is Power) // Only valid if: Power(Variable, Constant)
                {
                    Power power = node as Power;
                    if (!(power.Left is Variable) && !(power.Left is Constant))
                        throw new System.ArithmeticException("Attempting to get the Terms of an invalid expression (power operations not simplified).");
                    if (!(power.Right is Constant))
                        throw new System.ArithmeticException("Attempting to get the Terms of an invalid expression (cannot have variable exponents).");
                    Constant exponent = power.Right as Constant;
                    if (!Compute.IsInteger(exponent) || !Compute.IsNonNegative(exponent))
                        throw new System.ArithmeticException("Attempting to get the Terms of an invalid expression (conatains an invalid exponent constant).");
                    goto AddTerm;
                }
                else if (node is Variable) // Only enters if there is no exponent (would have been caught by Power)
                {
                    goto AddTerm;
                }
                else if (node is Constant)
                {
                    goto AddTerm;
                }
                else if (node is Negate)
                {
                    Terms((node as Negate).Operand, withinTerm, true, terms);
                }
                else
                {
                    throw new System.ArithmeticException("Attempting to get the Terms of an invalid expression (unexpected node type).");
                }
                return;
            AddTerm:
                if (!withinTerm)
                {
                    if (negate)
                    {
                        terms.Add(new Negate(node));
                    }
                    else
                    {
                        terms.Add(node);
                    }
                }
            }

        /// <summary>
            /// Gets all the unique variables in the node.
            /// </summary>
            /// <param name="node">The node to get the variables in.</param>
            /// <returns>The variables in the node.</returns>
        public static Set<Variable> Variables(Node node)
            {
                SetHashArray<Variable> variables = new SetHashArray<Variable>();
                Variables(node, variables);
                return variables;
            }
        private static void Variables(Node node, Set<Variable> variables)
            {
                if (node is Variable)
                {
                    Variable variable = node as Variable;
                    if (!variables.Contains(variable))
                    {
                        variables.Add(variable);
                    }
                }
                else
                {
                    node.Stepper((Node child) => { Variables(child, variables); });
                }
            }

        /// <summary>
            /// Coefficient: the multiplicative constant factor in a term. Note: node must be a simplyfied term (IsSimplifiedTerm).
            /// </summary>
            /// <param name="node">The term to get the coefficient of.</param>
            /// <returns>The coefficient of the term.</returns>
        public static T Coefficient(Node node)
            {
                if (!IsSimplifiedTerm(node))
                {
                    throw new System.ArithmeticException("Attempting to get the coefficient of a node that is not a simplified term.");
                }
                T coefficient;
                if (Coefficient(node, out coefficient))
                {
                    return coefficient;
                }
                else
                {
                    return Compute.Constant<T>.One;
                }
            }
        private static bool Coefficient(Node node, out T coefficient)
            {
                if (node is Negate)
                {
                    if (Coefficient((node as Negate).Operand, out coefficient))
                    {
                        coefficient = Compute.Negate(coefficient);
                        return true;
                    }
                    else
                    {
                        coefficient = Compute.Negate(Compute.Constant<T>.One);
                        return true;
                    }
                }
                if (node is Multiplication)
                {
                    Multiplication multiplication = node as Multiplication;
                    if (Coefficient(multiplication.Left, out coefficient))
                    {
                        return true;
                    }
                    else if (Coefficient(multiplication.Right, out coefficient))
                    {
                        return true;
                    }
                }
                else if (node is Constant)
                {
                    coefficient = node as Constant;
                }
                coefficient = default(T);
                return false;
            }

        /// <summary>
        /// Gets all the nodes that are multilied
        /// </summary>
        /// <returns></returns>
        public static void MultiplicationAndDivisionChain(MultiplicationOrDivision multiplicationOrDivision)
        {
            throw new System.NotImplementedException();
            //Stepper<Link<Node, System.Type>> stepper = 
        }
        public static void MultipliedAndDividedNodes(Node node, bool dividing, List<Node> multipliedAndDividedNodes)
        {

        }
        
        #endregion

        #endregion

        // substituting, rearanging, wrapping, etc. node tree operations
        #region Modification

        internal static OperationType ShallowOperationClone<OperationType>(OperationType operation) where OperationType : Operation
        {
            try
            {
                return System.Activator.CreateInstance(operation.GetType()) as OperationType;
            }
            catch (System.Exception exception)
            {
                throw new System.ArithmeticException("There was a bug in the Towel framework when cloning a node.", exception);
            }
        }

        public static Node Clone(Node node)
        {
            try
            {
                if (node is Constant)
                {
                    return new Constant((node as Constant).Value);
                }
                else if (node is Variable)
                {
                    return new Variable((node as Variable).Name);
                }
                else if (node is Unary)
                {
                    return System.Activator.CreateInstance(
                        node.GetType(),
                        new object[]
                        { 
                            Clone((node as Unary).Operand),
                        }) as Node;
                }
                else if (node is Binary)
                {
                    return System.Activator.CreateInstance(
                        node.GetType(),
                        new object[]
                        { 
                            Clone((node as Binary).Left),
                            Clone((node as Binary).Right),
                        }) as Node;
                }
                else if (node is Ternary)
                {
                    return System.Activator.CreateInstance(
                        node.GetType(),
                        new object[]
                        { 
                            Clone((node as Ternary).One),
                            Clone((node as Ternary).Two),
                            Clone((node as Ternary).Three),
                        }) as Node;
                }
                else if (node is Multinary)
                {
                    Node[] operands = (node as Multinary).Operands;
                    object[] args = new object[operands.Length];
                    for (int i = 0; i < operands.Length; i++)
                    {
                        args[i] = Clone(operands[i]);
                    }
                    return System.Activator.CreateInstance(node.GetType(), args) as Node;
                }
                else
                {
                    throw new System.ArithmeticException("An unexpected node type was found while cloning a node.");
                }
            }
            catch (System.Exception exception)
            {
                throw new System.ArithmeticException("There was a bug in the Towel framework when cloning a node.", exception);
            }
        }

        /// <summary>
        /// Substitutes a constant in for  variable.
        /// </summary>
        /// <param name="node">The tree to substitute node in.</param>
        /// <param name="variable">The variable to substitute.</param>
        /// <param name="value">The value to substitute in for the variables.</param>
        /// <returns>The node tree after substitution.</returns>
        public static Node Substitute(Node node, string variable, T value)
        {
            try
            {
                return Replace<Variable>(node, (Variable _variable) => { return variable == _variable; }, () => { return new Constant(value); });
            }
            catch (System.Exception ex)
            {
                throw new System.ArithmeticException("A substitution failed.", ex);
            }
        }

        /// <summary>
        /// Substitutes an expression in for  variable.
        /// </summary>
        /// <param name="node">The tree to substitute node in.</param>
        /// <param name="variable">The variable to substitute.</param>
        /// <param name="value">The value to substitute in for the variables.</param>
        /// <returns>The node tree after substitution.</returns>
        public static Node Substitute(Node node, string variable, Node value)
        {
            try
            {
                throw new System.NotImplementedException(); // need a way to clone "value" below so each assignment is not the same reference
                return Replace<Variable>(node, (Variable _variable) => { return variable == _variable; }, () => { return value; });
            }
            catch (System.Exception ex)
            {
                throw new System.ArithmeticException("A substitution failed.", ex);
            }
        }

        public delegate NodeType NodeConstructor<NodeType>();

        public static Node Replace<NodeType>(Node node, Predicate<NodeType> where, NodeConstructor<Node> replacementFactory)
            where NodeType : Node
        {
            if (node is NodeType && where(node as NodeType))
            {
                return replacementFactory();
            }
            if (node is Constant)
            {
                return new Constant((node as Constant).Value);
            }
            else if (node is Variable)
            {
                return new Variable((node as Variable)._name);
            }
            if (node is Unary)
            {
                return System.Activator.CreateInstance(
                    node.GetType(),
                    new object[]
					{ 
						Replace((node as Unary).Operand, where, replacementFactory)
					}) as Node;
            }
            else if (node is Binary)
            {
                return System.Activator.CreateInstance(
                    node.GetType(),
                    new object[]
					{ 
						Replace((node as Binary).Left, where, replacementFactory),
						Replace((node as Binary).Right, where, replacementFactory),
					}) as Node;
            }
            else if (node is Ternary)
            {
                return System.Activator.CreateInstance(
                    node.GetType(),
                    new object[]
					{ 
						Replace((node as Ternary).One, where, replacementFactory),
						Replace((node as Ternary).Two, where, replacementFactory),
						Replace((node as Ternary).Three, where, replacementFactory),
					}) as Node;
            }
            else if (node is Multinary)
            {
                throw new System.NotImplementedException();
            }
            throw new System.NotImplementedException();
        }

        public static void OrderTerms(Node node)
        {
            throw new System.NotImplementedException();
        }

        #endregion

        // base mathematical simplification
		#region Simplification

		public static Node Simplify(Node node)
		{
			#region Constant
			if (node is Constant)
			{
				return new Constant((node as Constant).Value);
			}
			#endregion
			#region Variable
			else if (node is Variable)
			{
				return new Variable((node as Variable)._name);
			}
			#endregion
			#region Operation
			else if (node is Operation)
			{
				#region Unary
				if (node is Unary)
				{
					Unary unary = node as Unary;
					Node operand = Simplify(unary.Operand);

					#region Negate
					if (node is Negate)
					{
						// Rule: [-A] => [B] where A is constant and B is -A
						if (unary.Operand is Constant)
						{
							var A = unary.Operand as Constant;
							var B = Compute.Negate(A);
							return B;
						}
					}
					#endregion
                    #region Trigonometry
                    if (node is Trigonometry)
                    {

                    }
                    #endregion


					return System.Activator.CreateInstance(
						node.GetType(),
						new object[]
						{ 
							operand,
						}) as Node;
				}
				#endregion
				#region Binary
				else if (node is Binary)
				{
					Binary binary = node as Binary;
					Node left = Simplify(binary.Left);
					Node right = Simplify(binary.Right);

					#region Addition
					if (node is Addition)
					{
						#region Computation
						// Rule: [A + B] => [C] where A is constant, B is constant, and C is A + B
						if (left is Constant && right is Constant)
						{
							var A = left as Constant;
							var B = right as Constant;
							var C = Compute.Add(A, B);
							return C;
						}
						#endregion
						#region Additive Identity Property
						// Rule: [X + 0] => [X]
						if (right is Constant && Compute.Equal((right as Constant).Value, Compute.Constant<T>.Zero))
						{
							var X = left;
							return X;
						}
						// Rule: [0 + X] => [X]
						if (left is Constant && Compute.Equal((left as Constant).Value, Compute.Constant<T>.Zero))
						{
							var X = right;
							return X;
						}
						#endregion
						#region Commutative/Associative Property
						// Rule: ['X + A' + B] => [X + C] where A is constant, B is constant, and C is A + B
						if (left is Addition && (left as Addition).Right is Constant && right is Constant)
						{
							var A = (left as Addition).Right as Constant;
							var B = right as Constant;
							var C = Compute.Add(A, B);
							var X = (left as Addition).Left;
							return new Addition(X, C);
						}
						// Rule: ['A + X' + B] => [X + C] where A is constant, B is constant, and C is A + B
						if (left is Addition && (left as Addition).Left is Constant && right is Constant)
						{
							var A = (left as Addition).Left as Constant;
							var B = right as Constant;
							var C = Compute.Add(A, B);
							var X = (left as Addition).Right;
							return new Addition(X, C);
						}
						// Rule: [B + 'X + A'] => [X + C] where A is constant, B is constant, and C is A + B
						if (right is Addition && (right as Addition).Right is Constant && left is Constant)
						{
							var A = (right as Addition).Right as Constant;
							var B = left as Constant;
							var C = Compute.Add(A, B);
							var X = (right as Addition).Left;
							return new Addition(X, C);
						}
						// Rule: [B + 'A + X'] => [X + C] where A is constant, B is constant, and C is A + B
						if (right is Addition && (right as Addition).Left is Constant && left is Constant)
						{
							var A = (right as Addition).Left as Constant;
							var B = left as Constant;
							var C = Compute.Add(A, B);
							var X = (right as Addition).Right;
							return new Addition(X, C);
						}
						// Rule: ['X - A' + B] => [X + C] where A is constant, B is constant, and C is B - A
						if (left is Subtraction && (left as Subtraction).Right is Constant && right is Constant)
						{
							var A = (left as Subtraction).Right as Constant;
							var B = right as Constant;
							var C = Compute.Subtract(B, A);
							var X = (left as Subtraction).Left;
							return new Addition(X, C);
						}
						// Rule: ['A - X' + B] => [C - X] where A is constant, B is constant, and C is A + B
						if (left is Subtraction && (left as Subtraction).Left is Constant && right is Constant)
						{
							var A = (left as Subtraction).Left as Constant;
							var B = right as Constant;
							var C = Compute.Add(A, B);
							var X = (left as Subtraction).Right;
							return new Subtraction(C, X);
						}
						// Rule: [B + 'X - A'] => [X + C] where A is constant, B is constant, and C is B - A
						if (right is Subtraction && (right as Subtraction).Right is Constant && left is Constant)
						{
							var A = (right as Subtraction).Right as Constant;
							var B = left as Constant;
							var C = Compute.Subtract(B, A);
							var X = (right as Subtraction).Left;
							return new Addition(X, C);
						}
						// Rule: [B + 'A - X'] => [C - X] where A is constant, B is constant, and C is A + B
						if (right is Subtraction && (right as Subtraction).Left is Constant && left is Constant)
						{
							var A = (right as Subtraction).Left as Constant;
							var B = left as Constant;
							var C = Compute.Subtract(B, A);
							var X = (right as Subtraction).Right;
							return new Addition(X, C);
						}
						#endregion
					}
					#endregion
					#region Subtraction
					if (node is Subtraction)
					{
						#region Computation
						// Rule: [A - B] => [C] where A is constant, B is constant, and C is A - B
						if (left is Constant && right is Constant)
						{
							var A = left as Constant;
							var B = right as Constant;
							var C = Compute.Subtract(A, B);
							return C;
						}
						#endregion
						#region Identity Property
						// Rule: [X - 0] => [X]
						if (right is Constant && Compute.Equal((right as Constant).Value, Compute.Constant<T>.Zero))
						{
							var X = left;
							return X;
						}
						// Rule: [0 - X] => [-X]
						if (left is Constant && Compute.Equal((left as Constant).Value, Compute.Constant<T>.Zero))
						{
							var X = right;
							return new Negate(X);
						}
						#endregion
						#region Commutative/Associative Property
						// Rule: ['X - A' - B] => [X - C] where A is constant, B is constant, and C is A + B
						if (left is Subtraction && (left as Subtraction).Right is Constant && right is Constant)
						{
							var A = (left as Subtraction).Right as Constant;
							var B = right as Constant;
							var C = Compute.Add(A, B);
							var X = (left as Subtraction).Left;
							return new Subtraction(X, C);
						}
						// Rule: ['A - X' - B] => [C - X] where A is constant, B is constant, and C is A - B
						if (left is Subtraction && (left as Subtraction).Left is Constant && right is Constant)
						{
							var A = (left as Subtraction).Left as Constant;
							var B = right as Constant;
							var C = Compute.Subtract(A, B);
							var X = (left as Subtraction).Right;
							return new Subtraction(C, X);
						}
						// Rule: [B - 'X - A'] => [C - X] where A is constant, B is constant, and C is B - A
						if (right is Subtraction && (right as Subtraction).Right is Constant && left is Constant)
						{
							var A = (right as Subtraction).Right as Constant;
							var B = left as Constant;
							var C = Compute.Subtract(B, A);
							var X = (right as Subtraction).Left;
							return new Subtraction(C, X);
						}
						// Rule: [B - 'A - X'] => [C - X] where A is constant, B is constant, and C is B - A
						if (right is Subtraction && (right as Subtraction).Left is Constant && left is Constant)
						{
							var A = (right as Subtraction).Left as Constant;
							var B = left as Constant;
							var C = Compute.Subtract(B, A);
							var X = (right as Subtraction).Left;
							return new Subtraction(C, X);
						}
						// Rule: ['X + A' - B] => [X + C] where A is constant, B is constant, and C is A - B
						if (left is Addition && (left as Addition).Right is Constant && right is Constant)
						{
							var A = (left as Addition).Right as Constant;
							var B = right as Constant;
							var C = Compute.Subtract(A, B);
							var X = (left as Addition).Left;
							return new Addition(X, C);
						}
						// Rule: ['A + X' - B] => [C + X] where A is constant, B is constant, and C is A - B
						if (left is Addition && (left as Addition).Right is Constant && right is Constant)
						{
							var A = (left as Addition).Left as Constant;
							var B = right as Constant;
							var C = Compute.Subtract(A, B);
							var X = (left as Addition).Right;
							return new Addition(X, C);
						}
						// Rule: [B - 'X + A'] => [C - X] where A is constant, B is constant, and C is A + B
						if (right is Addition && (right as Addition).Right is Constant && left is Constant)
						{
							var A = (right as Addition).Right as Constant;
							var B = left as Constant;
							var C = Compute.Add(A, B);
							var X = (right as Addition).Left;
							return new Subtraction(C, X);
						}
						// Rule: [B - 'A + X'] => [C + X] where A is constant, B is constant, and C is B - A
						if (right is Addition && (right as Addition).Left is Constant && left is Constant)
						{
							var A = (right as Addition).Left as Constant;
							var B = left as Constant;
							var C = Compute.Subtract(B, A);
							var X = (right as Addition).Right;
							return new Addition(C, X);
						}
						#endregion
					}
					#endregion
					#region Multiplication
					if (node is Multiplication)
					{
						#region Computation
						// Rule: [A * B] => [C] where A is constant, B is constant, and C is A * B
						if (left is Constant && right is Constant)
						{
							var A = left as Constant;
							var B = right as Constant;
							var C = Compute.Multiply(A, B);
							return C;
						}
						#endregion
						#region Zero Property
						// Rule: [X * 0] => [0]
						if (right is Constant && Compute.Equal((right as Constant).Value, Compute.Constant<T>.Zero))
						{
							return Compute.Constant<T>.Zero;
						}
						// Rule: [0 * X] => [0]
						if (left is Constant && Compute.Equal((left as Constant).Value, Compute.Constant<T>.Zero))
						{
							return Compute.Constant<T>.Zero;
						}
						#endregion
						#region Identity Property
						// Rule: [X * 1] => [X]
						if (right is Constant && Compute.Equal((right as Constant).Value, Compute.Constant<T>.One))
						{
							var A = left;
							return A;
						}
						// Rule: [1 * X] => [X]
						if (left is Constant && Compute.Equal((left as Constant).Value, Compute.Constant<T>.One))
						{
							var A = right;
							return A;
						}
						#endregion
						#region Commutative/Associative Property
						// Rule: [(X * A) * B] => [X * C] where A is constant, B is constant, and C is A * B
						if (left is Multiplication && (left as Multiplication).Right is Constant && right is Constant)
						{
							var A = (left as Multiplication).Right as Constant;
							var B = right as Constant;
							var C = Compute.Multiply(A, B);
							var X = (left as Multiplication).Left;
							return new Multiplication(X, C);
						}
						// Rule: [(A * X) * B] => [X * C] where A is constant, B is constant, and C is A * B
						if (left is Multiplication && (left as Multiplication).Left is Constant && right is Constant)
						{
							var A = (left as Multiplication).Left as Constant;
							var B = right as Constant;
							var C = Compute.Multiply(A, B);
							var X = (left as Multiplication).Right;
							return new Multiplication(X, C);
						}
						// Rule: [B * (X * A)] => [X * C] where A is constant, B is constant, and C is A * B
						if (right is Multiplication && (right as Multiplication).Right is Constant && left is Constant)
						{
							var A = (right as Multiplication).Right as Constant;
							var B = left as Constant;
							var C = Compute.Multiply(A, B);
							var X = (left as Multiplication).Left;
							return new Multiplication(X, C);
						}
						// Rule: [B * (A * X)] => [X * C] where A is constant, B is constant, and C is A * B
						if (right is Multiplication && (right as Multiplication).Left is Constant && left is Constant)
						{
							var A = (right as Multiplication).Left as Constant;
							var B = left as Constant;
							var C = Compute.Multiply(A, B);
							var X = (right as Multiplication).Right;
							return new Multiplication(X, C);
						}
						// Rule: [(X / A) * B] => [X * C] where A is constant, B is constant, and C is B / A
						if (left is Division && (left as Division).Right is Constant && right is Constant)
						{
							var A = (left as Division).Right as Constant;
							var B = right as Constant;
							var C = Compute.Divide(B, A);
							var X = (right as Division).Left;
							return new Multiplication(X, C);
						}
						// Rule: [(A / X) * B] => [C / X] where A is constant, B is constant, and C is A * B
						if (left is Division && (left as Division).Left is Constant && right is Constant)
						{
							var A = (left as Division).Left as Constant;
							var B = right as Constant;
							var C = Compute.Multiply(A, B);
							var X = (left as Division).Right;
							return new Division(C, X);
						}
						// Rule: [B * (X / A)] => [X * C] where A is constant, B is constant, and C is B / A
						if (right is Division && (right as Division).Right is Constant && left is Constant)
						{
							var A = (right as Division).Right as Constant;
							var B = left as Constant;
							var C = Compute.Divide(B, A);
							var X = (right as Division).Left;
							return new Multiplication(X, C);
						}
						// Rule: [B * (A / X)] => [C / X] where A is constant, B is constant, and C is A * B
						if (right is Division && (right as Division).Left is Constant && left is Constant)
						{
							var A = (right as Division).Left as Constant;
							var B = left as Constant;
							var C = Compute.Multiply(A, B);
							var X = (right as Division).Right;
							return new Division(C, X);
						}
						#endregion
                        #region Distributive Property
                        // Rule: [X * (A +/- B)] => [X * A + X * B] where X is Variable
                        // Rule: [(A +/- B) * X] => [X * A + X * B] where X is Variable
                        if ((left is Variable && right is AdditionOrSubtraction) || (left is AdditionOrSubtraction && right is Variable))
                        {
                            Variable variable = left as Variable ?? right as Variable;
                            AdditionOrSubtraction addOrSub = left as AdditionOrSubtraction ?? right as AdditionOrSubtraction;
                            AdditionOrSubtraction operationClone = ShallowOperationClone<AdditionOrSubtraction>(addOrSub);
                            operationClone.Left = new Multiplication(Clone(variable), Clone(addOrSub.Left)).Simplify();
                            operationClone.Right = new Multiplication(Clone(variable), Clone(addOrSub.Right)).Simplify();
                            return operationClone;
                        }
                        #endregion
                        #region Duplicate Variable Multiplications
                        // Rule: [X * X * X] => [X ^ 3] where X is Variable
                        //Node[] operands = MultiplicationAndDivisionChain(node);
                        //if ()
                        //{
                        //    Map<int, Variable> variableCounts = new MapHashLinked<int, Variable>();
                        //    foreach (Node operandNode in operands)
                        //    {

                        //    }
                        //}
                        #endregion
                    }
					#endregion
					#region Division
					if (node is Division)
					{
						#region Error Handling
						// Rule: [X / 0] => Error
						if (right is Constant && Compute.Equal((right as Constant).Value, Compute.Constant<T>.Zero))
						{
							throw new System.DivideByZeroException();
						}
						#endregion
						#region Computation
						// Rule: [A / B] => [C] where A is constant, B is constant, and C is A / B
						if (left is Constant && right is Constant)
						{
							var A = left as Constant;
							var B = right as Constant;
							var C = Compute.Divide(A, B);
							return C;
						}
						#endregion
						#region Zero Property
						// Rule: [0 / X] => [0]
						if (left is Constant && Compute.Equal((left as Constant).Value, Compute.Constant<T>.Zero))
						{
							return left;
						}
						#endregion
						#region Identity Property
						// Rule: [X / 1] => [X]
						if (right is Constant && Compute.Equal((right as Constant).Value, Compute.Constant<T>.One))
						{
							return left;
						}
						#endregion
						#region Commutative/Associative Property
						// Rule: [(X / A) / B] => [X / C] where A is constant, B is constant, and C is A * B
						if (left is Division && (left as Division).Right is Constant && right is Constant)
						{
							var A = (left as Division).Right as Constant;
							var B = right as Constant;
							var C = Compute.Multiply(A, B);
							var X = (left as Division).Left;
							return new Division(X, C);
						}
						// Rule: [(A / X) / B] => [C / X] where A is constant, B is constant, and C is A / B
						if (left is Division && (left as Division).Left is Constant && right is Constant)
						{
							var A = (left as Division).Left as Constant;
							var B = right as Constant;
							var C = Compute.Divide(A, B);
							var X = (left as Division).Right;
							return new Division(C, X);
						}
						// Rule: [B / (X / A)] => [C / X] where A is constant, B is constant, and C is B / A
						if (right is Division && (right as Division).Right is Constant && left is Constant)
						{
							var A = (right as Division).Right as Constant;
							var B = left as Constant;
							var C = Compute.Divide(B, A);
							var X = (left as Division).Left;
							return new Division(X, C);
						}
						// Rule: [B / (A / X)] => [C / X] where A is constant, B is constant, and C is B / A
						if (right is Division && (right as Division).Left is Constant && left is Constant)
						{
							var A = (right as Division).Left as Constant;
							var B = left as Constant;
							var C = Compute.Divide(B, A);
							var X = (right as Division).Right;
							return new Division(C, X);
						}
						// Rule: [(X * A) / B] => [X * C] where A is constant, B is constant, and C is A / B
						if (left is Multiplication && (left as Multiplication).Right is Constant && right is Constant)
						{
							var A = (left as Multiplication).Right as Constant;
							var B = right as Constant;
							var C = Compute.Divide(A, B);
							var X = (right as Multiplication).Left;
							return new Multiplication(X, C);
						}
						// Rule: [(A * X) / B] => [X * C] where A is constant, B is constant, and C is A / B
						if (left is Multiplication && (left as Multiplication).Left is Constant && right is Constant)
						{
							var A = (left as Multiplication).Left as Constant;
							var B = right as Constant;
							var C = Compute.Divide(A, B);
							var X = (left as Multiplication).Right;
							return new Multiplication(X, C);
						}
						// Rule: [B / (X * A)] => [C / X] where A is constant, B is constant, and C is A * B
						if (right is Multiplication && (right as Multiplication).Right is Constant && left is Constant)
						{
							var A = (right as Multiplication).Right as Constant;
							var B = left as Constant;
							var C = Compute.Multiply(A, B);
							var X = (right as Multiplication).Left;
							return new Division(C, X);
						}
						// Rule: [B / (A * X)] => [X * C] where A is constant, B is constant, and C is B / A
						if (right is Multiplication && (right as Multiplication).Left is Constant && left is Constant)
						{
							var A = (right as Multiplication).Left as Constant;
							var B = left as Constant;
							var C = Compute.Divide(B, A);
							var X = (right as Multiplication).Right;
							return new Multiplication(X, C);
						}
						#endregion
					}
					#endregion
					#region Power
					if (node is Power)
					{
						#region Computation
						// Rule: [A ^ B] => [C] where A is constant, B is constant, and C is A ^ B
						if (left is Constant && right is Constant)
						{
							var A = left as Constant;
							var B = right as Constant;
							var C = Compute.Power(A, B);
							return C;
						}
						#endregion
						#region Zero Base
						// Rule: [0 ^ X] => [0]
						if (left is Constant && Compute.Equal((left as Constant).Value, Compute.Constant<T>.Zero))
						{
							return Compute.Constant<T>.Zero;
						}
						#endregion
						#region One Power
						// Rule: [X ^ 1] => [X]
						if (right is Constant && Compute.Equal((right as Constant).Value, Compute.Constant<T>.One))
						{
							var A = left;
							return A;
						}
						#endregion
						#region Zero Power
						// Rule: [X ^ 0] => [1]
						if (right is Constant && Compute.Equal(right as Constant, Compute.Constant<T>.Zero))
						{
							return new Constant(Compute.Constant<T>.One);
						}
						#endregion
					}
					#endregion

					return System.Activator.CreateInstance(
						node.GetType(),
						new object[]
						{ 
							left,
							right,
						}) as Node;
				}
				#endregion
				#region Ternary
				else if (node is Ternary)
				{
					Ternary ternary = node as Ternary;
					Node one = Simplify(ternary.One);
					Node two = Simplify(ternary.Two);
					Node three = Simplify(ternary.Three);

					return System.Activator.CreateInstance(
						node.GetType(),
						new object[]
						{ 
							one,
							two,
							three
						}) as Node;
				}
				#endregion
				#region Multinary
				else if (node is Multinary)
				{
					Multinary multinary = node as Multinary;
					Node[] operands = new Node[multinary.Operands.Length];
					for (int i = 0; i < operands.Length; i++)
						operands[i] = Simplify(multinary.Operands[i]);

				}
				#endregion
			}
			#endregion
			throw new System.NotImplementedException();
		}

		#endregion

        // derivation implementations
		#region Derivation

		public static Node Derive(Node node, string variable)
		{
			throw new System.NotImplementedException();
		}

		#endregion

        // integration implementations
		#region Integration

		public static Node Integrate(Node node, string variable)
		{
            if (!Contains<Variable>(node, x => x.Name.Equals(variable)))
            {
                // the expression doesn't contain the variable; as long as it is a 
                // valid algebraic expression, it is a very easy integral

                if (!IsValidAlgebraicExpression(node))
                {
                    throw new System.ArithmeticException("attempting to integrate an invalid algebraic epression");
                }

                // The node is a valid algebraic expression
                // Examples:
                // ∫ 2 dx = 2x + c
                // ∫ 2y dx = 2yx + c

                return new Multiplication(Clone(node), new Variable(variable));
            }

            if (IsSimplifiedPolynomial(node))
            {
                // The node is a polynomial
                // Examples:
                // y = 3x^3
                // y = 3x^3 + 2x^2 + 1
                // z = 3yx^3 + 2yx^2 + 1y

                if (IsSimplifiedTerm(node))
                {
                    // The node is a single term
                    // Examples:
                    // y = 3x^3
                    // z = 3yx^3


                }
            }
            throw new System.NotImplementedException();
		}

		#endregion
    }
}