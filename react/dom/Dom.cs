using System;
using System.Collections.Generic;
using System.ComponentModel;
using Godot;
using Microsoft.ClearScript;

namespace Spectral.React
{
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

        protected ScriptObject _style = null;
        protected ScriptObject _classStyles = null;
        protected string _classes;

        public ScriptObject style
        {
            get
            {
                if (_classStyles == null)
                {
                    return _style;
                }
                // TODO: add class styles
                getDocument().Engine().Script.__oldStyle = _style;
                getDocument().Engine().Script.__newStyle = _classStyles;

                object style = this.getDocument()
                    .Engine()
                    .Evaluate(
                        @"
                            function mergeStyles() {
                                if (__oldStyle === null) {
                                    return __newStyle;
                                }
                                if (__newStyle === null) {
                                    return __oldStyle;
                                }
                                return Object.assign({}, __oldStyle, __newStyle);
                            }
                            mergeStyles();
                        "
                    );

                if (style is ScriptObject scriptStyle)
                {
                    return scriptStyle;
                }

                return _style;
            }
            set
            {
                getDocument().Engine().Script.__oldStyle = _style;
                getDocument().Engine().Script.__newStyle = value;

                object style = this.getDocument()
                    .Engine()
                    .Evaluate(
                        @"
                            function mergeStyles() {
                                if (__oldStyle === null) {
                                    return __newStyle;
                                }
                                if (__newStyle === null) {
                                    return __oldStyle;
                                }
                                return Object.assign({}, __oldStyle, __newStyle);
                            }
                            mergeStyles();
                        "
                    );

                if (style is ScriptObject scriptStyle)
                {
                    _style = scriptStyle;
                }
            }
        }

        public DomNode()
        {
            _children = new List<IDom>();
            _instance = new T();
        }

        public void updateProps(ScriptObject newProps)
        {
            if (C.TryGetProps(newProps, "style", out object style))
            {
                if (style is ScriptObject scriptStyle)
                {
                    _style = scriptStyle;
                }
            }
            if (C.TryGetProps(newProps, "class", out object classes))
            {
                if (classes is string classString)
                {
                    if (classString != _classes)
                    {
                        _classes = classString;
                        setClass(classString);
                    }
                }
            }
            updatePropsImpl(newProps);
            _previousProps = newProps;
        }

        public void setStyle(ScriptObject newStyles)
        {
            _style = newStyles;
            // we have to update props again, with just the style props
            getDocument().Engine().Script.__newStyle = newStyles;
            object newProps = getDocument()
                .Engine()
                .Evaluate(
                    @"
                        function getStyleObj() {
                            return {
                                style: __newStyle
                            }
                        }
                        getStyleObj()
                    "
                );
            if (newProps is ScriptObject newPropsObj)
            {
                updatePropsImpl(newPropsObj);
            }
        }

        public void setClass(string className)
        {
            ScriptObject tempClassStyles = null;
            // split the classes into
            var classes = className.Split(" ");
            foreach (var c in classes)
            {
                var classObj = getDocument().getClassFromStyleSheet(c.Trim());

                if (classObj == null)
                {
                    continue;
                }
                if (tempClassStyles == null)
                {
                    tempClassStyles = classObj;
                    continue;
                }
                // merge
                getDocument().Engine().Script.__oldStyle = tempClassStyles;
                getDocument().Engine().Script.__newStyle = classObj;
                object style = getDocument()
                    .Engine()
                    .Evaluate(
                        @"
                            function mergeStyles() {
                                if (__oldStyle === null) {
                                    return __newStyle;
                                }
                                if (__newStyle === null) {
                                    return __oldStyle;
                                }
                                return Object.assign({}, __oldStyle, __newStyle);
                            }
                            mergeStyles();
                        "
                    );

                if (style is ScriptObject scriptStyle)
                {
                    tempClassStyles = scriptStyle;
                }
            }

            _classStyles = tempClassStyles;

            getDocument().Engine().Script.__newStyle = style;
            object newProps = getDocument()
                .Engine()
                .Evaluate(
                    @"
                        function getStyleObj() {
                            return {
                                style: __newStyle
                            }
                        }
                        getStyleObj()
                    "
                );

            if (newProps is ScriptObject newPropsObj)
            {
                updatePropsImpl(newPropsObj);
            }
        }

        protected virtual void updatePropsImpl(ScriptObject newProps) { }

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
                callTransitionEnd();
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
            if (!hasTransitionProperties(property))
            {
                return;
            }
            _transitionProperties.Remove(property);
        }

        dynamic _transitionRun = null;
        dynamic _transitionEnd = null;

        public void setTransitionRunEvent(dynamic callback)
        {
            _transitionRun = callback;
        }

        public void setTransitionEndEvent(dynamic callback)
        {
            _transitionEnd = callback;
        }

        public void callTransitionRun()
        {
            if (_transitionRun == null || _transitionRun is Undefined) {
                return;
            }
            _transitionRun();
        }

        public void callTransitionEnd()
        {
            if (_transitionEnd == null || _transitionEnd is Undefined) {
                return;
            }
            _transitionEnd();
        }
    }

    public class ControlNode : DomNode<Control>
    {
        protected override void updatePropsImpl(ScriptObject newProps)
        {
            ControlPropHelpers.InjectProps(this, _instance, _previousProps, newProps);
        }
    }

    public class ContainerNode : DomNode<BoxContainer>
    {
        protected override void updatePropsImpl(ScriptObject newProps)
        {
            ControlPropHelpers.InjectProps(this, _instance, _previousProps, newProps);
        }
    }
}
