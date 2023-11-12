using Godot;
using Microsoft.ClearScript;

namespace Spectral.React
{
    public class ButtonNode : DomNode<Button>
    {
        System.Action clickEvent;

        protected override void updatePropsImpl(ScriptObject newProps)
        {
            ControlPropHelpers.InjectProps(this, _instance, _previousProps, newProps);

            if (TextPropHelpers.ShouldUpdateTextContent(newProps))
            {
                _instance.Text = TextPropHelpers.GetTextContent(newProps);
            }
            ThemePropHelpers.InjectFontProps(_instance, _previousProps, newProps);
            ThemePropHelpers.InjectSeparationProps(_instance, _previousProps, newProps);

            if (C.TryGetProps(newProps, "onClick", out dynamic onClick))
            {
                if (clickEvent != null)
                {
                    _instance.Pressed -= clickEvent;
                }
                clickEvent = () =>
                {
                    onClick();
                };
                _instance.Pressed += clickEvent;
            }

            if (C.TryGetProps(newProps, "disabled", out object disabled))
            {
                _instance.Disabled = (bool)disabled;
            }

            if (C.TryGetProps(newProps, "flat", out object flat))
            {
                _instance.Flat = (bool)flat;
            }

            if (C.TryGetProps(newProps, "expandIcon", out object expandIcon))
            {
                _instance.ExpandIcon = (bool)expandIcon;
            }

            if (C.TryGetProps(newProps, "icon", out object icon))
            {
                try
                {
                    _instance.Icon = GD.Load<Texture2D>((string)icon);
                }
                catch (System.Exception e)
                {
                    GD.Print("Texture2D for button icon not found ", e);
                }

            }

            // theme props
            C.InjectThemeColorProps(_instance, newProps, "fontDisabledColor", "font_disabled_color");
            C.InjectThemeColorProps(_instance, newProps, "fontFocusColor", "font_focus_color");
            C.InjectThemeColorProps(_instance, newProps, "fontHoverColor", "font_hover_color");
            C.InjectThemeColorProps(_instance, newProps, "fontHoverPressedColor", "font_hover_pressed_color");
            C.InjectThemeColorProps(_instance, newProps, "fontPressedColor", "font_pressed_color");
            C.InjectThemeColorProps(_instance, newProps, "iconColor", "icon_normal_color");
            C.InjectThemeColorProps(_instance, newProps, "iconDisabledColor", "icon_disabled_color");
            C.InjectThemeColorProps(_instance, newProps, "iconFocusColor", "icon_focus_color");
            C.InjectThemeColorProps(_instance, newProps, "iconHoverColor", "icon_hover_color");
            C.InjectThemeColorProps(_instance, newProps, "iconHoverPressedColor", "icon_hover_pressed_color");
            C.InjectThemeColorProps(_instance, newProps, "iconPressedColor", "icon_pressed_color");

            C.InjectThemeIntProps(_instance, newProps, "iconMaxWidth", "icon_max_width");

            C.InjectThemeStyleboxProps(_instance, newProps, "disabledStyle", "disabled");
            C.InjectThemeStyleboxProps(_instance, newProps, "focusStyle", "focus");
            C.InjectThemeStyleboxProps(_instance, newProps, "hoverStyle", "hover");
            C.InjectThemeStyleboxProps(_instance, newProps, "disabledStyle", "disabled");
            C.InjectThemeStyleboxProps(_instance, newProps, "normalStyle", "normal");
            C.InjectThemeStyleboxProps(_instance, newProps, "pressedStyle", "pressed");
        }
    }
}
