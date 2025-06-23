
using LexiconExercise5_Garage.ConsoleRelated;
using LexiconExercise5_Garage.Vehicles.LicensePlate.Registry;
using LexiconExercise5_Garage.Vehicles.VehicleBase;
using LexiconExercise5_GarageAssignment.ConsoleRelated;

namespace LexiconExercise5_Garage.Vehicles.VehicleFactories
{
	/// <summary>
	/// Handles the process of creating vehicles by gathering input from the user
	/// and delegating object creation to the appropriate vehicle factory methods.
	/// </summary>
	public class BuildVehicle
	{
		private const int _c_VEHICLE_WHEELS_MIN = 0;
		private const int _c_VEHICLE_WHEELS_MAX = 56;
		private const int _c_AIRPLANE_ENGINES_MIN = 0;
		private const int _c_AIRPLANE_ENGINES_MAX = 10;
		private const int _c_BUS_DOORS_MIN = 1;
		private const int _c_BUS_DOORS_MAX = 2;
		private const int _c_CAR_SEATS_MIN = 1;
		private const int _c_CAR_SEATS_MAX = 7;


		private readonly IVehicleFactory _vehicleFactory;
		private readonly ILicensePlateRegistry _licensePlateRegistry;
		private readonly IConsoleUI _consoleUI;


		/// <summary>
		/// Initializes a new instance of the <see cref="BuildVehicle"/> class.
		/// </summary>
		/// <param name="vehicleFactory">The factory responsible for constructing vehicle instances.</param>
		/// <param name="licensePlateRegistry">The registry used to validate and store license plates.</param>
		/// <param name="consoleUI">The UI interface for user interaction.</param>
		public BuildVehicle(
			IVehicleFactory vehicleFactory,
			ILicensePlateRegistry licensePlateRegistry,
			IConsoleUI consoleUI)
		{
			_vehicleFactory = vehicleFactory;
			_licensePlateRegistry = licensePlateRegistry;
			_consoleUI = consoleUI;
		}

		/// <summary>
		/// Builds a vehicle of the specified type based on user input.
		/// </summary>
		/// <param name="vehicle">An integer corresponding to a specific vehicle type.</param>
		/// <returns>The constructed vehicle instance.</returns>
		public IVehicle GetVehicle(int vehicle)
		{
			IVehicle builtVehicle = null!;

			string licensePlate = RegisterLicensePlateInput();

			_consoleUI.ShowFeedbackMessage($"Unique license plate {licensePlate} chosen!");

			int color = _consoleUI.RegisterInputFromEnumOptions<VehicleColor>(
				message: "What color is the vehicle: "
			);
			_consoleUI.ShowFeedbackMessage($"The color {(VehicleColor)color} was selected!");

			uint wheels = _consoleUI.RegisterNumericUintInput(
				message: "How many wheels does the vehicle have (0 to 56): ",
				rangeMin: _c_VEHICLE_WHEELS_MIN,
				rangeMax: _c_VEHICLE_WHEELS_MAX
			);

			_consoleUI.ShowFeedbackMessage($"{wheels} wheels chosen!");

			switch (vehicle)
			{
				case 1:
					builtVehicle = BuildAirPlain(licensePlate, (VehicleColor)color, wheels);
					break;
				case 2:
					builtVehicle = BuildBoat(licensePlate, (VehicleColor)color, wheels);
					break;
				case 3:
					builtVehicle = BuildBus(licensePlate, (VehicleColor)color, wheels);
					break;
				case 4:
					builtVehicle = BuildCar(licensePlate, (VehicleColor)color, wheels);
					break;
				case 5:
					builtVehicle = BuildMotorcycle(licensePlate, (VehicleColor)color, wheels);
					break;
				default:
					break;
			}

			_licensePlateRegistry.RegisterLicensePlate(licensePlate);

			return builtVehicle;
		}

		private string RegisterLicensePlateInput()
		{
			bool validLicensePlate = false;
			string licensePlate = string.Empty;

			do
			{
				try
				{
					licensePlate = _consoleUI.RegisterLicensePlateInput(
						message: "Supply a unique license plate input: "
					);

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
			uint numberOfEngines = _consoleUI.RegisterNumericUintInput(
				message: "How many Engines does the airplane have (0 to 10): ",
				rangeMin: _c_AIRPLANE_ENGINES_MIN,
				rangeMax: _c_AIRPLANE_ENGINES_MAX
			);

			_consoleUI.ShowFeedbackMessage($"{numberOfEngines} engines chosen!");

			return _vehicleFactory.CreateAirPlain(
				_licensePlateRegistry.IsValidLicensePlate,
				licensePlate,
				color,
				wheels,
				numberOfEngines
			);
		}

		private IVehicle BuildBoat(string licensePlate, VehicleColor color, uint wheels)
		{
			int fuelType = _consoleUI.RegisterInputFromEnumOptions<FuelType>(
				message: "What fuel does the boat use: "
			);
			_consoleUI.ShowFeedbackMessage($"The boat uses {fuelType}!");

			return _vehicleFactory.CreateBoat(
				_licensePlateRegistry.IsValidLicensePlate,
				licensePlate,
				color,
				wheels,
				(FuelType)fuelType
			);
		}

		private IVehicle BuildBus(string licensePlate, VehicleColor color, uint wheels)
		{
			uint nrOfDoors = _consoleUI.RegisterNumericUintInput(
				message: "How many doors does the bus have (1 or 2): ",
				rangeMin: _c_BUS_DOORS_MIN,
				rangeMax: _c_BUS_DOORS_MAX
			);

			_consoleUI.ShowFeedbackMessage($"The bus has {nrOfDoors} doors!");

			return _vehicleFactory.CreateBus(
				_licensePlateRegistry.IsValidLicensePlate,
				licensePlate,
				color,
				wheels,
				nrOfDoors
			);
		}

		private IVehicle BuildCar(string licensePlate, VehicleColor color, uint wheels)
		{
			uint nrOfSeats = _consoleUI.RegisterNumericUintInput(
				message: "How many seats does the car have (1 or 7): ",
				rangeMin: _c_CAR_SEATS_MIN,
				rangeMax: _c_CAR_SEATS_MAX
			);

			_consoleUI.ShowFeedbackMessage($"The car will have {nrOfSeats} seats");

			return _vehicleFactory.CreateCar(
				_licensePlateRegistry.IsValidLicensePlate,
				licensePlate,
				color,
				wheels,
				nrOfSeats
			);
		}

		private IVehicle BuildMotorcycle(string licensePlate, VehicleColor color, uint wheels)
		{
			bool hasSidecar = Convert.ToBoolean(
				_consoleUI.RegisterInputFromEnumOptions<YesNoEnum>(
					message: "Does the motorcycle have a sidecar?: "
				)
			);

			return _vehicleFactory.CreateMotorcycle(
				_licensePlateRegistry.IsValidLicensePlate,
				licensePlate,
				color,
				wheels,
				hasSidecar
			);
		}
	}
}