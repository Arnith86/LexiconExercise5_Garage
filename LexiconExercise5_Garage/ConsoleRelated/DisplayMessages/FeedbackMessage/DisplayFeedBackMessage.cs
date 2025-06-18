using LexiconExercise5_Garage.ConsoleRelated.CWritePrint;

namespace LexiconExercise5_Garage.ConsoleRelated.DisplayMessages.FeedbackMessage;

/// <summary>
/// Provides functionality to display feedback messages using a colored console output.
/// </summary>
public class DisplayFeedBackMessage : IDisplayFeedbackMessage
{
	private readonly IConsoleWritePrint _consoleWP;

	/// <summary>
	/// Initializes a new instance of the <see cref="DisplayFeedBackMessage"/> class.
	/// </summary>
	/// <param name="consoleWritePrint">An implementation of <see cref="IConsoleWritePrint"/> to handle console output.</param>
	public DisplayFeedBackMessage(IConsoleWritePrint consoleWritePrint)
	{
		_consoleWP = consoleWritePrint;
	}

	/// <inheritdoc/>
	void IDisplayFeedbackMessage.DisplayFeedBackMessage(string message)
	{
		_consoleWP.ForegroundColor(ConsoleColor.DarkBlue);
		_consoleWP.WriteLine(message);
		_consoleWP.ResetConsoleColor();
	}
}