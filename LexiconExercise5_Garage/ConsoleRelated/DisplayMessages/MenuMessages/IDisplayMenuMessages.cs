/// <summary>
/// Contains the interface definition for displaying menu-related messages in the console UI.
/// </summary>
namespace LexiconExercise5_Garage.ConsoleRelated.DisplayMessages.MenuMessages
{
	/// <summary>
	/// Defines a contract for displaying menu messages in the console application.
	/// </summary>
	public interface IDisplayMenuMessages
	{
		/// <summary>
		/// Displays the specified menu string to the user.
		/// </summary>
		/// <param name="menu">A formatted string representing the menu to be displayed.</param>
		void DisplayMenu(string menu);
	}
}