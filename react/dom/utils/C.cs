using System;
using Godot;
using Microsoft.ClearScript;

namespace Spectral.React
{
    /// <summary>
    /// helper class to pull info out of props
    /// </summary>
    public static class C
    {
        public static Color ToColor(object obj)
        {
            if (obj is Color)
            {
                return (Color)obj;
            }
            return Color.FromHtml(Convert.ToString(obj));
        }

        public static Texture2D ToTexture(object obj)
        {
            if (obj is Texture2D tex)
            {
                return tex;
            }
            else
            {
                return GD.Load<Texture2D>((string)obj);
            }
        }

        public static bool TryGetProps(ScriptObject props, string property, out object result)
        {
            if (props == null)
            {
                result = null;
                return false;
            }
            result = props.GetProperty(property);
            return DoesPropExist(result);
        }

        /// <summary>
        /// Use this to get the `style` prop, which has its own nested props.
        /// </summary>
        /// <param name="props"></param>
        /// <param name="property"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static bool TryGetStyleProps(ScriptObject props, string property, out object result)
        {
            if (props == null)
            {
                result = null;
                return false;
            }
            if (!TryGetProps(props, "style", out object styleObj))
            {
                result = null;
                return false;
            }
            ScriptObject style = (ScriptObject)styleObj;
            result = style.GetProperty(property);
            return DoesPropExist(result);
        }

        public static bool DoesPropExist(object prop)
        {
            if (prop == null)
            {
                return false;
            }
            if (prop is Undefined)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Helper to inject a font into a theme override.
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="props"></param>
        /// <param name="styleProp">The name of the prop used in react</param>
        /// <param name="themeNameToOverride">The name of the theme override in Godot.</param>
        public static void InjectThemeFontProps(
            Control instance,
            ScriptObject props,
            string styleProp,
            string themeNameToOverride
        )
        {
            if (C.TryGetStyleProps(props, styleProp, out object font))
            {
                try
                {
                    if (font is Font font1)
                    {
                        instance.AddThemeFontOverride(themeNameToOverride, font1);
                    }
                    else
                    {
                        instance.AddThemeFontOverride(
                            themeNameToOverride,
                            GD.Load<Font>((string)font)
                        );
                    }
                }
                catch (Exception e)
                {
                    GD.Print("Error loading font ", e);
                }
            }
        }

        /// <summary>
        /// Helper to inject an integer into a theme override.
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="props"></param>
        /// <param name="styleProp">The name of the prop used in react</param>
        /// <param name="themeNameToOverride">The name of the theme override in Godot.</param>
        public static void InjectThemeIntProps(
            Control instance,
            ScriptObject props,
            string styleProp,
            string themeNameToOverride
        )
        {
            if (C.TryGetStyleProps(props, styleProp, out object value))
            {
                instance.AddThemeConstantOverride(themeNameToOverride, (int)value);
            }
        }

        /// <summary>
        /// Helper to inject a font size into a theme override.
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="props"></param>
        /// <param name="styleProp">The name of the prop used in react</param>
        /// <param name="themeNameToOverride">The name of the theme override in Godot.</param>
        public static void InjectThemeFontSizeProps(
            Control instance,
            ScriptObject props,
            string styleProp,
            string themeNameToOverride
        )
        {
            if (C.TryGetStyleProps(props, styleProp, out object value))
            {
                instance.AddThemeFontSizeOverride(themeNameToOverride, (int)value);
            }
        }

        /// <summary>
        /// Helper to inject a texture / icon into a theme override.
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="props"></param>
        /// <param name="styleProp">The name of the prop used in react</param>
        /// <param name="themeNameToOverride">The name of the theme override in Godot.</param>
        public static void InjectThemeTextureProps(
            Control instance,
            ScriptObject props,
            string styleProp,
            string themeNameToOverride
        )
        {
            if (C.TryGetStyleProps(props, styleProp, out object texturePath))
            {
                try
                {
                    instance.AddThemeIconOverride(themeNameToOverride, ToTexture(texturePath));
                }
                catch (Exception e)
                {
                    GD.Print("Error loading texture2D ", e);
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="props"></param>
        /// <param name="styleProp">The name of the prop used in react</param>
        /// <param name="themeNameToOverride">The name of the theme override in Godot.</param>
        public static void InjectThemeColorProps(
            Control instance,
            ScriptObject props,
            string styleProp,
            string themeNameToOverride
        )
        {
            if (C.TryGetStyleProps(props, styleProp, out object color))
            {
                instance.AddThemeColorOverride(themeNameToOverride, ToColor(color));
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="props"></param>
        /// <param name="styleProp">The name of the prop used in react</param>
        /// <param name="themeNameToOverride">The name of the theme override in Godot.</param>
        public static void InjectThemeStyleboxProps(
            Control instance,
            ScriptObject props,
            string styleProp,
            string themeNameToOverride
        )
        {
            if (C.TryGetStyleProps(props, styleProp, out object styleBox))
            {
                try
                {
                    if (styleBox is StyleBox s)
                    {
                        instance.AddThemeStyleboxOverride(themeNameToOverride, s);
                    }
                    else
                    {
                        instance.AddThemeStyleboxOverride(
                            themeNameToOverride,
                            GD.Load<StyleBox>((string)styleBox)
                        );
                    }
                }
                catch (Exception e)
                {
                    GD.Print("Error loading stylebox ", e);
                }
            }
        }

        public static void InjectBaseProps(
            IAnimatedDom component,
            CanvasItem instance,
            ScriptObject prevProps,
            ScriptObject props
        )
        {
            T.InjectAnimatable(component, prevProps, props);
            if (C.TryGetProps(props, "name", out object stringName))
            {
                instance.Name = (string)stringName;
            }

            if (!C.TryGetProps(props, "style", out object style))
            {
                return;
            }
            if (C.TryGetStyleProps(props, "modulate", out object modulate))
            {
                // TODO: actually factor in the transition
                var modulateTween = component.getTween("modulate");
                modulateTween.TweenProperty(instance, "modulate", C.ToColor(modulate), .4);
                // instance.Modulate = C.ToColor(modulate);
            }
            if (C.TryGetStyleProps(props, "modulateSelf", out object modulateSelf))
            {
                T.SetOrPerformTransition(component, "self_modulate", C.ToColor(modulateSelf));
            }
            if (C.TryGetStyleProps(props, "visible", out object visible))
            {
                instance.Visible = (bool)visible;
            }
            if (C.TryGetStyleProps(props, "zIndex", out object zIndex))
            {
                instance.ZIndex = (int)zIndex;
            }
        }
    }
}
