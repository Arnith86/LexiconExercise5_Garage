namespace LexiconExercise5_Garage.ConsoleRelated.CWritePrint
{
	/// <summary>
	/// A class that abstracts console input and output operations.
	/// </summary>
	public class ConsoleWritePrint : IConsoleWritePrint
	{
		/// <inheritdoc/>
		public void WriteLine(string message) => Console.WriteLine(message);

		/// <inheritdoc/>
		public void Write(string message) => Console.Write(message);


		/// <inheritdoc/>
		public string ReadLine() => Console.ReadLine() ?? string.Empty;

		/// <inheritdoc/>
		public void ForegroundColor(ConsoleColor color) =>
			Console.ForegroundColor = color;

		/// <inheritdoc/>
		public void ResetConsoleColor() => Console.ResetColor();

		/// <inheritdoc/>
		public void Clear() => Console.Clear();

		/// <inheritdoc/>
		public void ReadKey() => Console.ReadKey();
	}
}
