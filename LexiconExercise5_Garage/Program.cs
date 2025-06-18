using System.Runtime.CompilerServices;
using LexiconExercise5_Garage.ConsoleRelated.CWritePrint;
using LexiconExercise5_Garage.ConsoleRelated.DisplayMessages.ErrorMessages;
using LexiconExercise5_Garage.ConsoleRelated.DisplayMessages.MenuMessages;
using LexiconExercise5_Garage.GaragesHandler;
using LexiconExercise5_GarageAssignment.ConsoleRelated;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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

				services.AddSingleton<GarageHandler>();
				
			})
			.UseConsoleLifetime()
			.Build();

		host.Services.GetRequiredService<GarageHandler>().MainMenuSelection();
	
    }
}
