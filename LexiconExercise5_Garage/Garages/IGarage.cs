using LexiconExercise5_Garage.Vehicles.VehicleBase;

namespace LexiconExercise5_Garage.Garages
{
	/// <summary>
	/// Defines the contract for a garage that stores vehicles of type <typeparamref name="T"/>.
	/// </summary>
	/// <typeparam name="T">A type that inherits from <see cref="IVehicle"/>.</typeparam>
	public interface IGarage<T> where T : IVehicle
	{
		/// <summary>
		/// Gets the current capacity of the internal array used to store vehicles.
		/// </summary>
		int Capacity { get; }

		/// <summary>
		/// Gets the maximum number of vehicles that the garage can hold.
		/// </summary>
		int GarageVehicleLimit { get; }

		/// <summary>
		/// Gets the number of currently used spaces in the garage.
		/// </summary>
		int UsedSpaces { get; }

		/// <summary>
		/// Adds a vehicle to the garage.
		/// </summary>
		/// <param name="vehicle">The vehicle to add.</param>
		/// <returns>True if the vehicle was successfully added; otherwise, false.</returns>
		bool AddVehicle(T vehicle);

		/// <summary>
		/// Returns an enumerator that iterates through the vehicles in the garage.
		/// </summary>
		/// <returns>An enumerator for the vehicle collection.</returns>
		IEnumerator<T> GetEnumerator();

		/// <summary>
		/// Retrieves detailed information about a specific vehicle based on its license plate.
		/// </summary>
		/// <param name="licensePlate">The license plate of the vehicle.</param>
		/// <returns>A string representation of the vehicle if found; otherwise, null.</returns>
		string? GetVehicleInformation(string licensePlate);

		/// <summary>
		/// Removes and returns a vehicle from the garage based on its license plate.
		/// </summary>
		/// <param name="licensePlate">The license plate of the vehicle to remove.</param>
		/// <returns>The removed vehicle if found; otherwise, null.</returns>
		T? RemoveVehicle(string licensePlate);

		/// <summary>
		/// Fills the garage with a predefined set of 40 sample vehicles for testing or demonstration purposes.
		/// </summary>
		void Add40VehiclesToCollection();

		/// <summary>
		/// Executes a LINQ query on the internal vehicle collection, excluding null entries, and returns the result.
		/// </summary>
		/// <typeparam name="TResult">The type of the result elements returned by the query.</typeparam>
		/// <param name="query">
		/// A function that defines the LINQ query to perform on the non-null elements of the internal collection.
		/// </param>
		/// <returns>
		/// An <see cref="IEnumerable{TResult}"/> containing the results of the query.
		/// </returns>
		public IEnumerable<TResult> PerformedLinqQuery<TResult>(
			Func<
				IEnumerable<T>,
				IEnumerable<TResult>
				> query);
	}
}