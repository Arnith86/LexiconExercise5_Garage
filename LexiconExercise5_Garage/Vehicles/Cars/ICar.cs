namespace LexiconExercise5_Garage.Vehicles.Cars;

/// <summary>
/// Defines a contract for Car-specific properties.
/// </summary>
public interface ICar
{
	/// <summary>
	/// Gets or sets the number of seats in the car.
	/// Must be within the range of 1 to 7.
	/// </summary>
	uint NrOfSeats { get; }
}