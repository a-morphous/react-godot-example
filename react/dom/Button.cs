using Godot;
using Microsoft.ClearScript;

namespace Spectral.React {
    public class ButtonNode : DomNode<Button> {
        System.Action clickEvent;

        protected override void updatePropsImpl(ScriptObject newProps) {
            base.updatePropsImpl(newProps);

            _instance.Text = TextPropHelpers.GetTextContent(newProps);

            if (C.TryGetProps(newProps, "onClick", out dynamic onClick)) {
                if (clickEvent != null) {
                    _instance.Pressed -= clickEvent;
                }
                clickEvent = () => {
                    onClick();
                };
                _instance.Pressed += clickEvent;
            }
        }
    }
}
