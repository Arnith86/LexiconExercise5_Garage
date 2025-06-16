using LexiconExcercise5.Garage.TestProject.Vehicles.Mocks;
using LexiconExercise5_Garage.Garages;
using LexiconExercise5_Garage.Vehicles;

namespace LexiconExcercise5.Garage.TestProject.GaragesTests;

/// <summary>
/// Contains unit tests for the <see cref="Garage{T}"/> class
/// </summary>
public class GarageTests 
{
	// VALID Vehicle base class attributes
	private MockLicensePlateRegistry _c_MockLicensePlateRegistry = new MockLicensePlateRegistry();
	private const string _c_LicensePlate = "BBK159";
	private const VehicleColor _c_Color = VehicleColor.Blue;
	private const uint _c_Wheel = 4;

	// Valid garage sizes through constructor.
	private const int _c_ArraySizeEdgeCaseMin1 = 1;
	private const int _c_ArraySize2 = 2;
	private const int _c_ArraySizeBeforeLimit4 = 4;
	private const int _c_ArraySizeAfterLimit5 = 5;
	private const int _c_ArraySizeEdgeCaseHighestValue = 524288;

	// Invalid garage sizes.
	private const int _c_ArraySizeNegative = -1;
	private const int _c_ArraySizeOverUpperLimit524289 = 524289;



	/// <summary>
	/// Verifies that various valid sizes result in a properly set internal capacity.
	/// Expected capacity: 4
	/// </summary>
	[Theory]
	[InlineData(_c_ArraySizeEdgeCaseMin1)]
	[InlineData(_c_ArraySize2)]
	[InlineData(_c_ArraySizeBeforeLimit4)]
	public void Vehicles_CapacityArraySize_SetViaConstructor_ValidValues_ShouldPass_Expected_UsedSpaces0_Capacity4(int size)
	{
		//Arrange & Act
		int expectedUsedSpaces = 0;
		int expectedCapacity = 4;
		
		Garage<MockVehicle> garage = new Garage<MockVehicle>(size);

		//Assert
		Assert.Equal(expectedCapacity, garage.Capacity);
		Assert.Equal(expectedUsedSpaces, garage.UsedSpaces);
	}


	/// <summary>
	/// Verifies that a size just over a power-of-two boundary sets capacity to 8.
	/// </summary>
	[Fact]
	public void Vehicles_CapacityArraySize_SetViaConstructor_ValidValues_ShouldPass_UsedSpaces0_Capacity8()
	{
		//Arrange & Act
		int expectedUsedSpaces = 0;
		int expectedCapacity = 8;

		Garage<MockVehicle> garage = new Garage<MockVehicle>(_c_ArraySizeAfterLimit5);

		//Assert
		Assert.Equal(expectedCapacity, garage.Capacity);
		Assert.Equal(expectedUsedSpaces, garage.UsedSpaces);
	}

	/// <summary>
	/// Verifies that the maximum allowed size results in a capacity of 524288.
	/// </summary>
	[Fact]
	public void Vehicles_CapacityArraySize_SetViaConstructor_ValidValues_ShouldPass_Expected_UsedSpaces0_Capacity524288()
	{
		//Arrange & Act
		int expectedUsedSpaces = 0;
		int expectedCapacity = 524288;

		Garage<MockVehicle> garage = new Garage<MockVehicle>(_c_ArraySizeEdgeCaseHighestValue);

		//Assert
		Assert.Equal(expectedCapacity, garage.Capacity);
		Assert.Equal(expectedUsedSpaces, garage.UsedSpaces);
	}


	/// <summary>
	/// Verifies that invalid sizes (negative or over max limit) throw ArgumentOutOfRangeException.
	/// </summary>
	[Theory]
	[InlineData(_c_ArraySizeNegative)]
	[InlineData(_c_ArraySizeOverUpperLimit524289)]
	public void Vehicles_CapacityArraySize_SetViaConstructor_InValidValues_ShouldThrowArgumentOutOfRangeException(int size)
	{
		// Act & Assert
		Assert.Throws<ArgumentOutOfRangeException>(() =>
			new Garage<MockVehicle>(size)
		);
	}

	[Fact]
	public void AddVehicle_EmptyGarage_ShouldPass_Expected_UsedSpaces2_Capacity4()
	{
		//Arrange & Act
		int expectedUsedSpaces = 2;
		int expectedCapacity = 4;

		Garage<MockVehicle> garage = new Garage<MockVehicle>(_c_ArraySizeBeforeLimit4);

		for (int i = 0; i < 2; i++)
		{
			garage.AddVehicle(
				new MockVehicle(
					licensePlateValidator => true,
					_c_LicensePlate,
					_c_Color,
					_c_Wheel
				)
			);
		}

		//Assert
		Assert.Equal(expectedCapacity, garage.Capacity);
		Assert.Equal(expectedUsedSpaces, garage.UsedSpaces);
	}

	[Fact]
	public void AddVehicle_FullGarage_ShouldThrowIndexOutOfRangeException()
	{
		//Arrange & Act
	
		Garage<MockVehicle> garage = new Garage<MockVehicle>(_c_ArraySizeEdgeCaseHighestValue);


		for (int i = 0; i < 524288; i++)
		{
			garage.AddVehicle(
				new MockVehicle(
					LicensePlateIsUnique => true,
					_c_LicensePlate,
					_c_Color,
					_c_Wheel
				)
			);
		}
		
		//Assert
		Assert.Throws<IndexOutOfRangeException>(() => 
			garage.AddVehicle(
				new MockVehicle(
				LicensePlateIsUnique => true,
				_c_LicensePlate,
				_c_Color,
				_c_Wheel)
			)
		);
	}

	///// <summary>
	///// Clean-up method called after each test. Currently empty.
	///// Use this to release shared resources if needed.
	///// </summary>
	//public void Dispose()
	//{
		
	//}
}
