using System;
using Godot;
using Microsoft.ClearScript;

namespace Spectral.React
{
    public class ControlPropHelpers
    {
        public static void InjectProps(
            IAnimatedDom component,
            Control instance,
            ScriptObject prevProps,
            ScriptObject props
        )
        {
            C.InjectBaseProps(component, instance, prevProps, props);

            if (
                C.TryGetProps(props, "tooltip", out object tooltipProps)
                && tooltipProps is string tooltip
            )
            {
                instance.TooltipText = tooltip;
            }

            if (C.TryGetProps(props, "theme", out object theme))
            {
                try
                {
                    if (theme is Theme themeObj)
                    {
                        instance.Theme = (Theme)themeObj;
                    }
                    else
                    {
                        instance.Theme = GD.Load<Theme>((string)theme);
                    }
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

            // STYLE ACTIONS
            if (!C.TryGetProps(props, "style", out object style))
            {
                return;
            }

            if (C.TryGetStyleProps(props, "autoTranslate", out object autoTranslate))
            {
                instance.AutoTranslate = (bool)autoTranslate;
            }

            if (C.TryGetStyleProps(props, "anchorPreset", out object anchorPreset))
            {
                instance.SetAnchorsPreset((Control.LayoutPreset)Convert.ToInt64(anchorPreset));
            }

            if (C.TryGetStyleProps(props, "focusMode", out object focusMode))
            {
                instance.FocusMode = (Control.FocusModeEnum)Convert.ToInt64(focusMode);
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
                instance.MouseDefaultCursorShape = (Control.CursorShape)
                    Convert.ToInt64(mouseCursorShape);
            }
            if (C.TryGetStyleProps(props, "mouseFilter", out object mouseFilter))
            {
                instance.MouseFilter = (Control.MouseFilterEnum)Convert.ToInt64(mouseFilter);
            }
            if (
                C.TryGetStyleProps(
                    props,
                    "mouseForcePassScrollEvents",
                    out object mouseForcePassScrollEvents
                )
            )
            {
                instance.MouseForcePassScrollEvents = (bool)mouseForcePassScrollEvents;
            }

            // absolute position
            bool hasGlobalPosition = false;
            if (C.TryGetStyleProps(props, "x", out object x))
            {
                hasGlobalPosition = true;
            }
            if (C.TryGetStyleProps(props, "y", out object y))
            {
                hasGlobalPosition = true;
            }
            if (hasGlobalPosition)
            {
                SyncGlobalPosition(component, instance, props);
            }

            if (C.TryGetStyleProps(props, "minWidth", out object minWidth))
            {
                T.SetOrPerformTransition(
                    component,
                    props,
                    T.GetPropertyNameForAnimatableNode(AnimatableNode.MinWidth),
                    Convert.ToInt32(minWidth)
                );
            }
            if (C.TryGetStyleProps(props, "minHeight", out object minHeight))
            {
                T.SetOrPerformTransition(
                    component,
                    props,
                    T.GetPropertyNameForAnimatableNode(AnimatableNode.MinHeight),
                    Convert.ToInt32(minHeight)
                );
            }

            // size
            if (C.TryGetStyleProps(props, "width", out object width))
            {
                T.SetOrPerformTransition(
                    component,
                    props,
                    T.GetPropertyNameForAnimatableNode(AnimatableNode.Width),
                    Convert.ToInt32(width)
                );
            }
            if (C.TryGetStyleProps(props, "height", out object height))
            {
                T.SetOrPerformTransition(
                    component,
                    props,
                    T.GetPropertyNameForAnimatableNode(AnimatableNode.Height),
                    Convert.ToInt32(height)
                );
            }

            // scale
            if (C.TryGetStyleProps(props, "scaleX", out object scaleX))
            {
                T.SetOrPerformTransition(
                    component,
                    props,
                    T.GetPropertyNameForAnimatableNode(AnimatableNode.ScaleX),
                    Convert.ToSingle(scaleX)
                );
            }

            if (C.TryGetStyleProps(props, "scaleY", out object scaleY))
            {
                T.SetOrPerformTransition(
                    component,
                    props,
                    T.GetPropertyNameForAnimatableNode(AnimatableNode.ScaleY),
                    Convert.ToSingle(scaleY)
                );
            }

            // grow direction
            if (C.TryGetStyleProps(props, "growHorizontal", out object growHorizontal))
            {
                instance.GrowHorizontal = (Control.GrowDirection)Convert.ToInt64(growHorizontal);
            }
            if (C.TryGetStyleProps(props, "growVertical", out object growVertical))
            {
                instance.GrowVertical = (Control.GrowDirection)Convert.ToInt64(growVertical);
            }

            if (C.TryGetStyleProps(props, "layoutDirection", out object layoutDirection))
            {
                instance.LayoutDirection = (Control.LayoutDirectionEnum)
                    Convert.ToInt64(layoutDirection);
            }
            
            InjectAnchorProps(instance, prevProps, props);
        }

        protected static void InjectAnchorProps(
            Control instance,
            ScriptObject prevProps,
            ScriptObject props)
        {
            if (C.TryGetStyleProps(props, "anchorBottom", out object anchorBottom))
            {
                // instance.AnchorBottom = Convert.ToSingle(anchorBottom);
                instance.SetAnchor(Side.Bottom, Convert.ToSingle(anchorBottom));
            }
            if (C.TryGetStyleProps(props, "anchorLeft", out object anchorLeft))
            {
                // instance.AnchorLeft = Convert.ToSingle(anchorLeft);
                instance.SetAnchor(Side.Left, Convert.ToSingle(anchorLeft));
            }
            if (C.TryGetStyleProps(props, "anchorTop", out object anchorTop))
            {
                // instance.AnchorTop = Convert.ToSingle(anchorTop);
                instance.SetAnchor(Side.Top, Convert.ToSingle(anchorTop));
            }
            if (C.TryGetStyleProps(props, "anchorRight", out object anchorRight))
            {
                // instance.AnchorRight = Convert.ToSingle(anchorRight);
                instance.SetAnchor(Side.Right, Convert.ToSingle(anchorRight));
            }
            
            // offset
            if (C.TryGetStyleProps(props, "offsetBottom", out object offsetBottom))
            {
                instance.OffsetBottom = Convert.ToSingle(offsetBottom);
            }
            if (C.TryGetStyleProps(props, "offsetLeft", out object offsetLeft))
            {
                instance.OffsetLeft = Convert.ToSingle(offsetLeft);
            }
            if (C.TryGetStyleProps(props, "offsetTop", out object offsetTop))
            {
                instance.OffsetTop = Convert.ToSingle(offsetTop);
            }
            if (C.TryGetStyleProps(props, "offsetRight", out object offsetRight))
            {
                instance.OffsetRight = Convert.ToSingle(offsetRight);
            }
        }
        private static async void SyncGlobalPosition(
            IAnimatedDom component,
            Control instance,
            ScriptObject props
        )
        {
            await component
                .getDocument()
                .ToSignal(component.getDocument().GetTree(), SceneTree.SignalName.ProcessFrame);
            // absolute position
            if (C.TryGetStyleProps(props, "x", out object x))
            {
                T.SetOrPerformTransition(
                    component,
                    props,
                    "global_position:x",
                    Convert.ToSingle(x)
                );
            }
            if (C.TryGetStyleProps(props, "y", out object y))
            {
                T.SetOrPerformTransition(
                    component,
                    props,
                    "global_position:y",
                    Convert.ToSingle(y)
                );
            }
        }
    }
}
