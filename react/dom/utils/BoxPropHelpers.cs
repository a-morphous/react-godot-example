using System;
using Godot;
using Microsoft.ClearScript;

namespace Spectral.React
{
	public class BoxPropHelpers
	{

		public static void InjectProps(BoxContainer instance, ScriptObject prevProps, ScriptObject props)
		{
			if (
				C.TryGetStyleProps(props, "separation", out object separation)
			)
			{
				instance.AddThemeConstantOverride("separation", (int)separation);
			}
		}
	}
}
