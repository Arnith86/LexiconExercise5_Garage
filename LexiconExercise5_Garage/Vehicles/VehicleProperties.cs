using System.ComponentModel;

namespace LexiconExercise5_Garage.Vehicles;

/// <summary>
/// Represents the available vehicle properties that can be used for display, filtering, or querying.
/// Each member is annotated with a <see cref="DescriptionAttribute"/> for user-friendly display text.
/// </summary>
public enum VehicleProperties
{
	/// <summary>
	/// The license plate identifier of the vehicle.
	/// </summary>
	[Description("License plate")]
	LicensePlate,

	/// <summary>
	/// The color of the vehicle.
	/// </summary>
	[Description("Color")]
	Color,

	/// <summary>
	/// The number of wheels on the vehicle.
	/// </summary>
	[Description("Wheels")]
	Wheels,

	/// <summary>
	/// The number of engines the vehicle has.
	/// </summary>
	[Description("Number of engines")]
	NumberOfEngines,

	/// <summary>
	/// The fuel type used by the vehicle (e.g., petrol, diesel, electric).
	/// </summary>
	[Description("Fuel type")]
	FuelType,

	/// <summary>
	/// The number of doors on the vehicle.
	/// </summary>
	[Description("Number of doors")]
	NumberOfDoors,

	/// <summary>
	/// The number of seats in the vehicle.
	/// </summary>
	[Description("Number of seats")]
	NumberOfSeats,

	/// <summary>
	/// Indicates whether the motorcycle has a sidecar.
	/// </summary>
	[Description("Has a sidecar")]
	HasSideCar
}