using System;
using Godot;
using Microsoft.ClearScript;

namespace Spectral.React
{
	public class ThemePropHelpers
	{
		public static void InjectSeparationProps(Control instance, ScriptObject prevProps, ScriptObject props)
		{
			C.InjectThemeIntProps(instance, props, "hSeparation", "h_separation");
			C.InjectThemeIntProps(instance, props, "vSeparation", "v_separation");
			
		}
		public static void InjectFontProps(Control instance, ScriptObject prevProps, ScriptObject props)
		{
			C.InjectThemeFontProps(instance, props, "font", "font");
			C.InjectThemeFontSizeProps(instance, props, "fontSize", "font_size");
			C.InjectThemeColorProps(instance, props, "fontColor", "font_color");
			C.InjectThemeColorProps(instance, props, "fontOutlineColor", "font_outline_color");
			C.InjectThemeIntProps(instance, props, "fontOutlineSize", "outline_size");
		}

		public static void InjectFontShadowProps(Control instance, ScriptObject prevProps, ScriptObject props)
		{
			C.InjectThemeColorProps(instance, props, "fontShadowColor", "font_shadow_color");
		}
	}
}
