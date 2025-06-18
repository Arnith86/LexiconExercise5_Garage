namespace LexiconExercise5_Garage.Vehicles.Buss;

/// <summary>
/// Defines a contract for Buss-specific properties.
/// </summary>
public interface IBus
{
	/// <summary>
	/// Gets the number of doors the bus has.
	/// </summary>
	uint NrOfDoors { get; }
}