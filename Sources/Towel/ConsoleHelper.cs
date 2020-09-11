using System;

namespace Towel
{
	/// <summary>Contains static helper methods for <see cref="Console"/>.</summary>
	public static class ConsoleHelper
	{
		/// <summary>Flushes the console input buffer.</summary>
		public static void FlushInputBuffer(bool intercept = true)
		{
			while (Console.KeyAvailable)
			{
				Console.ReadKey(intercept);
			}
		}

		/// <summary>Prompts the user to press [enter] in the console before continuing.</summary>
		/// <param name="key">The key to wait for the user to press before continuing.</param>
		public static void PressToContinue(ConsoleKey key = ConsoleKey.Enter)
		{
			if (!key.IsDefined())
			{
				throw new ArgumentOutOfRangeException(nameof(key), key, $"{nameof(key)} is not a defined value in the {nameof(ConsoleKey)} enum");
			}
			while (Console.ReadKey(true).Key != key)
			{
				continue;
			}
		}

		/// <summary>Prompts the user to press [enter] in the console before continuing.</summary>
		/// <param name="key">The key to wait for the user to press before continuing.</param>
		/// <param name="prompt">The prompt to display to the user. Default: "Press [enter] to continue...".</param>
		public static void PromptPressToContinue(string prompt = null, ConsoleKey key = ConsoleKey.Enter)
		{
			if (!key.IsDefined())
			{
				throw new ArgumentOutOfRangeException(nameof(key), key, $"{nameof(key)} is not a defined value in the {nameof(ConsoleKey)} enum");
			}
			prompt ??= $"Press [{key}] to continue...";
			Console.Write(prompt);
			PressToContinue(key);
		}

		/// <summary>Prompts the user to select a menu option in the console before continuing.</summary>
		/// <param name="options">The options of the menu.</param>
		public static void IntMenu(params (string DisplayName, Action Action)[] options) =>
			IntMenu(null, null, null, options);

		/// <summary>Prompts the user to select a menu option in the console before continuing.</summary>
		/// <param name="title">The title of the menu.</param>
		/// <param name="prompt">The prompt message to display when requesting console input from the user.</param>
		/// <param name="invalidMessage">The message to display if invalid input is detected.</param>
		/// <param name="options">The options of the menu.</param>
		public static void IntMenu(
			string title = null,
			string prompt = null,
			string invalidMessage = null,
			params (string DisplayName, Action Action)[] options)
		{
			_ = options ?? throw new ArgumentNullException(nameof(options));
			if (options.Length <= 0)
			{
				throw new ArgumentException($"{nameof(options)} is empty", nameof(options));
			}
			prompt ??= $"Choose an option (1-{options.Length}): ";
			invalidMessage ??= "Invalid Input. Try Again...";
			if (!(title is null))
			{
				Console.WriteLine(title);
			}
			for (int i = 0; i < options.Length; i++)
			{
				Console.WriteLine($"{i + 1}. {options[i].DisplayName ?? "null"}");
			}
			int inputValue;
			Console.Write(prompt);
			while (!int.TryParse(Console.ReadLine(), out inputValue) || inputValue < 1 || options.Length < inputValue)
			{
				Console.WriteLine("Invalid Input. Try Again...");
				Console.Write(prompt);
			}
			options[inputValue - 1].Action?.Invoke();
		}

		/// <summary>Gets console input from the user.</summary>
		/// <typeparam name="T">The generic type of console input to get from the user.</typeparam>
		/// <param name="prompt">The prompt message to display when requesting console input from the user.</param>
		/// <param name="invalidMessage">The message to display if invalid input is detected.</param>
		/// <param name="tryParse">The TryParse method for converting the console input into the generic type.</param>
		/// <param name="validation">The predicate for validating the value of the input.</param>
		/// <returns>The validated value of the console input provided by the user.</returns>
		public static T GetInput<T>(
			string prompt = null,
			string invalidMessage = null,
			TryParse<T> tryParse = null,
			Predicate<T> validation = null)
		{
			if (tryParse is null && (typeof(T) != typeof(string) && !typeof(T).IsEnum && Meta.GetTryParseMethod<T>() is null))
			{
				throw new InvalidOperationException($"Using {nameof(ConsoleHelper)}.{nameof(GetInput)} without providing a {nameof(tryParse)} delegate for a non-supported type {typeof(T).Name}.");
			}
			tryParse ??= typeof(T) == typeof(string)
				? (string s, out T v) => { v = (T)(object)s; return true; }
			: (TryParse<T>)Syntax.TryParse;
			validation ??= v => true;
			GetInput:
			Console.Write(prompt ?? $"Input a {typeof(T).Name} value: ");
			if (!tryParse(Console.ReadLine(), out T value) || !validation(value))
			{
				Console.WriteLine(invalidMessage ?? $"Invalid input. Try again...");
				goto GetInput;
			}
			return value;
		}

