
using LexiconExcercise5.Garage.TestProject.Vehicles.Mocks;
using LexiconExercise5_Garage.Vehicles;
using LexiconExercise5_Garage.Vehicles.LicensePlate.Registry;
using LexiconExercise5_Garage.Vehicles.Motorcycles;

namespace LexiconExcercise5.Garage.TestProject.Vehicles;

/// <summary>
/// Contains unit tests for the <see cref="Motorcycle"/> class,
/// specifically verifying the correct behavior of the <see cref="Motorcycle.HasSidecar"/> property.
/// </summary>
[Collection("NonParallelGroup")] // Ensures these tests do not run in parallel with other test classes in the same collection.
public class MotorcycleTests
{
	// VALID license plate examples matching expected format (3 letters followed by 3 digits)
	private const string _c_LicensePlate = "BBK159";

	// VALID vehicle colors
	private const VehicleColor _c_Color = VehicleColor.Yellow;

	// VALID numbers of wheels 
	private const uint _c_Wheel = 6;

	// VALID hasSideCar values
	private const bool _c_True = true;
	private const bool _c_False = false;

	/// <summary>
	/// Tests that the NumberOfEngines property accepts valid engine counts (including edge cases) 
	/// when set via the constructor, and that the value is correctly assigned.
	/// </summary>
	[Theory]
	[InlineData(_c_True)]
	[InlineData(_c_False)]
	public void HasSideCar_SetViaConstructor_ValidValues_ShouldPass(bool hasSideCar)
	{
		//Arrange
		string tempFile = Path.Combine(Path.GetTempPath(), $"test-{Guid.NewGuid()}.json");
		ILicensePlateRegistry registry = new MockLicensePlateRegistry(tempFile);
		
		// Act
		IMotorcycle motorcycle = new Motorcycle(
			registry.IsValidLicensePlate,
			_c_LicensePlate,
			_c_Color,
			_c_Wheel,
			hasSideCar
		);

		//Assert
		Assert.Equal(hasSideCar, motorcycle.HasSidecar);

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
