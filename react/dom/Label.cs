using Godot;
using Microsoft.ClearScript;

namespace Spectral.React {
    public class LabelNode : DomNode<Label> {
        protected override void updatePropsImpl(ScriptObject newProps) {
            ControlPropHelpers.InjectProps(this, _instance, _previousProps, newProps);
            LabelPropHelpers.InjectProps(_instance, _previousProps, newProps);
        }
    }

    public class RichLabelNode : DomNode<RichTextLabel> {
        public RichLabelNode() : base() {
            _instance.BbcodeEnabled = true;
            _instance.FitContent = true;
        }
        
        protected override void updatePropsImpl(ScriptObject newProps) {
            ControlPropHelpers.InjectProps(this, _instance, _previousProps, newProps);
            LabelPropHelpers.InjectProps(_instance, _previousProps, newProps);
        }
    }
}
