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
		int RegisterInputFromEnumOptions<TEnum>(string? message = null) where TEnum : Enum;
		string RegisterLicensePlateInput(string? message = null);
		int RegisterWhatVehicleToCreateMenu();
		public uint RegisterNumericUintInput(string message, int rangeMin, int rangeMax);
		void DisplayInformation(string vehicleInfo);
	}
}