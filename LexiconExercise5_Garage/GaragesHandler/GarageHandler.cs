
using LexiconExercise5_Garage.Garages;
using LexiconExercise5_Garage.Garages.GarageFactory;
using LexiconExercise5_Garage.Util;
using LexiconExercise5_Garage.Vehicles;
using LexiconExercise5_Garage.Vehicles.AirPlains;
using LexiconExercise5_Garage.Vehicles.Boats;
using LexiconExercise5_Garage.Vehicles.Buss;
using LexiconExercise5_Garage.Vehicles.Cars;
using LexiconExercise5_Garage.Vehicles.LicensePlate.Registry;
using LexiconExercise5_Garage.Vehicles.Motorcycles;
using LexiconExercise5_Garage.Vehicles.VehicleBase;
using LexiconExercise5_Garage.Vehicles.VehicleFactories;
using LexiconExercise5_GarageAssignment.ConsoleRelated;
using System.Text;

namespace LexiconExercise5_Garage.GaragesHandler;

/// <summary>
/// Handles user interactions and operations related to garages and parked vehicles.
/// Functions as a go between of the garage and user interface. 
/// </summary>
/// <remarks>
/// Provides menu navigation, garage creation, vehicle parking/removal, and filtering operations.
/// </remarks>
public class GarageHandler
{
	private readonly IConsoleUI _consoleUI;
	private readonly IGarageCreator<IVehicle> _garageCreator;
	private readonly BuildVehicle _buildVehicle;
	private readonly ILicensePlateRegistry _licensePlateRegistry;
	private readonly VehiclesFilterFunctions _vehiclesFilterFunctions;

	private Dictionary<int, IGarage<IVehicle>> _garages;
	private readonly StringBuilder _stringBuilder = new StringBuilder();

	/// <summary>
	/// Initializes a new instance of <see cref="GarageHandler"/> with dependencies.
	/// </summary>
	public GarageHandler(
		IConsoleUI consoleUI,
		IGarageCreator<IVehicle> garageCreator,
		BuildVehicle buildVehicle,
		ILicensePlateRegistry licensePlateRegistry,
		VehiclesFilterFunctions vehiclesFilterFunctions)
	{
		_consoleUI = consoleUI;
		_garageCreator = garageCreator;
		_buildVehicle = buildVehicle;
		_licensePlateRegistry = licensePlateRegistry;
		_vehiclesFilterFunctions = vehiclesFilterFunctions;
		_garages = new Dictionary<int, IGarage<IVehicle>>();
	}

	/// <summary>
	/// Displays the main menu and processes user selections.
	/// </summary>
	public void MainMenuSelection()
	{
		bool exitProgram = false;

		do
		{
			int menuOption = _consoleUI.RegisterMainMenuSelection();

			switch (menuOption)
			{
				case 1:
					GarageCreation();
					break;
				case 2:
					if (AnyGaragesExist()) GarageHandlingMenuSelection(garageKey: SelectGarage());
					break;
				case 0:
					exitProgram = true;
					break;
				default:
					break;
			}

		} while (!exitProgram);
	}

	/// <summary>
	/// Starts the garage creation process based on user input.
	/// </summary>
	public void GarageCreation()
	{
		bool isValid = false;
		int size = 0;

		do
		{
			size = _consoleUI.GetGarageSize();

			try
			{
				int newKey = _garages.Any() ? _garages.Keys.Max() + 1 : 0;
				_garages.Add(newKey, _garageCreator.CreateGarage(size));

				isValid = true;
			}
			catch (ArgumentOutOfRangeException e)
			{
				_consoleUI.ShowError(message: e.Message);
			}

		} while (!isValid);

		_consoleUI.ShowFeedbackMessage($"Garage of size {size} created!");
	}

	private bool AnyGaragesExist()
	{
		if (_garages.Count > 0) return true;

		_consoleUI.ShowError("There are no created garages yet!");

		return false;
	}

	/// <summary>
	/// Handles operations inside a specific garage, such as parking, removing, or listing vehicles.
	/// </summary>
	/// <param name="garageKey">The key (ID) of the garage to operate on.</param>
	private void GarageHandlingMenuSelection(int garageKey)
	{
		bool exitGarageHandlingMenu = false;

		do
		{
			int menuOption = _consoleUI.RegisterGarageHandlingMenuSelection(garageKey);

			switch (menuOption)
			{
				case 1:
					AddMixedSetOfVehiclesToGarage(garageKey);
					break;
				case 2:
					WhatVehicleToCreateMenu(garageKey);
					break;
				case 3:
					RemoveAVehicle(garageKey);
					break;
				case 4:
					GetVehicleInformation(garageKey);
					break;
				case 5:
					ListAllVehicles(garageKey);
					break;
				case 6:
					ListHowManyVehiclesOfEachType(garageKey);
					break;
				case 7:
					FilterBasedOnProperties(garageKey);
					break;
				case 0:
					exitGarageHandlingMenu = true;
					break;
				default:
					break;
			}

		} while (!exitGarageHandlingMenu);
	}

