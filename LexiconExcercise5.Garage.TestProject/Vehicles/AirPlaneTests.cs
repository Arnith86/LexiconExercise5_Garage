using LexiconExercise5_Garage.Vehicles;

namespace LexiconExcercise5.Garage.TestProject.Vehicles;

public class AirPlaneTests
{
	// VALID license plate examples matching expected format (3 letters followed by 3 digits)
	private const string _c_LicensePlate = "BBK159";
	
	// VALID vehicle colors
	private const VehicleColor _c_Color = VehicleColor.Yellow;

	// VALID numbers of wheels 
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


	public class AirPlain : VehicleBase, IAirPlain
	{
		private uint _numberOfEngines = 0;

		public uint NumberOfEngines
		{
			get => _numberOfEngines;
			set
			{
				if (IsValidNumberOfEngines(value))
					_numberOfEngines = value;
			}
		}

		private bool IsValidNumberOfEngines(uint value)
		{
			if (value < 0 || value > 10)
				throw new ArgumentOutOfRangeException(nameof(value), "Number of engines should be within the range of 0 - 10");
			return true;
		}

		public AirPlain(string licensePlate, VehicleColor color, uint wheels, uint numberOfEngines)
			: base(licensePlate, color, wheels)
		{
			NumberOfEngines = numberOfEngines;
		}

	}
}
