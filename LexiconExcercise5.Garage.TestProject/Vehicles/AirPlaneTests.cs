using LexiconExercise5_Garage.Vehicles;
using LexiconExercise5_Garage.Vehicles.FlyingVehicles;

namespace LexiconExcercise5.Garage.TestProject.Vehicles;

public class AirPlaneTests
{
	// VALID base class attributes
	private const string _c_LicensePlate = "BBK159";
	private const VehicleColor _c_Color = VehicleColor.Yellow;
	private const uint _c_Wheel = 6;
	
	// VALID engine values. Includes edge cases 0 and 10 and a couple of middle cases. 
	private const uint _c_EdgeCaseMin0Engines = 0;
	private const uint _c_2Engines = 2;
	private const uint _c_6Engines = 6;
	private const uint _c_EdgeCaseMax10Engines = 10;

	// INVALID engine value.
	private const uint _c_ExcessiveEngines11 = 11;


	/// <summary>
	/// Tests that the NumberOfEngines property accepts valid engine counts (including edge cases) 
	/// when set via the constructor, and that the value is correctly assigned.
	/// </summary>
	[Theory]
	[InlineData(_c_EdgeCaseMin0Engines)]
	[InlineData(_c_2Engines)]
	[InlineData(_c_6Engines)]
	[InlineData(_c_EdgeCaseMax10Engines)]
	public void NumberOfEngines_SetViaConstructor_ValidValues_ShouldPass(uint engines)
	{
		//Arrange & Act
		IAirPlain airPlain = new AirPlain(_c_LicensePlate, _c_Color, _c_Wheel,  engines);
		//Assert
		Assert.Equal(engines, airPlain.NumberOfEngines);
	}

	[Fact]
	public void NumberOfEngines_SetViaConstructor_InValidValues_ShouldThrowOutOfRangeException()
	{
		// Act & Assert
		Assert.Throws<ArgumentOutOfRangeException>(() => 
			new AirPlain(_c_LicensePlate, _c_Color, _c_Wheel, _c_ExcessiveEngines11)
		);
	}
}
