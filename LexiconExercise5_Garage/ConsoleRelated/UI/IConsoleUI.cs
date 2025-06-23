using LexiconExercise5_Garage.Vehicles.VehicleBase;

namespace LexiconExercise5_GarageAssignment.ConsoleRelated;

/// <summary>
/// Interface for handling all user interactions via the console.
/// </summary>
public interface IConsoleUI
{
	/// <summary>
	/// Prompts the user to enter the size of the garage.
	/// </summary>
	/// <returns>The user-specified size of the garage.</returns>
	int GetGarageSize();

	/// <summary>
	/// Displays an error message to the user.
	/// </summary>
	/// <param name="message">The error message to display.</param>
	void ShowError(string message);

	/// <summary>
	/// Displays the main menu and returns the selected option.
	/// </summary>
	/// <returns>The user's selected menu option.</returns>
	int RegisterMainMenuSelection();

	/// <summary>
	/// Prompts the user to select a garage from a list of available garage IDs.
	/// </summary>
	/// <param name="garageNumbers">A list of existing garage identifiers.</param>
	/// <returns>The selected garage ID.</returns>
	int SelectGarage(List<int> garageNumbers);

	/// <summary>
	/// Displays a feedback message to the user (e.g., after successful actions).
	/// </summary>
	/// <param name="message">The message to display.</param>
	void ShowFeedbackMessage(string message);

	/// <summary>
	/// Displays the garage handling menu for a selected garage and returns the selected option.
	/// </summary>
	/// <param name="garageKey">The identifier of the selected garage.</param>
	/// <returns>The user's selected menu option.</returns>
	int RegisterGarageHandlingMenuSelection(int garageKey);

	/// <summary>
	/// Prompts the user to choose an option from an enum and returns the selected value as an int.
	/// </summary>
	/// <typeparam name="TEnum">The enum type to choose from.</typeparam>
	/// <param name="message">Optional message to display with the prompt.</param>
	/// <returns>The selected enum value as an integer.</returns>
	int RegisterInputFromEnumOptions<TEnum>(string? message = null) where TEnum : Enum;

	/// <summary>
	/// Prompts the user to input a license plate string.
	/// </summary>
	/// <param name="message">Optional message to display with the prompt.</param>
	/// <returns>The entered license plate.</returns>
	string RegisterLicensePlateInput(string? message = null);

	/// <summary>
	/// Displays the menu for choosing which type of vehicle to create.
	/// </summary>
	/// <returns>The selected vehicle type as an integer.</returns>
	int RegisterWhatVehicleToCreateMenu();

	/// <summary>
	/// Prompts the user for an unsigned integer input within a given range.
	/// </summary>
	/// <param name="message">The message to display with the prompt.</param>
	/// <param name="rangeMin">The minimum valid value.</param>
	/// <param name="rangeMax">The maximum valid value.</param>
	/// <returns>The user-entered unsigned integer.</returns>
	public uint RegisterNumericUintInput(string message, int rangeMin, int rangeMax);

	/// <summary>
	/// Displays informational text, such as vehicle data or summaries.
	/// </summary>
	/// <param name="vehicleInfo">The information to display.</param>
	void DisplayInformation(string vehicleInfo);

	/// <summary>
	/// Displays the filter menu for vehicle properties and returns the selected option.
	/// </summary>
	/// <returns>The selected filter menu option.</returns>
	int RegisterPropertyFiltersInput();

	/// <summary>
	/// Prompts the user to select a specific value from an enum used for filtering.
	/// </summary>
	/// <typeparam name="TEnum">The enum type to select from.</typeparam>
	/// <param name="message">Message to display with the prompt.</param>
	/// <returns>The selected enum value as an integer.</returns>
	int WhichFilterPropertyFromEnum<TEnum>(string message) where TEnum : Enum;

	/// <summary>
	/// Displays the result of filtering the vehicle list based on user-selected criteria.
	/// </summary>
	/// <param name="result">A filtered collection of vehicles.</param>
	void DisplayFilteredInformation(IEnumerable<IVehicle> result);
}