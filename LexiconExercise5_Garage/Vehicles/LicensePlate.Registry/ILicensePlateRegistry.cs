namespace LexiconExercise5_Garage.Vehicles.LicensePlate.Registry;

/// <summary>
/// Defines a contract for validating and registering vehicle license plates,
/// ensuring correct format and uniqueness.
/// </summary>
public interface ILicensePlateRegistry
{
	/// <summary>
	/// Validates the given license plate for correct format and uniqueness.
	/// If valid, the license plate is registered.
	/// </summary>
	/// <param name="licensePlate">The license plate to validate and register.</param>
	/// <returns><c>true</c> if the license plate is valid and successfully registered; otherwise <c>false</c>.</returns>
	bool IsValidLicensePlate(string licensePlate);

	/// <summary>
	/// Removes a specific license Plate from register.
	/// </summary>
	/// <param name="licensePlate">The license plate to remove from register.</param>
	public void RemoveLicensePlate(string licensePlate);

	/// <summary>
	/// Empties the registry.
	/// </summary>
	public void ClearAllLicensePlates();
}