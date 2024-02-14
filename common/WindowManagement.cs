using Godot;

namespace Spectral.Utils {
	public partial class WindowManagement : Node {
		public override void _Ready() {
			DisplayServer.WindowSetMinSize(new Vector2I(320, 240));
			GD.Print("DPI: ", DisplayServer.ScreenGetDpi());
			GD.Print("Scaling: ", DisplayServer.ScreenGetScale());
			
			GetWindow().Size = GetWindow().Size * (int) DisplayServer.ScreenGetScale();

			CenterWindow();
		}

		protected void CenterWindow() {
			DisplayServer.WindowSetPosition(DisplayServer.ScreenGetPosition() + DisplayServer.ScreenGetSize()/2 - DisplayServer.WindowGetSize()/2);
		}
	}
}