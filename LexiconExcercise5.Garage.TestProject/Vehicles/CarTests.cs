using LexiconExercise5_Garage.Vehicles;
using LexiconExercise5_Garage.Vehicles.Cars;

namespace LexiconExcercise5.Garage.TestProject.Vehicles;

public class CarTests
{
	// VALID base class attributes
	private const string _c_LicensePlate = "BBK159";
	private const VehicleColor _c_Color = VehicleColor.Blue;
	private const uint _c_Wheel = 4;
	
	// VALID
	private const uint _c_EdgeCaseMin0NrOfSeats = 0;
	private const uint _c_EdgeCaseMax7NrOfSeats = 7;
	private const uint _c_2NrOfSeats = 2;
	private const uint _c_5NrOfSeats = 5;

	// INVALID 
	private const uint _c_OutsideOfRange8NrOfSeats = 8;

	/// <summary>
	/// Tests that the constructor correctly sets the number of seats
	/// when provided with valid values, including edge cases.
	/// </summary>
	/// <param name="nrOfSeats">The number of seats to test.</param>
	[Theory]
	[InlineData(_c_EdgeCaseMin0NrOfSeats)]
	[InlineData(_c_EdgeCaseMax7NrOfSeats)]
	[InlineData(_c_2NrOfSeats)]
	[InlineData(_c_5NrOfSeats)]
	public void NrOfSeats_SetViaConstructor_ValidValues_ShouldPass(uint nrOfSeats)
	{
		//Assign & Act
		ICar car = new Car(_c_LicensePlate, _c_Color, _c_Wheel, nrOfSeats);
		//Assert
		Assert.Equal(nrOfSeats, car.NrOfSeats);
	}

	/// <summary>
	/// Tests that the constructor throws an <see cref="ArgumentOutOfRangeException"/>
	/// when the number of seats provided is outside the valid range.
	/// </summary>
	[Fact]
	public void NrOfSeats_SetViaConstructor_InValidValue_ShouldThrowArgumentOutOfRangeException()
	{
		// Act & Assert
		Assert.Throws<ArgumentOutOfRangeException>(() => 
			new Car(_c_LicensePlate, _c_Color, _c_Wheel, _c_OutsideOfRange8NrOfSeats));
	}
}
