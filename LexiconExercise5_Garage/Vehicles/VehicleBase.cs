using System.Diagnostics.CodeAnalysis;

namespace LexiconExercise5_Garage.Vehicles;

/// <summary>
/// Abstract base class representing a general vehicle. 
/// Provides common functionality such as license plate validation, color, and wheel count constraints.
/// </summary>
public abstract class VehicleBase
{
	private string _licensePlate;
	private readonly Func<string, bool> _licensePlateValidator;
	private VehicleColor _color;
	private uint _wheels;

	/// <summary>
	/// Gets or sets the license plate value for the vehicle.
	/// The value must match the format 'AAA123' or 'aaa123'.
	/// </summary>
	/// <exception cref="ArgumentNullException">Thrown if the value is null or whitespace.</exception>
	/// <exception cref="ArgumentOutOfRangeException">Thrown if the value is not exactly six characters.</exception>
	/// <exception cref="ArgumentException">Thrown if the value does not match the expected format.</exception>
	/// <exception cref="InvalidOperationException">Thrown if the value already exist in registry.</exception>
	public string LicensePlate
	{
		get => _licensePlate;

		[MemberNotNull(nameof(_licensePlate))]
		set
		{
			if (_licensePlateValidator(value))
				_licensePlate = value;
		}
	}

	/// <summary>
	/// Gets the color of the vehicle.
	/// </summary>
	public VehicleColor Color => _color;

	/// <summary>
	/// Gets or sets the number of wheels the vehicle has.
	/// Must be between 0 and 56.
	/// </summary>
	/// <exception cref="ArgumentOutOfRangeException">Thrown if the value is outside the range 0–56.</exception>
	public uint Wheels
	{
		get => _wheels;
		set
		{
			if (IsWheelsValid(value))
				_wheels = value;
		}
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="VehicleBase"/> class with specified license plate, color, and wheels.
	/// </summary>
	/// <param name="licensePlateValidator">Validates license plates entries</param>
	/// <param name="licensePlate">The vehicle's license plate.</param>
	/// <param name="color">The color of the vehicle.</param>
	/// <param name="wheels">The number of wheels the vehicle has.</param>
	/// <exception cref="ArgumentNullException">Thrown if the license plate is null or whitespace.</exception>
	/// <exception cref="ArgumentOutOfRangeException">Thrown if the license plate has incorrect length or if wheels are out of range.</exception>
	/// <exception cref="ArgumentException">Thrown if the license plate has an invalid structure.</exception>
	public VehicleBase(
		Func<string, bool> licensePlateValidator,
		string licensePlate,
		VehicleColor color,
		uint wheels)
	{
		_licensePlateValidator = licensePlateValidator;
		LicensePlate = licensePlate;
		_color = color;
		Wheels = wheels;
	}

	// ToDo: extract to validation class
	private bool IsWheelsValid(uint value)
	{
		if (value < 0 || value > 56)
			throw new ArgumentOutOfRangeException(nameof(value), "Must be within the range of 0 - 56.");

		return true;
	}

	/// <summary>
	/// Each type of vehicle will have separate properties that need to be printed.
	/// </summary>
	/// <returns>A string representing the vehicle types property.</returns>
	public abstract override string ToString();
	
}
