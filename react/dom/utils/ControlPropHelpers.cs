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
    }
    public class ControlPropHelpers
    {
        public static void InjectProps(IDom component, ScriptObject prevProps, ScriptObject props)
        {
            var instance = component.getNode();
            if (
                C.TryGetProps(props, "tooltip", out object tooltipProps)
                && tooltipProps is string tooltip
            )
            {
                instance.TooltipText = tooltip;
            }

            if (C.TryGetProps(props, "theme", out object theme) && theme is string themePath)
            {
                try
                {
                    instance.Theme = (Theme)GD.Load(themePath);
                }
                catch (Exception e)
                {
                    GD.Print(e);
                }
            }

            // MOUSE ACTIONS
            if (C.TryGetProps(prevProps, "onMouseEnter", out object prevMouseEnterProps))
            {
                foreach (
                    var connection in instance.GetSignalConnectionList(
                        nameof(instance.MouseEntered)
                    )
                )
                {
                    // TODO: figure this out? It's not too big a deal since it looks like they don't change easily.
                }
            }
            if (C.TryGetProps(props, "onMouseEnter", out object mouseEnterProps))
            {
                instance.MouseEntered += () => ((dynamic)mouseEnterProps)();
            }
            var prevMouseExitProps = prevProps?.GetProperty("onMouseExit");

            if (C.TryGetProps(props, "onMouseEnter", out object mouseExitProps))
            {
                instance.MouseExited += () => ((dynamic)mouseExitProps)();
            }
            if (C.TryGetProps(props, "name", out object stringName))
            {
                instance.Name = (string)stringName;
            }

            // STYLE ACTIONS
            if (!C.TryGetProps(props, "style", out object style))
            {
                return;
            }
            ScriptObject styleProps = (ScriptObject)style;

            if (C.TryGetStyleProps(props, "modulate", out object modulate))
            {
                instance.Modulate = (Color)modulate;
            }
            if (C.TryGetStyleProps(props, "visible", out object visible))
            {
                instance.Visible = (bool)visible;
            }
            if (C.TryGetStyleProps(props, "zIndex", out object zIndex))
            {
                instance.ZIndex = (int)zIndex;
            }
            if (C.TryGetStyleProps(props, "autoTranslate", out object autoTranslate))
            {
                instance.AutoTranslate = (bool)autoTranslate;
            }

            if (C.TryGetStyleProps(props, "anchorPreset", out object anchorPreset))
            {
                instance.SetAnchorsPreset((Control.LayoutPreset)Convert.ToInt64(anchorPreset));
            }

            if (C.TryGetStyleProps(props, "minWidth", out object minWidth))
            {
                var newMinSize = instance.CustomMinimumSize;
                newMinSize.X = (int)minWidth;
                instance.CustomMinimumSize = newMinSize;
            }
            if (C.TryGetStyleProps(props, "minHeight", out object minHeight))
            {
                var newMinSize = instance.CustomMinimumSize;
                newMinSize.Y = (int)minHeight;
                instance.CustomMinimumSize = newMinSize;
            }

            if (C.TryGetStyleProps(props, "expandBehaviorH", out object sizeFlagsH))
            {
                instance.SizeFlagsHorizontal = (Control.SizeFlags)Convert.ToInt64(sizeFlagsH);
            }
            if (C.TryGetStyleProps(props, "expandBehaviorV", out object sizeFlagsV))
            {
                instance.SizeFlagsVertical = (Control.SizeFlags)Convert.ToInt64(sizeFlagsV);
            }

            // mouse options
            if (C.TryGetStyleProps(props, "mouseDefaultCursorShape", out object mouseCursorShape))
            {
                instance.MouseDefaultCursorShape = (Control.CursorShape)Convert.ToInt64(mouseCursorShape);
            }
            if (C.TryGetStyleProps(props, "mouseFilter", out object mouseFilter))
            {
                instance.MouseFilter = (Control.MouseFilterEnum)Convert.ToInt64(mouseFilter);
            }
            if (C.TryGetStyleProps(props, "mouseForcePassScrollEvents", out object mouseForcePassScrollEvents))
            {
                instance.MouseForcePassScrollEvents = (bool)mouseForcePassScrollEvents;
            }

            // absolute position
            bool hasGlobalPosition = false;
            if (C.TryGetStyleProps(props, "x", out object x))
            {
                var globalPosition = instance.GlobalPosition;
                globalPosition.X = (int)x;
                instance.SetGlobalPosition(globalPosition);
                hasGlobalPosition = true;
            }
            if (C.TryGetStyleProps(props, "y", out object y))
            {
                var globalPosition = instance.GlobalPosition;
                globalPosition.Y = (int)y;
                instance.SetGlobalPosition(globalPosition);
                hasGlobalPosition = true;
            }
            if (hasGlobalPosition)
            {
                SyncGlobalPosition(component, props);
            }

            // size
            if (C.TryGetStyleProps(props, "width", out object width))
            {
                var newSize = instance.Size;
                newSize.X = (int)width;
                instance.Size = newSize;
            }
            if (C.TryGetStyleProps(props, "height", out object height))
            {
                var newSize = instance.Size;
                newSize.Y = (int)height;
                instance.Size = newSize;
            }

            // grow direction
            if (C.TryGetStyleProps(props, "growHorizontal", out object growHorizontal)) {
                instance.GrowHorizontal = (Control.GrowDirection)Convert.ToInt64(growHorizontal);
            }
            if (C.TryGetStyleProps(props, "growVertical", out object growVertical)) {
                instance.GrowVertical = (Control.GrowDirection)Convert.ToInt64(growVertical);
            }

            if (C.TryGetStyleProps(props, "layoutDirection", out object layoutDirection)) {
                instance.LayoutDirection = (Control.LayoutDirectionEnum)Convert.ToInt64(layoutDirection);
            }
        }

        private static async void SyncGlobalPosition(IDom component, ScriptObject props)
        {
            await component.getDocument().ToSignal(component.getDocument().GetTree(), SceneTree.SignalName.ProcessFrame);
            // absolute position
            var instance = component.getNode();
            if (C.TryGetStyleProps(props, "x", out object x))
            {
                var globalPosition = instance.GlobalPosition;
                globalPosition.X = (int)x;
                instance.SetGlobalPosition(globalPosition);
            }
            if (C.TryGetStyleProps(props, "y", out object y))
            {
                var globalPosition = instance.GlobalPosition;
                globalPosition.Y = (int)y;
                instance.SetGlobalPosition(globalPosition);
            }
        }
    }
}
