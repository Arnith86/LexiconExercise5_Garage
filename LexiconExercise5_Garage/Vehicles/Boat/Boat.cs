namespace LexiconExercise5_Garage.Vehicles.Boat;

/// <summary>
/// Represents a boat vehicle.
/// Inherits from <see cref="VehicleBase"/> and implements <see cref="IBoat"/>.
/// </summary>
public class Boat : VehicleBase, IBoat
{
	/// <summary>
	/// Initializes a new instance of the <see cref="Boat"/> class with specified attributes.
	/// </summary>
	/// <param name="licensePlate">The license plate of the boat.</param>
	/// <param name="color">The color of the boat.</param>
	/// <param name="wheels">The number of wheels (typically 0 for boats).</param>
	/// <param name="fuelType">The type of fuel the boat uses.</param>
	public Boat(string licensePlate, VehicleColor color, uint wheels, FuelType fuelType)
		: base(licensePlate, color, wheels)
	{
		FuelType = fuelType;
	}

	/// <inheritdoc/>
	public FuelType FuelType { get; }
}
