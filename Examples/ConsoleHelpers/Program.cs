using System;
using System.Threading;
using Towel;

namespace ConsoleHelpers
{
	class Program
	{
		static void Main()
		{
			Console.WriteLine("You are runnning the ConsoleHelpers example.");
			Console.WriteLine("============================================");
			Console.WriteLine();

			#region Prompt Press To Continue Example
			{
				Console.WriteLine("---------------------");
				Console.WriteLine("PromptPressToContinue is an reasy method");
				Console.WriteLine("to wait for user input to continue.");
				ConsoleHelper.PromptPressToContinue();
				Console.WriteLine();
				Console.WriteLine();
			}
			#endregion

			#region Get Input Example
			{
				Console.WriteLine("---------------------");
				Console.WriteLine("GetInput is an reasy method to loop until the");
				Console.WriteLine("user provides valid input of a generic type.");
				double double_intput = ConsoleHelper.GetInput<double>();
				Console.WriteLine($"Your input: {double_intput}.");
				int min = 0;
				int max = 100;
				int int_input = ConsoleHelper.GetInput<int>(
					prompt: $"Insert an integer ({min}-{max}): ",
					validation: value => min <= value && value <= max);
				Console.WriteLine($"Your input: {int_input}.");
				Console.WriteLine();
			}
			#endregion

			#region Animated Ellipsis Example
			{
				Console.WriteLine("---------------------");
				Console.WriteLine("Animated Ellipsis Example");
				Console.Write("Press Enter to start");
				ConsoleHelper.PressToContinue();
				DoWith(cursorVisible: false, action: () =>
				{
					Thread thread = new Thread(() => Thread.Sleep(TimeSpan.FromSeconds(5)));
					thread.Start();
					ConsoleHelper.AnimatedElipsis(
						condition: () => thread.IsAlive,
						delay: () => Thread.Sleep(TimeSpan.FromMilliseconds(300)));
				});
				Console.WriteLine("...");
				Console.WriteLine();
			}
			#endregion

			#region Progress Bar Example
			{
				Console.WriteLine("---------------------");
				Random random = new Random();
				Console.WriteLine("Progress Bar Example.");
				ConsoleHelper.PromptPressToContinue();
				Console.WriteLine();
				DoWith(cursorVisible: false, action: () =>
					{
						ConsoleHelper.ProgressBar(
							postClear: false,
							length: 26,
							action: action =>
							{
								int iterations = random.Next(5, 20);
								for (int i = 0; i < iterations; i++)
								{
									Thread.Sleep(TimeSpan.FromMilliseconds(random.Next(1000 / iterations, 15000 / iterations)));
									action(i / (double)iterations * 100);
								}
							});
					});
				Console.WriteLine();
				Console.WriteLine();
			}
			#endregion

			#region Hidden ReadLine Example
			{
				Console.WriteLine("---------------------");
				Console.WriteLine("This is an example of a custom \"Console.ReadLine\"");
				Console.WriteLine("with masked characters for use cases such as passwords.");
				Console.WriteLine("Type \"exit\" to close the program. It supports Backspace,");
				Console.WriteLine("Delete, Left/Right Arrows, Escape, Home, and End keypresses");
				Console.WriteLine("and the CTRL modifier.");
				Console.Write("Input: ");
				string input = ConsoleHelper.HiddenReadLine();
				Console.WriteLine("Your input: " + input);
				Console.WriteLine();
			}
			#endregion

			#region Int Menu Example
			{
				Console.WriteLine("---------------------");
				Console.WriteLine("IntMenu allows easy console menus.");
				Console.WriteLine();

				static void Option1()
				{
					Console.WriteLine("You chose the first option.");
				}

				static void Option2()
				{
					Console.WriteLine("You chose the second option.");
				}

				static void Option3()
				{
					Console.WriteLine("You chose the third option.");
				}

				ConsoleHelper.IntMenu(
					("Option 1", Option1),
					("Option 2", Option2),
					("Option 3", Option3));
				Console.WriteLine();
			}
			#endregion

			#region Flush Input Buffer Example
			{
				Console.WriteLine("---------------------");
				Console.WriteLine("If you want to prefent console input, you can");
				Console.WriteLine("use FlushInputBuffer and all input prior to");
				Console.WriteLine("calling it will be ignored.");
				ConsoleHelper.FlushInputBuffer();
				Console.WriteLine();
			}
			#endregion

			Console.WriteLine("============================================");
			Console.WriteLine("Example Complete...");
			Console.WriteLine();
			ConsoleHelper.PromptPressToContinue();
		}

		public static void DoWith(
			Action action,
			ConsoleColor? foreground = null,
			ConsoleColor? background = null,
			bool? cursorVisible = null)
		{
			ConsoleColor? foregroundRevert = null;
			ConsoleColor? backgroundRevert = null;
			bool? cursorVisibleRevert = null;
			try
			{
				if (foreground.HasValue)
				{
					foregroundRevert = Console.ForegroundColor;
					Console.ForegroundColor = foreground.Value;
				}
				if (background.HasValue)
				{
					backgroundRevert = Console.BackgroundColor;
					Console.BackgroundColor = background.Value;
				}
				if (cursorVisible.HasValue)
				{
					try
					{
						cursorVisibleRevert = Console.CursorVisible;
					}
					catch (PlatformNotSupportedException)
					{
						cursorVisibleRevert = true;
					}
					Console.CursorVisible = cursorVisible.Value;
				}
				action();
			}
			finally
			{
				if (foregroundRevert.HasValue)
				{
					Console.ForegroundColor = foregroundRevert.Value;
				}
				if (backgroundRevert.HasValue)
				{
					Console.BackgroundColor = backgroundRevert.Value;
				}
				if (cursorVisibleRevert.HasValue)
				{
					Console.CursorVisible = cursorVisibleRevert.Value;
				}
			}
		}
	}
}
