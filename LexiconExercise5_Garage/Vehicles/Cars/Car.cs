
namespace LexiconExercise5_Garage.Vehicles.Cars;

/// <summary>
/// Represents a Car vehicle.
/// Inherits from <see cref="VehicleBase"/> and implements <see cref="ICar"/>.
/// </summary>
public class Car : VehicleBase, ICar
{
	private uint _nrOfSeats = 0;

	///<inheritdoc/>
	public uint NrOfSeats 
	{ 
		get => _nrOfSeats; 
		private set 
		{
			if (IsValidNrOfSeats(value))
				_nrOfSeats = value;
		}
	}


	/// <summary>
	/// Initializes a new instance of the <see cref="Car"/> class
	/// with the specified license plate, color, number of wheels, and sidecar status.
	/// </summary>
	/// <param name="licensePlateValidator">Validates license plates entries</param>
	/// <param name="licensePlate">The license plate of the <see cref="Car"/>.</param>
	/// <param name="color">The color of the <see cref="Car"/>.</param>
	/// <param name="wheels">The number of wheels the <see cref="Car"/> has.</param>
	/// <param name="nrOfSeats">Indicates how many seats the <see cref="Car"/> has.</param>
	public Car(
		Func<string, bool> licensePlateValidator, 
		string licensePlate, 
		VehicleColor color, 
		uint wheels, 
		uint nrOfSeats)
		: base(licensePlateValidator, licensePlate, color, wheels)
	{
		NrOfSeats = nrOfSeats;
	}
	
	private bool IsValidNrOfSeats(uint value)
	{
		if (value < 0 || value > 7)
			throw new ArgumentOutOfRangeException(nameof(value), "Must have a value within the range of 1 - 7.");
		
		return true;
	}

	public override string ToString()
	{
		return $"License plate: {LicensePlate}\nColor: {Color}\nNr of wheels: {Wheels}\nSeats: {NrOfSeats}\n)";
	}
}
