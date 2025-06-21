using LexiconExercise5_Garage.Vehicles.VehicleBase;

namespace LexiconExercise5_Garage.Vehicles.AirPlains;

/// <summary>
/// Represents an airplane vehicle with a specified number of engines.
/// Inherits from <see cref="VehicleBase"/> and implements <see cref="IAirPlain"/>.
/// </summary>
public class AirPlain : Vehicle, IAirPlain
{
	private uint _numberOfEngines = 0;

	/// <inheritdoc/>
	/// <exception cref="ArgumentOutOfRangeException">Thrown if number of engines is outside of acceptable range of 0 - 10.</exception>
	public uint NumberOfEngines
	{
		get => _numberOfEngines;
		private set
		{
			if (IsValidNumberOfEngines(value))
				_numberOfEngines = value;
		}
	}

	private bool IsValidNumberOfEngines(uint value)
	{
		if (value < 0 || value > 10)
			throw new ArgumentOutOfRangeException(nameof(value), "Number of engines should be within the range of 0 - 10");
		return true;
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="AirPlain"/> class 
	/// with the specified license plate, color, number of wheels, and number of engines.
	/// </summary>
	/// <param name="licensePlateValidator">Validates license plates entries</param>
	/// <param name="licensePlate">The license plate of the airplane.</param>
	/// <param name="color">The color of the airplane.</param>
	/// <param name="wheels">The number of wheels on the airplane.</param>
	/// <param name="numberOfEngines">The number of engines on the airplane (0–10).</param>
	public AirPlain(
		Func<string, bool> licensePlateValidator, 
		string licensePlate, 
		VehicleColor color, 
		uint wheels, 
		uint numberOfEngines)
		: base(licensePlateValidator, licensePlate, color, numberOfEngines)
	{
		NumberOfEngines = numberOfEngines;
	}

	public override string ToString()
	{
		return $"\nVehicle Type: {this.GetType} \nLicense plate: {LicensePlate}\nColor: {Color}\nNr of wheels: {Wheels}\n Number of engines: {NumberOfEngines}\n)";
	}
}

