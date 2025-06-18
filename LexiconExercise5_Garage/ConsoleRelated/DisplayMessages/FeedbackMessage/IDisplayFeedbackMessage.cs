namespace LexiconExercise5_Garage.ConsoleRelated.DisplayMessages.FeedbackMessage;

/// <summary>
/// Defines a contract for displaying feedback messages to the user.
/// </summary>
public interface IDisplayFeedbackMessage
{
	/// <summary>
	/// Displays a feedback message to the console or user interface.
	/// </summary>
	/// <param name="message">The message to be displayed.</param>
	void DisplayFeedBackMessage(string message);
}