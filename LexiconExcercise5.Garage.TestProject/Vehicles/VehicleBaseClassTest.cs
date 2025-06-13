using LexiconExercise5_Garage.Vehicles;

namespace LexiconExcercise5.Garage.TestProject.Vehicles;

public class VehicleBaseClassTest
{
	// VALID license plate examples matching expected format (3 letters followed by 3 digits)
	private const string _c_LicensePlateCaps = "BBK159";
	private const string _c_LicensePlateLow = "azm129";
	private const string _c_LicensePlateMix = "uRE832";

	// INVALID license plate examples to cover various failure cases:
	// - Wrong order (digits before letters)
	// - Too long or too short strings
	// - Mixed character placement
	// - Null or empty strings
	private const string _c_LicensePlateWrongOrder = "159KBB";
	private const string _c_LicensePlateToLong = "azmm129";
	private const string _c_LicensePlateToShort = "uRE32";
	private const string _c_LicensePlateMixedPlacements = "u3E3b2";
	private const string _c_LicensePlateIsNull = null;
	private const string _c_LicensePlateIsEmpty = "";

	// VALID vehicle colors for testing property assignments
	private const VehicleColor _c_GREEN = VehicleColor.Green;
	private const VehicleColor _c_RED = VehicleColor.Red;
	private const VehicleColor _c_BLUE = VehicleColor.Blue;
	private const VehicleColor _c_YELLOW = VehicleColor.Yellow;


	// VALID numbers of _wheels for testing different vehicle configurations including edge cases
	// Edge cases: 0 & 56 _wheels
	private const uint _c_EdgeCaseMin0Wheel = 0;
	private const uint _c_2Wheel = 2;
	private const uint _c_4Wheel = 4;
	private const uint _c_6Wheel = 6;
	private const uint _c_12Wheel = 12;
	private const uint _c_EdgeCaseMax56Wheel = 56;

	// INVALID number of _wheels
	private const uint _c_ExcessiveWheel57 = 57;


	/// **LicensePlate Tests**

	/// <summary>
	/// Tests that valid license plate inputs are accepted and correctly assigned.
	/// </summary>
	[Theory]
	[InlineData(_c_LicensePlateCaps)]
	[InlineData(_c_LicensePlateLow)]
	[InlineData(_c_LicensePlateMix)]
	public void LicensePlate_SetViaConstructor_ValidValues_ShouldPass(string licensePlate)
	{
		// Arrange & Act 
		var testVehicle = new TestVehicle(licensePlate, _c_GREEN, _c_4Wheel);
		// Assert
		Assert.Equal(licensePlate, testVehicle.LicensePlate);
	}


	/// <summary>
	/// Tests that null or empty license plates throw ArgumentNullException.
	/// </summary>
	[Theory]
	[InlineData(_c_LicensePlateIsNull)]
	[InlineData(_c_LicensePlateIsEmpty)]
	public void LicensePlate_SetViaConstructor_InValidValues_ShouldThrowArgumentNullException(string licensePlate)
	{
		// Act & Assert
		Assert.Throws<ArgumentNullException>(() =>
			new TestVehicle(licensePlate, _c_GREEN, _c_4Wheel));
	}

	/// <summary>
	/// Tests that license plates with invalid length throw ArgumentOutOfRangeException.
	/// </summary>
	[Theory]
	[InlineData(_c_LicensePlateToLong)]
	[InlineData(_c_LicensePlateToShort)]
	public void LicensePlate_SetViaConstructor_InValidValues_ShouldThrowArgumentOutOfRangeException(string licensePlate)
	{
		// Act & Assert
		Assert.Throws<ArgumentOutOfRangeException>(() =>
			new TestVehicle(licensePlate, _c_GREEN, _c_4Wheel));
	}

	/// <summary>
	/// Tests that license plates with wrong order or mixed placements throw ArgumentException.
	/// </summary>
	[Theory]
	[InlineData(_c_LicensePlateWrongOrder)]
	[InlineData(_c_LicensePlateMixedPlacements)]
	public void LicensePlate_SetViaConstructor_InValidValues_ShouldThrowArgumentException(string licensePlate)
	{
		// Act & Assert
		Assert.Throws<ArgumentException>(() =>
			new TestVehicle(licensePlate, _c_GREEN, _c_4Wheel));
	}


	/// <summary>
	/// Tests that valid vehicle colors are correctly assigned via the constructor.
	/// </summary>
	[Theory]
	[InlineData(_c_GREEN)]
	[InlineData(_c_RED)]
	[InlineData(_c_BLUE)]
	[InlineData(_c_YELLOW)]
	public void Color_SetViaConstructor_ValidValues_ShouldPass(VehicleColor color)
	{
		// Arrange & Act 
		var testVehicle = new TestVehicle(_c_LicensePlateCaps, color, _c_4Wheel);
		// Assert
		Assert.Equal(color, testVehicle.Color);
	}

	/// <summary>
	/// Tests that valid wheel counts are correctly assigned via the constructor.
	/// Includes a variety of typical and edge cases (0, 2, 3, 4, 6, 12 _wheels).
	/// </summary>
	[Theory]
	[InlineData(_c_EdgeCaseMin0Wheel)]
	[InlineData(_c_2Wheel)]
	[InlineData(_c_4Wheel)]
	[InlineData(_c_6Wheel)]
	[InlineData(_c_12Wheel)]
	[InlineData(_c_EdgeCaseMax56Wheel)]
	public void Wheels_SetViaConstructor_ValidValues_ShouldPass(uint wheels)
	{
		// Arrange & Act 
		var testVehicle = new TestVehicle(_c_LicensePlateCaps, _c_RED, wheels);
		// Assert
		Assert.Equal(wheels, testVehicle.Wheels);
	}

	/// <summary>
	/// Tests that INVALID wheel counts assigned via the constructor correctly throws ArgumentOutOfRangeException.
	/// </summary>
	[Theory]
	[InlineData(_c_ExcessiveWheel57)]
	public void Wheels_SetViaConstructor_InValidValues_ShouldThrowArgumentOutOfRangeException(uint wheels)
	{
		// Act & Assert
		Assert.Throws<ArgumentOutOfRangeException>(() =>
			new TestVehicle(_c_LicensePlateCaps, _c_RED, wheels));
	}

	// Allows testing of the abstract class VehicleBase 
	public class TestVehicle : VehicleBase
	{
		public TestVehicle(string licensePlate, VehicleColor color, uint wheels)
			: base(licensePlate, color, wheels)
		{
		}
	}
}