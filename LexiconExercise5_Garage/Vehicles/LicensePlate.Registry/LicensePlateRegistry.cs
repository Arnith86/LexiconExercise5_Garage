using System.Text.RegularExpressions;

namespace LexiconExercise5_Garage.Vehicles.LicensePlate.Registry;

/// <summary>
/// Handles validation and registration of vehicle license plates,
/// ensuring format correctness and uniqueness across the application.
/// </summary>
public class LicensePlateRegistry : ILicensePlateRegistry
{
	// Regular expression to validate license plate format (3 letters followed by 3 digits).
	private readonly Regex _validLicensePlateStructure = new Regex(@"^[a-zA-Z]{3}[0-9]{3}$");
	
	// Todo: extract to a file or SQLite. 
	protected static HashSet<string> RegisteredLicensePlates = new HashSet<string>();

	///<inheritdoc/>
	/// <exception cref="ArgumentNullException">Thrown if the license plate is null or whitespace.</exception>
	/// <exception cref="ArgumentOutOfRangeException">Thrown if the license plate is not exactly six characters.</exception>
	/// <exception cref="ArgumentException">Thrown if the license plate format does not match the expected pattern.</exception>
	/// <exception cref="InvalidOperationException">Thrown if the license plate is already registered.</exception>
	public bool IsValidLicensePlate(string licensePlate)
	{
		if (string.IsNullOrWhiteSpace(licensePlate))
			throw new ArgumentNullException(nameof(licensePlate), "A license plate value must be provided.");

		if (licensePlate.Length != 6)
			throw new ArgumentOutOfRangeException(nameof(licensePlate), "A license plate value must contain exactly six characters.");

		if (!_validLicensePlateStructure.IsMatch(licensePlate))
			throw new ArgumentException(nameof(licensePlate), "A license plate value must only contain letters and digits in this format aaa123 or AAA123.");

		if (!IsUniqueLicensePlate(licensePlate))
			throw new InvalidOperationException("License plate already exists.");

		RegisterLicensePlate(licensePlate);

		return true;
	}

	// ToDo: use contains instead. more efficient 
	private bool IsUniqueLicensePlate(string licensePlate) =>
		!RegisteredLicensePlates.Any(l => l == licensePlate);

	private void RegisterLicensePlate(string licensePlate)
	{
		RegisteredLicensePlates.Add(licensePlate);
	}
}
