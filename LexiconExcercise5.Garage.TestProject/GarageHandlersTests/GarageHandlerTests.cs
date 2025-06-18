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
	private const int _c_ArraySize64 = 64;
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
		mockConsoleUI.Verify(ui => ui.ShowFeedbackMessage(It.IsAny<string>()), Times.Once);
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
		mockConsoleUI.Verify(cUI => cUI.ShowFeedbackMessage(It.IsAny<string>()), Times.Once);	// Valid input registered
	}

	[Fact]
	public void MainMenuSelection_GarageCreationSelected_GarageIsCreated_ShouldPass()
	{
		// Arrange
		Mock<IConsoleUI> mockConsoleUI = new();
		mockConsoleUI.SetupSequence(cUI => cUI.RegisterMainMenuSelection())
			.Returns(_c_GarageCreation_1)
			.Returns(_c_ExitProgram_0);

		mockConsoleUI.Setup(cUI => cUI.GetGarageSize()).Returns(_c_ArraySizeBeforeLimit4);
		GarageHandler garageHandler = new GarageHandler(mockConsoleUI.Object);

		// Act
		garageHandler.MainMenuSelection();

		// Assert
		mockConsoleUI.Verify(cUI => cUI.ShowFeedbackMessage(It.IsAny<string>()), Times.Once);
	}

	[Fact]
	public void MainMenuSelection_SelectGarageSelected_GarageSelected_GarageExists_ShowFeedBackMessage_ShouldPass()
	{
		// Arrange
		Mock<IConsoleUI> mockConsoleUI = new();
		mockConsoleUI.SetupSequence(cUI => cUI.RegisterMainMenuSelection())
			.Returns(_c_GarageCreation_1)
			.Returns(_c_SelectGarage_2);

		mockConsoleUI.Setup(cUI => cUI.GetGarageSize()).Returns(_c_ArraySizeBeforeLimit4);

		List<int> garageNumbers = new List<int>() { 0 };

		mockConsoleUI.Setup(cUI => cUI.SelectGarage(garageNumbers)).Returns(_c_Garage0Selected);

		GarageHandler garageHandler = new GarageHandler(mockConsoleUI.Object);

		// Act
		garageHandler.MainMenuSelection();

		// Assert
		// Once on garage creation, and once on successful garage selection.
		mockConsoleUI.Verify(cUI => cUI.ShowFeedbackMessage(It.IsAny<string>()), Times.Exactly(2));
	}
	
	[Fact]
	public void MainMenuSelection_SelectGarageSelected_GarageSelected_GarageDoesNotExists_ShowError_ShouldPass()
	{
		// Arrange
		Mock<IConsoleUI> mockConsoleUI = new();
		mockConsoleUI.SetupSequence(cUI => cUI.RegisterMainMenuSelection())
			.Returns(_c_SelectGarage_2)
			.Returns(_c_ExitProgram_0);

		List<int> garageNumbers = new List<int>() { 0 };

		mockConsoleUI.Setup(cUI => cUI.SelectGarage(garageNumbers)).Returns(_c_Garage0Selected);

		GarageHandler garageHandler = new GarageHandler(mockConsoleUI.Object);

		// Act
		garageHandler.MainMenuSelection();

		// Assert
		mockConsoleUI.Verify(cUI => cUI.ShowError(It.IsAny<string>()), Times.Once);
	}

}
