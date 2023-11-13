using System;
using Godot;
using Microsoft.ClearScript;

namespace Spectral.React
{
    public class TextureNode : DomNode<TextureRect>
    {
		public TextureNode() : base() {
			_instance.ExpandMode = TextureRect.ExpandModeEnum.IgnoreSize;
			_instance.StretchMode = TextureRect.StretchModeEnum.Scale;
		}
        protected override void updatePropsImpl(ScriptObject newProps)
        {
            ControlPropHelpers.InjectProps(this, _instance, _previousProps, newProps);
            if (C.TryGetProps(newProps, "texture", out object icon))
            {
                try
                {
                    _instance.Texture = C.ToTexture(icon);
                }
                catch (System.Exception e)
                {
                    GD.Print("Texture2D for button icon not found ", e);
                }
            }
            if (C.TryGetStyleProps(newProps, "expandMode", out object expandMode))
            {
                _instance.ExpandMode = (TextureRect.ExpandModeEnum)Convert.ToInt64(expandMode);
            }
            if (C.TryGetStyleProps(newProps, "stretchMode", out object stretchMode))
            {
                _instance.StretchMode = (TextureRect.StretchModeEnum)Convert.ToInt64(stretchMode);
            }
            if (C.TryGetStyleProps(newProps, "flipH", out object flipH))
            {
                _instance.FlipH = (bool)flipH;
            }
            if (C.TryGetStyleProps(newProps, "flipV", out object flipV))
            {
                _instance.FlipV = (bool)flipV;
            }
        }
    }
}
