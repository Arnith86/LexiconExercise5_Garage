using LexiconExercise5_Garage.Vehicles;
using System.Collections;


namespace LexiconExercise5_Garage.Garages;

public class Garage<T> : IEnumerable<T> where T : VehicleBase
{
	// Upper limit for the garage size
	private const int _c_ARRAY_SIZE_LIMIT = 524288;
	private T[] _vehicles;
	private int _capacity = 0;

	public int Capacity
	{ 
		get => _capacity;
		private set 
		{
			if (IsValidGarageSize(value))
			{
				_capacity = FindArrayCapacitySize(value);
				_vehicles = new T[_capacity];
			}
		}
	}


	// ToDo: Limit on size ? 
	public Garage(int size)
	{
		Capacity = size;
	}

	private int FindArrayCapacitySize(int size)
	{
		int result = 0;

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

	private bool IsValidGarageSize(int size)
	{
		// ToDo: Catch and handle this exception.
		if (size < 1 || size > _c_ARRAY_SIZE_LIMIT)
			throw new ArgumentOutOfRangeException(nameof(size), "Garage size must be within the range of 1 - 524288.");
		
		return true;
	}

	public IEnumerator<T> GetEnumerator()
	{
		// return a single value, then yield until next operation 
		foreach (var vehicle in _vehicles)
		{
			if ( vehicle != null )
				yield return vehicle;	
		}
		
		//return ((IEnumerable<T>)_vehicles).GetEnumerator();
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return GetEnumerator();
	}
}
