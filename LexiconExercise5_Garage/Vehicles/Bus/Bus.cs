
namespace LexiconExercise5_Garage.Vehicles.Bus;

public class Bus : VehicleBase, IBus
{
	private uint _nrOfDoors = 0;

	/// <exception cref="ArgumentNullException">Thrown if the license plate is null or whitespace.</exception>
	/// <exception cref="ArgumentOutOfRangeException">Thrown if the license plate has incorrect length or if wheels are out of range.</exception>
	/// <exception cref="ArgumentException">Thrown if the license plate has an invalid structure.</exception>
	public Bus(
		Func<string, bool> licensePlateValidator, 
		string licensePlate, 
		VehicleColor color, 
		uint wheels, 
		uint nrOfDoors)
		: base (licensePlateValidator, licensePlate, color, wheels)
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

	public override string ToString()
	{
		return $"License plate: {LicensePlate}\nColor: {Color}\nNr of wheels: {Wheels}\n Doors: {NrOfDoors}\n)";
	}
}
