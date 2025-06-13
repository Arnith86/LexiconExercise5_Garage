using LexiconExercise5_Garage.Vehicles;

namespace LexiconExercise5_Garage.Vehicles.FlyingVehicles;

/// <summary>
/// Represents a contract for an airplane vehicle with basic properties.
/// Contains a property for the number of engines.
/// </summary>
public interface IAirPlain 
{
	/// <summary>
	/// Gets or sets the number of engines on the airplane.
	/// Must be within the range of 0 to 10.
	/// </summary>
	uint NumberOfEngines { get; set; }
}