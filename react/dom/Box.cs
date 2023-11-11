using Godot;
using Microsoft.ClearScript;

namespace Spectral.React {
    public class VBoxNode : DomNode<VBoxContainer> {
        protected override void updatePropsImpl(ScriptObject newProps) {
            base.updatePropsImpl(newProps);
            BoxPropHelpers.InjectProps(_instance, _previousProps, newProps);
        }
    }

	public class HBoxNode : DomNode<HBoxContainer> {
        protected override void updatePropsImpl(ScriptObject newProps) {
            base.updatePropsImpl(newProps);
            BoxPropHelpers.InjectProps(_instance, _previousProps, newProps);
        }
    }
}
