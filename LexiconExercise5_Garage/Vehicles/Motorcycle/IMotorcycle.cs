using System.Drawing;

namespace LexiconExercise5_Garage.Vehicles.Motorcycle;

/// <summary>
/// Defines a contract for motorcycle-specific properties.
/// </summary>
public interface IMotorcycle
{
	/// <summary>
	/// Gets a value indicating whether the motorcycle has a sidecar attached.
	/// </summary>
	bool HasSidecar { get; }
}