using System;
using Godot;
using Microsoft.ClearScript;

namespace Spectral.React
{
	public class BoxPropHelpers
	{

		public static void InjectProps(BoxContainer instance, ScriptObject prevProps, ScriptObject props)
		{
			C.InjectThemeIntProps(instance, props, "separation", "separation");
		}
	}
}
