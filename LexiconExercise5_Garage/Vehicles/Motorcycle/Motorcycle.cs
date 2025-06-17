
namespace LexiconExercise5_Garage.Vehicles.Motorcycle;

/// <summary>
/// Represents a motorcycle vehicle.
/// Inherits from <see cref="VehicleBase"/> and implements <see cref="IMotorcycle"/>.
/// </summary>
public class Motorcycle : VehicleBase, IMotorcycle
{
	///<inheritdoc/>
	public bool HasSidecar { get; }

	/// <summary>
	/// Initializes a new instance of the <see cref="Car"/> class
	/// with the specified license plate, color, number of wheels, and sidecar status.
	/// </summary>
	/// <param name="licensePlateValidator">Validates license plates entries</param>
	/// <param name="licensePlate">The license plate of the motorcycle.</param>
	/// <param name="color">The color of the motorcycle.</param>
	/// <param name="wheels">The number of wheels the motorcycle has.</param>
	/// <param name="hasSidecar">Indicates whether the motorcycle has a sidecar.</param>
	public Motorcycle(
		Func<string, bool> licensePlateValidator,
		string licensePlate,
		VehicleColor color,
		uint wheels,
		bool hasSidecar)
		: base(licensePlateValidator, licensePlate, color, wheels)
	{
		HasSidecar = hasSidecar;
	}

	/// <inheritdoc/>
	public override string ToString()
	{
		return $"License plate: {LicensePlate}\nColor: {Color}\nNr of wheels: {Wheels}\n Has sidecar: {HasSidecar}\n)";
	}
}
