namespace LexiconExercise5_Garage.Vehicles.Bus;

/// <summary>
/// Defines a contract for Bus-specific properties.
/// </summary>
public interface IBus
{
	/// <summary>
	/// Gets the number of doors the bus has.
	/// </summary>
	uint NrOfDoors { get; }
}