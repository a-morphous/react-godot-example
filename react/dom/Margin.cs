using Godot;
using Microsoft.ClearScript;

namespace Spectral.React {
    public class MarginNode : DomNode<MarginContainer> {
		public MarginNode() : base() {
            _instance.SizeFlagsHorizontal = Control.SizeFlags.ExpandFill;
			_instance.SizeFlagsVertical = Control.SizeFlags.ExpandFill;
        }
        protected override void updatePropsImpl(ScriptObject newProps) {
            ControlPropHelpers.InjectProps(this, _instance, _previousProps, newProps);
			C.InjectThemeIntProps(_instance, newProps, "margin", "margin_top");
			C.InjectThemeIntProps(_instance, newProps, "margin", "margin_left");
			C.InjectThemeIntProps(_instance, newProps, "margin", "margin_right");
			C.InjectThemeIntProps(_instance, newProps, "margin", "margin_bottom");
            C.InjectThemeIntProps(_instance, newProps, "marginTop", "margin_top");
			C.InjectThemeIntProps(_instance, newProps, "marginLeft", "margin_left");
			C.InjectThemeIntProps(_instance, newProps, "marginRight", "margin_right");
			C.InjectThemeIntProps(_instance, newProps, "marginBottom", "margin_bottom");
        }
    }
}
