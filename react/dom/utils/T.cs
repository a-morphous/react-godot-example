using System;
using System.Collections.Generic;
using System.Reflection;
using Godot;
using Microsoft.ClearScript;

namespace Spectral.React
{
    /// <summary>
    /// Helper static class used for transitions
    /// </summary>
    public enum AnimatableNode
    {
        Modulate,
        ModulateSelf,
        Width,
        Height,
        MinWidth,
        MinHeight,
        X,
        Y,
        ScaleX,
        ScaleY,
    }

    public class T
    {
        static Dictionary<string, bool> _usedAnimationBuffer = new();

        public static void InjectAnimatable(
            IAnimatedDom component,
            ScriptObject prevProps,
            ScriptObject props
        )
        {
            if (C.TryGetProps(props, "onTransitionRun", out dynamic onTransitionRun))
            {
                component.setTransitionRunEvent(onTransitionRun);
            }
            if (C.TryGetProps(props, "onTransitionEnd", out dynamic onTransitionEnd))
            {
                component.setTransitionEndEvent(onTransitionEnd);
            }

            bool hasPrevProps = C.TryGetStyleProps(
                prevProps,
                "transitions",
                out dynamic prevTransitions
            );
            _usedAnimationBuffer.Clear();

            if (C.TryGetStyleProps(props, "transitions", out dynamic transitions))
            {
                bool hasTransitionTimes = C.TryGetStyleProps(
                    props,
                    "transitionTimeMS",
                    out dynamic transitionTimeMS
                );
                for (int i = 0; i < transitions.length; i++)
                {
                    AnimatableNode animatable = (AnimatableNode)transitions[i];
                    string animatableProperty = GetPropertyNameForAnimatableNode(animatable);
                    _usedAnimationBuffer.Add(animatableProperty, true);
                    float delay = 0; // default value
                    if (hasTransitionTimes)
                    {
                        if (transitionTimeMS.length > i)
                        {
                            delay = Convert.ToSingle(transitionTimeMS[i]) / 1000f;
                        }
                    }
                    component.setTransitionProperties(
                        animatableProperty,
                        new TransitionProperties(
                            delay,
                            Tween.TransitionType.Linear, // TODO: implement
                            Tween.EaseType.InOut // TODO: implement
                        )
                    );
                }
            }

            if (hasPrevProps)
            {
                for (int i = 0; i < prevTransitions.length; i++)
                {
                    AnimatableNode animatable = (AnimatableNode)prevTransitions[i];
                    string animatableProperty = GetPropertyNameForAnimatableNode(animatable);
                    if (_usedAnimationBuffer.ContainsKey(animatableProperty))
                    {
                        continue;
                    }
                    component.removeTransitionProperties(animatableProperty);
                }
            }
        }

        public static string GetPropertyNameForAnimatableNode(AnimatableNode animatable)
        {
            switch (animatable)
            {
                case AnimatableNode.MinWidth:
                    return "custom_minimum_size:x";
                case AnimatableNode.MinHeight:
                    return "custom_minimum_size:y";
                case AnimatableNode.Modulate:
                    return "modulate";
                case AnimatableNode.ModulateSelf:
                    return "self_modulate";
                case AnimatableNode.Width:
                    return "size:x";
                case AnimatableNode.Height:
                    return "size:y";
                case AnimatableNode.X:
                    return "position:x";
                case AnimatableNode.Y:
                    return "position:y";
                case AnimatableNode.ScaleX:
                    return "scale:x";
                case AnimatableNode.ScaleY:
                    return "scale:y";
                default:
                    return null;
            }
        }

        private static void PerformAnimation(
            IAnimatedDom component,
            ScriptObject props,
            string propertyToAnimate,
            Variant value,
            float duration,
            Tween.TransitionType transitionType = Tween.TransitionType.Linear,
            Tween.EaseType easeType = Tween.EaseType.InOut
        )
        {
            var tween = component.getTween(propertyToAnimate);
            tween
                .TweenProperty(component.getNode(), propertyToAnimate, value, duration)
                .SetTrans(transitionType)
                .SetEase(easeType);
            component.callTransitionRun();
        }

        public static void SetOrPerformTransition(
            IAnimatedDom component,
            ScriptObject props,
            string propertyToSet,
            Variant value
        )
        {
            if (component.getNode().Get(propertyToSet).Equals(value))
            {
                return;
            }
            if (component.hasTransitionProperties(propertyToSet))
            {
                var transProps = component.getTransitionProperties(propertyToSet);
                PerformAnimation(
                    component,
                    props,
                    propertyToSet,
                    value,
                    transProps.duration,
                    transProps.trans,
                    transProps.ease
                );
                return;
            }
            component.getNode().SetIndexed(propertyToSet, value);
        }

    }
}
