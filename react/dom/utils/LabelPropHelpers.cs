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
            if (C.TryGetStyleProps(props, "autowrapMode", out object autowrapMode))
            {
                instance.AutowrapMode = (TextServer.AutowrapMode)Convert.ToInt64(autowrapMode);
            }
            ThemePropHelpers.InjectFontProps(instance, prevProps, props);
            ThemePropHelpers.InjectFontShadowProps(instance, prevProps, props);
            C.InjectThemeIntProps(instance, props, "lineSpacing", "line-spacing");
            C.InjectThemeStyleboxProps(instance, props, "normalStyle", "normal");
        }

        public static void InjectProps(
            RichTextLabel instance,
            ScriptObject prevProps,
            ScriptObject props
        )
        {
            if (TextPropHelpers.ShouldUpdateTextContent(props))
            {
                instance.Text = TextPropHelpers.GetTextContent(props);
            }

            if (C.TryGetProps(props, "contextMenuEnabled", out object contextMenuEnabled))
            {
                instance.ContextMenuEnabled = (bool)contextMenuEnabled;
            }
            if (C.TryGetProps(props, "deselectOnFocusLoss", out object deselectOnFocusLoss))
            {
                instance.DeselectOnFocusLossEnabled = (bool)deselectOnFocusLoss;
            }

            if (C.TryGetStyleProps(props, "autowrapMode", out object autowrapMode))
            {
                instance.AutowrapMode = (TextServer.AutowrapMode)Convert.ToInt64(autowrapMode);
            }
            if (C.TryGetStyleProps(props, "fitContent", out object fitContent))
            {
                instance.FitContent = (bool)fitContent;
            }
            if (C.TryGetStyleProps(props, "scrollActive", out object scrollActive))
            {
                instance.ScrollActive = (bool)scrollActive;
            }
            if (C.TryGetStyleProps(props, "scrollFollowing", out object scrollFollowing))
            {
                instance.ScrollFollowing = (bool)scrollFollowing;
            }
            if (C.TryGetStyleProps(props, "selectionEnabled", out object selectionEnabled))
            {
                instance.SelectionEnabled = (bool)selectionEnabled;
            }

            ThemePropHelpers.InjectFontProps(instance, prevProps, props);
            ThemePropHelpers.InjectFontShadowProps(instance, prevProps, props);
            C.InjectThemeIntProps(instance, props, "lineSpacing", "line-spacing");
            C.InjectThemeStyleboxProps(instance, props, "normalStyle", "normal");

            C.InjectThemeStyleboxProps(instance, props, "focusStyle", "focus");

            // fonts
            C.InjectThemeFontProps(instance, props, "boldFont", "bold_font");
			C.InjectThemeFontSizeProps(instance, props, "boldFontSize", "bold_font_size");
            C.InjectThemeFontProps(instance, props, "boldItalicFont", "bold_italics_font");
			C.InjectThemeFontSizeProps(instance, props, "boldItalicFontSize", "bold_italics_font_size");
            C.InjectThemeFontProps(instance, props, "italicFont", "italics_font");
			C.InjectThemeFontSizeProps(instance, props, "italicFontSize", "italics_font_size");
            C.InjectThemeFontProps(instance, props, "font", "normal_font");
			C.InjectThemeFontSizeProps(instance, props, "fontSize", "normal_font_size");
            C.InjectThemeFontProps(instance, props, "monoFont", "mono_font");
			C.InjectThemeFontSizeProps(instance, props, "monoFontSize", "mono_font_size");
        }
    }
}
