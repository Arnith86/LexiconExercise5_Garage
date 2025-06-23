

using LexiconExercise5_Garage.ConsoleRelated.CWritePrint;
using LexiconExercise5_Garage.ConsoleRelated.DisplayMessages.ErrorMessages;
using LexiconExercise5_Garage.ConsoleRelated.DisplayMessages.FeedbackMessage;
using LexiconExercise5_Garage.ConsoleRelated.DisplayMessages.MenuMessages;
using LexiconExercise5_Garage.Vehicles;
using LexiconExercise5_Garage.Vehicles.VehicleBase;
using System.Text;


namespace LexiconExercise5_GarageAssignment.ConsoleRelated;

/// <summary>
/// Handles all user interaction via the console, including input validation,
/// menu navigation, and message displays.
/// </summary>
public class ConsoleUI : IConsoleUI
{
	private readonly IConsoleWritePrint _consoleWP;
	private readonly IDisplayErrorMessages _displayErrorMessages;
	private readonly IDisplayMenuMessages _displayMenuMessages;
	private readonly IDisplayFeedbackMessage _displayFeedbackMessage;

	/// <summary>
	/// Initializes a new instance of the <see cref="ConsoleUI"/> class.
	/// </summary>
	/// <param name="consoleWritePrint">Abstraction for console read/write.</param>
	/// <param name="displayErrorMessages">Handles error message output.</param>
	/// <param name="displayMenuMessages">Handles menu message output.</param>
	/// <param name="displayFeedbackMessage">Handles feedback message output.</param>
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

	/// <inheritdoc/>
	public int GetGarageSize()
	{
		_consoleWP.Write("Choose the size of the garage, upper limit is 524288: ");

		return ValidateNumericInput(rangeMin: 1, rangeMax: 524288);
	}

	/// <summary>
	/// Validates numeric input from the user within the specified range.
	/// </summary>
	/// <param name="rangeMin">Minimum allowed value.</param>
	/// <param name="rangeMax">Maximum allowed value.</param>
	/// <returns>A valid integer within the specified range.</returns>
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

	/// <inheritdoc/>
	public void ShowError(string message)
	{
		_displayErrorMessages.DisplayErrorMessage(message);
		_consoleWP.ReadKey();
	}

	/// <inheritdoc/>
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

	/// <inheritdoc/>
	public int RegisterGarageHandlingMenuSelection(int garageId)
	{
		_displayMenuMessages.DisplayMenu(
			$"Garage Handling: currently selected {garageId}\n\n" +
			"1: Park a mixed set of vehicle in garage. (This is your fast forward vehicle adding for testing\n" + // only works ONCE per execution with mixed vehicle garages for now 
			"2: Park a vehicle in garage.\n" +
			"3: Remove a vehicle, from garage.\n" +
			"4: Get vehicle information of a single vehicle, currently parked in garage.\n" +
			"5: Show all vehicle currently parked in garage.\n" +
			"6: List how many of each type of vehicle is parked in garage.\n" +
			"7: Get filtered information of all vehicles.\n" +
			"0: Exit garage handling menu.\n\n"
		);

		_consoleWP.Write("Chose an option: ");

		return ValidateNumericInput(rangeMin: 0, rangeMax: 7);
	}

	/// <inheritdoc/>
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

	/// <inheritdoc/>
	public void ShowFeedbackMessage(string message)
	{
		_displayFeedbackMessage.DisplayFeedBackMessage(message);
		_consoleWP.ReadKey();
	}

	/// <inheritdoc/>
	public string RegisterLicensePlateInput(string? message = null)
	{
		if (message != null) _consoleWP.Write(message);
		return _consoleWP.ReadLine();
	}

	/// <inheritdoc/>
	public int RegisterInputFromEnumOptions<TEnum>(string? message = null) where TEnum : Enum
	{
		if (message != null) _consoleWP.WriteLine($"{message}");
		ShowEnumValuesAsOptions<TEnum>();

		int minOption = Enum.GetValues(typeof(TEnum)).Cast<int>().Min();
		int maxOptions = Enum.GetValues(typeof(TEnum)).Cast<int>().Max();

		return ValidateNumericInput(rangeMin: minOption, rangeMax: maxOptions);
	}

	/// <summary>
	/// Outputs all values of an enum as numbered options for user selection.
	/// </summary>
	/// <typeparam name="TEnum">The enum type to display.</typeparam>
	private void ShowEnumValuesAsOptions<TEnum>() where TEnum : Enum
	{
		_consoleWP.WriteLine("");

		foreach (var item in Enum.GetValues(typeof(TEnum)))
			_consoleWP.WriteLine($"{(int)item} : {item}");
	}

	/// <inheritdoc/>
	public int RegisterWhatVehicleToCreateMenu() =>
		RegisterInputFromEnumOptions<VehicleType>();

	/// <inheritdoc/>
	public uint RegisterNumericUintInput(string message, int rangeMin, int rangeMax)
	{
		_consoleWP.Write(message);
		return (uint)ValidateNumericInput(rangeMin, rangeMax);
	}

	/// <inheritdoc/>
	public void DisplayInformation(string vehicleInfo)
	{
		_consoleWP.WriteLine(vehicleInfo);
		_consoleWP.ReadKey();
	}

	/// <inheritdoc/>
	public int RegisterPropertyFiltersInput()
	{
		_displayMenuMessages.DisplayMenu(
			$"Select the properties to filter by.\nOnly one filter per menu option supported.\n" +
			"1: Vehicle type\n" +
			"2: Color\n" +
			"3: Number of wheels\n" +
			"4: Start filter!\n" +
			"0: Exit menu\n\n"
		);

		_consoleWP.Write("Chose an option: ");

		return ValidateNumericInput(rangeMin: 0, rangeMax: 4);
	}

	/// <inheritdoc/>
	public int WhichFilterPropertyFromEnum<TEnum>(string message) where TEnum : Enum
	{
		_displayMenuMessages.DisplayMenu(message);
		return RegisterInputFromEnumOptions<TEnum>();
	}

	/// <inheritdoc/>
	public void DisplayFilteredInformation(IEnumerable<IVehicle> result)
	{
		if (result.Count() > 0)
		{
			foreach (var vehicle in result)
				_consoleWP.WriteLine(vehicle.ToString());
		}
		else
		{
			ShowFeedbackMessage("No matches to your search..");
		}

		_consoleWP.ReadKey();
	}
}