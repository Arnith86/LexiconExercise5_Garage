using LexiconExercise5_Garage.Vehicles.AirPlains;
using LexiconExercise5_Garage.Vehicles.Boats;
using LexiconExercise5_Garage.Vehicles.Buss;
using LexiconExercise5_Garage.Vehicles.Cars;
using LexiconExercise5_Garage.Vehicles.Motorcycles;

namespace LexiconExercise5_Garage.Vehicles.VehicleFactories
{
	/// <summary>
	/// Defines a factory interface for creating various types of vehicles.
	/// Each method validates the license plate and returns a specific vehicle instance.
	/// </summary>
	public interface IVehicleFactory
	{
		/// <summary>
		/// Creates a new airplane instance.
		/// </summary>
		/// <param name="licensePlateValidator">Function to validate the license plate's format and uniqueness.</param>
		/// <param name="licensePlate">The license plate to assign to the airplane.</param>
		/// <param name="color">The color of the airplane.</param>
		/// <param name="wheels">The number of wheels on the airplane.</param>
		/// <param name="numberOfEngines">The number of engines the airplane has.</param>
		/// <returns>A new <see cref="IAirPlain"/> instance representing an airplane.</returns>
		IAirPlain CreateAirPlain(
			Func<string, bool> licensePlateValidator,
			string licensePlate,
			VehicleColor color,
			uint wheels,
			uint numberOfEngines
		);

		/// <summary>
		/// Creates a new boat instance.
		/// </summary>
		/// <param name="licensePlateValidator">Function to validate the license plate's format and uniqueness.</param>
		/// <param name="licensePlate">The license plate to assign to the boat.</param>
		/// <param name="color">The color of the boat.</param>
		/// <param name="wheels">The number of wheels on the boat (e.g., trailer).</param>
		/// <param name="fuelType">The fuel type used by the boat.</param>
		/// <returns>A new <see cref="IBoat"/> instance representing a boat.</returns>
		IBoat CreateBoat(
			Func<string, bool> licensePlateValidator,
			string licensePlate,
			VehicleColor color,
			uint wheels,
			FuelType fuelType
		);

		/// <summary>
		/// Creates a new bus instance.
		/// </summary>
		/// <param name="licensePlateValidator">Function to validate the license plate's format and uniqueness.</param>
		/// <param name="licensePlate">The license plate to assign to the bus.</param>
		/// <param name="color">The color of the bus.</param>
		/// <param name="wheels">The number of wheels on the bus.</param>
		/// <param name="nrOfDoors">The number of doors on the bus.</param>
		/// <returns>A new <see cref="IBus"/> instance representing a bus.</returns>
		IBus CreateBus(
			Func<string, bool> licensePlateValidator,
			string licensePlate,
			VehicleColor color,
			uint wheels,
			uint nrOfDoors
		);

		/// <summary>
		/// Creates a new car instance.
		/// </summary>
		/// <param name="licensePlateValidator">Function to validate the license plate's format and uniqueness.</param>
		/// <param name="licensePlate">The license plate to assign to the car.</param>
		/// <param name="color">The color of the car.</param>
		/// <param name="wheels">The number of wheels on the car.</param>
		/// <param name="nrOfSeats">The number of seats in the car.</param>
		/// <returns>A new <see cref="ICar"/> instance representing a car.</returns>
		ICar CreateCar(
			Func<string, bool> licensePlateValidator,
			string licensePlate,
			VehicleColor color,
			uint wheels,
			uint nrOfSeats
		);

		/// <summary>
		/// Creates a new motorcycle instance.
		/// </summary>
		/// <param name="licensePlateValidator">Function to validate the license plate's format and uniqueness.</param>
		/// <param name="licensePlate">The license plate to assign to the motorcycle.</param>
		/// <param name="color">The color of the motorcycle.</param>
		/// <param name="wheels">The number of wheels on the motorcycle.</param>
		/// <param name="hasSidecar">Indicates whether the motorcycle has a sidecar attached.</param>
		/// <returns>A new <see cref="IMotorcycle"/> instance representing a motorcycle.</returns>
		IMotorcycle CreateMotorcycle(
			Func<string, bool> licensePlateValidator,
			string licensePlate,
			VehicleColor color,
			uint wheels,
			bool hasSidecar
		);
	}
}