	/// <summary>
	/// Prompts user to apply one or more filters to the vehicle collection and displays the result.
	/// </summary>
	/// <param name="garageKey">Garage to filter vehicles in.</param>
	private void FilterBasedOnProperties(int garageKey)
	{
		// ToDo: Extract to own class 
		Dictionary<int, Type> VehicleTypeMap = new()
		{
			{ 1, typeof(AirPlain) },
			{ 2, typeof(Boat) },
			{ 3, typeof(Bus) },
			{ 4, typeof(Car) },
			{ 5, typeof(Motorcycle) },
			// Add more types as needed
		};

		List<Func<IVehicle, bool>> predicates = new();
		bool[] chosenOptions = new bool[3];

		int menuOption = -1;

		do
		{
			menuOption = _consoleUI.RegisterPropertyFiltersInput();

			switch (menuOption)
			{
				case 1:

					if (chosenOptions[0] == true) OnlyOnce();
					else
					{
						chosenOptions[0] = true;
						Type chosenType = VehicleTypeMap[_consoleUI.WhichFilterPropertyFromEnum<VehicleType>(message: "Chose the type of vehicle: ")];

						FilterSelectionAdded(
							message: $"Filter by {chosenType.Name}",
							predicate: _vehiclesFilterFunctions.VehicleTypePredicate(chosenType)
						);
					}

					break;
				case 2:

					if (chosenOptions[1] == true) OnlyOnce();
					else
					{
						chosenOptions[1] = true;
						VehicleColor chosenColor = (VehicleColor)_consoleUI.WhichFilterPropertyFromEnum<VehicleColor>(message: "Which color: ");

						FilterSelectionAdded(
							message: $"Filter by {chosenColor}",
							predicate: _vehiclesFilterFunctions.ByColorPredicate(chosenColor)
						);
					}

					break;
				case 3:

					if (chosenOptions[2] == true) OnlyOnce();
					else
					{
						chosenOptions[2] = true;
						CompareOptions chosenOption = (CompareOptions)_consoleUI.WhichFilterPropertyFromEnum<CompareOptions>(message: "Which Compare option do you want: ");
						int chosenValue = (int)_consoleUI.RegisterNumericUintInput(message: "Against what value: ", rangeMin: 0, rangeMax: 58);

						FilterSelectionAdded(
							message: $"Filter by {EnumHelper.GetEnumDescription(chosenOption)} {chosenValue}",
							predicate: _vehiclesFilterFunctions.ByWheelCountPredicate(
								_vehiclesFilterFunctions.WhichNumericComparePredicate(chosenOption, chosenValue)
							)
						);
					}

					break;
				default:
					break;
			}
		} while (menuOption != 4 && menuOption != 0);

		void OnlyOnce()
		{
			_consoleUI.ShowError("Filter already applied once!");
		}

		void FilterSelectionAdded(string message, Func<IVehicle, bool> predicate)
		{
			_consoleUI.ShowFeedbackMessage(message);
			predicates.Add(predicate);
		}

		if (menuOption == 4)
		{
			var filtrableList = _garages[garageKey].PerformedLinqQuery(
				vehicles => vehicles.ToList()
			);

			IEnumerable<IVehicle> result = ApplyFilters(filtrableList, predicates);


			_consoleUI.DisplayFilteredInformation(result);
		}
	}

	/// <summary>
	/// Applies a list of filtering predicates to the provided vehicle collection.
	/// </summary>
	/// <param name="filtrableList">The collection of vehicles to filter.</param>
	/// <param name="predicates">Filtering conditions.</param>
	/// <returns>Filtered vehicle collection.</returns>
	private IEnumerable<IVehicle> ApplyFilters(
		IEnumerable<IVehicle> filtrableList,
		List<Func<IVehicle, bool>> predicates)
	{
		IEnumerable<IVehicle> result = filtrableList;

		foreach (var predicate in predicates)
			result = result.Where(predicate);

		return result;
	}

	private void ListHowManyVehiclesOfEachType(int garageKey)
	{
		var vehicleTypeCounter = _garages[garageKey].PerformedLinqQuery(vehicles =>
			vehicles
				.GroupBy(vehicle => vehicle.GetType().Name)
				.Select(group => $"{group.Key}: {group.Count()}")
		);

		foreach (var item in vehicleTypeCounter)
			_stringBuilder.Append($"{item}\n");

		_consoleUI.DisplayInformation(_stringBuilder.ToString());

		_stringBuilder.Clear();
	}

