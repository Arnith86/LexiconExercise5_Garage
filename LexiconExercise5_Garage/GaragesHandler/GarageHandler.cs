

using LexiconExercise5_Garage.Garages;
using LexiconExercise5_Garage.Vehicles;
using LexiconExercise5_GarageAssignment.ConsoleRelated;
using System.Linq.Expressions;

namespace LexiconExercise5_Garage.GaragesHandler;

public class GarageHandler
{
	private readonly IConsoleUI _consoleUI;
	private IGarage<VehicleBase> _garage;

	public GarageHandler(IConsoleUI consoleUI)
	{
		_consoleUI = consoleUI;
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
				_garage = new Garage<VehicleBase>(size);
				isValid = true;
			}
			catch (ArgumentOutOfRangeException e)
			{
				_consoleUI.ShowError(message: e.Message);
			}

		} while (!isValid);
		

		_consoleUI.GarageCreated(size);

	}
}
