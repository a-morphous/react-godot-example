using Godot;
using Microsoft.ClearScript;

namespace Spectral.React {
    public class LabelNode : DomNode<Label> {
        protected override void updatePropsImpl(ScriptObject newProps) {
            base.updatePropsImpl(newProps);
            _instance.Text = TextPropHelpers.GetTextContent(newProps);
        }
    }

    public class RichLabelNode : DomNode<RichTextLabel> {
        public RichLabelNode() : base() {
            _instance.BbcodeEnabled = true;
            _instance.CustomMinimumSize = new Vector2(0, 32);
        }
        protected override void updatePropsImpl(ScriptObject newProps) {
            base.updatePropsImpl(newProps);
            _instance.Text = TextPropHelpers.GetTextContent(newProps);
        }
    }
}
