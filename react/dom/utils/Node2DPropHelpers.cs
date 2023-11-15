using System;
using Godot;
using Microsoft.ClearScript;

namespace Spectral.React
{
    public class Node2DPropHelpers
    {
        public static void InjectProps(
            IAnimatedDom component,
            Sprite2D instance,
            ScriptObject prevProps,
            ScriptObject props
        )
        {
            C.InjectBaseProps(component, instance, prevProps, props);
            if (C.TryGetProps(props, "texture", out object icon))
            {
                try
                {
                    instance.Texture = C.ToTexture(icon);
                }
                catch (System.Exception e)
                {
                    GD.Print("Texture2D for sprite2D not found ", e);
                }
            }

            if (C.TryGetProps(props, "frame", out object frame))
            {
                instance.Frame = Convert.ToInt32(frame);
            }

            if (C.TryGetStyleProps(props, "flipH", out object flipH))
            {
                instance.FlipH = (bool)flipH;
            }
            if (C.TryGetStyleProps(props, "flipV", out object flipV))
            {
                instance.FlipV = (bool)flipV;
            }

            if (C.TryGetStyleProps(props, "x", out object x))
            {
                T.SetOrPerformTransition(
                    component,
                    props,
                    T.GetPropertyNameForAnimatableNode(AnimatableNode.X),
                    Convert.ToSingle(x)
                );
            }
            if (C.TryGetStyleProps(props, "y", out object y))
            {
                T.SetOrPerformTransition(
                    component,
                    props,
                    T.GetPropertyNameForAnimatableNode(AnimatableNode.Y),
                    Convert.ToSingle(y)
                );
            }
            if (C.TryGetStyleProps(props, "rotation", out object rotation))
            {
                instance.Rotation = Convert.ToSingle(rotation);
            }
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
        }
    }
}
