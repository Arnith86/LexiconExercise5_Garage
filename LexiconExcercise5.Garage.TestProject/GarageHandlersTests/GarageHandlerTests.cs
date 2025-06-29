﻿using LexiconExcercise5.Garage.TestProject.Vehicles.Mocks;
using LexiconExercise5_Garage.Garages.GarageFactory;
using LexiconExercise5_Garage.GaragesHandler;
using LexiconExercise5_Garage.Util;
using LexiconExercise5_Garage.Vehicles.LicensePlate.Registry;
using LexiconExercise5_Garage.Vehicles.VehicleBase;
using LexiconExercise5_Garage.Vehicles.VehicleFactories;
using LexiconExercise5_GarageAssignment.ConsoleRelated;
using Moq;

namespace LexiconExcercise5.Garage.TestProject.GarageHandlersTests;

/// <summary>
/// Unit tests for the <see cref="GarageHandler"/> class, covering garage creation,
/// main menu interactions, and vehicle handling in garages.
/// </summary>
[Collection("NonParallelGroup")] // Will not run in parallel with any other class in the same collection
public class GarageHandlerTests
{
	// Valid garage sizes through constructor.
	private const int _c_ArraySizeEdgeCaseMin1 = 1;
	private const int _c_ArraySize2 = 2;
	private const int _c_ArraySize64 = 64;
	private const int _c_ArraySize128 = 128;
	private const int _c_ArraySizeBeforeLimit4 = 4;
	private const int _c_ArraySizeAfterLimit5 = 5;
	private const int _c_ArraySizeEdgeCaseHighestValue = 524288;

	// Expected UsedSpaces values 
	private const int _c_ExpectedUsedSpaces4 = 4;

	// Invalid garage sizes.
	private const int _c_ArraySizeNegative = -1;
	private const int _c_ArraySizeOverUpperLimit524289 = 524289;

	// Main menu options
	private const int _c_GarageCreation_1 = 1;
	private const int _c_SelectGarage_2 = 2;
	private const int _c_ExitProgram_0 = 0;

	// Garage Selection (which garage is selected)
	private const int _c_Garage0Selected = 0;

	// Garage Handling Menu Options.
	private const int _c_AddMixedSetOfVehicles_1 = 1;
	private const int _c_ExitMenu_0 = 0;

	private readonly IVehicleFactory _vehicleFactory = new VehicleFactory();
	private readonly VehiclesFilterFunctions _vehiclesFilterFunctions = new VehiclesFilterFunctions();

	/// <summary>
	/// Verifies that a garage can be created with valid sizes, and a feedback message is shown once.
	/// </summary>
	[Theory]
	[InlineData(_c_ArraySizeEdgeCaseMin1)]
	[InlineData(_c_ArraySizeBeforeLimit4)]
	public void CreateGarage_ValidUserInput_ShouldPass(int size)
	{
		// Arrange
		string tempFile = Path.Combine(Path.GetTempPath(), $"test-{Guid.NewGuid()}.json");
		ILicensePlateRegistry registry = new MockLicensePlateRegistry(tempFile);
		IGarageCreator<IVehicle> garageCreator = new GarageMixedCreator<IVehicle>(registry);

		Mock<IConsoleUI> mockConsoleUI = new();
		mockConsoleUI.Setup(cUI => cUI.GetGarageSize()).Returns(size);

		BuildVehicle buildVehicle = new BuildVehicle(_vehicleFactory, registry, mockConsoleUI.Object);

		GarageHandler garageHandler = new GarageHandler(mockConsoleUI.Object, garageCreator, buildVehicle, registry, _vehiclesFilterFunctions);

		// Act
		garageHandler.GarageCreation();

		// Assert
		mockConsoleUI.Verify(ui => ui.ShowFeedbackMessage(It.IsAny<string>()), Times.Once);

		// Cleanup
		Dispose(tempFile, registry);
	}

	/// <summary>
	/// Verifies that the garage creation loop gracefully handles invalid inputs.
	/// </summary>
	[Theory]
	[InlineData(_c_ArraySizeNegative, _c_ArraySizeBeforeLimit4)]
	[InlineData(_c_ArraySizeOverUpperLimit524289, _c_ArraySizeBeforeLimit4)]
	public void CreateGarage_VerifyGarageCreationLoopFunctionsAsIntended_ShouldPass(int invalidSize, int validSize)
	{
		// Arrange
		string tempFile = Path.Combine(Path.GetTempPath(), $"test-{Guid.NewGuid()}.json");
		ILicensePlateRegistry registry = new MockLicensePlateRegistry(tempFile);
		IGarageCreator<IVehicle> garageCreator = new GarageMixedCreator<IVehicle>(registry);

		Mock<IConsoleUI> mockConsoleUI = new();

		mockConsoleUI.SetupSequence(cUI => cUI.GetGarageSize())
			.Returns(invalidSize)
			.Returns(validSize); // Simulates valid input to end loop.

		BuildVehicle buildVehicle = new BuildVehicle(_vehicleFactory, registry, mockConsoleUI.Object);

		GarageHandler garageHandler = new GarageHandler(mockConsoleUI.Object, garageCreator, buildVehicle, registry, _vehiclesFilterFunctions);

		// Act
		garageHandler.GarageCreation();

		// Assert
		mockConsoleUI.Verify(cUI => cUI.ShowError(It.IsAny<string>()), Times.Once); // Exception was registered, message sent ConsoleUI.
		mockConsoleUI.Verify(cUI => cUI.ShowFeedbackMessage(It.IsAny<string>()), Times.Once);   // Valid input registered

		// Cleanup
		Dispose(tempFile, registry);
	}

