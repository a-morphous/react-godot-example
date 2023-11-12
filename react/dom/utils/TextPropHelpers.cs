using System.Collections;
using System.Text;
using Godot;
using Microsoft.ClearScript;

namespace Spectral.React
{
    public class TextPropHelpers
    {
        static readonly StringBuilder sb = new();

        public static bool ShouldUpdateTextContent(ScriptObject props) {
            if (!C.TryGetProps(props, "children", out object children)) {
                return false;
            }
            return true;
        }

        public static string GetTextContent(ScriptObject props)
        {
            if (C.TryGetProps(props, "children", out object children))
            {
                if (children != null && children is string v)
                {
                    return v;
                }

                if (children != null && children is IList)
                {
                    sb.Clear();
                    foreach (var item in (dynamic)children)
                    {
                        if (item is string itemText)
                        {
                            sb.Append(itemText);
                            continue;
                        }
                        sb.Append((object)item.ToString());
                    }

                    return sb.ToString();
                }
            }
            return "";
        }
    }
}
