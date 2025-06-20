using LexiconExcercise5.Garage.TestProject.Vehicles.Mocks;
using LexiconExercise5_Garage.Garages;
using LexiconExercise5_Garage.Garages.GarageFactory;
using LexiconExercise5_Garage.Vehicles;
using LexiconExercise5_Garage.Vehicles.AirPlains;
using LexiconExercise5_Garage.Vehicles.Boats;
using LexiconExercise5_Garage.Vehicles.Buss;
using LexiconExercise5_Garage.Vehicles.Cars;
using LexiconExercise5_Garage.Vehicles.LicensePlate.Registry;
using LexiconExercise5_Garage.Vehicles.Motorcycles;
using LexiconExercise5_Garage.Vehicles.VehicleBase;

namespace LexiconExcercise5.Garage.TestProject.GaragesTests;

/// <summary>
/// Contains unit tests for the <see cref="Garage{T}"/> class
/// </summary>
[Collection("NonParallelGroup")] // Will not run in parallel with any other class in the same collection
public class GarageTests
{

	// VALID VehicleBase base class attributes
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

	// Valid collection with a vehicle of every kind
	List<Vehicle> oneVehiclesOfEveryKind = new List<Vehicle>()
	{
		new AirPlain(LPR => true, "abc111", VehicleColor.Red, 4, 2),
		new Boat(LPR => true, "abc119", VehicleColor.Red, 0, FuelType.Diesel),
		new Bus(LPR => true, "abc127", VehicleColor.Green, 4, 1),
		new Car(LPR => true, "abc135", VehicleColor.Red, 4, 5),
		new Motorcycle(LPR => true, "abc143", VehicleColor.Red, 2, false)
	};

		
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
		// Arrange
		string tempFile = Path.Combine(Path.GetTempPath(), $"test-{Guid.NewGuid()}.json");
		ILicensePlateRegistry registry = new MockLicensePlateRegistry(tempFile);

		// Act
		IGarageCreator<TestVehicle> garageCreator = new GarageMixedCreator<TestVehicle>(registry);

		int expectedUsedSpaces = 0;
		int expectedCapacity = 4;

		IGarage<TestVehicle> garage = garageCreator.CreateGarage(size);

		//Assert
		Assert.Equal(expectedCapacity, garage.Capacity);
		Assert.Equal(expectedUsedSpaces, garage.UsedSpaces);

