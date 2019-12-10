using System;
using System.Reflection;
using System.Text;
using static Towel.Syntax;

namespace Towel
{
	public static class CommandLine
	{
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
							stringBuilder.Append(genericArgument.DefaultValueString);
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

		public enum ArgumentStatus
		{
			Null = 0,
			Default,
			SyntaxError,
			NotProvided,
			DuplicateProvided,
			ParseFailed,
			ValueProvided,
		}

		internal interface IGenericArgument
		{
			bool HasDefaultValue { get; }
			string DefaultValueString { get; }
			Type Type { get; }
		}

		public struct Argument
		{
			internal class Data
			{
				internal ArgumentStatus _status;
			}

			internal Data _data;

			public bool Exists
			{
				get
				{
					Process();
					return _data._status is ArgumentStatus.ValueProvided;
				}
			}

			public ArgumentStatus Status
			{
				get
				{
					Process();
					return _data._status;
				}
			}

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
										return;
									}
									else
									{
										index = i;
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

		public struct Argument<T> : IGenericArgument
		{
			internal class Data
			{
				internal ArgumentStatus _status;
				internal T _value;
				internal T _defaultValue;
				internal bool _hasDefault;
			}

			internal Data _data;

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

			public bool HasDefaultValue
			{
				get
				{
					Process();
					return _data._hasDefault;
				}
			}

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

			public string DefaultValueString
			{
				get
				{
					Process();
					return _data._hasDefault
						? _data._defaultValue.ToString()
						: throw new InvalidOperationException("Attempted to get the default value string of a command line argument with no default value.");
				}
			}

			public bool HasValue
			{
				get
				{
					Process();
					return _data._status is ArgumentStatus.ValueProvided || _data._status is ArgumentStatus.Default;
				}
			}

			public ArgumentStatus Status
			{
				get
				{
					Process();
					return _data._status;
				}
			}

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
										return;
									}
									else
									{
										index = i;
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

			public static implicit operator T(Argument<T> argument) => argument.Value;

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
