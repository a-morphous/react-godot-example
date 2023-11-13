using System;
using System.Collections.Generic;
using System.Dynamic;
using Godot;
using Microsoft.ClearScript;

namespace Spectral.React
{
    public class SpriteNode : DomNode<Sprite2D>
    {
        protected override void updatePropsImpl(ScriptObject newProps)
        {
            Node2DPropHelpers.InjectProps(this, _instance, _previousProps, newProps);
        }
    }
}
