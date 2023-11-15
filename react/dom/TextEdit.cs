using System;
using Godot;
using Microsoft.ClearScript;

namespace Spectral.React
{
    public class TextEditNode : DomNode<TextEdit>
    {
        public TextEditNode()
            : base() { }

        protected override void updatePropsImpl(ScriptObject newProps)
        {
            ControlPropHelpers.InjectProps(this, _instance, _previousProps, newProps);
            C.InjectThemeStyleboxProps(_instance, newProps, "backgroundStyle", "panel");

            if (C.TryGetProps(newProps, "editable", out object editable))
            {
                _instance.Editable = (bool)editable;
            }

            if (C.TryGetProps(newProps, "text", out object text))
            {
                _instance.Text = (string)text;
            }

            if (C.TryGetProps(newProps, "placeholder", out object placeholder))
            {
                _instance.PlaceholderText = (string)placeholder;
            }

			if (C.TryGetProps(newProps, "minimap", out object minimap))
            {
                _instance.MinimapDraw = (bool)minimap;
            }

			if (C.TryGetProps(newProps, "wrapMode", out object wrapMode))
            {
                _instance.WrapMode = (TextEdit.LineWrappingMode)Convert.ToInt64(wrapMode);
            }
        }
    }
}
