using System.Collections.Generic;
using Godot;
using Microsoft.ClearScript;

namespace Spectral.React {
    public interface IDom {
        public void setDocument(Document doc);
        public Document getDocument();
        public Control getNode();
        public void updateProps(ScriptObject newProps);

        public void clearChildren();

        public void appendChild(IDom node);

        public void removeChild(IDom node);
    }

    public partial class DomNode<T> : IDom
        where T : Control, new() {
        protected Document _document;

        protected List<IDom> _children;

        protected T _instance;

        protected ScriptObject _previousProps = null;

        public DomNode() {
            _children = new List<IDom>();
            _instance = new T();
        }

        public void updateProps(ScriptObject newProps) {
            updatePropsImpl(newProps);
            _previousProps = newProps;
        }

        protected virtual void updatePropsImpl(ScriptObject newProps) {
            ControlPropHelpers.InjectProps(this, _previousProps, newProps);
        }

        public Control getNode() {
            return _instance;
        }

        public T getNodeAsType() {
            return _instance;
        }

        public void setDocument(Document doc) {
            _document = doc;
        }

        public Document getDocument() {
            return _document;
        }

        public void clearChildren() {
            for (int i = _children.Count - 1; i >= 0; i--) {
                removeChild(_children[i]);
            }
        }

        public void appendChild(IDom node) {
            _children.Add(node);
            _instance.AddChild(node.getNode());
        }

        public void removeChild(IDom node) {
            _children.Remove(node);
            _instance.RemoveChild(node.getNode());
            node.getNode().QueueFree();
        }
    }

    public class ControlNode : DomNode<Control> { }

    public class ContainerNode : DomNode<BoxContainer> { }
}
