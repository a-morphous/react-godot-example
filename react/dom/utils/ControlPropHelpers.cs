using System;
using Godot;
using Microsoft.ClearScript;

namespace Spectral.React {
    public class ControlPropHelpers {
        public static bool TryGetProps(ScriptObject props, string property, out object result) {
            if (props == null) {
                result = null;
                return false;
            }
            result = props.GetProperty(property);
            return DoesPropExist(result);
        }

        public static bool DoesPropExist(object prop) {
            if (prop == null) {
                return false;
            }
            if (prop is Undefined) {
                return false;
            }
            return true;
        }

        public static void InjectProps(Control instance, ScriptObject prevProps, ScriptObject props) {
            if (
                TryGetProps(props, "tooltip", out object tooltipProps)
                && tooltipProps is string tooltip
            ) {
                instance.TooltipText = tooltip;
            }

            if (TryGetProps(props, "theme", out object theme) && theme is string themePath) {
                try {
                    instance.Theme = (Theme)GD.Load(themePath);
                }
                catch (Exception e) {
                    GD.Print(e);
                }
            }

            // MOUSE ACTIONS
            if (TryGetProps(prevProps, "onMouseEnter", out object prevMouseEnterProps)) {
                foreach (
                    var connection in instance.GetSignalConnectionList(
                        nameof(instance.MouseEntered)
                    )
                ) {
                    // TODO: figure this out? It's not too big a deal since it looks like they don't change easily.
                }
            }
            if (TryGetProps(props, "onMouseEnter", out object mouseEnterProps)) {
                instance.MouseEntered += () => ((dynamic)mouseEnterProps)();
            }
            var prevMouseExitProps = prevProps?.GetProperty("onMouseExit");

            if (TryGetProps(props, "onMouseEnter", out object mouseExitProps)) {
                instance.MouseExited += () => ((dynamic)mouseExitProps)();
            }

            // STYLE ACTIONS
            if (!TryGetProps(props, "style", out object style)) {
                return;
            }
            ScriptObject styleProps = (ScriptObject)style;

            if (TryGetProps(styleProps, "modulate", out object modulate)) {
                instance.Modulate = (Color)modulate;
            }
            if (TryGetProps(styleProps, "visible", out object visible)) {
                instance.Visible = (bool)visible;
            }
            if (TryGetProps(styleProps, "zIndex", out object zIndex)) {
                instance.ZIndex = (int)zIndex;
            }
            if (TryGetProps(styleProps, "autoTranslate", out object autoTranslate)) {
                instance.AutoTranslate = (bool)autoTranslate;
            }

            if (TryGetProps(styleProps, "anchorPreset", out object anchorPreset)) {
                instance.SetAnchorsPreset((Control.LayoutPreset)Convert.ToInt64(anchorPreset));
            }

            if (TryGetProps(styleProps, "minWidth", out object minWidth)) {
                var newMinSize = instance.CustomMinimumSize;
                newMinSize.X = (int)minWidth;
                instance.CustomMinimumSize = newMinSize;
            }
            if (TryGetProps(styleProps, "minHeight", out object minHeight)) {
                var newMinSize = instance.CustomMinimumSize;
                newMinSize.Y = (int)minHeight;
                instance.CustomMinimumSize = newMinSize;
            }

            if (TryGetProps(styleProps, "expandBehaviorH", out object sizeFlagsH)) {
                instance.SizeFlagsHorizontal = (Control.SizeFlags)Convert.ToInt64(sizeFlagsH);
            }
            if (TryGetProps(styleProps, "expandBehaviorV", out object sizeFlagsV)) {
                instance.SizeFlagsVertical = (Control.SizeFlags)Convert.ToInt64(sizeFlagsV);
            }
        }
    }
}
