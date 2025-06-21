

using LexiconExercise5_Garage.ConsoleRelated.CWritePrint;
using LexiconExercise5_Garage.ConsoleRelated.DisplayMessages.ErrorMessages;
using LexiconExercise5_Garage.ConsoleRelated.DisplayMessages.FeedbackMessage;
using LexiconExercise5_Garage.ConsoleRelated.DisplayMessages.MenuMessages;
using LexiconExercise5_Garage.Vehicles;
using LexiconExercise5_Garage.Vehicles.LicensePlate.Registry;
using System.Text;

namespace LexiconExercise5_GarageAssignment.ConsoleRelated
{
	public class ConsoleUI : IConsoleUI
	{
		private readonly IConsoleWritePrint _consoleWP;
		private readonly IDisplayErrorMessages _displayErrorMessages;
		private readonly IDisplayMenuMessages _displayMenuMessages;
		private readonly IDisplayFeedbackMessage _displayFeedbackMessage;

		public ConsoleUI(
			IConsoleWritePrint consoleWritePrint,
			IDisplayErrorMessages displayErrorMessages,
			IDisplayMenuMessages displayMenuMessages,
			IDisplayFeedbackMessage displayFeedbackMessage)
		{
			_consoleWP = consoleWritePrint;
			_displayErrorMessages = displayErrorMessages;
			_displayMenuMessages = displayMenuMessages;
			_displayFeedbackMessage = displayFeedbackMessage;
		}

		public int GetGarageSize()
		{
			_consoleWP.Write("Choose the size of the garage, upper limit is 524288: ");

			return ValidateNumericInput(rangeMin: 1, rangeMax: 524288);
		}

		private int ValidateNumericInput(int rangeMin, int rangeMax)
		{
			bool inputValid = false;
			int returnValue = -1;

			do
			{
				string input = _consoleWP.ReadLine();

				if (String.IsNullOrWhiteSpace(input))
					_displayErrorMessages.InvalidInputEmpty();
				if (!int.TryParse(input, out int intInput))
					_displayErrorMessages.InvalidInputNotNumber();
				else if (intInput < rangeMin || intInput > rangeMax)
					_displayErrorMessages.InvalidInputOutsideOfMenuRange();
				else
				{
					returnValue = intInput;
					inputValid = true;
				}

			} while (!inputValid);

			return returnValue;
		}

		public void ShowError(string message)
		{
			_displayErrorMessages.DisplayErrorMessage(message);
			_consoleWP.ReadKey();
		}

		public int RegisterMainMenuSelection()
		{
			_displayMenuMessages.DisplayMenu(
				"Main Menu: \n\n" +
				"1: Create a garage, with a size inside the range of 1 - 524288.\n" +
				"2: Select garage to handle. \n" +
				"0: Exit program.\n\n"
			);

			_consoleWP.Write("Chose an option: ");

			return ValidateNumericInput(rangeMin: 0, rangeMax: 2);
		}

		public int RegisterGarageHandlingMenuSelection(int garageId)
		{
			_displayMenuMessages.DisplayMenu(
				$"Garage Handling: currently selected {garageId}\n\n" +
				"1: Park a mixed set of vehicle in garage. (This is your fast forward vehicle adding for testing\n" + // only works ONCE per execution with mixed vehicle garages for now 
				"2: Park a vehicle in garage.\n" +
				"3: Remove a vehicle, from garage.\n" +
				"4: Get vehicle information of a single vehicle, currently parked in garage.\n" +
				"5: Get vehicle information of all vehicles, currently parked in garage.\n" +
				"6: Get filtered information of all vehicles.\n" +
				"0: Exit garage handling menu.\n\n"
			);

			_consoleWP.Write("Chose an option: ");

			return ValidateNumericInput(rangeMin: 0, rangeMax: 6);
		}

		public int SelectGarage(List<int> garageNumbers)
		{
			ShowCurrentGarages(garageNumbers);

			_consoleWP.Write("Chose which garage to handle: ");

			return ValidateNumericInput(
				rangeMin: 0,
				rangeMax:
				garageNumbers.Count
			);
		}

		private void ShowCurrentGarages(List<int> garageNumbers)
		{
			StringBuilder stringBuilder = new StringBuilder();

			stringBuilder.Append("Current created garages: \n");

			foreach (int number in garageNumbers)
				stringBuilder.Append($"Garage ID: {number}\n");

			_consoleWP.WriteLine(stringBuilder.ToString());
		}

		public void ShowFeedbackMessage(string message)
		{
			_displayFeedbackMessage.DisplayFeedBackMessage(message);
			_consoleWP.ReadKey();
		}


		public string RegisterLicensePlateInput(string? message = null)
		{
			if (message != null) _consoleWP.Write(message);
			return _consoleWP.ReadLine();
		}

		public int RegisterInputFromEnumOptions<TEnum>(string? message = null) where TEnum : Enum
		{
			if (message != null) _consoleWP.WriteLine($"{message}");
			ShowEnumValuesAsOptions<TEnum>();

			int minOption = Enum.GetValues(typeof(TEnum)).Cast<int>().Min();
			int maxOptions = Enum.GetValues(typeof(TEnum)).Cast<int>().Max();

			return ValidateNumericInput(rangeMin: minOption, rangeMax: maxOptions);
		}

		private void ShowEnumValuesAsOptions<TEnum>() where TEnum : Enum
		{
			_consoleWP.WriteLine("");

			foreach (var item in Enum.GetValues(typeof(TEnum)))
				_consoleWP.WriteLine($"{(int)item} : {item}");
		}

		public int RegisterWhatVehicleToCreateMenu() =>
			RegisterInputFromEnumOptions<VehicleType>();
		
		public uint RegisterNumericUintInput(string message, int rangeMin, int rangeMax)
		{
			_consoleWP.Write(message);
			return (uint)ValidateNumericInput(rangeMin, rangeMax);
		}
	}
}