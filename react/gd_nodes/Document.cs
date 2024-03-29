using System;
using System.Collections.Generic;
using Godot;
using Microsoft.ClearScript;
using Microsoft.ClearScript.V8;

namespace Spectral.React
{
	public partial class Document : Node
	{
		readonly List<IDom> _children;
		V8ScriptEngine _engine;

		[Export]
		bool LiveReload = false;

		ScriptObject _styleSheet = null;

		static Dictionary<string, Type> _customNodes = new();

		public Document()
		{
			_children = new List<IDom>();
		}

		public override void _Ready()
		{
			base._Ready();
			Setup();
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
			AddChild(node.getNode());
		}

		public void removeChild(IDom node)
		{
			_children.Remove(node);
			RemoveChild(node.getNode());
			node.getNode().QueueFree();
		}

		// CLASSES

		/// <summary>
		/// To be called from JS. This sets a stylesheet.
		/// </summary>
		/// <param name="obj"></param>
		public void setStyleSheet(ScriptObject obj)
		{
			_styleSheet = obj;
		}

		public ScriptObject getClassFromStyleSheet(string className)
		{
			if (_styleSheet == null)
			{
				return null;
			}
			_engine.Script.__stylesheet = _styleSheet;
			_engine.Script.__classname = className;
			object styles = _engine.Evaluate(
				@"
				__stylesheet[__classname];
			"
			);
			if (styles is ScriptObject stylesObj)
			{
				return stylesObj;
			}
			return null;
		}

		// INTEROP
		public void ExposeObjectToJS(string name, object objectToSend)
		{
			_engine.AddHostObject(name, objectToSend);
		}

		public void ExposeTypeToJS(string name, System.Type typeToSend)
		{
			_engine.AddHostType(name, typeToSend);
		}

		public V8ScriptEngine Engine()
		{
			return _engine;
		}

		// DEBUGGING
		public void PrintJSObj(object obj)
		{
			Engine().Script.__log = obj;
			Engine()
				.Evaluate(
					@"
						GD.Print(JSON.stringify(Object.keys(__log)))
					"
				);
		}

		// PRIVATE

		void Setup()
		{
			_engine = new V8ScriptEngine(
				V8ScriptEngineFlags.EnableDebugging & V8ScriptEngineFlags.EnableRemoteDebugging,
				9222
			);
			var _ = new SetTimeout(_engine);

			_engine.AddHostType("GD", typeof(GD));
			_engine.AddHostType("Color", typeof(Color));
			_engine.AddHostType("Vector2", typeof(Vector2));
			_engine.AddHostType("Texture2D", typeof(Texture2D));
			_engine.AddHostType("Theme", typeof(Theme));
			_engine.AddHostType("Font", typeof(Font));
			_engine.AddHostType("StyleBox", typeof(StyleBox));

			_engine.AddHostType("Document", typeof(Document));
			_engine.AddHostObject("root", this);

			using var file = Godot.FileAccess.Open(
				"res://app/dist/index.js",
				Godot.FileAccess.ModeFlags.Read
			);
			_engine.Execute(file.GetAsText());

			if (LiveReload)
			{
				GDScript DirectoryWatcherScript = (GDScript)
					GD.Load("res://vendor/DirectoryWatcher.gd");
				Node DirectoryWatcher = (Node)DirectoryWatcherScript.New(); // This is a GodotObject
				DirectoryWatcher.Call("add_scan_directory", "res://app/dist");
				AddChild(DirectoryWatcher);
				DirectoryWatcher.Connect(
					"files_modified",
					Callable.From<Godot.Collections.Array>(OnLiveReload)
				);
				GD.Print("Watching!");
			}
		}

		protected void OnLiveReload(Godot.Collections.Array files)
		{
			GD.Print("Live reload!");
			clearChildren();
			using var file = Godot.FileAccess.Open(
				"res://app/dist/index.js",
				Godot.FileAccess.ModeFlags.Read
			);
			try
			{
				_engine.Execute(file.GetAsText());
			}
			catch (System.Exception e)
			{
				GD.Print("Failed to run livereload ", e);
			}
		}

		public static void AddCustomNode(string type, Type nodeType) { 
			if (_customNodes.ContainsKey(type)) {
				return;
			}
			_customNodes.Add(type, nodeType);
		}

		public static IAnimatedDom createElement(
			string type,
			ScriptObject props,
			Document rootContainer = null
		)
		{
			IAnimatedDom newNode;
			if (_customNodes.ContainsKey(type))
			{
				try
				{
					newNode = (IAnimatedDom)Activator.CreateInstance(_customNodes[type]);
					return newNode;
				}
				catch (Exception e)
				{
					GD.Print(e);
				}
			}
			switch (type.ToLower())
			{
				case "textedit":
					newNode = new TextEditNode();
					break;
				case "margin":
					newNode = new MarginNode();
					break;
				case "image":
					newNode = new SpriteNode();
					break;
				case "texture":
					newNode = new TextureNode();
					break;
				case "flow":
					newNode = new FlowNode();
					break;
				case "label":
					if (C.TryGetProps(props, "rich", out object isRich))
					{
						newNode = new RichLabelNode();
						break;
					}
					newNode = new LabelNode();
					break;
				case "button":
					newNode = new ButtonNode();
					break;

				// box containers
				case "hbox":
					newNode = new HBoxNode();
					break;
				case "vbox":
					newNode = new VBoxNode();
					break;
				case "control":
					newNode = new ControlNode();
					break;
				case "div":
					if (C.TryGetStyleProps(props, "backgroundStyle", out object hasBackground))
					{
						newNode = new PanelNode();
						break;
					}
					newNode = new ContainerNode();
					break;
				case "raw":
					if (C.TryGetProps(props, "type", out object rawType))
					{
						newNode = new RawNode((string)rawType);
						break;
					}
					newNode = new DomNode<Node>();
					break;
				default:
					newNode = new ContainerNode();
					break;
			}
			if (newNode != null && rootContainer != null)
			{
				newNode.setDocument(rootContainer);
				newNode.updateProps(props);
			}
			return newNode;
		}
	}
}
