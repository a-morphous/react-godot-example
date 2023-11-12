using System.Collections.Generic;
using System.ComponentModel;
using Godot;
using Microsoft.ClearScript;

namespace Spectral.React
{
    public interface IDom
    {
        public void setDocument(Document doc);
        public Document getDocument();
        public Node getNode();
        public void updateProps(ScriptObject newProps);

        public void clearChildren();

        public void appendChild(IDom node);

        public void removeChild(IDom node);
    }

    public partial class DomNode<T> : IAnimatedDom
        where T : Node, new()
    {
        protected Document _document;

        protected List<IDom> _children;

        protected T _instance;

        protected ScriptObject _previousProps = null;

        protected Dictionary<string, Tween> _animatedTweens = new Dictionary<string, Tween>();
        protected Dictionary<string, TransitionProperties> _transitionProperties =
            new Dictionary<string, TransitionProperties>();

        public DomNode()
        {
            _children = new List<IDom>();
            _instance = new T();
        }

        public void updateProps(ScriptObject newProps)
        {
            updatePropsImpl(newProps);
            _previousProps = newProps;
        }

        protected virtual void updatePropsImpl(ScriptObject newProps)
        {
        }

        public Node getNode()
        {
            return _instance;
        }

        public T getNodeAsType()
        {
            return _instance;
        }

        public void setDocument(Document doc)
        {
            _document = doc;
        }

        public Document getDocument()
        {
            return _document;
        }

        public void clearChildren()
        {
            for (int i = _children.Count - 1; i >= 0; i--)
            {
                removeChild(_children[i]);
            }
        }

        public void appendChild(IDom node)
        {
            _children.Add(node);
            _instance.AddChild(node.getNode());
        }

        public void removeChild(IDom node)
        {
            _children.Remove(node);
            _instance.RemoveChild(node.getNode());
            node.getNode().QueueFree();
        }

        public Tween getTween(string property)
        {
            if (_animatedTweens.ContainsKey(property))
            {
                _animatedTweens[property].Kill();
                _animatedTweens.Remove(property);
            }

            var newTween = getDocument().CreateTween();
            newTween.Finished += () =>
            {
                _animatedTweens.Remove(property);
                newTween.Kill();
            };
            _animatedTweens.Add(property, newTween);
            return _animatedTweens[property];
        }

        public TransitionProperties getTransitionProperties(string property)
        {
            if (!hasTransitionProperties(property))
            {
                return new TransitionProperties();
            }
            return _transitionProperties[property];
        }

        public void setTransitionProperties(string property, TransitionProperties props)
        {
            if (!hasTransitionProperties(property))
            {
                _transitionProperties.Add(property, props);
                return;
            }
            _transitionProperties[property] = props;
        }

        public bool hasTransitionProperties(string property)
        {
            return _transitionProperties.ContainsKey(property);
        }

        public void removeTransitionProperties(string property)
        {
            if (!hasTransitionProperties(property)) {
                return;
            }
            _transitionProperties.Remove(property);
        }
    }

    public class ControlNode : DomNode<Control> {
        protected override void updatePropsImpl(ScriptObject newProps)
        {
            ControlPropHelpers.InjectProps(this, _instance, _previousProps, newProps);
        }
    }

    public class ContainerNode : DomNode<BoxContainer> { 
        protected override void updatePropsImpl(ScriptObject newProps)
        {
            ControlPropHelpers.InjectProps(this, _instance, _previousProps, newProps);
        }
    }
}
