
using LexiconExercise5_Garage.Garages;
using LexiconExercise5_Garage.Garages.GarageFactory;
using LexiconExercise5_Garage.Vehicles;
using LexiconExercise5_GarageAssignment.ConsoleRelated;

namespace LexiconExercise5_Garage.GaragesHandler;

public class GarageHandler
{
	private readonly IConsoleUI _consoleUI;
	private readonly IGarageCreator<VehicleBase> _garageCreator;
	private Dictionary<int, IGarage<VehicleBase>> _garages;
	
	public GarageHandler(IConsoleUI consoleUI, IGarageCreator<VehicleBase> garageCreator)
	{
		_consoleUI = consoleUI;
		_garageCreator = garageCreator;
		_garages = new Dictionary<int, IGarage<VehicleBase>>();
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
					GarageHandlingMenuSelection(garageKey: SelectGarage());
					break;
				case 0:
					exitProgram = true;
					break;
				default:
					break;
			}

		} while (!exitProgram);
	}

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
					//WhatVehicleToCreateMenu(garageKey);
					//ParkAVehicleInGarage(garageKey);
					break;
				case 0:
					exitGarageHandlingMenu = true;
					break;
				default:
					break;
			}

		} while (!exitGarageHandlingMenu);
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
			_consoleUI.ShowError($"{e}, test vehicles can only be added once per garage!");
		}
	}

	

	
	//"2: Park a vehicle in garage.\n" +
	//"3: Remove a vehicle, from garage.\n" +
	//"4: Get vehicle information of a single vehicle, currently parked in garage.\n" +
	//"5: Get vehicle information of all vehicles, currently parked in garage.\n" +
	//"6: Get filtered information of all vehicles.\n" +
	//"0: Exit garage handling menu.\n\n"


	private int SelectGarage()
	{
		bool isValid = false;
		int chosenGarage = 0;

		var garageNumbers = _garages.Keys.ToList();

		if (_garages.Count > 0)
		{
			do
			{
				chosenGarage = _consoleUI.SelectGarage(garageNumbers);

				if (_garages.ContainsKey(chosenGarage)) isValid = true;
				
			} while (!isValid);
			
			_consoleUI.ShowFeedbackMessage(message: $"Garage {chosenGarage} selected.");
		}
		else
		{
			_consoleUI.ShowError("There are no created garages yet!");
		}

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
