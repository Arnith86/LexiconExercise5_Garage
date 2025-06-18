using LexiconExercise5_Garage.ConsoleRelated.CWritePrint;

namespace LexiconExercise5_Garage.ConsoleRelated.DisplayMessages.MenuMessages;

/// <summary>
/// Provides a console-based implementation for displaying menu messages with colored formatting.
/// </summary>
public class DisplayMenuMessages : IDisplayMenuMessages
{
	private readonly IConsoleWritePrint _consoleWP;

	/// <summary>
	/// Initializes a new instance of the <see cref="DisplayMenuMessages"/> class.
	/// </summary>
	/// <param name="consoleWritePrint">An implementation of <see cref="IConsoleWritePrint"/> used to handle console output operations.</param>
	public DisplayMenuMessages(IConsoleWritePrint consoleWritePrint) =>
		_consoleWP = consoleWritePrint;

	/// <inheritdoc/>
	public void DisplayMenu(string menu)
	{
		_consoleWP.ForegroundColor(ConsoleColor.Yellow);
		_consoleWP.WriteLine(menu);
		_consoleWP.ResetConsoleColor();
	}
}