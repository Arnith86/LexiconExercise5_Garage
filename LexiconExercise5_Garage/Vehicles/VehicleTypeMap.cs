using LexiconExercise5_Garage.Vehicles.AirPlains;
using LexiconExercise5_Garage.Vehicles.Boats;
using LexiconExercise5_Garage.Vehicles.Buss;
using LexiconExercise5_Garage.Vehicles.Cars;
using LexiconExercise5_Garage.Vehicles.Motorcycles;

namespace LexiconExercise5_Garage.Vehicles;

/// <summary>
/// Immutable mapping of integer keys to vehicle types.
/// </summary>
public record VehicleTypeMap(Dictionary<int, Type> Map)
{
	public static VehicleTypeMap Default => new(new Dictionary<int, Type>
	{
		{ 1, typeof(AirPlain) },
		{ 2, typeof(Boat) },
		{ 3, typeof(Bus) },
		{ 4, typeof(Car) },
		{ 5, typeof(Motorcycle) },
        // Add more types here
    });
}