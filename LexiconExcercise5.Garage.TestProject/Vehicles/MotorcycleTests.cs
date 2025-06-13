
using LexiconExercise5_Garage.Vehicles;
using LexiconExercise5_Garage.Vehicles.Motorcycle;

namespace LexiconExcercise5.Garage.TestProject.Vehicles;

public class MotorcycleTests
{
	// VALID license plate examples matching expected format (3 letters followed by 3 digits)
	private const string _c_LicensePlate = "BBK159";
	
	// VALID vehicle colors
	private const VehicleColor _c_Color = VehicleColor.Yellow;

	// VALID numbers of wheels 
	private const uint _c_Wheel = 6;
	
	// VALID hasSideCar values
	private const bool  _c_True = true;
	private const bool  _c_False = false;
	
	/// <summary>
	/// Tests that the NumberOfEngines property accepts valid engine counts (including edge cases) 
	/// when set via the constructor, and that the value is correctly assigned.
	/// </summary>
	[Theory]
	[InlineData(_c_True)]
	[InlineData(_c_False)]
	public void HasSideCar_SetViaConstructor_ValidValues_ShouldPass(bool hasSideCar)
	{
		//Arrange & Act
		IMotorcycle motorcycle = new Motorcycle(_c_LicensePlate, _c_Color, _c_Wheel,  hasSideCar);
		//Assert
		Assert.Equal(hasSideCar, motorcycle.HasSidecar);
	}
}
