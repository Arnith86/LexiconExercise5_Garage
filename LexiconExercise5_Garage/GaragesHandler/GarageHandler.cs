

using LexiconExercise5_Garage.Garages;
using LexiconExercise5_Garage.Vehicles;
using LexiconExercise5_GarageAssignment.ConsoleRelated;

namespace LexiconExercise5_Garage.GaragesHandler;

public class GarageHandler
{
	private readonly IConsoleUI _consoleUI;
	private Dictionary<int, IGarage<VehicleBase>> _garages;
	private IGarage<VehicleBase> _currentGarage;

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
					SelectGarage();
					break;
				case 0:
					return;
				default:
					break;
			}

		} while (!exitProgram);
	}

	private void SelectGarage()
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

			_currentGarage = _garages[chosenGarage];
			
			_consoleUI.ShowFeedbackMessage(message: $"Garage {chosenGarage} selected.");
		}
		else
		{
			_consoleUI.ShowError("There are no created garages yet!");
		}
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
