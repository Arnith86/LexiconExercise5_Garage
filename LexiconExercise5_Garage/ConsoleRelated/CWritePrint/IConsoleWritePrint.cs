namespace LexiconExercise5_Garage.ConsoleRelated.CWritePrint;

/// <summary>
/// Defines methods for abstracting console input and output operations.
/// This interface allows decoupling console logic from business logic, enabling easier testing and mocking.
/// </summary>
public interface IConsoleWritePrint
{
	/// <summary>
	/// Clears the console screen.
	/// </summary>
	void Clear();

	/// <summary>
	/// Sets the console's foreground text color.
	/// </summary>
	/// <param name="color">The <see cref="ConsoleColor"/> to set.</param>
	void ForegroundColor(ConsoleColor color);

	/// <summary>
	/// Waits for the user to press a key.
	/// </summary>
	void ReadKey();

	/// <summary>
	/// Reads the next line of input from the console.
	/// </summary>
	/// <returns>The input string, or an empty string if null.</returns>
	string ReadLine();

	/// <summary>
	/// Resets the console colors to their default settings.
	/// </summary>
	void ResetConsoleColor();

	/// <summary>
	/// Writes a message to the console without a newline character.
	/// </summary>
	/// <param name="message">The message to write.</param>
	void Write(string message);

	/// <summary>
	/// Writes a message to the console followed by a newline character.
	/// </summary>
	/// <param name="message">The message to write.</param>
	void WriteLine(string message);
}