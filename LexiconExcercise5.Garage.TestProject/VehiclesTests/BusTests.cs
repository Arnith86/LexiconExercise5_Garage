using LexiconExcercise5.Garage.TestProject.Vehicles.Mocks;
using LexiconExercise5_Garage.Vehicles;
using LexiconExercise5_Garage.Vehicles.Buss;
using LexiconExercise5_Garage.Vehicles.LicensePlate.Registry;

namespace LexiconExcercise5.Garage.TestProject.Vehicles;

/// <summary>
/// Contains unit tests for the <see cref="Bus"/> class,
/// specifically verifying the correct behavior of the <see cref="Bus.NrOfDoors"/> property.
/// </summary>
[Collection("NonParallelGroup")] // Ensures these tests do not run in parallel with other test classes in the same collection.
public class BusTests
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

	/// <summary>
	/// Verifies that valid values for <see cref="Bus.NrOfDoors"/> passed via the constructor are accepted and stored correctly.
	/// </summary>
	/// <param name="doors">The number of doors to test with.</param>
	[Theory]
	[InlineData(_c_MinNrOfDoors1)]
	[InlineData(_c_MaxNrOfDoors2)]
	public void NumberOfDoors_SetViaConstructor_ValidValues_ShouldPass(uint doors)
	{
		// Assign
		string tempFile = Path.Combine(Path.GetTempPath(), $"test-{Guid.NewGuid()}.json");
		ILicensePlateRegistry registry = new MockLicensePlateRegistry(tempFile);

		// Act
		IBus bus = new Bus(
			registry.IsValidLicensePlate,
			_c_LicensePlate, 
			_c_Color, 
			_c_Wheel, 
			doors
		);

		// Assert
		Assert.Equal(doors, bus.NrOfDoors);

		Dispose(tempFile, registry);
	}

	/// <summary>
	/// Verifies that passing an invalid number of doors to the <see cref="Bus"/> constructor throws an exception.
	/// </summary>
	[Fact]
	public void NumberOfDoors_SetViaConstructor_InValidValues_ShouldThrowArgumentOutOfRangeException()
	{
		// Assert
		string tempFile = Path.Combine(Path.GetTempPath(), $"test-{Guid.NewGuid()}.json");
		ILicensePlateRegistry registry = new MockLicensePlateRegistry(tempFile);

		// Act
		Assert.Throws<ArgumentOutOfRangeException>(() =>
			new Bus(
				registry.IsValidLicensePlate,
				_c_LicensePlate,
				_c_Color, 
				_c_Wheel, 
				_c_OutsideOfRangeNrOfDoors3
			)
		);
		
		// Cleanup
		Dispose(tempFile, registry);
	}

	/// <summary>
	/// Cleans up the mock registry after each test run.
	/// </summary>
	public void Dispose(string tempFile, ILicensePlateRegistry registry)
	{
		registry.ClearAllLicensePlates();
		if (File.Exists(tempFile))
			File.Delete(tempFile);
	}
}
