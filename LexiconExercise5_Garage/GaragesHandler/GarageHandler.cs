
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

public class GarageHandler
{
	private readonly IConsoleUI _consoleUI;
	private readonly IGarageCreator<IVehicle> _garageCreator;
	private readonly BuildVehicle _buildVehicle;
	private readonly ILicensePlateRegistry _licensePlateRegistry;
	private readonly VehiclesFilterFunctions _vehiclesFilterFunctions;
	private Dictionary<int, IGarage<IVehicle>> _garages;
	private readonly StringBuilder _stringBuilder = new StringBuilder();

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

	private bool AnyGaragesExist()
	{
		if (_garages.Count > 0) return true;

		_consoleUI.ShowError("There are no created garages yet!");

		return false;
	}

	/*
	  			"1: Park a mixed set of licensePlate in garage. (This is your fast forward licensePlate adding for testing\n" + // only works ONCE per execution with mixed licensePlate garages for now 
				"2: Park a licensePlate in garage.\n" +
				"3: Remove a licensePlate, from garage.\n" +
				"4: Get licensePlate information of a single licensePlate, currently parked in garage.\n" +
				"5: Get licensePlate information of all vehicles, currently parked in garage.\n" +
				"6: Get filtered information of all vehicles.\n" +
				"0: Exit garage handling menu.\n\n"
	 */
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
					GetVehicleInformation(garageKey, singleVehicle: true);
					break;
				case 5:
					ListAllVehicles(garageKey);
					break;
				case 6:
					ListHowManyOfEachType(garageKey);
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

	private void FilterBasedOnProperties(int garageKey)
	{
		// Extract to own class 
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
						predicates.Add(_vehiclesFilterFunctions.VehicleTypePredicate(chosenType)); 
					}

					break;
				case 2:

					if (chosenOptions[1] == true) OnlyOnce();
					else
					{
						chosenOptions[1] = true;
						VehicleColor chosenColor = (VehicleColor)_consoleUI.WhichFilterPropertyFromEnum<VehicleColor>(message: "Which color: ");
						predicates.Add(_vehiclesFilterFunctions.ByColorPredicate(chosenColor));
					}

					break;
				case 3:

					if (chosenOptions[2] == true) OnlyOnce();
					else
					{
						chosenOptions[2] = true;
						CompareOptions chosenOption = (CompareOptions)_consoleUI.WhichFilterPropertyFromEnum<CompareOptions>(message: "Which Compare option do you want: ");
						int chosenValue = (int)_consoleUI.RegisterNumericUintInput(message: "Against what value: ", rangeMin: 0, rangeMax: 58);
					
						predicates.Add(
							_vehiclesFilterFunctions.ByWheelCountPredicate(
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

		if (menuOption == 4)
		{
			var filtrableList = _garages[garageKey].PerformedLinqQuery(
				vehicles => vehicles.ToList()
			);

			IEnumerable<IVehicle> result = ApplyFilters(filtrableList, predicates);


			_consoleUI.DisplayFilteredInformation(result);
		}
	}

	private IEnumerable<IVehicle> ApplyFilters(
		IEnumerable<IVehicle> filtrableList,
		List<Func<IVehicle, bool>> predicates)
	{
		IEnumerable<IVehicle> result = filtrableList;

		foreach (var predicate in predicates)
			result = result.Where(predicate);

		return result;
	}

	private void ListHowManyOfEachType(int garageKey)
	{
		var vehicleTypeCounter = _garages[garageKey].PerformedLinqQuery( vehicles =>
			vehicles
				.GroupBy(vehicle => vehicle.GetType().Name)
				.Select(group => $"{group.Key}: {group.Count()}")
		);

		foreach (var item in vehicleTypeCounter)
			_stringBuilder.Append($"{item}\n");

		_consoleUI.DisplayInformation( _stringBuilder.ToString());

		_stringBuilder.Clear();
	}


	private void ListAllVehicles(int garageKey)
	{
		var vehicleLicensePlates = _garages[garageKey].PerformedLinqQuery( vehicles => 
			vehicles 
				.Where(vehicle => vehicle != null)
				.Select(vehicle => vehicle.LicensePlate)
		);

		foreach (var licensePlate in vehicleLicensePlates)
			_stringBuilder.Append($"{licensePlate}\n");
			
		_consoleUI.DisplayInformation(_stringBuilder.ToString());

		_stringBuilder.Clear();
	}

	private void GetVehicleInformation(int garageKey, bool singleVehicle)
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

	/// <summary>
	/// Initiates <see cref="Garages"/> creation.
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

}
