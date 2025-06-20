using LexiconExercise5_Garage.Vehicles.LicensePlate.Registry;
using LexiconExercise5_Garage.Vehicles.VehicleBase;

namespace LexiconExercise5_Garage.Garages.GarageFactory
{
	/// <summary>
	/// A concrete factory class responsible for creating mixed-type garages that store vehicles of type <typeparamref name="T"/>.
	/// </summary>
	/// <typeparam name="T">
	/// The type of vehicle to be stored in the garage. Must inherit from <see cref="IVehicle"/> 
	/// and implement <see cref="IGarageCreator{T}"/>.
	/// </typeparam>
	public class GarageMixedCreator<T> : IGarageCreator<T> where T : IVehicle
	{
		private readonly ILicensePlateRegistry _licensePlateRegistry;

		public GarageMixedCreator(ILicensePlateRegistry licensePlateRegistry)
		{
			_licensePlateRegistry = licensePlateRegistry;
		}

		/// <summary>
		/// Creates a new instance of <see cref="Garage{T}"/> with the specified size.
		/// </summary>
		/// <param name="size">The maximum number of vehicles the garage can hold.</param>
		/// <param name="licensePlateRegistry">Validates and stores a list of unique license plates</param>
		/// <returns>A new instance of <see cref="IGarage{T}"/>.</returns>
		public IGarage<T> CreateGarage(int size) => 
			new Garage<T>(size, _licensePlateRegistry);

	}
}