		/// <summary>Similar to <see cref="Console.ReadLine"/> but with hidden input characters.</summary>
		/// <param name="shownCharacter">The display character to use for all input.</param>
		/// <returns>The <see cref="string"/> input provided by the user.</returns>
		public static string HiddenReadLine(char shownCharacter = '*')
		{
			System.Collections.Generic.List<char> list = new System.Collections.Generic.List<char>();
			HiddenReadLineBase(
				shownCharacter: shownCharacter,
				GetLength: () => list.Count,
				Append: list.Add,
				InsertAt: list.Insert,
				RemoveAt: list.RemoveAt,
				RemoveRange: list.RemoveRange,
				Clear: list.Clear);
			return string.Concat(list);
		}

		internal static void HiddenReadLineBase(
			char shownCharacter,
			Func<int> GetLength,
			Action<char> Append,
			Action<int, char> InsertAt,
			Action<int> RemoveAt,
			Action<int, int> RemoveRange = null,
			Action Clear = null)
		{
			int position = 0;

			RemoveRange ??= (index, length) =>
			{
				for (int i = 0; i < length; i++)
				{
					RemoveAt(index);
				}
			};

			Clear ??= () => RemoveRange(0, GetLength());

			void MoveToOrigin() => MoveNegative(position);

			void MoveToTail() => MovePositive(GetLength() - position);

			while (true)
			{
				ConsoleKeyInfo keyInfo = Console.ReadKey(true);
				if (keyInfo.Key is ConsoleKey.Enter)
				{
					if (!keyInfo.Modifiers.HasFlag(ConsoleModifiers.Control) &&
						!keyInfo.Modifiers.HasFlag(ConsoleModifiers.Shift) &&
						!keyInfo.Modifiers.HasFlag(ConsoleModifiers.Alt))
					{
						MovePositive(GetLength() - position);
						Console.WriteLine();
						break;
					}
				}
				else if (keyInfo.Key is ConsoleKey.Backspace)
				{
					if (keyInfo.Modifiers.HasFlag(ConsoleModifiers.Control))
					{
						MoveToOrigin();
						ConsoleWriteString(new string(shownCharacter, GetLength() - position) + new string(' ', position));
						MoveNegative(GetLength());
						RemoveRange(0, position);
						position = 0;
					}
					else if (position > 0)
					{
						if (position == GetLength())
						{
							MoveNegative(1);
							ConsoleWriteChar(' ');
							MoveNegative(1);
						}
						else
						{
							MoveToTail();
							MoveNegative(1);
							ConsoleWriteChar(' ');
							MoveNegative(GetLength() - position + 1);
						}
						RemoveAt(position - 1);
						position--;
					}
				}
				else if (keyInfo.Key is ConsoleKey.Delete)
				{
					if (!keyInfo.Modifiers.HasFlag(ConsoleModifiers.Control) &&
						!keyInfo.Modifiers.HasFlag(ConsoleModifiers.Shift) &&
						!keyInfo.Modifiers.HasFlag(ConsoleModifiers.Alt))
					{
						if (position < GetLength())
						{
							int left = Console.CursorLeft;
							int top = Console.CursorTop;
							MoveToTail();
							MoveNegative(1);
							ConsoleWriteChar(' ');
							Console.CursorLeft = left;
							Console.CursorTop = top;
							RemoveAt(position);
							continue;
						}
					}
				}
				else if (keyInfo.Key is ConsoleKey.Escape)
				{
					if (!keyInfo.Modifiers.HasFlag(ConsoleModifiers.Control) &&
						!keyInfo.Modifiers.HasFlag(ConsoleModifiers.Shift) &&
						!keyInfo.Modifiers.HasFlag(ConsoleModifiers.Alt))
					{
						MoveToOrigin();
						int left = Console.CursorLeft;
						int top = Console.CursorTop;
						ConsoleWriteString(new string(' ', GetLength()));
						Console.CursorLeft = left;
						Console.CursorTop = top;
						Clear();
						position = 0;
					}
				}
				else if (keyInfo.Key is ConsoleKey.Home)
				{
					if (!keyInfo.Modifiers.HasFlag(ConsoleModifiers.Shift) &&
						!keyInfo.Modifiers.HasFlag(ConsoleModifiers.Alt))
					{
						if (keyInfo.Modifiers.HasFlag(ConsoleModifiers.Control))
						{
							MoveToOrigin();
							ConsoleWriteString(new string(shownCharacter, GetLength() - position) + new string(' ', position));
							MoveNegative(GetLength());
							RemoveRange(0, position);
							position = 0;
						}
						else
						{
							MoveToOrigin();
							position = 0;
						}
					}
				}
				else if (keyInfo.Key is ConsoleKey.End)
				{
					if (!keyInfo.Modifiers.HasFlag(ConsoleModifiers.Shift) &&
						!keyInfo.Modifiers.HasFlag(ConsoleModifiers.Alt))
					{
						if (keyInfo.Modifiers.HasFlag(ConsoleModifiers.Control))
						{
							MoveToOrigin();
							ConsoleWriteString(new string(shownCharacter, position) + new string(' ', GetLength() - position));
							MoveNegative(GetLength() - position);
							RemoveRange(position, GetLength() - position);
						}
						else
						{
							MoveToTail();
							position = GetLength();
						}
					}
				}
				else if (keyInfo.Key is ConsoleKey.LeftArrow)
				{
					if (!keyInfo.Modifiers.HasFlag(ConsoleModifiers.Shift) &&
						!keyInfo.Modifiers.HasFlag(ConsoleModifiers.Alt))
					{
						if (keyInfo.Modifiers.HasFlag(ConsoleModifiers.Control))
						{
							MoveToOrigin();
							position = 0;
						}
						else
						{
							if (position > 0)
							{
								MoveNegative(1);
								position--;
							}
						}
					}
				}
				else if (keyInfo.Key is ConsoleKey.RightArrow)
				{
					if (!keyInfo.Modifiers.HasFlag(ConsoleModifiers.Shift) &&
						!keyInfo.Modifiers.HasFlag(ConsoleModifiers.Alt))
					{
						if (keyInfo.Modifiers.HasFlag(ConsoleModifiers.Control))
						{
							MoveToTail();
							position = GetLength();
						}
						else
						{
							if (position < GetLength())
							{
								MovePositive(1);
								position++;
							}
						}
					}
				}
				else
				{
					if (!(keyInfo.KeyChar is '\0'))
					{
						if (position == GetLength())
						{
							ConsoleWriteChar(shownCharacter);
							Append(keyInfo.KeyChar);
							position++;
						}
						else
						{
							int left = Console.CursorLeft;
							int top = Console.CursorTop;
							MoveToTail();
							ConsoleWriteChar(shownCharacter);
							Console.CursorLeft = left;
							Console.CursorTop = top;
							MovePositive(1);
							InsertAt(position, keyInfo.KeyChar);
							position++;
						}
					}
				}
			}
		}

