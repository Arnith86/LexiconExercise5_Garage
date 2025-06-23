using LexiconExcercise5.Garage.TestProject.Vehicles.Mocks;
using LexiconExercise5_Garage.Garages.GarageFactory;
using LexiconExercise5_Garage.Util;
using LexiconExercise5_Garage.Vehicles;
using LexiconExercise5_Garage.Vehicles.AirPlains;
using LexiconExercise5_Garage.Vehicles.LicensePlate.Registry;
using LexiconExercise5_Garage.Vehicles.VehicleBase;
using LexiconExercise5_Garage.Vehicles.VehicleFactories;
using LexiconExercise5_GarageAssignment.ConsoleRelated;
using Microsoft.Win32;
using Moq;
using System.Drawing;

namespace LexiconExcercise5.Garage.TestProject;

[Collection("NonParallelGroup")] // Will not run in parallel with any other class in the same collection
public class BuildVehicleTests
{
	private const int _c_VEHICLE_WHEELS_MIN = 0;
	private const int _c_VEHICLE_WHEELS_MAX = 56;
	private const int _c_AIRPLANE_ENGINES_MIN = 0;
	private const int _c_AIRPLANE_ENGINES_MAX = 10;
	private const int _c_BUS_DOORS_MIN = 1;
	private const int _c_BUS_DOORS_MAX = 2;
	private const int _c_CAR_SEATS_MIN = 1;
	private const int _c_CAR_SEATS_MAX = 7;

	private const string _c_LicensePlateUnique = "ZZZ999";
	private const VehicleColor _c_color_Blue = VehicleColor.Blue;
	private const int _c_NrOfWheels_3 = 3;
	private const int _c_NrOfEngines_2 = 2;
	
	// GetVehicle menu options.
	private const int _c_AirPlain_Int_1 = 1;

	private readonly ILicensePlateRegistry _registry = new MockLicensePlateRegistry();
	private readonly IVehicleFactory _vehicleFactory = new VehicleFactory();
	//private readonly VehiclesFilterFunctions _vehiclesFilterFunctions = new VehiclesFilterFunctions();

	private BuildVehicle CreateBuildVehicle(Mock<IConsoleUI> mockConsoleUI)
	{
		BuildVehicle buildVehicle = new BuildVehicle(_vehicleFactory, _registry, mockConsoleUI.Object);
		return buildVehicle;
	}

	[Fact]
	public void GetVehicle_BuildAirPlaneSelected_ValidValues_ShouldPass()
	{
		// Arrange
		Mock<IConsoleUI> mockConsoleUI = new();
		mockConsoleUI.Setup(cUI => 
			cUI.RegisterLicensePlateInput(It.IsAny<string>()))
			.Returns(_c_LicensePlateUnique);

		mockConsoleUI.Setup(cUI => 
			cUI.RegisterInputFromEnumOptions<VehicleColor>(It.IsAny<string>()))
			.Returns((int)_c_color_Blue);

		// For wheels
		mockConsoleUI.Setup(cUI =>
			cUI.RegisterNumericUintInput(It.IsAny<string>(), _c_VEHICLE_WHEELS_MIN, _c_VEHICLE_WHEELS_MAX))
			.Returns(_c_NrOfWheels_3);

		// For airplane engines
		mockConsoleUI.Setup(cUI =>
			cUI.RegisterNumericUintInput(It.IsAny<string>(), _c_AIRPLANE_ENGINES_MIN, _c_AIRPLANE_ENGINES_MAX))
			.Returns(_c_NrOfEngines_2);

		BuildVehicle buildVehicle = CreateBuildVehicle(mockConsoleUI);

		// Act
		var returnVehicle = buildVehicle.GetVehicle(_c_AirPlain_Int_1);

		// Assert
		Assert.IsType<AirPlain>(returnVehicle);
	}

}
