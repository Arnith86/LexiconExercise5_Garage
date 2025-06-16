using LexiconExercise5_Garage.Vehicles;
using System.Collections;
using System.Threading.Channels;

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
	/// <exception cref="IndexOutOfRangeException">Thrown if garage is full.</exception>
	public bool AddVehicle(T vehicle)
	{
		// ToDo: catch and handle this exception.
		if (UsedSpaces == _c_GARAGE_SIZE_UPPER_LIMIT || UsedSpaces == GarageVehicleLimit)
			throw new IndexOutOfRangeException(nameof(vehicle));

		int nextIndex = UsedSpaces;
		
		Capacity = nextIndex + 1;
		_vehicles[nextIndex] = vehicle;
		UsedSpaces++;

		return true;
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
}
