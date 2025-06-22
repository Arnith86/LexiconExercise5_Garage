namespace LexiconExercise5_Garage.Util;

using System;
using System.ComponentModel;
using System.Reflection;

public static class EnumHelper
{
	public static string GetEnumDescription(Enum value)
	{
		var field = value.GetType().GetField(value.ToString());
		var attribute = field.GetCustomAttribute<DescriptionAttribute>();
		return attribute != null ? attribute.Description : value.ToString();
	}
}