using System.ComponentModel;

namespace LexiconExercise5_Garage.Util;

/// <summary>
/// Specifies comparison options for filtering or evaluating numeric values.
/// </summary>
public enum CompareOptions
{
	[Description("Equal to")]
	Equal,

	[Description("Greater than")]
	GreaterThan,

	[Description("Less than")]
	LessThen
}
