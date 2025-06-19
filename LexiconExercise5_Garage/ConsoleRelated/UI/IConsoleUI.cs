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
		VehicleColor RegisterColorInput();
		uint RegisterNrOfWheelsInput();
		uint RegisterNumberOfEnginesInput();
		VehicleType RegisterWhatVehicleToCreateMenu();
		void ShowBuildableVehicles();
	}
}