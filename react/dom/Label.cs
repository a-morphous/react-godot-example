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
            _instance.CustomMinimumSize = new Vector2(0, 32);
        }
        
        protected override void updatePropsImpl(ScriptObject newProps) {
            ControlPropHelpers.InjectProps(this, _instance, _previousProps, newProps);
            _instance.Text = TextPropHelpers.GetTextContent(newProps);
            ThemePropHelpers.InjectFontProps(_instance, _previousProps, newProps);
            ThemePropHelpers.InjectFontShadowProps(_instance, _previousProps, newProps);
            C.InjectThemeStyleboxProps(_instance, newProps, "normal", "normal");
        }
    }
}
