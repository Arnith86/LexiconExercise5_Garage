
namespace LexiconExercise5_Garage.Vehicles.Boat
{
	/// <summary>
	/// Defines a contract for boat-specific properties in the garage system.
	/// </summary>
	public interface IBoat
	{
		/// <summary>
		/// Gets the type of fuel the boat uses (e.g., Diesel, Electric, None).
		/// </summary>
		FuelType FuelType { get; }
	}
}