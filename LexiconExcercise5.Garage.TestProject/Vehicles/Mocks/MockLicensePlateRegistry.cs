using LexiconExercise5_Garage.Vehicles.LicensePlate.Registry;

namespace LexiconExcercise5.Garage.TestProject.Vehicles.Mocks;

// ToDo: move to separate file
public class MockLicensePlateRegistry : LicensePlateRegistry
{
	public void ClearRegistry()
	{
		RegisteredLicensePlates.Clear();
	}

	public void FillRegistry()
	{
		RegisteredLicensePlates.Add("BBK159");
		RegisteredLicensePlates.Add("azm129");
		RegisteredLicensePlates.Add("uRE832");
	}
}