	/// <summary>
	/// Verifies that selecting the garage creation option from the main menu results in garage creation.
	/// </summary>
	[Fact]
	public void MainMenuSelection_GarageCreationSelected_GarageIsCreated_ShouldPass()
	{
		// Arrange
		string tempFile = Path.Combine(Path.GetTempPath(), $"test-{Guid.NewGuid()}.json");
		ILicensePlateRegistry registry = new MockLicensePlateRegistry(tempFile);
		IGarageCreator<IVehicle> garageCreator = new GarageMixedCreator<IVehicle>(registry);

		Mock<IConsoleUI> mockConsoleUI = new();

		mockConsoleUI.SetupSequence(cUI => cUI.RegisterMainMenuSelection())
			.Returns(_c_GarageCreation_1)
			.Returns(_c_ExitProgram_0);

		mockConsoleUI.Setup(cUI => cUI.GetGarageSize()).Returns(_c_ArraySizeBeforeLimit4);

		BuildVehicle buildVehicle = new BuildVehicle(_vehicleFactory, registry, mockConsoleUI.Object);

		GarageHandler garageHandler = new GarageHandler(mockConsoleUI.Object, garageCreator, buildVehicle, registry, _vehiclesFilterFunctions);

		// Act
		garageHandler.MainMenuSelection();

		// Assert
		mockConsoleUI.Verify(cUI => cUI.ShowFeedbackMessage(It.IsAny<string>()), Times.Once);

		// Cleanup
		Dispose(tempFile, registry);
	}

	/// <summary>
	/// Verifies that an existing garage can be selected from the main menu and a feedback message is shown.
	/// </summary>
	[Fact]
	public void MainMenuSelection_SelectGarageSelected_GarageSelected_GarageExists_ShowFeedBackMessage_ShouldPass()
	{
		// Arrange
		string tempFile = Path.Combine(Path.GetTempPath(), $"test-{Guid.NewGuid()}.json");
		ILicensePlateRegistry registry = new MockLicensePlateRegistry(tempFile);
		IGarageCreator<IVehicle> garageCreator = new GarageMixedCreator<IVehicle>(registry);

		Mock<IConsoleUI> mockConsoleUI = new();

		mockConsoleUI.SetupSequence(cUI => cUI.RegisterMainMenuSelection())
			.Returns(_c_GarageCreation_1)
			.Returns(_c_SelectGarage_2);

		mockConsoleUI.Setup(cUI => cUI.GetGarageSize()).Returns(_c_ArraySizeBeforeLimit4);

		List<int> garageNumbers = new List<int>() { 0 };

		mockConsoleUI.Setup(cUI => cUI.SelectGarage(garageNumbers)).Returns(_c_Garage0Selected);

		BuildVehicle buildVehicle = new BuildVehicle(_vehicleFactory, registry, mockConsoleUI.Object);

		GarageHandler garageHandler = new GarageHandler(mockConsoleUI.Object, garageCreator, buildVehicle, registry, _vehiclesFilterFunctions);

		// Act
		garageHandler.MainMenuSelection();

		// Assert
		// Once on garage creation, and once on successful garage selection.
		mockConsoleUI.Verify(cUI => cUI.ShowFeedbackMessage(It.IsAny<string>()), Times.Exactly(2));

		// Cleanup
		Dispose(tempFile, registry);
	}

	/// <summary>
	/// Verifies that attempting to select a non-existent garage from the main menu triggers an error message.
	/// </summary>
	[Fact]
	public void MainMenuSelection_SelectGarageSelected_GarageSelected_GarageDoesNotExists_ShowError_ShouldPass()
	{
		// Arrange
		string tempFile = Path.Combine(Path.GetTempPath(), $"test-{Guid.NewGuid()}.json");
		ILicensePlateRegistry registry = new MockLicensePlateRegistry(tempFile);
		IGarageCreator<IVehicle> garageCreator = new GarageMixedCreator<IVehicle>(registry);

		Mock<IConsoleUI> mockConsoleUI = new();

		mockConsoleUI.SetupSequence(cUI => cUI.RegisterMainMenuSelection())
			.Returns(_c_SelectGarage_2)
			.Returns(_c_ExitProgram_0);

		List<int> garageNumbers = new List<int>() { 0 };

		mockConsoleUI.Setup(cUI => cUI.SelectGarage(garageNumbers)).Returns(_c_Garage0Selected);

		BuildVehicle buildVehicle = new BuildVehicle(_vehicleFactory, registry, mockConsoleUI.Object);

		GarageHandler garageHandler = new GarageHandler(mockConsoleUI.Object, garageCreator, buildVehicle, registry, _vehiclesFilterFunctions);

		// Act
		garageHandler.MainMenuSelection();

		// Assert
		mockConsoleUI.Verify(cUI => cUI.ShowError(It.IsAny<string>()), Times.Once);

		// Cleanup
		Dispose(tempFile, registry);
	}

