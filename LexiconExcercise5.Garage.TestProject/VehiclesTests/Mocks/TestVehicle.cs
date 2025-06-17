using LexiconExercise5_Garage.Vehicles;

namespace LexiconExcercise5.Garage.TestProject.Vehicles.Mocks;

/// <summary>
/// Concrete class, allows testing of the abstract class VehicleBase 
/// </summary>
public class TestVehicle : VehicleBase
{
	public TestVehicle(
		Func<string, bool> licensePlateValidator,
		string licensePlate,
		VehicleColor color,
		uint wheels)
		: base(licensePlateValidator, licensePlate, color, wheels)
	{ }
}