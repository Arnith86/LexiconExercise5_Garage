
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
			_consoleUI.ShowFeedbackMessage($"License plate {licensePlate} registered!");

			int color = _consoleUI.RegisterColorInput();
			_consoleUI.ShowFeedbackMessage($"The color {(VehicleColor)color} was selected!");
			
			uint wheels = _consoleUI.RegisterNrOfWheelsInput();
			_consoleUI.ShowFeedbackMessage($"{wheels} wheels chosen!");

			switch (vehicle)
			{
				case 1:
					return BuildAirPlain(licensePlate, (VehicleColor)color, wheels);
				//case 2:
				//	return BuildBoat(licensePlate, color, wheels);
				//case 3:
				//	return BuildBus(licensePlate, color, wheels);
				//case 4:
				//	return BuildCar(licensePlate, color, wheels);
				//case 5:
				//	return BuildMotorcycle(licensePlate, color, wheels);
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

			return licensePlate;
		}

		private IVehicle BuildAirPlain(string licensePlate, VehicleColor color, uint wheels)
		{
			uint numberOfEngines = _consoleUI.RegisterNumberOfEnginesInput();
			_consoleUI.ShowFeedbackMessage($"{numberOfEngines} engines chosen!");

			return _vehicleFactory.CreateAirPlain(
				_licensePlateRegistry.IsValidLicensePlate, 
				licensePlate,
				color,
				wheels,
				numberOfEngines
			);
		}

		//private IVehicle BuildBoat(string licensePlate, VehicleColor color, uint wheels)
		//{
		//	FuelType fuelType = _consoleUI.RegisterFuelTypeInput();

		//	return _vehicleFactory.CreateBoat(
		//		_licensePlateRegistry.IsValidLicensePlate,
		//		licensePlate,
		//		color,
		//		wheels,
		//		fuelType
		//	);
		//}

		//private IVehicle BuildBus(string licensePlate, VehicleColor color, uint wheels)
		//{
		//	uint nrOfDoors = _consoleUI.RegisterNrOfDoorsInput();

		//	return _vehicleFactory.CreateBus(
		//		_licensePlateRegistry.IsValidLicensePlate,
		//		licensePlate,
		//		color,
		//		wheels,
		//		nrOfDoors
		//	);
		//}

		//private IVehicle BuildCar(string licensePlate, VehicleColor color, uint wheels)
		//{
		//	uint nrOfSeats = _consoleUI.RegisteruintNrOfSeatsInput();

		//	return _vehicleFactory.CreateCar(
		//		_licensePlateRegistry.IsValidLicensePlate,
		//		licensePlate,
		//		color,
		//		wheels,
		//		nrOfSeats
		//	);
		//}

		//private IVehicle BuildMotorcycle(string licensePlate, VehicleColor color, uint wheels)
		//{
		//	bool hasSidecar = _consoleUI.RegisterHasSidecarInput();

		//	return _vehicleFactory.CreateMotorcycle(
		//		_licensePlateRegistry.IsValidLicensePlate,
		//		licensePlate,
		//		color,
		//		wheels,
		//		hasSidecar
		//	);
		//}
	}
}