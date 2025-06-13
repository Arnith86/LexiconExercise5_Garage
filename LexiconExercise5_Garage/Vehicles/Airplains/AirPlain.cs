using LexiconExercise5_Garage.Vehicles;

namespace LexiconExercise5_Garage.Vehicles.FlyingVehicles;

/// <summary>
/// Represents an airplane vehicle with a specified number of engines.
/// Inherits from <see cref="VehicleBase"/> and implements <see cref="IAirPlain"/>.
/// </summary>
public class AirPlain : VehicleBase, IAirPlain
{
	private uint _numberOfEngines = 0;

	/// <inheritdoc/>
	/// <exception cref="ArgumentOutOfRangeException">Thrown if number of engines is outside of acceptable range of 0 - 10.</exception>
	public uint NumberOfEngines
	{
		get => _numberOfEngines;
		set
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
	/// <param name="licensePlate">The license plate of the airplane.</param>
	/// <param name="color">The color of the airplane.</param>
	/// <param name="wheels">The number of wheels on the airplane.</param>
	/// <param name="numberOfEngines">The number of engines on the airplane (0–10).</param>
	public AirPlain(string licensePlate, VehicleColor color, uint wheels, uint numberOfEngines)
		: base(licensePlate, color, wheels)
	{
		NumberOfEngines = numberOfEngines;
	}

}

