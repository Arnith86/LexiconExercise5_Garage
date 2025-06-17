using LexiconExercise5_Garage.Vehicles;
using LexiconExercise5_Garage.Vehicles.Boat;
using LexiconExercise5_Garage.Vehicles.Bus;
using LexiconExercise5_Garage.Vehicles.Cars;
using LexiconExercise5_Garage.Vehicles.FlyingVehicles;
using LexiconExercise5_Garage.Vehicles.Motorcycle;
using System.Collections;

namespace LexiconExercise5_Garage.Garages;


/// <summary>
/// Represents a generic garage that can store a collection of vehicles up to a specified limit or upper limit of 524288.
/// Manages capacity dynamically using an internal array with power-of-two sizing logic.
/// </summary>
/// <typeparam name="T">Types that inherit from VehicleBase.</typeparam>
public class Garage<T> : IEnumerable<T> where T : VehicleBase
{
	// Upper and lower limits for the garage size
	private const int _c_GARAGE_SIZE_UPPER_LIMIT = 524288;
	private const int _c_GARAGE_SIZE_LOWER_LIMIT = 1;

	private T[] _vehicles = new T[0];
	private int _capacity = 0;
	private int _garageVehicleLimit = 0;
	private int _usedSpaces = 0;

	/// <summary>
	/// Gets the current capacity of the garage's internal array.
	/// Expands the array size if the new capacity exceeds the current one.
	/// </summary>
	public int Capacity
	{
		get => _capacity;
		private set
		{
			if (IsWithinGarageMinMaxRange(value))
			{
				int newCapacity = FindArrayCapacitySize(value);

				if (IsCapacityToSmall(newCapacity)) ResizeVehicleArray(newCapacity);
			}
		}
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

	/// <summary>
	/// Gets the maximum number of vehicles that can be added to the garage.
	/// </summary>
	public int GarageVehicleLimit
	{
		get => _garageVehicleLimit;
		private set
		{
			if (IsWithinGarageMinMaxRange(value))
				_garageVehicleLimit = value;
		}
	}

	/// <summary>
	/// Gets the number of currently occupied spaces in the garage.
	/// </summary>
	public int UsedSpaces
	{
		get => _usedSpaces;
		private set => _usedSpaces = value;
	}


	/// <summary>
	/// Initializes a new instance of the <see cref="Garage{T}"/> class with a specified size limit.
	/// </summary>
	/// <param name="size">Initial size limit for the garage.</param>
	public Garage(int size)
	{
		Capacity = size;
		GarageVehicleLimit = size;
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

	/// <summary>
	/// Adds a vehicle to the garage.
	/// Automatically resizes the internal array if necessary.
	/// </summary>
	/// <param name="vehicle">The vehicle to add.</param>
	/// <returns>True if the vehicle was successfully added.</returns>
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


	/// <summary>
	/// Checks if specified <see cref="VehicleBase"/> with <c>LicensePlate</c> can be found in the garage.
	/// And returns a string representation of the vehicle.
	/// </summary>
	/// <param name="licensePlate">String representing the license plate of a <see cref="VehicleBase"/>.</param>
	/// <returns>A string representation of the vehicle if found, otherwise null.</returns>
	public string? ShowVehicle(string licensePlate)
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

	/// <summary>
	/// Removes and returns specified vehicle from the garage.
	/// </summary>
	/// <param name="licensePlate">Unique identifier of the vehicle to be removed.</param>
	/// <returns>A <see cref="T"/> if found, otherwise null.</returns>
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

	/// <summary>
	/// Returns an enumerator that iterates through the garage's stored vehicles.
	/// </summary>
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

	IEnumerator IEnumerable.GetEnumerator()
	{
		return GetEnumerator();
	}

	/// <summary>
	/// Fills the garage with vehicles of every kind with different properties.
	/// </summary>
	public void Add40VehiclesToCollection()
	{
		List<VehicleBase> collectionOfDifferentVehicles = new List<VehicleBase>()
		{
			new AirPlain(LPR => true, "abc111", VehicleColor.Red, 4, 2),
			new AirPlain(LPR => true, "abc112", VehicleColor.Blue, 6, 4),
			new AirPlain(LPR => true, "abc113", VehicleColor.Green, 8, 4),
			new AirPlain(LPR => true, "abc114", VehicleColor.Yellow, 6, 2),
			new AirPlain(LPR => true, "abc115", VehicleColor.Red, 8, 1),
			new AirPlain(LPR => true, "abc116", VehicleColor.Blue, 12, 1),
			new AirPlain(LPR => true, "abc117", VehicleColor.Green, 6, 3),
			new AirPlain(LPR => true, "abc118", VehicleColor.Red, 9, 3),

			new Boat(LPR => true, "abc119", VehicleColor.Red, 0, FuelType.Diesel),
			new Boat(LPR => true, "abc120", VehicleColor.Green, 0, FuelType.Gasoline),
			new Boat(LPR => true, "abc121", VehicleColor.Blue, 0, FuelType.Gasoline),
			new Boat(LPR => true, "abc122", VehicleColor.Yellow, 0, FuelType.Electric),
			new Boat(LPR => true, "abc123", VehicleColor.Blue, 0, FuelType.None),
			new Boat(LPR => true, "abc124", VehicleColor.Red, 0, FuelType.None),
			new Boat(LPR => true, "abc125", VehicleColor.Green, 0, FuelType.Diesel),
			new Boat(LPR => true, "abc126", VehicleColor.Green, 0, FuelType.Gasoline),

			new Bus(LPR => true, "abc127", VehicleColor.Green, 4, 1),
			new Bus(LPR => true, "abc128", VehicleColor.Red, 6, 2),
			new Bus(LPR => true, "abc129", VehicleColor.Red, 6, 2),
			new Bus(LPR => true, "abc130", VehicleColor.Green, 4, 1),
			new Bus(LPR => true, "abc131", VehicleColor.Blue, 8, 2),
			new Bus(LPR => true, "abc132", VehicleColor.Blue, 4, 2),
			new Bus(LPR => true, "abc133", VehicleColor.Yellow, 12, 2),
			new Bus(LPR => true, "abc134", VehicleColor.Yellow, 12, 2),

			new Car(LPR => true, "abc135", VehicleColor.Red, 4, 5),
			new Car(LPR => true, "abc136", VehicleColor.Red, 4, 6),
			new Car(LPR => true, "abc137", VehicleColor.Green, 4, 6),
			new Car(LPR => true, "abc138", VehicleColor.Blue, 4, 7),
			new Car(LPR => true, "abc139", VehicleColor.Blue, 4, 7),
			new Car(LPR => true, "abc140", VehicleColor.Yellow, 4, 2),
			new Car(LPR => true, "abc141", VehicleColor.Yellow, 4, 2),
			new Car(LPR => true, "abc142", VehicleColor.Green, 4, 5),

			new Motorcycle(LPR => true, "abc143", VehicleColor.Red, 2, false),
			new Motorcycle(LPR => true, "abc144", VehicleColor.Red, 2, false),
			new Motorcycle(LPR => true, "abc145", VehicleColor.Green, 2, false),
			new Motorcycle(LPR => true, "abc146", VehicleColor.Blue, 2, false),
			new Motorcycle(LPR => true, "abc147", VehicleColor.Blue, 2, true),
			new Motorcycle(LPR => true, "abc148", VehicleColor.Yellow, 2, true),
			new Motorcycle(LPR => true, "abc149", VehicleColor.Yellow, 2, true),
			new Motorcycle(LPR => true, "abc150", VehicleColor.Green, 2, true),
		};

		foreach (var vehicle in collectionOfDifferentVehicles)
		{
			AddVehicle((T)vehicle);
		}
	}

}