	/// <summary>
	/// Verifies that a mixed set of vehicles can be added to a newly created garage, and feedback is shown.
	/// </summary>
	[Fact]
	public void GarageHandlingMenuSelection_AddMixedSetOfVehiclesToGarage_FirstTime_Successful_ShowFeedbackMessage_ShouldPass()
	{
		// Arrange
		string tempFile = Path.Combine(Path.GetTempPath(), $"test-{Guid.NewGuid()}.json");
		ILicensePlateRegistry registry = new MockLicensePlateRegistry(tempFile);
		IGarageCreator<IVehicle> garageCreator = new GarageMixedCreator<IVehicle>(registry);

		Mock<IConsoleUI> mockConsoleUI = new();

		mockConsoleUI.SetupSequence(cUI => cUI.RegisterMainMenuSelection())
			.Returns(_c_GarageCreation_1)
			.Returns(_c_SelectGarage_2)
			.Returns(_c_ExitProgram_0);

		mockConsoleUI.Setup(cUI => cUI.GetGarageSize()).Returns(_c_ArraySize64);

		List<int> garageNumbers = new List<int>() { 0 };

		mockConsoleUI.Setup(cUI =>
			cUI.SelectGarage(garageNumbers))
			.Returns(_c_Garage0Selected);

		mockConsoleUI.SetupSequence(cUI =>
			cUI.RegisterGarageHandlingMenuSelection(_c_Garage0Selected))
			.Returns(_c_AddMixedSetOfVehicles_1)
			.Returns(_c_ExitMenu_0);


		BuildVehicle buildVehicle = new BuildVehicle(_vehicleFactory, registry, mockConsoleUI.Object);

		GarageHandler garageHandler = new GarageHandler(mockConsoleUI.Object, garageCreator, buildVehicle, registry, _vehiclesFilterFunctions);

		// Act
		garageHandler.MainMenuSelection();

		// Assert
		// Once on garage creation, and once on successful garage selection, and lastly for adding the vehicles
		mockConsoleUI.Verify(cUI => cUI.ShowFeedbackMessage(It.IsAny<string>()), Times.Exactly(3));

		// Cleanup
		Dispose(tempFile, registry);
	}

	/// <summary>
	/// Verifies that adding a mixed set of vehicles twice to the same garage triggers an error on the second attempt.
	/// </summary>
	[Fact]
	public void GarageHandlingMenuSelection_AddMixedSetOfVehiclesToGarage_SecondTime_Fail_ShowErrorMessage_ShouldPass()
	{
		// Arrange
		string tempFile = Path.Combine(Path.GetTempPath(), $"test-{Guid.NewGuid()}.json");
		ILicensePlateRegistry registry = new MockLicensePlateRegistry(tempFile);
		IGarageCreator<IVehicle> garageCreator = new GarageMixedCreator<IVehicle>(registry);

		Mock<IConsoleUI> mockConsoleUI = new();

		mockConsoleUI.SetupSequence(cUI => cUI.RegisterMainMenuSelection())
			.Returns(_c_GarageCreation_1)
			.Returns(_c_SelectGarage_2)
			.Returns(_c_ExitProgram_0);

		mockConsoleUI.Setup(cUI => cUI.GetGarageSize()).Returns(_c_ArraySize128);

		List<int> garageNumbers = new List<int>() { 0 };

		mockConsoleUI.Setup(cUI =>
			cUI.SelectGarage(garageNumbers))
			.Returns(_c_Garage0Selected);

		mockConsoleUI.SetupSequence(cUI =>
			cUI.RegisterGarageHandlingMenuSelection(_c_Garage0Selected))
			.Returns(_c_AddMixedSetOfVehicles_1)
			.Returns(_c_AddMixedSetOfVehicles_1)
			.Returns(_c_ExitMenu_0);


		BuildVehicle buildVehicle = new BuildVehicle(_vehicleFactory, registry, mockConsoleUI.Object);

		GarageHandler garageHandler = new GarageHandler(mockConsoleUI.Object, garageCreator, buildVehicle, registry, _vehiclesFilterFunctions);

		// Act
		garageHandler.MainMenuSelection();

		// Assert
		// Once on garage creation, and once on successful garage selection, and lastly for adding the vehicles
		mockConsoleUI.Verify(cUI => cUI.ShowError(It.IsAny<string>()), Times.Once);

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
