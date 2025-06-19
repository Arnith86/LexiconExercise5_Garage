using LexiconExercise5_Garage.Vehicles;
using LexiconExercise5_Garage.Vehicles.VehicleBase;

namespace LexiconExcercise5.Garage.TestProject.Vehicles.Mocks;

/// <summary>
/// Concrete class, allows testing of the abstract class VehicleBase 
/// </summary>
public class TestVehicle : Vehicle
{
	public TestVehicle(
		Func<string, bool> licensePlateValidator,
		string licensePlate,
		VehicleColor color,
		uint wheels)
		: base(licensePlateValidator, licensePlate, color, wheels)
	{ }

	public override string ToString()
	{
		return $"License plate: {LicensePlate}\nColor: {Color}\nNr of wheels: {Wheels}\n)";
	}
}