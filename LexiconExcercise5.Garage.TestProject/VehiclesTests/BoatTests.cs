using LexiconExcercise5.Garage.TestProject.Vehicles.Mocks;
using LexiconExercise5_Garage.Vehicles;
using LexiconExercise5_Garage.Vehicles.Boat;


namespace LexiconExcercise5.Garage.TestProject.Vehicles;

/// <summary>
/// Contains unit tests for the <see cref="Boat"/> class,
/// specifically verifying the correct assignment of the <c>FuelType</c> property.
/// </summary>
[Collection("NonParallelGroup")] // Will not run in parallel with any other class in the same collection
public class BoatTests
{
	private MockLicensePlateRegistry _c_MockLicensePlateRegistry = new MockLicensePlateRegistry();

	// VALID base class attributes
	private const string _c_LicensePlate = "zBY283";
	private const VehicleColor _c_Color = VehicleColor.Yellow;
	private const uint _c_Wheel = 0;

	// VALID fuel types
	private const FuelType _c_fuelTypeNone = FuelType.None;
	private const FuelType _c_fuelTypeDiesel = FuelType.Diesel;
	private const FuelType _c_fuelTypeGasoline = FuelType.Gasoline;
	private const FuelType _c_fuelTypeElectric = FuelType.Electric;

	/// <summary>
	/// Tests that the constructor correctly sets the <see cref="FuelType"/> property
	/// when provided with valid enum values.
	/// </summary>
	/// <param name="fuel">The fuel type to assign to the boat.</param>
	[Theory]
	[InlineData(_c_fuelTypeNone)]
	[InlineData(_c_fuelTypeDiesel)]
	[InlineData(_c_fuelTypeGasoline)]
	[InlineData(_c_fuelTypeElectric)]
	public void FuelType_SetViaConstructor_ValidValues_ShouldPass(FuelType fuel)
	{
		// Arrange & Act 
		IBoat boat = new Boat(
			_c_MockLicensePlateRegistry.IsValidLicensePlate,
			_c_LicensePlate, 
			_c_Color, 
			_c_Wheel, 
			fuel
		);

		// Assert
		Assert.Equal(fuel, boat.FuelType);

		Dispose();
	}

	/// <summary>
	/// Cleans up the mock registry after each test run.
	/// </summary>
	public void Dispose()
	{
		_c_MockLicensePlateRegistry.ClearRegistry();
		_c_MockLicensePlateRegistry.IsValidLicensePlate("AAA111");
	}
}
