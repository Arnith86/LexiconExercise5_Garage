namespace LexiconExercise5_Garage.ConsoleRelated.DisplayMessages.ErrorMessages
{
	/// <summary>
	/// Defines a set of methods for displaying standardized error messages to the console.
	/// Intended to centralize user feedback related to invalid input or application errors.
	/// </summary>
	public interface IDisplayErrorMessages
	{
		/// <summary>
		/// Displays a custom error message to the console.
		/// The message is shown in red to indicate an error.
		/// </summary>
		/// <param name="message">The message to display.</param>
		void DisplayErrorMessage(string message);

		/// <summary>
		/// Displays an error message indicating that the input was empty.
		/// </summary>
		void InvalidInputEmpty();
	}
}