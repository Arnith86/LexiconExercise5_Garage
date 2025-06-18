using LexiconExercise5_Garage.Garages;
using LexiconExercise5_Garage.GaragesHandler;
using LexiconExercise5_Garage.Vehicles;
using LexiconExercise5_GarageAssignment.ConsoleRelated;
using Moq;
using System;

namespace LexiconExcercise5.Garage.TestProject.GarageHandlersTests;


public class GarageHandlerTests
{
	// Valid garage sizes through constructor.
	private const int _c_ArraySizeEdgeCaseMin1 = 1;
	private const int _c_ArraySize2 = 2;
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


	[Theory]
	[InlineData(_c_ArraySizeEdgeCaseMin1)]
	[InlineData(_c_ArraySizeBeforeLimit4)]
	public void CreateGarage_ValidUserInput_ShouldPass(int size)
	{
		// Arrange
		Mock<IConsoleUI> mockConsoleUI = new();
		mockConsoleUI.Setup(cUI => cUI.GetGarageSize()).Returns(size);
		GarageHandler garageHandler = new GarageHandler(mockConsoleUI.Object);

		// Act
		garageHandler.GarageCreation();

		// Assert
		mockConsoleUI.Verify(ui => ui.GarageCreated(size), Times.Once);
	}

	[Theory]
	[InlineData(_c_ArraySizeNegative, _c_ArraySizeBeforeLimit4)]
	[InlineData(_c_ArraySizeOverUpperLimit524289, _c_ArraySizeBeforeLimit4)]
	public void CreateGarage_VerifyGarageCreationLoopFunctionsAsIntended_ShouldPass(int invalidSize, int validSize)
	{
		// Arrange
		Mock<IConsoleUI> mockConsoleUI = new();
		mockConsoleUI.SetupSequence(cUI => cUI.GetGarageSize())
			.Returns(invalidSize)
			.Returns(validSize); // Simulates valid input to end loop.

		GarageHandler garageHandler = new GarageHandler(mockConsoleUI.Object);

		// Act
		garageHandler.GarageCreation();

		// Assert
		mockConsoleUI.Verify(cUI => cUI.ShowError(It.IsAny<string>()), Times.Once); // Exception was registered, message sent ConsoleUI.
		mockConsoleUI.Verify(cUI => cUI.GarageCreated(validSize), Times.Once);	// Valid input registered
	}

	[Fact]
	public void MainMenuSelection_GarageCreationSelected_ShouldPass()
	{
		// Arrange
		Mock<IConsoleUI> mockConsoleUI = new();
		mockConsoleUI.Setup(cUI => cUI.RegisterMainMenuSelection()).Returns(_c_GarageCreation_1);
		mockConsoleUI.Setup(cUI => cUI.GetGarageSize()).Returns(_c_ArraySizeBeforeLimit4);
		GarageHandler garageHandler = new GarageHandler(mockConsoleUI.Object);

		// Act
		garageHandler.MainMenuSelection();

		// Assert
		mockConsoleUI.Verify(cUI => cUI.GarageCreated(4), Times.Once);
	}
	
}
