using LexiconExercise5_Garage.Vehicles;

namespace LexiconExercise5_Garage.Garages.GarageFactory
{
	/// <summary>
	/// A concrete factory class responsible for creating mixed-type garages that store vehicles of type <typeparamref name="T"/>.
	/// </summary>
	/// <typeparam name="T">
	/// The type of vehicle to be stored in the garage. Must inherit from <see cref="VehicleBase"/> 
	/// and implement <see cref="IGarageCreator{T}"/>.
	/// </typeparam>
	public class GarageMixedCreator<T> where T : VehicleBase, IGarageCreator<T>
	{
		/// <summary>
		/// Creates a new instance of <see cref="Garage{T}"/> with the specified size.
		/// </summary>
		/// <param name="size">The maximum number of vehicles the garage can hold.</param>
		/// <returns>A new instance of <see cref="IGarage{T}"/>.</returns>
		public IGarage<T> CreateGarage(int size) => new Garage<T>(size);
	}
}