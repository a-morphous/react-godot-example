using System;
using Godot;
using Microsoft.ClearScript;

namespace Spectral.React
{
    public class LabelPropHelpers
    {
        public static void InjectProps(Label instance, ScriptObject prevProps, ScriptObject props)
        {
            if (TextPropHelpers.ShouldUpdateTextContent(props))
            {
                instance.Text = TextPropHelpers.GetTextContent(props);
            }

            if (C.TryGetProps(props, "uppercase", out object uppercase))
            {
                instance.Uppercase = (bool)uppercase;
            }
            ThemePropHelpers.InjectFontProps(instance, prevProps, props);
            ThemePropHelpers.InjectFontShadowProps(instance, prevProps, props);
            C.InjectThemeStyleboxProps(instance, props, "normalStyle", "normal");
        }
    }
}
