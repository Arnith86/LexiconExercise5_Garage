using LexiconExercise5_Garage.Vehicles;
using LexiconExercise5_Garage.Vehicles.VehicleBase;

namespace LexiconExercise5_Garage.Util;

/// <summary>
/// Provides reusable filtering functions for vehicles implementing <see cref="IVehicle"/>.
/// Supports filtering by type, wheel count, and color.
/// </summary>
/// <typeparam name="T">The vehicle type to compare against during filtering.</typeparam>
public class VehiclesFilterFunctions
{
	/// <summary>
	/// Creates a predicate that checks whether a vehicle is an instance of the specified <see cref="Type"/>.
	/// </summary>
	/// <param name="type">The <see cref="Type"/> to compare against.</param>
	/// <returns>A function that returns true if the vehicle is an instance of the specified type; otherwise, false.</returns>
	public Func<IVehicle, bool> VehicleTypePredicate(Type type) =>
		v => type.IsInstanceOfType(v);

	/// <summary>
	/// Creates a predicate that filters vehicles based on a wheel count comparison.
	/// </summary>
	/// <param name="comparison">A function that defines the comparison logic (e.g., x => x > 2).</param>
	/// <returns>A predicate that returns true if the vehicle's wheel count satisfies the comparison.</returns>
	public Func<IVehicle, bool> ByWheelCountPredicate(Func<int, bool> comparison) =>
		v => comparison((int)v.Wheels);

	/// <summary>
	/// Creates a predicate that filters vehicles by a specified color.
	/// </summary>
	/// <param name="desiredColor">The <see cref="VehicleColor"/> to filter by.</param>
	/// <returns>A predicate that returns true if the vehicle's color matches the desired color.</returns>
	public Func<IVehicle, bool> ByColorPredicate(VehicleColor desiredColor) =>
		v => v.Color == desiredColor;

	/// <summary>
	/// Creates a predicate function that checks if an integer is equal to the specified value.
	/// </summary>
	/// <param name="value">The value to compare against.</param>
	/// <returns>A function that returns true if the input equals <paramref name="value"/>, otherwise false.</returns>
	public Func<int, bool> EqualTo(int value) =>
		items => items == value;

	/// <summary>
	/// Creates a predicate function that checks if an integer is greater than the specified value.
	/// </summary>
	/// <param name="value">The value to compare against.</param>
	/// <returns>A function that returns true if the input is greater than <paramref name="value"/>, otherwise false.</returns>
	public Func<int, bool> GreaterThan(int value) =>
		items => items > value;

	/// <summary>
	/// Creates a predicate function that checks if an integer is less than the specified value.
	/// </summary>
	/// <param name="value">The value to compare against.</param>
	/// <returns>A function that returns true if the input is less than <paramref name="value"/>, otherwise false.</returns>
	public Func<int, bool> LessThan(int value) =>
		items => items < value;

	/// <summary>
	/// Returns a predicate function based on the specified numeric comparison option and target value.
	/// </summary>
	/// <param name="chosenOption">The comparison type to use (Equal, GreaterThan, or LessThan).</param>
	/// <param name="value">The value to compare against.</param>
	/// <returns>
	/// A function that takes an integer input and returns true if it satisfies the chosen comparison condition
	/// with respect to the specified <paramref name="value"/>.
	/// </returns>
	public Func<int, bool> WhichNumericComparePredicate(CompareOptions chosenOption, int value)
	{
		if (CompareOptions.Equal == chosenOption) 
			return EqualTo(value);
		else if (CompareOptions.GreaterThan == chosenOption) 
			return GreaterThan(value);
		
		return LessThan(value);
	}

	
}