using Godot;
using Microsoft.ClearScript;

namespace Spectral.React {
    public class PanelNode : DomNode<PanelContainer> {
		public PanelNode() : base() {
            _instance.SizeFlagsHorizontal = Control.SizeFlags.ExpandFill;
			_instance.SizeFlagsVertical = Control.SizeFlags.ExpandFill;
        }
        protected override void updatePropsImpl(ScriptObject newProps) {
            base.updatePropsImpl(newProps);
			C.InjectThemeStyleboxProps(_instance, newProps, "backgroundStyle", "panel");
        }
    }
}
