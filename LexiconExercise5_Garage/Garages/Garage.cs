using LexiconExercise5_Garage.Vehicles;
using LexiconExercise5_Garage.Vehicles.Boats;
using LexiconExercise5_Garage.Vehicles.Buss;
using LexiconExercise5_Garage.Vehicles.Cars;
using LexiconExercise5_Garage.Vehicles.AirPlains;
using LexiconExercise5_Garage.Vehicles.Motorcycles;
using System.Collections;
using LexiconExercise5_Garage.Vehicles.LicensePlate.Registry;

namespace LexiconExercise5_Garage.Garages;


/// <summary>
/// Represents a generic garage that can store a collection of vehicles up to a specified limit or upper limit of 524288.
/// Manages capacity dynamically using an internal array with power-of-two sizing logic.
/// </summary>
/// <typeparam name="T">Types that inherit from VehicleBase.</typeparam>
public class Garage<T> : IEnumerable<T>, IGarage<T> where T : VehicleBase
{
	// Upper and lower limits for the garage size
	private const int _c_GARAGE_SIZE_UPPER_LIMIT = 524288;
	private const int _c_GARAGE_SIZE_LOWER_LIMIT = 1;
	private readonly ILicensePlateRegistry _licensePlateRegistry;
	private T[] _vehicles = new T[0];
	private int _capacity = 0;
	private int _garageVehicleLimit = 0;
	private int _usedSpaces = 0;

	/// <inheritdoc/>
	public int Capacity
	{
		get => _capacity;
		// Set expands the array size if the new capacity exceeds the current one.
		private set
		{
			if (IsWithinGarageMinMaxRange(value))
			{
				int newCapacity = FindArrayCapacitySize(value);

				if (IsCapacityToSmall(newCapacity)) ResizeVehicleArray(newCapacity);
			}
		}
	}

	/// <inheritdoc/>
	public int GarageVehicleLimit
	{
		get => _garageVehicleLimit;
		private set
		{
			if (IsWithinGarageMinMaxRange(value))
				_garageVehicleLimit = value;
		}
	}

	/// <inheritdoc/>
	public int UsedSpaces
	{
		get => _usedSpaces;
		private set => _usedSpaces = value;
	}


	/// <summary>
	/// Initializes a new instance of the <see cref="Garage{T}"/> class with a specified size limit.
	/// </summary>
	/// <param name="size">Initial size limit for the garage.</param>
	public Garage(int size, ILicensePlateRegistry licensePlateRegistry)
	{
		Capacity = size;
		_licensePlateRegistry = licensePlateRegistry;
		GarageVehicleLimit = size;
	}

	/// <summary>
	/// Resizes the internal array to the new capacity while preserving existing vehicles.
	/// </summary>
	private void ResizeVehicleArray(int newCapacity)
	{
		T[] tempArray = new T[newCapacity];
		Array.Copy(_vehicles, tempArray, _vehicles.Length);
		_vehicles = tempArray;
		_capacity = newCapacity;
	}

	private bool IsCapacityToSmall(int newCapacity)
	{
		return _capacity < newCapacity;
	}

	/// <summary>
	/// Calculates the array size as a power of two that is equal to or greater than the given size.
	/// Minimum capacity is 4.
	/// </summary>
	private int FindArrayCapacitySize(int size)
	{
		int result = 0;

		// Smallest sized array is 4
		if (size > 0 && size < 5)
		{
			result = 4;
		}
		else
		{
			int ArraySizePowerOfTwoUpperLimit = 2;

			// 2^19 = 524288
			for (int i = 2; i < 20; i++)
			{
				ArraySizePowerOfTwoUpperLimit = (int)Math.Pow(2, i);

				if (size <= ArraySizePowerOfTwoUpperLimit)
				{
					result = ArraySizePowerOfTwoUpperLimit;
					break;
				}
			}
		}

		return result;
	}

	private bool IsWithinGarageMinMaxRange(int size)
	{
		// ToDo: Catch and handle this exception.
		if (size < _c_GARAGE_SIZE_LOWER_LIMIT || size > _c_GARAGE_SIZE_UPPER_LIMIT)
			throw new ArgumentOutOfRangeException(nameof(size), "Garage size must be within the range of 1 - 524288.");

		return true;
	}

