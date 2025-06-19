using LexiconExercise5_Garage.Vehicles;
using LexiconExercise5_Garage.Vehicles.LicensePlate.Registry;

namespace LexiconExercise5_Garage.Garages.GarageFactory
{
	/// <summary>
	/// Defines a contract for creating instances of a garage for vehicles of type <typeparamref name="T"/>.
	/// </summary>
	/// <typeparam name="T">A type that inherits from <see cref="VehicleBase"/>.</typeparam>
	public interface IGarageCreator<T> where T : VehicleBase
	{
		/// <summary>
		/// Creates a new garage with the specified size limit.
		/// </summary>
		/// <param name="size">The maximum number of vehicles the garage can hold.</param>
		/// <returns>An instance of <see cref="IGarage{T}"/> configured with the given size.</returns>
		IGarage<T> CreateGarage(int size);
	}
}