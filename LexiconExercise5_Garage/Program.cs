using LexiconExercise5_Garage.ConsoleRelated.CWritePrint;
using LexiconExercise5_Garage.ConsoleRelated.DisplayMessages.ErrorMessages;
using LexiconExercise5_Garage.ConsoleRelated.DisplayMessages.FeedbackMessage;
using LexiconExercise5_Garage.ConsoleRelated.DisplayMessages.MenuMessages;
using LexiconExercise5_Garage.GaragesHandler;
using LexiconExercise5_Garage.Vehicles.LicensePlate.Registry;
using LexiconExercise5_Garage.Vehicles.VehicleFactories;
using LexiconExercise5_GarageAssignment.ConsoleRelated;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("LexiconExercise5_Garage.TestProject")]

namespace LexiconExercise5_GarageAssignment;

internal class Program
{
    static void Main(string[] args)
    {

		var host = Host.CreateDefaultBuilder(args)
			.ConfigureServices(services =>
			{
				services.AddSingleton<IConsoleUI, ConsoleUI>();
				services.AddSingleton<IConsoleWritePrint, ConsoleWritePrint>();
				services.AddSingleton<IDisplayErrorMessages, DisplayErrorMessages>();
				services.AddSingleton<IDisplayMenuMessages, DisplayMenuMessages>();
				services.AddSingleton<IDisplayFeedbackMessage, DisplayFeedBackMessage>();
				services.AddSingleton<ILicensePlateRegistry, LicensePlateRegistry>(); 
				services.AddSingleton<IVehicleFactory, VehicleFactory>(); 
				
				

				services.AddSingleton<GarageHandler>();
				
			})
			.UseConsoleLifetime()
			.Build();

		host.Services.GetRequiredService<GarageHandler>().MainMenuSelection();
	
    }
}