	/// <inheritdoc/>
	/// <exception cref="ArgumentOutOfRangeException">Thrown if garage is full.</exception>
	public bool AddVehicle(T vehicle)
	{
		// ToDo: catch and handle this exception.
		if (UsedSpaces == GarageVehicleLimit)
			throw new ArgumentOutOfRangeException(nameof(vehicle), "Garage is full.");

		// If next index is null, this is the optimal selection
		int nextIndex = UsedSpaces;

		// If nextIndex already has a value, that means that there is a chance that
		// an element has been removed earlier in the array index, this loop conducts
		// a search for the the first empty array index, and insert the vehicle there.
		if (IsSlotEmpty(nextIndex))
			nextIndex = Array.IndexOf(_vehicles, null);

		Capacity = nextIndex + 1;
		_vehicles[nextIndex] = vehicle;
		UsedSpaces++;

		return true;
	}

	private bool IsSlotEmpty(int nextIndex) =>
		_vehicles[nextIndex] != null;

	/// <inheritdoc/>
	public string? GetVehicleInformation(string licensePlate)
	{
		// ToDo: validate inputted licensePlate value

		return _vehicles
			.Where(vehicle => vehicle != null)  // Guards against null indexes. 
			.FirstOrDefault(vehicle =>
				String.Equals(
					a: vehicle.LicensePlate,
					b: licensePlate,
					comparisonType: StringComparison.OrdinalIgnoreCase // Ignores letter casing.
				)
			)?.ToString();
	}

	/// <inheritdoc/>
	public IEnumerable<string> GetAllVehiclesInformation()
	{
		return _vehicles.Where(vehicle => vehicle != null)
			.Select(vehicle => vehicle.ToString());
	}

	/// <inheritdoc/>
	public T? RemoveVehicle(string licensePlate)
	{
		// ToDo: validate inputted licensePlate value

		int index = Array.FindIndex(_vehicles, vehicle =>
			String.Equals(
				a: licensePlate,
				b: vehicle.LicensePlate,
				comparisonType: StringComparison.OrdinalIgnoreCase
			)
		);

		// Array.FindIndex returns -1, if not found.
		if (index >= 0)
		{
			T returnVehicle = _vehicles[index];

			_vehicles[index] = null!;
			UsedSpaces--;

			return returnVehicle;
		}

		return null;
	}

	/// <inheritdoc/>
	public IEnumerator<T> GetEnumerator()
	{
		// return a single value, then yield until next operation 
		foreach (var vehicle in _vehicles)
		{
			if (vehicle != null)
				yield return vehicle;
		}

		//return ((IEnumerable<T>)_vehicles).GetEnumerator();
	}
	
	/// <inheritdoc/>
	IEnumerator IEnumerable.GetEnumerator()
	{
		return GetEnumerator();
	}

