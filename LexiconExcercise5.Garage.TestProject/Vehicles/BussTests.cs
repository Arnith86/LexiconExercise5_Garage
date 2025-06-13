using LexiconExercise5_Garage.Vehicles.Bus;
using LexiconExercise5_Garage.Vehicles;

namespace LexiconExcercise5.Garage.TestProject.Vehicles;

public class BussTests
{
	// VALID base class attributes
	private const string _c_LicensePlate = "abk421";
	private const VehicleColor _c_Color = VehicleColor.Red;
	private const uint _c_Wheel = 8;

	// VALID 
	private const uint _c_MinNrOfDoors1 = 1;
	private const uint _c_MaxNrOfDoors2 = 2;

	// INVALID 
	private const uint _c_OutsideOfRangeNrOfDoors3 = 3;

	[Theory]
	[InlineData(_c_MinNrOfDoors1)]
	[InlineData(_c_MaxNrOfDoors2)]
	public void NumberOfDoors_SetViaConstructor_ValidValues_ShouldPass(uint doors)
	{
		// Assign & Act
		IBus bus = new Bus(_c_LicensePlate, _c_Color, _c_Wheel, doors);
		// Assert
		Assert.Equal(doors, bus.NrOfDoors);
	}

	[Fact]
	public void NumberOfDoors_SetViaConstructor_InValidValues_ShouldThrowArgumentOutOfRangeException()
	{
		// Assert & Act
		Assert.Throws<ArgumentOutOfRangeException>(() =>
			new Bus(_c_LicensePlate, _c_Color, _c_Wheel, _c_OutsideOfRangeNrOfDoors3));
	}
}