		//Cleanup
		Dispose(tempFile, registry);
	}

	/// <summary>
	/// Verifies that a size just over a power-of-two boundary sets capacity to 8.
	/// </summary>
	[Fact]
	public void Vehicles_CapacityArraySize_SetViaConstructor_ValidValues_ShouldPass_UsedSpaces0_Capacity8()
	{
		// Arrange
		string tempFile = Path.Combine(Path.GetTempPath(), $"test-{Guid.NewGuid()}.json");
		ILicensePlateRegistry registry = new MockLicensePlateRegistry(tempFile);

		// Act
		IGarageCreator<TestVehicle> garageCreator = new GarageMixedCreator<TestVehicle>(registry);

		int expectedUsedSpaces = 0;
		int expectedCapacity = 8;

		IGarage<TestVehicle> garage = garageCreator.CreateGarage(_c_ArraySizeAfterLimit5);

		//Assert
		Assert.Equal(expectedCapacity, garage.Capacity);
		Assert.Equal(expectedUsedSpaces, garage.UsedSpaces);

		// Cleanup
		Dispose(tempFile, registry);
	}

	/// <summary>
	/// Verifies that the maximum allowed size results in a capacity of 524288.
	/// </summary>
	[Fact]
	public void Vehicles_CapacityArraySize_SetViaConstructor_ValidValues_ShouldPass_Expected_UsedSpaces0_Capacity524288()
	{
		// Arrange
		string tempFile = Path.Combine(Path.GetTempPath(), $"test-{Guid.NewGuid()}.json");
		ILicensePlateRegistry registry = new MockLicensePlateRegistry(tempFile);

		// Act
		IGarageCreator<TestVehicle> garageCreator = new GarageMixedCreator<TestVehicle>(registry);

		int expectedUsedSpaces = 0;
		int expectedCapacity = 524288;

		IGarage<TestVehicle> garage = garageCreator.CreateGarage(_c_ArraySizeEdgeCaseHighestValue);

		//Assert
		Assert.Equal(expectedCapacity, garage.Capacity);
		Assert.Equal(expectedUsedSpaces, garage.UsedSpaces);

		// Cleanup
		Dispose(tempFile, registry);
	}


	/// <summary>
	/// Verifies that invalid sizes (negative or over max limit) throw ArgumentOutOfRangeException.
	/// </summary>
	[Theory]
	[InlineData(_c_ArraySizeNegative)]
	[InlineData(_c_ArraySizeOverUpperLimit524289)]
	public void Vehicles_CapacityArraySize_SetViaConstructor_InValidValues_ShouldThrowArgumentOutOfRangeException(int size)
	{
		// Arrange
		string tempFile = Path.Combine(Path.GetTempPath(), $"test-{Guid.NewGuid()}.json");
		ILicensePlateRegistry registry = new MockLicensePlateRegistry(tempFile);

		IGarageCreator<TestVehicle> garageCreator = new GarageMixedCreator<TestVehicle>(registry); 

		// Act & Assert
		Assert.Throws<ArgumentOutOfRangeException>(() =>
			garageCreator.CreateGarage(size)
		);

		// Cleanup
		Dispose(tempFile, registry);
	}

	[Fact]
	public void GetVehicleInformation_IfHasSpecifiedVehicle_ValidValue_ShouldPass_Expected_String()
	{
		// Arrange
		string tempFile = Path.Combine(Path.GetTempPath(), $"test-{Guid.NewGuid()}.json");
		ILicensePlateRegistry registry = new MockLicensePlateRegistry(tempFile);

		IGarageCreator<Vehicle> garageCreator = new GarageMixedCreator<Vehicle>(registry);

		IGarage<Vehicle> garage = garageCreator.CreateGarage(_c_ArraySizeEdgeCaseHighestValue);

		foreach (var vehicle in oneVehiclesOfEveryKind)
		{
			garage.AddVehicle(vehicle);
		}

		//Act & Assert
		foreach (var vehicle in oneVehiclesOfEveryKind)
		{
			Assert.Equal(garage.GetVehicleInformation(vehicle.LicensePlate), vehicle.ToString());
		}

		// Cleanup 
		Dispose(tempFile, registry);
	}


	[Fact]
	public void GetVehicleInformation_IfSpecifiedVehicleNotInGarage_ValidValue_ShouldPass_Expected_null()
	{
		// Arrange
		string tempFile = Path.Combine(Path.GetTempPath(), $"test-{Guid.NewGuid()}.json");
		ILicensePlateRegistry registry = new MockLicensePlateRegistry(tempFile);

		IGarageCreator<Vehicle> garageCreator = new GarageMixedCreator<Vehicle>(registry);

		IGarage<Vehicle> garage = garageCreator.CreateGarage(_c_ArraySizeEdgeCaseHighestValue);

		foreach (var vehicle in oneVehiclesOfEveryKind)
		{
			garage.AddVehicle(vehicle);
		}

		//Act & Assert
		Assert.Null(garage.GetVehicleInformation("zzz999"));

		// Cleanup
		Dispose(tempFile, registry);
	}

	[Fact]
	public void GetAllVehiclesInformation_VerifiesAllInformationIsRetrieved_ShouldPass_Returns_StringCollection()
	{
		// Arrange
		string tempFile = Path.Combine(Path.GetTempPath(), $"test-{Guid.NewGuid()}.json");
		ILicensePlateRegistry registry = new MockLicensePlateRegistry(tempFile);

		IGarageCreator<Vehicle> garageCreator = new GarageMixedCreator<Vehicle>(registry);

		IGarage<Vehicle> garage = garageCreator.CreateGarage(_c_ArraySizeEdgeCaseHighestValue);

		foreach (var vehicle in oneVehiclesOfEveryKind)
		{
			garage.AddVehicle(vehicle);
		}

		List<string> toStringCollection = garage.GetAllVehiclesInformation()!.ToList();

		int index = 0;

		//Act & Assert
		foreach (var vehicle in oneVehiclesOfEveryKind)
		{
			Assert.Contains(vehicle.ToString(), toStringCollection[index++]);
		}

		// Cleanup
		Dispose(tempFile,registry);
	}

	[Fact]
	public void GetAllVehiclesInformation_GarageEmpty_ValidValue_ShouldPass_Returns_EmptyStringCollection()
	{
		// Arrange
		string tempFile = Path.Combine(Path.GetTempPath(), $"test-{Guid.NewGuid()}.json");
		ILicensePlateRegistry registry = new MockLicensePlateRegistry(tempFile);

		IGarageCreator<Vehicle> garageCreator = new GarageMixedCreator<Vehicle>(registry);
		IGarage<Vehicle> garage = garageCreator.CreateGarage(_c_ArraySizeEdgeCaseHighestValue);
		
		//Act
		List<string> toStringCollection = garage.GetAllVehiclesInformation()!.ToList();
		
		//Assert
		Assert.True(toStringCollection.Count == 0);

		// Cleanup
		Dispose(tempFile, registry);
	}

	[Fact]
	public void AddVehicle_EmptyGarage_ShouldPass_Expected_UsedSpaces2_Capacity4()
	{
		// Arrange
		int expectedUsedSpaces = 2;
		int expectedCapacity = 4;

		string tempFile = Path.Combine(Path.GetTempPath(), $"test-{Guid.NewGuid()}.json");
		ILicensePlateRegistry registry = new MockLicensePlateRegistry(tempFile);

		IGarageCreator<Vehicle> garageCreator = new GarageMixedCreator<Vehicle>(registry);
				
		IGarage<Vehicle> garage = garageCreator.CreateGarage(_c_ArraySizeBeforeLimit4);


		// Act
		for (int i = 0; i < 2; i++)
		{
			garage.AddVehicle(
				new TestVehicle(
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

		// Cleanup
		Dispose(tempFile, registry);
	}

	[Fact]
	public void AddVehicle_FullGarage_ShouldThrowArgumentOutOfRangeException()
	{
		// Arrange
		string tempFile = Path.Combine(Path.GetTempPath(), $"test-{Guid.NewGuid()}.json");
		ILicensePlateRegistry registry = new MockLicensePlateRegistry(tempFile);

		IGarageCreator<Vehicle> garageCreator = new GarageMixedCreator<Vehicle>(registry);
		IGarage<Vehicle> garage = garageCreator.CreateGarage(_c_ArraySizeEdgeCaseHighestValue);

		for (int i = 0; i < 524288; i++)
		{
			garage.AddVehicle(
				new TestVehicle(
					LicensePlateIsUnique => true,
					_c_LicensePlate,
					_c_Color,
					_c_Wheel
				)
			);
		}

		// Act and Assert
		Assert.Throws<ArgumentOutOfRangeException>(() =>
			garage.AddVehicle(
				new TestVehicle(
				LicensePlateIsUnique => true,
				_c_LicensePlate,
				_c_Color,
				_c_Wheel)
			)
		);

		// Cleanup
		Dispose(tempFile, registry);
	}

	[Fact]
	public void AddVehicle_GarageUpperLimitReached_ShouldThrowArgumentOutOfRangeException()
	{
		//Arrange
		string tempFile = Path.Combine(Path.GetTempPath(), $"test-{Guid.NewGuid()}.json");
		ILicensePlateRegistry registry = new MockLicensePlateRegistry(tempFile);

		IGarageCreator<Vehicle> garageCreator = new GarageMixedCreator<Vehicle>(registry);

		IGarage<Vehicle> garage = garageCreator.CreateGarage(_c_ArraySizeAfterLimit5);

		foreach (var vehicle in oneVehiclesOfEveryKind)
		{
			garage.AddVehicle(vehicle);
		}

		// Act & Assert
		Assert.Throws<ArgumentOutOfRangeException>(() =>
			garage.AddVehicle(
				new TestVehicle(
				LicensePlateIsUnique => true,
				_c_LicensePlate,
				_c_Color,
				_c_Wheel)
			)
		);

		// Cleanup
		Dispose(tempFile, registry);
	}

	[Fact]
	public void AddVehicle_EveryKindOfVehicle_ShouldPass_Expected_UsedSpaces5_Capacity8()
	{
		//Arrange 
		int expectedUsedSpaces = 5;
		int expectedCapacity = 8;

		string tempFile = Path.Combine(Path.GetTempPath(), $"test-{Guid.NewGuid()}.json");
		ILicensePlateRegistry registry = new MockLicensePlateRegistry(tempFile);

		IGarageCreator<Vehicle> garageCreator = new GarageMixedCreator<Vehicle>(registry);

		IGarage<Vehicle> garage = garageCreator.CreateGarage(_c_ArraySizeAfterLimit5);

		// Act
		foreach (var vehicle in oneVehiclesOfEveryKind)
		{
			garage.AddVehicle(vehicle);
		}

		//Assert
		Assert.Equal(expectedCapacity, garage.Capacity);
		Assert.Equal(expectedUsedSpaces, garage.UsedSpaces);

		// Cleanup
		Dispose(tempFile, registry);
	}

	[Fact]
	public void AddVehicle_ChecksIfVehicleIsAddedToEmptySpaceAfterRemove_ValidValues_ShouldPass()
	{
		//Arrange
		int expectedUsedSpaces = 4;

		string tempFile = Path.Combine(Path.GetTempPath(), $"test-{Guid.NewGuid()}.json");
		ILicensePlateRegistry registry = new MockLicensePlateRegistry(tempFile);
		IGarageCreator<Vehicle> garageCreator = new GarageMixedCreator<Vehicle>(registry);
		
		IGarage<Vehicle> garage = garageCreator.CreateGarage(_c_ArraySizeBeforeLimit4);

		// Adds 4 vehicles with valid values (and unique license plates)
		for (int i = 0; i < 4; i++)
			garage.AddVehicle(oneVehiclesOfEveryKind[i]);

		// Act
		// Removes vehicle from _vehicle[1].
		garage.RemoveVehicle(oneVehiclesOfEveryKind[1].LicensePlate);
		// Only one index that it can be placed in (_vehicle[1]).
		garage.AddVehicle(oneVehiclesOfEveryKind[1]);

		// Assert
		// Only one a single index with null value 
		Assert.Equal(expectedUsedSpaces, garage.UsedSpaces);

		Dispose(tempFile, registry);
	}

	[Fact]
	public void RemoveVehicle_ChecksIfVehicleWasRemoved_AndReturned_ValidValues_ShouldPass()
	{
		//Arrange
		int expectedUsedSpaces = 4;

		string tempFile = Path.Combine(Path.GetTempPath(), $"test-{Guid.NewGuid()}.json");
		ILicensePlateRegistry registry = new MockLicensePlateRegistry(tempFile);
		IGarageCreator<Vehicle> garageCreator = new GarageMixedCreator<Vehicle>(registry);
		
		IGarage<Vehicle> garage = garageCreator.CreateGarage(_c_ArraySizeEdgeCaseHighestValue);

		// Act
		foreach (var vehicle in oneVehiclesOfEveryKind)
		{
			garage.AddVehicle(vehicle);
		}

		//Assert
		Assert.Equal(
			garage.RemoveVehicle(oneVehiclesOfEveryKind[1].LicensePlate),
			oneVehiclesOfEveryKind[1]
		);

		Assert.Equal(expectedUsedSpaces, garage.UsedSpaces);

		Assert.Null(
			garage.GetVehicleInformation(oneVehiclesOfEveryKind[1].LicensePlate)
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
