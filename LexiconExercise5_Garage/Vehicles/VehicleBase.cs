using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace LexiconExercise5_Garage.Vehicles;

/// <summary>
/// Abstract base class representing a general vehicle. 
/// Provides common functionality such as license plate validation, color, and wheel count constraints.
/// </summary>
public abstract class VehicleBase
{
	// Regular expression to validate license plate format (3 letters followed by 3 digits).
	private readonly Regex _validLicensePlateStructure = new Regex(@"^[a-zA-Z]{3}[0-9]{3}$");
	private string _licensePlate;
	private VehicleColor _color;
	private uint _wheels;

	/// <summary>
	/// Gets or sets the license plate value for the vehicle.
	/// The value must match the format 'AAA123' or 'aaa123'.
	/// </summary>
	/// <exception cref="ArgumentNullException">Thrown if the value is null or whitespace.</exception>
	/// <exception cref="ArgumentOutOfRangeException">Thrown if the value is not exactly six characters.</exception>
	/// <exception cref="ArgumentException">Thrown if the value does not match the expected format.</exception>

	public string LicensePlate
	{
		get => _licensePlate;

		[MemberNotNull(nameof(_licensePlate))]
		set
		{
			if (IsValidLicensePlate(value)) 
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
	/// <param name="licensePlate">The vehicle's license plate.</param>
	/// <param name="color">The color of the vehicle.</param>
	/// <param name="wheels">The number of wheels the vehicle has.</param>
	/// <exception cref="ArgumentNullException">Thrown if the license plate is null or whitespace.</exception>
	/// <exception cref="ArgumentOutOfRangeException">Thrown if the license plate has incorrect length or if wheels are out of range.</exception>
	/// <exception cref="ArgumentException">Thrown if the license plate has an invalid structure.</exception>
	public VehicleBase(string licensePlate, VehicleColor color, uint wheels)
	{
		LicensePlate = licensePlate;
		_color = color;
		Wheels = wheels;
	}

	// ToDo: extract to validation class
	private bool IsValidLicensePlate(string licensePlate)
	{
		if (string.IsNullOrWhiteSpace(licensePlate))
			throw new ArgumentNullException(nameof(licensePlate), "A license plate value must be provided.");
		if (licensePlate.Length != 6)
			throw new ArgumentOutOfRangeException(nameof(licensePlate), "A license plate value must contain exactly six characters.");
		if (!_validLicensePlateStructure.IsMatch(licensePlate))
			throw new ArgumentException(nameof(licensePlate), "A license plate value must only contain letters and digits in this format aaa123 or AAA123.");

		return true;
	}

	// ToDo: extract to validation class
	private bool IsWheelsValid(uint value)
	{
		if (value < 0 || value > 56)
			throw new ArgumentOutOfRangeException(nameof(value), "Must be within the range of 0 - 56.");

		return true;
	}
}