	/// <summary>
	/// Displays license plates of all vehicles in the specified garage.
	/// </summary>
	/// <param name="garageKey">The ID of the garage.</param>
	private void ListAllVehicles(int garageKey)
	{
		var vehicleLicensePlates = _garages[garageKey].PerformedLinqQuery(vehicles =>
			vehicles
				.Where(vehicle => vehicle != null)
				.Select(vehicle => vehicle.LicensePlate)
		);

		foreach (var licensePlate in vehicleLicensePlates)
			_stringBuilder.Append($"{licensePlate}\n");

		_consoleUI.DisplayInformation(_stringBuilder.ToString());

		_stringBuilder.Clear();
	}

	/// <summary>
	/// Displays information for a specific vehicle in a garage.
	/// </summary>
	/// <param name="garageKey">The garage ID.</param>
	private void GetVehicleInformation(int garageKey)
	{
		string chosenLicensePlate = string.Empty;
		bool validLicensePlate = false;

		do
		{
			chosenLicensePlate = _consoleUI.RegisterLicensePlateInput(
				message: "Supply the license plate of the licensePlate you want information on: "
			);

			try
			{
				if (_licensePlateRegistry.IsValidLicensePlate(chosenLicensePlate))
				{
					string? vehicleInfo = _garages[garageKey].GetVehicleInformation(chosenLicensePlate);

					if (vehicleInfo == null)
						_consoleUI.ShowError("No licensePlate with that license plate in garage.");
					else
						_consoleUI.DisplayInformation(vehicleInfo);

					validLicensePlate = true;
				}
			}
			catch (Exception ex) when (
					ex is ArgumentNullException ||
					ex is ArgumentOutOfRangeException ||
					ex is ArgumentException ||
					ex is InvalidOperationException)
			{
				_consoleUI.ShowError($"License plate error: {ex.Message}");
			}

		} while (!validLicensePlate);

	}

	private void RemoveAVehicle(int garageKey)
	{
		string chosenLicensePlate = string.Empty;
		bool validLicensePlate = false;

		do
		{
			chosenLicensePlate = _consoleUI.RegisterLicensePlateInput(
					message: "Supply the license plate of the licensePlate you want to remove: "
			);

			try
			{
				if (_licensePlateRegistry.IsValidLicensePlate(chosenLicensePlate))
				{
					IVehicle? removedVehicle = _garages[garageKey].RemoveVehicle(chosenLicensePlate);

					if (removedVehicle == null)
						_consoleUI.ShowError("No licensePlate with that license in garage.");
					else
						_consoleUI.ShowFeedbackMessage(
							$"Vehicle with license plate {removedVehicle.LicensePlate} has left the garage.");

					validLicensePlate = true;
				}
			}
			catch (Exception ex) when (
					ex is ArgumentNullException ||
					ex is ArgumentOutOfRangeException ||
					ex is ArgumentException ||
					ex is InvalidOperationException)
			{
				_consoleUI.ShowError($"License plate error: {ex.Message}");
			}

		} while (!validLicensePlate);
	}

	/// <summary>
	/// Displays vehicle creation menu, creates the vehicle, and parks it.
	/// </summary>
	/// <param name="garageKey">Garage to park the vehicle in.</param>
	private void WhatVehicleToCreateMenu(int garageKey)
	{
		int selectedVehicle = _consoleUI.RegisterWhatVehicleToCreateMenu();

		_consoleUI.ShowFeedbackMessage($"Lets build a {(VehicleType)selectedVehicle}!");

		ParkAVehicleInGarage(
			garageKey,
			_buildVehicle.GetVehicle(selectedVehicle)
		);
	}

	private void ParkAVehicleInGarage(int garageKey, IVehicle vehicle) =>
		_garages[garageKey].AddVehicle(vehicle);

	private void AddMixedSetOfVehiclesToGarage(int garageKey)
	{
		try
		{
			_garages[garageKey].Add40VehiclesToCollection();
			_consoleUI.ShowFeedbackMessage("40 vehicles added to the garage.");
		}
		catch (InvalidOperationException e)
		{
			// ToDo : log exception
			_consoleUI.ShowError($"Test vehicles can only be added once per garage!");
		}
	}

	/// <summary>
	/// Prompts the user to select an existing garage.
	/// </summary>
	/// <returns>The ID of the selected garage.</returns>
	private int SelectGarage()
	{
		bool isValid = false;
		int chosenGarage = -1;

		var garageNumbers = _garages.Keys.ToList();

		do
		{
			chosenGarage = _consoleUI.SelectGarage(garageNumbers);

			if (_garages.ContainsKey(chosenGarage)) isValid = true;
			else _consoleUI.ShowError("No garage with that ID, chose another!");

		} while (!isValid);

		_consoleUI.ShowFeedbackMessage(message: $"Garage {chosenGarage} selected.");

		return chosenGarage;
	}
}
