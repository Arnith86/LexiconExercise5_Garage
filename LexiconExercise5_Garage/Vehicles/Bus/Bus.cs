
namespace LexiconExercise5_Garage.Vehicles.Bus;

public class Bus : VehicleBase, IBus
{
	private uint _nrOfDoors = 0;

	public Bus(string licensePlate, VehicleColor color, uint wheels, uint nrOfDoors)
		: base(licensePlate, color, wheels)
	{
		NrOfDoors = nrOfDoors;
	}

	public uint NrOfDoors
	{
		get => _nrOfDoors;
		private set
		{
			if (IsValidNrOfBussDoor(value))
				_nrOfDoors = value;
		}
	}

	private bool IsValidNrOfBussDoor(uint value)
	{
		if (value < 1 || value > 2)
			throw new ArgumentOutOfRangeException(nameof(value), "A buss must have a minimum of 1 door, and no more then 2 doors.");

		return true;
	}
}
