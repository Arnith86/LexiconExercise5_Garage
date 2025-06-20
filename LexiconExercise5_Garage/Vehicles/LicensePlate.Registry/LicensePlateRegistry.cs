using System.Text.Json;
using System.Text.RegularExpressions;

namespace LexiconExercise5_Garage.Vehicles.LicensePlate.Registry;

// Todo: create test for this class as well, even though all aspects are tested with the vehicle classes?

/// <summary>
/// Handles validation and registration of vehicle license plates,
/// ensuring format correctness and uniqueness across the application.
/// License plates are persisted to a local file.
/// </summary>
public class LicensePlateRegistry : ILicensePlateRegistry
{
	// Regular expression to validate license plate format (3 letters followed by 3 digits).
	private readonly Regex _validLicensePlateStructure = new Regex(@"^[a-zA-Z]{3}[0-9]{3}$");
	private readonly string _storageFilePath;
	
	protected static HashSet<string> RegisteredLicensePlates = new HashSet<string>();

	public LicensePlateRegistry(string? storageFilePath = null)
	{
		_storageFilePath = storageFilePath ?? "licensePlates.json";
		LoadLicensePlateFromFile();
	}


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

		//if (!IsUniqueLicensePlate(licensePlate))
		//	throw new InvalidOperationException("License plate already exists.");

		//RegisterLicensePlateInput(licensePlate);

		return true;
	}

	// ToDo: use contains instead. more efficient 
	/// <inheritdoc/>
	public bool IsUniqueLicensePlate(string licensePlate)
	{
		if (RegisteredLicensePlates.Any(l => l == licensePlate))
			throw new InvalidOperationException("License plate already exists.");
		
		return true; 
	}

	/// <inheritdoc/>
	public void RegisterLicensePlate(string licensePlate)
	{
		RegisteredLicensePlates.Add(licensePlate);
		SaveLicensePlateToFile();
	}

	/// <inheritdoc/>
	public void ClearAllLicensePlates()
	{
		RegisteredLicensePlates.Clear();
		SaveLicensePlateToFile();
	}

	/// <inheritdoc/>
	public void RemoveLicensePlate(string licensePlate)
	{
		RegisteredLicensePlates.Remove(licensePlate);
		SaveLicensePlateToFile();
	}

	private void LoadLicensePlateFromFile()
	{
		// Creates a new empty file, if no file exists.
		if (!File.Exists(_storageFilePath))
		{
			File.WriteAllText(_storageFilePath, JsonSerializer.Serialize(new HashSet<string>()));
			return;
		}

		// Loads license plates from file.
		var json = File.ReadAllText(_storageFilePath);
		var loaded = JsonSerializer.Deserialize<HashSet<string>>(json);
		if (loaded is not null)
			RegisteredLicensePlates = loaded;
	}

	private void SaveLicensePlateToFile()
	{
		var json = JsonSerializer.Serialize(RegisteredLicensePlates, new JsonSerializerOptions { WriteIndented = true });
		File.WriteAllText(_storageFilePath, json);
	}
}
