namespace LexiconExercise5_Garage.Vehicles.VehicleBase
{
	public interface IVehicle
	{
		VehicleColor Color { get; }
		string LicensePlate { get; set; }
		uint Wheels { get; set; }

		string ToString();
	}
}