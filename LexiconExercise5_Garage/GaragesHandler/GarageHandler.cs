
using LexiconExercise5_Garage.Garages;
using LexiconExercise5_Garage.Vehicles;
using LexiconExercise5_GarageAssignment.ConsoleRelated;

namespace LexiconExercise5_Garage.GaragesHandler;

public class GarageHandler
{
	private readonly IConsoleUI _consoleUI;
	private Dictionary<int, IGarage<VehicleBase>> _garages;
	
	public GarageHandler(IConsoleUI consoleUI)
	{
		_consoleUI = consoleUI;
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
				_garages.Add(newKey, new Garage<VehicleBase>(size));
				
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
