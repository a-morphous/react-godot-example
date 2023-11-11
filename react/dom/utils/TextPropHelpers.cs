using System.Collections;
using System.Text;
using Godot;
using Microsoft.ClearScript;

namespace Spectral.React {
    public class TextPropHelpers {
        static readonly StringBuilder sb = new();

        public static string GetTextContent(ScriptObject props) {
            var children = props.GetProperty("children");

            if (children != null && children is string v) {
                return v;
            }

            if (children != null && children is IList) {
                sb.Clear();
                foreach (var item in (dynamic)children) {
                    if (item is string itemText) {
                        sb.Append(itemText);
                        continue;
                    }
                    sb.Append((object)item.ToString());
                }

                return sb.ToString();
            }

            return "";
        }
    }
}