	/// <inheritdoc/>
	public void Add40VehiclesToCollection()
	{
		

		List<VehicleBase> collectionOfDifferentVehicles = new List<VehicleBase>()
		{
			new AirPlain(_licensePlateRegistry.IsValidLicensePlate, "abc111", VehicleColor.Red, 4, 2),
			new AirPlain(_licensePlateRegistry.IsValidLicensePlate, "abc112", VehicleColor.Blue, 6, 4),
			new AirPlain(_licensePlateRegistry.IsValidLicensePlate, "abc113", VehicleColor.Green, 8, 4),
			new AirPlain(_licensePlateRegistry.IsValidLicensePlate, "abc114", VehicleColor.Yellow, 6, 2),
			new AirPlain(_licensePlateRegistry.IsValidLicensePlate, "abc115", VehicleColor.Red, 8, 1),
			new AirPlain(_licensePlateRegistry.IsValidLicensePlate, "abc116", VehicleColor.Blue, 12, 1),
			new AirPlain(_licensePlateRegistry.IsValidLicensePlate, "abc117", VehicleColor.Green, 6, 3),
			new AirPlain(_licensePlateRegistry.IsValidLicensePlate, "abc118", VehicleColor.Red, 9, 3),

			new Boat(_licensePlateRegistry.IsValidLicensePlate, "abc119", VehicleColor.Red, 0, FuelType.Diesel),
			new Boat(_licensePlateRegistry.IsValidLicensePlate, "abc120", VehicleColor.Green, 0, FuelType.Gasoline),
			new Boat(_licensePlateRegistry.IsValidLicensePlate, "abc121", VehicleColor.Blue, 0, FuelType.Gasoline),
			new Boat(_licensePlateRegistry.IsValidLicensePlate, "abc122", VehicleColor.Yellow, 0, FuelType.Electric),
			new Boat(_licensePlateRegistry.IsValidLicensePlate, "abc123", VehicleColor.Blue, 0, FuelType.None),
			new Boat(_licensePlateRegistry.IsValidLicensePlate, "abc124", VehicleColor.Red, 0, FuelType.None),
			new Boat(_licensePlateRegistry.IsValidLicensePlate, "abc125", VehicleColor.Green, 0, FuelType.Diesel),
			new Boat(_licensePlateRegistry.IsValidLicensePlate, "abc126", VehicleColor.Green, 0, FuelType.Gasoline),

			new Bus(_licensePlateRegistry.IsValidLicensePlate, "abc127", VehicleColor.Green, 4, 1),
			new Bus(_licensePlateRegistry.IsValidLicensePlate, "abc128", VehicleColor.Red, 6, 2),
			new Bus(_licensePlateRegistry.IsValidLicensePlate, "abc129", VehicleColor.Red, 6, 2),
			new Bus(_licensePlateRegistry.IsValidLicensePlate, "abc130", VehicleColor.Green, 4, 1),
			new Bus(_licensePlateRegistry.IsValidLicensePlate, "abc131", VehicleColor.Blue, 8, 2),
			new Bus(_licensePlateRegistry.IsValidLicensePlate, "abc132", VehicleColor.Blue, 4, 2),
			new Bus(_licensePlateRegistry.IsValidLicensePlate, "abc133", VehicleColor.Yellow, 12, 2),
			new Bus(_licensePlateRegistry.IsValidLicensePlate, "abc134", VehicleColor.Yellow, 12, 2),

			new Car(_licensePlateRegistry.IsValidLicensePlate, "abc135", VehicleColor.Red, 4, 5),
			new Car(_licensePlateRegistry.IsValidLicensePlate, "abc136", VehicleColor.Red, 4, 6),
			new Car(_licensePlateRegistry.IsValidLicensePlate, "abc137", VehicleColor.Green, 4, 6),
			new Car(_licensePlateRegistry.IsValidLicensePlate, "abc138", VehicleColor.Blue, 4, 7),
			new Car(_licensePlateRegistry.IsValidLicensePlate, "abc139", VehicleColor.Blue, 4, 7),
			new Car(_licensePlateRegistry.IsValidLicensePlate, "abc140", VehicleColor.Yellow, 4, 2),
			new Car(_licensePlateRegistry.IsValidLicensePlate, "abc141", VehicleColor.Yellow, 4, 2),
			new Car(_licensePlateRegistry.IsValidLicensePlate, "abc142", VehicleColor.Green, 4, 5),

			new Motorcycle(_licensePlateRegistry.IsValidLicensePlate, "abc143", VehicleColor.Red, 2, false),
			new Motorcycle(_licensePlateRegistry.IsValidLicensePlate, "abc144", VehicleColor.Red, 2, false),
			new Motorcycle(_licensePlateRegistry.IsValidLicensePlate, "abc145", VehicleColor.Green, 2, false),
			new Motorcycle(_licensePlateRegistry.IsValidLicensePlate, "abc146", VehicleColor.Blue, 2, false),
			new Motorcycle(_licensePlateRegistry.IsValidLicensePlate, "abc147", VehicleColor.Blue, 2, true),
			new Motorcycle(_licensePlateRegistry.IsValidLicensePlate, "abc148", VehicleColor.Yellow, 2, true),
			new Motorcycle(_licensePlateRegistry.IsValidLicensePlate, "abc149", VehicleColor.Yellow, 2, true),
			new Motorcycle(_licensePlateRegistry.IsValidLicensePlate, "abc150", VehicleColor.Green, 2, true),
		};

		foreach (var vehicle in collectionOfDifferentVehicles)
		{
			AddVehicle((T)vehicle);
		}
	}

}
