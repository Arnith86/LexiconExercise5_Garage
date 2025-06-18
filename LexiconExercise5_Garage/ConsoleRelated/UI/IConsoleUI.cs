namespace LexiconExercise5_GarageAssignment.ConsoleRelated
{
	public interface IConsoleUI
	{
		int GetGarageSize();
		void GarageCreated(int garageSize);
		void ShowError(string message);
		int RegisterMainMenuSelection();
	}
}