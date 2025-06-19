
using LexiconExercise5_Garage.Vehicles.AirPlains;
using LexiconExercise5_Garage.Vehicles.Cars;
using LexiconExercise5_Garage.Vehicles.LicensePlate.Registry;
using LexiconExercise5_Garage.Vehicles.VehicleBase;
using LexiconExercise5_GarageAssignment.ConsoleRelated;

namespace LexiconExercise5_Garage.Vehicles.VehicleFactories
{
	public class BuildVehicle
	{
		private readonly IVehicleFactory _vehicleFactory;
		private readonly ILicensePlateRegistry _licensePlateRegistry;
		private readonly IConsoleUI _consoleUI;


		public BuildVehicle(
			IVehicleFactory vehicleFactory, 
			ILicensePlateRegistry licensePlateRegistry,
			IConsoleUI consoleUI)
		{
			_vehicleFactory = vehicleFactory;
			_licensePlateRegistry = licensePlateRegistry;
			_consoleUI = consoleUI;
		}

		public IVehicle GetVehicle(int vehicle)
		{
			string licensePlate = RegisterLicensePlate();

			VehicleColor color = _consoleUI.RegisterColorInput();
			uint wheels = _consoleUI.RegisterNrOfWheelsInput();

			switch (vehicle)
			{
				case 1:
					return BuildAirPlain(licensePlate, color, wheels);
				default:
					break;
			}

			return null;
		}

		private string RegisterLicensePlate()
		{
			bool validLicensePlate = false;
			string licensePlate = string.Empty;

			do
			{
				try
				{
					licensePlate = _consoleUI.RegisterLicensePlateInput();
					_licensePlateRegistry.IsValidLicensePlate(licensePlate);
					validLicensePlate = true;
				}
				catch (Exception ex) when (
					ex is ArgumentNullException ||
					ex is ArgumentOutOfRangeException ||
					ex is ArgumentException ||
					ex is InvalidOperationException)
				{
					_consoleUI.ShowError($"License plate error: {ex.Message}");
				}

			} while (!validLicensePlate);

			_consoleUI.ShowFeedbackMessage("License plate registered!");

			return licensePlate;
		}

		private IVehicle BuildAirPlain(string licensePlate, VehicleColor color, uint wheels)
		{
			uint numberOfEngines = _consoleUI.RegisterNumberOfEnginesInput();

			return _vehicleFactory.CreateAirPlain(
				_licensePlateRegistry.IsValidLicensePlate, 
				licensePlate,
				color,
				wheels,
				numberOfEngines
			);
		}
