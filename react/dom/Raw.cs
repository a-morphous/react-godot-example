using System;
using System.Collections.Generic;
using System.Dynamic;
using Godot;
using Microsoft.ClearScript;

namespace Spectral.React
{
    public class RawNode : DomNode<Node>
    {
        StringName _type;

        public RawNode(string type)
        {
            _type = (StringName)type;
            GD.Print(_type);
            _children = new List<IDom>();
            if (!ClassDB.ClassExists(_type))
            {
                _instance = new Node();
            }
            else
            {
                _instance = (Node)ClassDB.Instantiate(_type);
            }
        }

        protected override void updatePropsImpl(ScriptObject newProps)
        {
            if (C.TryGetProps(newProps, "name", out object stringName))
            {
                _instance.Name = (string)stringName;
            }
            if (C.TryGetProps(newProps, "raw", out dynamic rawAttributes))
            {
                try
                {
                    var rawDynamic = (DynamicObject)rawAttributes;
                    foreach (string prop in rawDynamic.GetDynamicMemberNames())
                    {
                        GD.Print(prop, " ", rawAttributes[prop]);
                        _instance.Set(prop, (Variant)rawAttributes[prop]);
                    }
                }
                catch (Exception e)
                {
                    GD.Print(
                        @"Failed to set props for raw element. Note that the use of the raw node is 
					discouraged; it is not performant and it can only set non-resource based fields. ",
                        e
                    );
                }
            }
        }
    }
}
