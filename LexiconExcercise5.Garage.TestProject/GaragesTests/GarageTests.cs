using LexiconExcercise5.Garage.TestProject.Vehicles.Mocks;
using LexiconExercise5_Garage.Garages;

namespace LexiconExcercise5.Garage.TestProject.GaragesTests;

/// <summary>
/// Contains unit tests for the <see cref="Garage{T}"/> class
/// </summary>
public class GarageTests : IDisposable
{
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
	public void Vehicles_CapacityArraySize_SetViaConstructor_ValidValues_ShouldPass_ExpectedSize4(int size)
	{
		//Arrange & Act
		int expected = 4;
		Garage<MockVehicle> mockVehicles = new Garage<MockVehicle>(size);

		//Assert
		Assert.Equal(expected, mockVehicles.Capacity);
		//Dispose
	}


	/// <summary>
	/// Verifies that a size just over a power-of-two boundary sets capacity to 8.
	/// </summary>
	[Fact]
	public void Vehicles_CapacityArraySize_SetViaConstructor_ValidValues_ShouldPass_ExpectedSize8()
	{
		//Arrange & Act
		int expected = 8;
		Garage<MockVehicle> mockVehicles = new Garage<MockVehicle>(_c_ArraySizeAfterLimit5);

		//Assert
		Assert.Equal(expected, mockVehicles.Capacity);
		//Dispose
	}

	/// <summary>
	/// Verifies that the maximum allowed size results in a capacity of 524288.
	/// </summary>
	[Fact]
	public void Vehicles_CapacityArraySize_SetViaConstructor_ValidValues_ShouldPass_ExpectedSize524288()
	{
		//Arrange & Act
		int expected = 524288;
		Garage<MockVehicle> mockVehicles = new Garage<MockVehicle>(_c_ArraySizeEdgeCaseHighestValue);

		//Assert
		Assert.Equal(expected, mockVehicles.Capacity);
		//Dispose
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

		//Dispose
	}

	/// <summary>
	/// Clean-up method called after each test. Currently empty.
	/// Use this to release shared resources if needed.
	/// </summary>
	public void Dispose()
	{
		// Cleanup logic here if needed
	}
}
