using LexiconExercise5_Garage.ConsoleRelated.CWritePrint;

namespace LexiconExercise5_Garage.ConsoleRelated.DisplayMessages.ErrorMessages
{
	/// <summary>
	/// This class supplies generic error messages that can be used throughout the application.
	/// </summary>
	public class DisplayErrorMessages : IDisplayErrorMessages
	{
		private readonly IConsoleWritePrint _consoleWritePrint;

		/// <summary>
		/// Initializes a new instance of the <see cref="DisplayErrorMessages"/> class with a given console output handler.
		/// </summary>
		/// <param name="consoleWritePrint">
		/// An implementation of <see cref="IConsoleWritePrint"/> used to handle console output operations.
		/// </param>
		public DisplayErrorMessages(IConsoleWritePrint consoleWritePrint)
		{
			_consoleWritePrint = consoleWritePrint;
		}

		/// <inheritdoc/>
		public void InvalidInputEmpty() =>
			DisplayErrorMessage("Input cannot be empty! Please try again.");


		/// <inheritdoc/>
		public void DisplayErrorMessage(string message)
		{
			Console.ForegroundColor = ConsoleColor.Red;
			_consoleWritePrint.WriteLine($"{message}\n");
			Console.ResetColor();
		}
	}
}