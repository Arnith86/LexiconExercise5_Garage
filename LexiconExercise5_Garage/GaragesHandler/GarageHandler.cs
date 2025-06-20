
using LexiconExercise5_Garage.Garages;
using LexiconExercise5_Garage.Garages.GarageFactory;
using LexiconExercise5_Garage.Vehicles;
using LexiconExercise5_Garage.Vehicles.VehicleBase;
using LexiconExercise5_Garage.Vehicles.VehicleFactories;
using LexiconExercise5_GarageAssignment.ConsoleRelated;

namespace LexiconExercise5_Garage.GaragesHandler;

public class GarageHandler
{
	private readonly IConsoleUI _consoleUI;
	private readonly IGarageCreator<IVehicle> _garageCreator;
	private readonly BuildVehicle _buildVehicle;

	private Dictionary<int, IGarage<IVehicle>> _garages;

	public GarageHandler(
		IConsoleUI consoleUI,
		IGarageCreator<IVehicle> garageCreator,
		BuildVehicle buildVehicle)
	{
		_consoleUI = consoleUI;
		_garageCreator = garageCreator;
		_buildVehicle = buildVehicle;
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
	  			"1: Park a mixed set of vehicle in garage. (This is your fast forward vehicle adding for testing\n" + // only works ONCE per execution with mixed vehicle garages for now 
				"2: Park a vehicle in garage.\n" +
				"3: Remove a vehicle, from garage.\n" +
				"4: Get vehicle information of a single vehicle, currently parked in garage.\n" +
				"5: Get vehicle information of all vehicles, currently parked in garage.\n" +
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
				case 0:
					exitGarageHandlingMenu = true;
					break;
				default:
					break;
			}

		} while (!exitGarageHandlingMenu);
	}

	//"3: Remove a vehicle, from garage.\n" +
	//"4: Get vehicle information of a single vehicle, currently parked in garage.\n" +
	//"5: Get vehicle information of all vehicles, currently parked in garage.\n" +
	//"6: Get filtered information of all vehicles.\n" +
	//"0: Exit garage handling menu.\n\n"

	private void WhatVehicleToCreateMenu(int garageKey)
	{
		int selectedVehicle = _consoleUI.RegisterWhatVehicleToCreateMenu();

		_consoleUI.ShowFeedbackMessage($"Lets build a {(VehicleType)selectedVehicle}!");

		ParkAVehicleInGarage(
			garageKey,
			_buildVehicle.GetVehicle(selectedVehicle)
		);
	}

	private void ParkAVehicleInGarage(int garageKey, IVehicle vehicle)
	{
		_garages[garageKey].AddVehicle(vehicle);
	}

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
