using System;
using System.Reflection;
using System.Text;
using static Towel.Syntax;

namespace Towel
{
	/// <summary>Contains static helpers for handling command line input and output.</summary>
	public static class CommandLine
	{
		/// <summary>The default information string for the entry assembly of the currently running application.</summary>
		public static string DefaultInfoString
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				Assembly entryAssembly = Assembly.GetEntryAssembly();
				AssemblyName assemblyName = entryAssembly.GetName();
				stringBuilder.Append("  Name: ");
				stringBuilder.AppendLine(assemblyName.Name);
				stringBuilder.Append("  Version: ");
				stringBuilder.AppendLine(assemblyName.Version.ToString());
				stringBuilder.Append("  Command Line Arguments:");
				MethodInfo entryMethod = entryAssembly.EntryPoint;
				Type entryType = entryMethod.DeclaringType;
				FieldInfo[] fieldInfos = entryType.GetFields(
					BindingFlags.Static |
					BindingFlags.Public |
					BindingFlags.NonPublic);
				bool hasCommandLineArguments = false;
				for (int i = 0; i < fieldInfos.Length; i++)
				{
					FieldInfo field = fieldInfos[i];
					object fieldValue = field.GetValue(null);
					if (fieldValue is IGenericArgument genericArgument)
					{
						hasCommandLineArguments = true;
						stringBuilder.AppendLine();
						stringBuilder.Append("    ");
						stringBuilder.Append(field.Name);
						stringBuilder.AppendLine(":");
						stringBuilder.Append("      Type: ");
						stringBuilder.Append(genericArgument.Type.Name);
						if (genericArgument.HasDefaultValue)
						{
							stringBuilder.AppendLine();
							stringBuilder.Append("      Default: ");
							stringBuilder.Append(genericArgument.DefaultValueObject.ToString());
						}
					}
					else if (fieldValue is Argument)
					{
						hasCommandLineArguments = true;
						stringBuilder.AppendLine();
						stringBuilder.Append("    ");
						stringBuilder.Append(field.Name);
					}
				}
				if (!hasCommandLineArguments)
				{
					stringBuilder.Append(" N/A");
				}
				string result = stringBuilder.ToString();
				return result;
			}
		}

		/// <summary>An enum representing the statuses of command line arguemnts.</summary>
		public enum ArgumentStatus
		{
			/// <summary>The default value of null representing a command line argument that has not been processed.</summary>
			Null = 0,
			/// <summary>The default value of null representing a command line argument that has not been processed.</summary>
			Default,
			/// <summary>There is a syntax error in the source code for this command line argument instance.</summary>
			SyntaxError,
			/// <summary>The command line argument was not provided and no default value exists.</summary>
			NotProvided,
			/// <summary>The command line argument was provided multiple times. The argument is in error.</summary>
			DuplicateProvided,
			/// <summary>The command line argument was provided but it failed to parse to the expected type. The argument is in error.</summary>
			ParseFailed,
			/// <summary>The command line argument was provided and it successfully parsed.</summary>
			ValueProvided,
		}

		internal interface IGenericArgument
		{
			bool HasDefaultValue { get; }
			object DefaultValueObject { get; }
			Type Type { get; }
		}

		/// <summary>A helper type for processing command line arguments.</summary>
		public struct Argument
		{
			internal class Data
			{
				internal ArgumentStatus _status;
				internal int? _index;
			}

			internal Data _data;

			/// <summary>True if a value for this command line argument exists.</summary>
			public bool Exists
			{
				get
				{
					Process();
					return _data._status is ArgumentStatus.ValueProvided;
				}
			}

			/// <summary>Gets the status of the command line argument.</summary>
			public ArgumentStatus Status
			{
				get
				{
					Process();
					return _data._status;
				}
			}

			/// <summary>The index of the parameter if it was found in the command line arguments.</summary>
			public int? Index => _data._index;

			internal void Process()
			{
				_data ??= new Data();
				if (!(_data._status is ArgumentStatus.Null))
				{
					return;
				}
				Assembly entryAssembly = Assembly.GetEntryAssembly();
				MethodInfo entryMethod = entryAssembly.EntryPoint;
				Type entryType = entryMethod.DeclaringType;
				FieldInfo[] fieldInfos = entryType.GetFields(
					BindingFlags.Static |
					BindingFlags.Public |
					BindingFlags.NonPublic);
				foreach (FieldInfo field in fieldInfos)
				{
					if (field.GetValue(null) is Argument argumentT)
					{
						if (argumentT._data is null || !(argumentT._data._status is ArgumentStatus.Null))
						{
							continue;
						}
						if (argumentT._data == _data)
						{
							string name = field.Name;
							int index = -1;
							string[] args = Environment.GetCommandLineArgs();
							for (int i = 0; i < args.Length; i++)
							{
								if (args[i] == name)
								{
									if (index > -1)
									{
										_data._status = ArgumentStatus.DuplicateProvided;
										_data._index = null;
										return;
									}
									else
									{
										index = i;
										_data._index = i;
									}
								}
							}
							if (index == -1)
							{
								_data._status = ArgumentStatus.NotProvided;
								return;
							}
							else
							{
								_data._status = ArgumentStatus.ValueProvided;
								return;
							}
						}
					}
				}
				_data._status = ArgumentStatus.SyntaxError;
				return;
			}
		}

		/// <summary>A helper type for processing command line arguments.</summary>
		public struct Argument<T> : IGenericArgument
		{
			internal class Data
			{
				internal ArgumentStatus _status;
				internal T _value;
				internal T _defaultValue;
				internal bool _hasDefault;
				internal int? _index;
			}

			internal Data _data;

			/// <summary>The value of the command line argument.</summary>
			public T Value
			{
				get
				{
					Process();
					return _data._status is ArgumentStatus.ValueProvided || _data._status is ArgumentStatus.Default
						? _data._value
						: throw new InvalidOperationException("Attempted to get a command line argument with a status of " + _data._status + ".");
				}
			}

			/// <summary>True if the command line argument is defined with a default value.</summary>
			public bool HasDefaultValue
			{
				get
				{
					Process();
					return _data._hasDefault;
				}
			}

			/// <summary>The default value of the command line argument.</summary>
			public T DefaultValue
			{
				get
				{
					Process();
					return _data._hasDefault
						? _data._defaultValue
						: throw new InvalidOperationException("Attempted to get the default value of a command line argument with no default value.");
				}
			}

			/// <summary>The default value of the command line argument.</summary>
			public object DefaultValueObject
			{
				get
				{
					Process();
					return _data._hasDefault
						? _data._defaultValue
						: throw new InvalidOperationException("Attempted to get the default value string of a command line argument with no default value.");
				}
			}

			/// <summary>True if the command line argument has a value.</summary>
			public bool HasValue
			{
				get
				{
					Process();
					return _data._status is ArgumentStatus.ValueProvided || _data._status is ArgumentStatus.Default;
				}
			}

			/// <summary>The status of the command line argument.</summary>
			public ArgumentStatus Status
			{
				get
				{
					Process();
					return _data._status;
				}
			}

			/// <summary>The index of the parameter if it was found in the command line arguments.</summary>
			public int? Index => _data._index;

			/// <summary>The type of the command line argument.</summary>
			public Type Type => typeof(T);

			internal void Process()
			{
				_data ??= new Data();
				if (!(_data._status is ArgumentStatus.Null))
				{
					return;
				}
				Assembly entryAssembly = Assembly.GetEntryAssembly();
				MethodInfo entryMethod = entryAssembly.EntryPoint;
				Type entryType = entryMethod.DeclaringType;
				FieldInfo[] fieldInfos = entryType.GetFields(
					BindingFlags.Static |
					BindingFlags.Public |
					BindingFlags.NonPublic);
				foreach (FieldInfo field in fieldInfos)
				{
					if (field.GetValue(null) is Argument<T> argumentT)
					{
						if (argumentT._data is null || !(argumentT._data._status is ArgumentStatus.Null))
						{
							continue;
						}
						if (argumentT._data == _data)
						{
							string name = field.Name + ":";
							int index = -1;
							string[] args = Environment.GetCommandLineArgs();
							for (int i = 0; i < args.Length; i++)
							{
								if (args[i] == name)
								{
									if (index > -1)
									{
										_data._status = ArgumentStatus.DuplicateProvided;
										_data._index = null;
										return;
									}
									else
									{
										index = i;
										_data._index = i;
									}
								}
							}
							if (index == -1)
							{
								if (_data._hasDefault)
								{
									_data._status = ArgumentStatus.Default;
									return;
								}
								else
								{
									_data._status = ArgumentStatus.NotProvided;
									return;
								}
							}
							else if (index == args.Length - 1)
							{
								_data._status = ArgumentStatus.NotProvided;
								return;
							}
							else if (typeof(T) == typeof(string))
							{
								Argument<string>.Data data_string = _data as Argument<string>.Data;
								data_string._value = args[index + 1];
								_data._status = ArgumentStatus.ValueProvided;
								return;
							}
							else if (TryParse(args[index + 1], out _data._value))
							{
								_data._status = ArgumentStatus.ValueProvided;
								return;
							}
							else
							{
								_data._status = ArgumentStatus.ParseFailed;
								return;
							}
						}
					}
				}
				_data._status = ArgumentStatus.SyntaxError;
				return;
			}

			/// <summary>Converts the command line argument into its current value.</summary>
			/// <param name="argument">The command line argument to get the value of.</param>
			public static implicit operator T(Argument<T> argument) => argument.Value;

			/// <summary>Creates a command line argument from a default value.</summary>
			/// <param name="value">The default value of the command line argument.</param>
			public static implicit operator Argument<T>(T value) =>
				new Argument<T>()
				{
					_data = new Data()
					{
						_defaultValue = value,
						_hasDefault = true,
						_value = value,
					}
				};
		}
	}
}
