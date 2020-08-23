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
			if ((title is null))
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
	}
}