		/// <summary>Animates an elipsis in the console to indicate processing.</summary>
		/// <param name="condition">The condition of the loop.</param>
		/// <param name="delay">The delay function.</param>
		/// <param name="length">The length of the ellipsis.</param>
		public static void AnimatedEllipsis(
			Func<bool> condition,
			Action delay,
			int length = 3)
		{
			_ = condition ?? throw new ArgumentNullException(nameof(condition));
			_ = delay ?? throw new ArgumentNullException(nameof(delay));
			if (length < 1)
			{
				throw new ArgumentOutOfRangeException(nameof(length), length, $"{nameof(length)} < 1");
			}

			void MoveToOrigin() => MoveNegative(length);

			void Render(int frame)
			{
				for (int i = 0; i < frame; i++)
				{
					ConsoleWriteChar('.');
				}
				for (int i = frame; i < length; i++)
				{
					ConsoleWriteChar(' ');
				}
			}

			int frame = 0;
			Render(frame++);
			while (condition())
			{
				MoveToOrigin();
				Render(frame++);
				delay();
				if (frame > length)
				{
					frame = 0;
				}
			}
			MoveToOrigin();
			ConsoleWriteString(new string(' ', length));
			MoveToOrigin();
		}

		/// <summary>Displays a progress bar in the console.</summary>
		/// <param name="action">The action to track the progress of.</param>
		/// <param name="length">The character length of the progress bar (must be >= 6).</param>
		/// <param name="header">The header character of the progress bar.</param>
		/// <param name="footer">The footer character of the progress bar.</param>
		/// <param name="done">The character for represening completed progress.</param>
		/// <param name="remaining">The character representing ongoing processing.</param>
		/// <param name="errorDigit">The characters to display in the numerical display when an invalid percentage is recieved.</param>
		/// <param name="postClear">Whether or not to clear the progress bar from the view when complete.</param>
		public static void ProgressBar(
			Action<Action<double>> action,
			int length = 17,
			char header = '[',
			char footer = ']',
			char done = '=',
			char remaining = '-',
			char errorDigit = '?',
			bool postClear = true)
		{
			_ = action ?? throw new ArgumentNullException(nameof(action));
			if (length < 6)
			{
				throw new ArgumentOutOfRangeException(nameof(length), length, $"{length} < 6");
			}

			void MoveToOrigin() => MoveNegative(length);

			void Render(double percentage)
			{
				ConsoleWriteChar(header);
				if (percentage < 0 || percentage > 100)
				{
					ConsoleWriteString(new string(remaining, length - 7));
				}
				else
				{
					int doneCount = (int)(percentage / 100 * (length - 7));
					int remainingCount = length - 7 - doneCount;
					ConsoleWriteString(new string(done, doneCount));
					ConsoleWriteString(new string(remaining, remainingCount));
				}
				ConsoleWriteChar(footer);
				ConsoleWriteChar(' ');
				if (percentage < 0 || percentage > 100)
				{
					ConsoleWriteString(new string(errorDigit, 2));
					ConsoleWriteChar('%');
					ConsoleWriteChar(' ');
				}
				else
				{
					string percentString = ((int)(percentage)).ToString(System.Globalization.CultureInfo.InvariantCulture);
					ConsoleWriteString(percentString);
					ConsoleWriteChar('%');
					for (int i = percentString.Length; i < 3; i++)
					{
						ConsoleWriteChar(' ');
					}
				}
			}

			Render(0);
			action(percent => { MoveToOrigin(); Render(percent); });
			if (postClear)
			{
				MoveToOrigin();
				ConsoleWriteString(new string(' ', length));
				MoveToOrigin();
			}
			else
			{
				MoveToOrigin();
				Render(100);
			}
		}

		internal static void MoveNegative(int count)
		{
			int bufferWidth = Console.BufferWidth;
			int left = Console.CursorLeft;
			int top = Console.CursorTop;
			for (int i = 0; i < count; i++)
			{
				if (left > 0)
				{
					left--;
				}
				else
				{
					top--;
					left = bufferWidth - 1;
				}
			}
			Console.CursorLeft = left;
			Console.CursorTop = top;
		}

		internal static void MovePositive(int count)
		{
			int bufferWidth = Console.BufferWidth;
			int left = Console.CursorLeft;
			int top = Console.CursorTop;
			for (int i = 0; i < count; i++)
			{
				if (left == bufferWidth - 1)
				{
					top++;
					left = 0;
				}
				else
				{
					left++;
				}
			}
			Console.CursorLeft = left;
			Console.CursorTop = top;
		}

		internal static void ConsoleWriteChar(char @char)
		{
			int temp = Console.CursorLeft;
			Console.Write(@char);
			if (Console.CursorLeft == temp)
			{
				MovePositive(1);
			}
		}

		internal static void ConsoleWriteString(string @string)
		{
			foreach (char c in @string)
			{
				ConsoleWriteChar(c);
			}
		}
	}
}
