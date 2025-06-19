using LexiconExcercise5.Garage.TestProject.Vehicles.Mocks;
using LexiconExercise5_Garage.Vehicles;
using LexiconExercise5_Garage.Vehicles.Cars;
using LexiconExercise5_Garage.Vehicles.LicensePlate.Registry;

namespace LexiconExcercise5.Garage.TestProject.Vehicles;

/// <summary>
/// Contains unit tests for the <see cref="Car"/> class,
/// specifically verifying the correct behavior of the <see cref="Car.NrOfSeats"/> property.
/// </summary>
[Collection("NonParallelGroup")] // Ensures these tests do not run in parallel with other test classes in the same collection.
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
		// Assign
		string tempFile = Path.Combine(Path.GetTempPath(), $"test-{Guid.NewGuid()}.json");
		ILicensePlateRegistry registry = new MockLicensePlateRegistry(tempFile);

		// Act
		ICar car = new Car(
			registry.IsValidLicensePlate,
			_c_LicensePlate,
			_c_Color,
			_c_Wheel,
			nrOfSeats
		);

		// Assert
		Assert.Equal(nrOfSeats, car.NrOfSeats);

		// Cleanup.
		Dispose(tempFile, registry);
	}

	/// <summary>
	/// Tests that the constructor throws an <see cref="ArgumentOutOfRangeException"/>
	/// when the number of seats provided is outside the valid range.
	/// </summary>
	[Fact]
	public void NrOfSeats_SetViaConstructor_InValidValue_ShouldThrowArgumentOutOfRangeException()
	{
		// Arrange 
		string tempFile = Path.Combine(Path.GetTempPath(), $"test-{Guid.NewGuid()}.json");
		ILicensePlateRegistry registry = new MockLicensePlateRegistry(tempFile);

		// Act & Assert
		Assert.Throws<ArgumentOutOfRangeException>(() =>
			new Car(
				registry.IsValidLicensePlate,
				_c_LicensePlate,
				_c_Color,
				_c_Wheel,
				_c_OutsideOfRange8NrOfSeats
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
