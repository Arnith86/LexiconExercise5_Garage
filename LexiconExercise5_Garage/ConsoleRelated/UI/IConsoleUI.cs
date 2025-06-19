using LexiconExercise5_Garage.Vehicles;

namespace LexiconExercise5_GarageAssignment.ConsoleRelated
{
	public interface IConsoleUI
	{
		int GetGarageSize();
		void ShowError(string message);
		int RegisterMainMenuSelection();
		int SelectGarage(List<int> garageNumbers);
		void ShowFeedbackMessage(string message);
		int RegisterGarageHandlingMenuSelection(int garageKey);
		string RegisterLicensePlateInput();
		int RegisterColorInput();
		uint RegisterNrOfWheelsInput();
		uint RegisterNumberOfEnginesInput();
		int RegisterWhatVehicleToCreateMenu();
	}
}