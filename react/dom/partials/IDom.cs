using Godot;
using Microsoft.ClearScript;

namespace Spectral.React {
	public interface IDom
    {
        public void setDocument(Document doc);
        public Document getDocument();
        public Node getNode();
        public void updateProps(ScriptObject newProps);

        public void clearChildren();
        public void appendChild(IDom node);
        public void removeChild(IDom node);

        // style
        // these are saved as raw JS objects.
        public ScriptObject style { get; }
        public void setStyle(ScriptObject newStyles);
        public void setClass(string className);
    }

}