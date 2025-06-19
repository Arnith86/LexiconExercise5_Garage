using LexiconExercise5_Garage.Vehicles.AirPlains;
using LexiconExercise5_Garage.Vehicles.Boats;
using LexiconExercise5_Garage.Vehicles.Buss;
using LexiconExercise5_Garage.Vehicles.Cars;
using LexiconExercise5_Garage.Vehicles.Motorcycles;
using LexiconExercise5_Garage.Vehicles.VehicleBase;

namespace LexiconExercise5_Garage.Vehicles.VehicleFactories;

/// <summary>
/// The <c>VehicleFactory</c> class provides factory methods for creating various types of vehicles, 
/// including airplanes, boats, buses, cars, and motorcycles.
/// Each method returns a <see cref="VehicleBase"/> object and accepts common vehicle parameters, 
/// such as license plate, color, and number of wheels, along with the specific property required 
/// for that vehicle type.
/// 
/// This centralized factory simplifies vehicle creation and enforces consistent instantiation logic 
/// across different vehicle types.
/// </summary>
public class VehicleFactory : IVehicleFactory
{
	/// <inheritdoc/>
	public IVehicle CreateAirPlain(
		Func<string, bool> licensePlateValidator,
		string licensePlate,
		VehicleColor color,
		uint wheels,
		uint numberOfEngines)
	{
		return new AirPlain(
			licensePlateValidator,
			licensePlate,
			color,
			wheels,
			numberOfEngines
		);
	}

	/// <inheritdoc/>
	public IVehicle CreateBoat(
		Func<string, bool> licensePlateValidator,
		string licensePlate,
		VehicleColor color,
		uint wheels,
		FuelType fuelType)
	{
		return new Boat(
			licensePlateValidator,
			licensePlate,
			color,
			wheels,
			fuelType
		);
	}

	/// <inheritdoc/>
	public IVehicle CreateBus(
		Func<string, bool> licensePlateValidator,
		string licensePlate,
		VehicleColor color,
		uint wheels,
		uint nrOfDoors)
	{
		return new Bus(
			licensePlateValidator,
			licensePlate,
			color,
			wheels,
			nrOfDoors
		);
	}

	/// <inheritdoc/>
	public IVehicle CreateCar(
		Func<string, bool> licensePlateValidator,
		string licensePlate,
		VehicleColor color,
		uint wheels,
		uint nrOfSeats)
	{
		return new Car(
			licensePlateValidator,
			licensePlate,
			color,
			wheels,
			nrOfSeats
		);
	}

	/// <inheritdoc/>
	public IVehicle CreateMotorcycle(
		Func<string, bool> licensePlateValidator,
		string licensePlate,
		VehicleColor color,
		uint wheels,
		bool hasSidecar)
	{
		return new Motorcycle(
			licensePlateValidator,
			licensePlate,
			color,
			wheels,
			hasSidecar
		);
	}
}
