/* This file contains all enums that should be mirrored in Godot. */

export enum FocusModeEnum {
	//
	// Summary:
	//     The node cannot grab focus. Use with Godot.Control.FocusMode.
	None,
	//
	// Summary:
	//     The node can only grab focus on mouse clicks. Use with Godot.Control.FocusMode.
	Click,
	//
	// Summary:
	//     The node can grab focus on mouse click, using the arrows and the Tab keys on
	//     the keyboard, or using the D-pad buttons on a gamepad. Use with Godot.Control.FocusMode.
	All,
}

export enum GrowDirection {
	//
	// Summary:
	//     The control will grow to the left or top to make up if its minimum size is changed
	//     to be greater than its current size on the respective axis.
	Begin = 0,
	//
	// Summary:
	//     The control will grow to the right or bottom to make up if its minimum size is
	//     changed to be greater than its current size on the respective axis.
	End = 1,
	//
	// Summary:
	//     The control will grow in both directions equally to make up if its minimum size
	//     is changed to be greater than its current size.
	Both = 2,
}

export enum LayoutDirectionEnum {
	//
	// Summary:
	//     Automatic layout direction, determined from the parent control layout direction.
	Inherited,
	//
	// Summary:
	//     Automatic layout direction, determined from the current locale.
	Locale,
	//
	// Summary:
	//     Left-to-right layout direction.
	Ltr,
	//
	// Summary:
	//     Right-to-left layout direction.
	Rtl,
}

export enum LayoutPreset {
	//
	// Summary:
	//     Snap all 4 anchors to the top-left of the parent control's bounds. Use with Godot.Control.SetAnchorsPreset(Godot.Control.LayoutPreset,System.Boolean).
	TopLeft,
	//
	// Summary:
	//     Snap all 4 anchors to the top-right of the parent control's bounds. Use with
	//     Godot.Control.SetAnchorsPreset(Godot.Control.LayoutPreset,System.Boolean).
	TopRight,
	//
	// Summary:
	//     Snap all 4 anchors to the bottom-left of the parent control's bounds. Use with
	//     Godot.Control.SetAnchorsPreset(Godot.Control.LayoutPreset,System.Boolean).
	BottomLeft,
	//
	// Summary:
	//     Snap all 4 anchors to the bottom-right of the parent control's bounds. Use with
	//     Godot.Control.SetAnchorsPreset(Godot.Control.LayoutPreset,System.Boolean).
	BottomRight,
	//
	// Summary:
	//     Snap all 4 anchors to the center of the left edge of the parent control's bounds.
	//     Use with Godot.Control.SetAnchorsPreset(Godot.Control.LayoutPreset,System.Boolean).
	CenterLeft,
	//
	// Summary:
	//     Snap all 4 anchors to the center of the top edge of the parent control's bounds.
	//     Use with Godot.Control.SetAnchorsPreset(Godot.Control.LayoutPreset,System.Boolean).
	CenterTop,
	//
	// Summary:
	//     Snap all 4 anchors to the center of the right edge of the parent control's bounds.
	//     Use with Godot.Control.SetAnchorsPreset(Godot.Control.LayoutPreset,System.Boolean).
	CenterRight,
	//
	// Summary:
	//     Snap all 4 anchors to the center of the bottom edge of the parent control's bounds.
	//     Use with Godot.Control.SetAnchorsPreset(Godot.Control.LayoutPreset,System.Boolean).
	CenterBottom,
	//
	// Summary:
	//     Snap all 4 anchors to the center of the parent control's bounds. Use with Godot.Control.SetAnchorsPreset(Godot.Control.LayoutPreset,System.Boolean).
	Center,
	//
	// Summary:
	//     Snap all 4 anchors to the left edge of the parent control. The left offset becomes
	//     relative to the left edge and the top offset relative to the top left corner
	//     of the node's parent. Use with Godot.Control.SetAnchorsPreset(Godot.Control.LayoutPreset,System.Boolean).
	LeftWide,
	//
	// Summary:
	//     Snap all 4 anchors to the top edge of the parent control. The left offset becomes
	//     relative to the top left corner, the top offset relative to the top edge, and
	//     the right offset relative to the top right corner of the node's parent. Use with
	//     Godot.Control.SetAnchorsPreset(Godot.Control.LayoutPreset,System.Boolean).
	TopWide,
	//
	// Summary:
	//     Snap all 4 anchors to the right edge of the parent control. The right offset
	//     becomes relative to the right edge and the top offset relative to the top right
	//     corner of the node's parent. Use with Godot.Control.SetAnchorsPreset(Godot.Control.LayoutPreset,System.Boolean).
	RightWide,
	//
	// Summary:
	//     Snap all 4 anchors to the bottom edge of the parent control. The left offset
	//     becomes relative to the bottom left corner, the bottom offset relative to the
	//     bottom edge, and the right offset relative to the bottom right corner of the
	//     node's parent. Use with Godot.Control.SetAnchorsPreset(Godot.Control.LayoutPreset,System.Boolean).
	BottomWide,
	//
	// Summary:
	//     Snap all 4 anchors to a vertical line that cuts the parent control in half. Use
	//     with Godot.Control.SetAnchorsPreset(Godot.Control.LayoutPreset,System.Boolean).
	VcenterWide,
	//
	// Summary:
	//     Snap all 4 anchors to a horizontal line that cuts the parent control in half.
	//     Use with Godot.Control.SetAnchorsPreset(Godot.Control.LayoutPreset,System.Boolean).
	HcenterWide,
	//
	// Summary:
	//     Snap all 4 anchors to the respective corners of the parent control. Set all 4
	//     offsets to 0 after you applied this preset and the Godot.Control will fit its
	//     parent control. Use with Godot.Control.SetAnchorsPreset(Godot.Control.LayoutPreset,System.Boolean).
	FullRect,
}

export enum MouseFilterEnum {
	//
	// Summary:
	//     The control will receive mouse button input events through Godot.Control._GuiInput(Godot.InputEvent)
	//     if clicked on. And the control will receive the Godot.Control.MouseEntered and
	//     Godot.Control.MouseExited signals. These events are automatically marked as handled,
	//     and they will not propagate further to other controls. This also results in blocking
	//     signals in other controls.
	Stop = 0,
	//
	// Summary:
	//     The control will receive mouse button input events through Godot.Control._GuiInput(Godot.InputEvent)
	//     if clicked on. And the control will receive the Godot.Control.MouseEntered and
	//     Godot.Control.MouseExited signals. If this control does not handle the event,
	//     the parent control (if any) will be considered, and so on until there is no more
	//     parent control to potentially handle it. This also allows signals to fire in
	//     other controls. If no control handled it, the event will be passed to Godot.Node._UnhandledInput(Godot.InputEvent)
	//     for further processing.
	Pass = 1,
	//
	// Summary:
	//     The control will not receive mouse button input events through Godot.Control._GuiInput(Godot.InputEvent).
	//     The control will also not receive the Godot.Control.MouseEntered nor Godot.Control.MouseExited
	//     signals. This will not block other controls from receiving these events or firing
	//     the signals. Ignored events will not be handled automatically.
	Ignore = 2,
}

export enum CursorShape {
	//
	// Summary:
	//     Show the system's arrow mouse cursor when the user hovers the node. Use with
	//     Godot.Control.MouseDefaultCursorShape.
	Arrow = 0,
	//
	// Summary:
	//     Show the system's I-beam mouse cursor when the user hovers the node. The I-beam
	//     pointer has a shape similar to "I". It tells the user they can highlight or insert
	//     text.
	Ibeam = 1,
	//
	// Summary:
	//     Show the system's pointing hand mouse cursor when the user hovers the node.
	PointingHand = 2,
	//
	// Summary:
	//     Show the system's cross mouse cursor when the user hovers the node.
	Cross = 3,
	//
	// Summary:
	//     Show the system's wait mouse cursor when the user hovers the node. Often an hourglass.
	Wait = 4,
	//
	// Summary:
	//     Show the system's busy mouse cursor when the user hovers the node. Often an arrow
	//     with a small hourglass.
	Busy = 5,
	//
	// Summary:
	//     Show the system's drag mouse cursor, often a closed fist or a cross symbol, when
	//     the user hovers the node. It tells the user they're currently dragging an item,
	//     like a node in the Scene dock.
	Drag = 6,
	//
	// Summary:
	//     Show the system's drop mouse cursor when the user hovers the node. It can be
	//     an open hand. It tells the user they can drop an item they're currently grabbing,
	//     like a node in the Scene dock.
	CanDrop = 7,
	//
	// Summary:
	//     Show the system's forbidden mouse cursor when the user hovers the node. Often
	//     a crossed circle.
	Forbidden = 8,
	//
	// Summary:
	//     Show the system's vertical resize mouse cursor when the user hovers the node.
	//     A double-headed vertical arrow. It tells the user they can resize the window
	//     or the panel vertically.
	Vsize = 9,
	//
	// Summary:
	//     Show the system's horizontal resize mouse cursor when the user hovers the node.
	//     A double-headed horizontal arrow. It tells the user they can resize the window
	//     or the panel horizontally.
	Hsize = 10,
	//
	// Summary:
	//     Show the system's window resize mouse cursor when the user hovers the node. The
	//     cursor is a double-headed arrow that goes from the bottom left to the top right.
	//     It tells the user they can resize the window or the panel both horizontally and
	//     vertically.
	Bdiagsize = 11,
	//
	// Summary:
	//     Show the system's window resize mouse cursor when the user hovers the node. The
	//     cursor is a double-headed arrow that goes from the top left to the bottom right,
	//     the opposite of Godot.Control.CursorShape.Bdiagsize. It tells the user they can
	//     resize the window or the panel both horizontally and vertically.
	Fdiagsize = 12,
	//
	// Summary:
	//     Show the system's move mouse cursor when the user hovers the node. It shows 2
	//     double-headed arrows at a 90 degree angle. It tells the user they can move a
	//     UI element freely.
	Move = 13,
	//
	// Summary:
	//     Show the system's vertical split mouse cursor when the user hovers the node.
	//     On Windows, it's the same as Godot.Control.CursorShape.Vsize.
	Vsplit = 14,
	//
	// Summary:
	//     Show the system's horizontal split mouse cursor when the user hovers the node.
	//     On Windows, it's the same as Godot.Control.CursorShape.Hsize.
	Hsplit = 15,
	//
	// Summary:
	//     Show the system's help mouse cursor when the user hovers the node, a question
	//     mark.
	Help = 16,
}

export enum SizeFlags {
	//
	// Summary:
	//     Tells the parent Godot.Container to align the node with its start, either the
	//     top or the left edge. It is mutually exclusive with Godot.Control.SizeFlags.Fill
	//     and other shrink size flags, but can be used with Godot.Control.SizeFlags.Expand
	//     in some containers. Use with Godot.Control.SizeFlagsHorizontal and Godot.Control.SizeFlagsVertical.
	//
	//
	//     Note: Setting this flag is equal to not having any size flags.
	ShrinkBegin = 0,
	//
	// Summary:
	//     Tells the parent Godot.Container to expand the bounds of this node to fill all
	//     the available space without pushing any other node. It is mutually exclusive
	//     with shrink size flags. Use with Godot.Control.SizeFlagsHorizontal and Godot.Control.SizeFlagsVertical.
	Fill = 1,
	//
	// Summary:
	//     Tells the parent Godot.Container to let this node take all the available space
	//     on the axis you flag. If multiple neighboring nodes are set to expand, they'll
	//     share the space based on their stretch ratio. See Godot.Control.SizeFlagsStretchRatio.
	//     Use with Godot.Control.SizeFlagsHorizontal and Godot.Control.SizeFlagsVertical.
	Expand = 2,
	//
	// Summary:
	//     Sets the node's size flags to both fill and expand. See Godot.Control.SizeFlags.Fill
	//     and Godot.Control.SizeFlags.Expand for more information.
	ExpandFill = 3,
	//
	// Summary:
	//     Tells the parent Godot.Container to center the node in the available space. It
	//     is mutually exclusive with Godot.Control.SizeFlags.Fill and other shrink size
	//     flags, but can be used with Godot.Control.SizeFlags.Expand in some containers.
	//     Use with Godot.Control.SizeFlagsHorizontal and Godot.Control.SizeFlagsVertical.
	ShrinkCenter = 4,
	//
	// Summary:
	//     Tells the parent Godot.Container to align the node with its end, either the bottom
	//     or the right edge. It is mutually exclusive with Godot.Control.SizeFlags.Fill
	//     and other shrink size flags, but can be used with Godot.Control.SizeFlags.Expand
	//     in some containers. Use with Godot.Control.SizeFlagsHorizontal and Godot.Control.SizeFlagsVertical.
	ShrinkEnd = 8,
}

export enum AlignmentMode {
	//
	// Summary:
	//     The child controls will be arranged at the beginning of the container, i.e. top
	//     if orientation is vertical, left if orientation is horizontal (right for RTL
	//     layout).
	Begin = 0,
	//
	// Summary:
	//     The child controls will be centered in the container.
	Center = 1,
	//
	// Summary:
	//     The child controls will be arranged at the end of the container, i.e. bottom
	//     if orientation is vertical, right if orientation is horizontal (left for RTL
	//     layout).
	End = 2,
}

export enum TransitionType {
	//
	// Summary:
	//     The animation is interpolated linearly.
	Linear,
	//
	// Summary:
	//     The animation is interpolated using a sine function.
	Sine,
	//
	// Summary:
	//     The animation is interpolated with a quintic (to the power of 5) function.
	Quint,
	//
	// Summary:
	//     The animation is interpolated with a quartic (to the power of 4) function.
	Quart,
	//
	// Summary:
	//     The animation is interpolated with a quadratic (to the power of 2) function.
	Quad,
	//
	// Summary:
	//     The animation is interpolated with an exponential (to the power of x) function.
	Expo,
	//
	// Summary:
	//     The animation is interpolated with elasticity, wiggling around the edges.
	Elastic,
	//
	// Summary:
	//     The animation is interpolated with a cubic (to the power of 3) function.
	Cubic,
	//
	// Summary:
	//     The animation is interpolated with a function using square roots.
	Circ,
	//
	// Summary:
	//     The animation is interpolated by bouncing at the end.
	Bounce,
	//
	// Summary:
	//     The animation is interpolated backing out at ends.
	Back,
	//
	// Summary:
	//     The animation is interpolated like a spring towards the end.
	Spring,
}

export enum EaseType {
	//
	// Summary:
	//     The interpolation starts slowly and speeds up towards the end.
	In,
	//
	// Summary:
	//     The interpolation starts quickly and slows down towards the end.
	Out,
	//
	// Summary:
	//     A combination of Godot.Tween.EaseType.In and Godot.Tween.EaseType.Out. The interpolation
	//     is slowest at both ends.
	InOut,
	//
	// Summary:
	//     A combination of Godot.Tween.EaseType.In and Godot.Tween.EaseType.Out. The interpolation
	//     is fastest at both ends.
	OutIn,
}

export enum AutowrapMode {
	//
	// Summary:
	//     Autowrap is disabled.
	Off,
	//
	// Summary:
	//     Wraps the text inside the node's bounding rectangle by allowing to break lines
	//     at arbitrary positions, which is useful when very limited space is available.
	Arbitrary,
	//
	// Summary:
	//     Wraps the text inside the node's bounding rectangle by soft-breaking between
	//     words.
	Word,
	//
	// Summary:
	//     Behaves similarly to Godot.TextServer.AutowrapMode.Word, but force-breaks a word
	//     if that single word does not fit in one line.
	WordSmart,
}

export enum ExpandModeEnum {
	//
	// Summary:
	//     The minimum size will be equal to texture size, i.e. Godot.TextureRect can't
	//     be smaller than the texture.
	KeepSize,
	//
	// Summary:
	//     The size of the texture won't be considered for minimum size calculation, so
	//     the Godot.TextureRect can be shrunk down past the texture size.
	IgnoreSize,
	//
	// Summary:
	//     The height of the texture will be ignored. Minimum width will be equal to the
	//     current height. Useful for horizontal layouts, e.g. inside Godot.HBoxContainer.
	FitWidth,
	//
	// Summary:
	//     Same as Godot.TextureRect.ExpandModeEnum.FitWidth, but keeps texture's aspect
	//     ratio.
	FitWidthProportional,
	//
	// Summary:
	//     The width of the texture will be ignored. Minimum height will be equal to the
	//     current width. Useful for vertical layouts, e.g. inside Godot.VBoxContainer.
	FitHeight,
	//
	// Summary:
	//     Same as Godot.TextureRect.ExpandModeEnum.FitHeight, but keeps texture's aspect
	//     ratio.
	FitHeightProportional,
}

export enum StretchModeEnum {
	//
	// Summary:
	//     Scale to fit the node's bounding rectangle.
	Scale,
	//
	// Summary:
	//     Tile inside the node's bounding rectangle.
	Tile,
	//
	// Summary:
	//     The texture keeps its original size and stays in the bounding rectangle's top-left
	//     corner.
	Keep,
	//
	// Summary:
	//     The texture keeps its original size and stays centered in the node's bounding
	//     rectangle.
	KeepCentered,
	//
	// Summary:
	//     Scale the texture to fit the node's bounding rectangle, but maintain the texture's
	//     aspect ratio.
	KeepAspect,
	//
	// Summary:
	//     Scale the texture to fit the node's bounding rectangle, center it and maintain
	//     its aspect ratio.
	KeepAspectCentered,
	//
	// Summary:
	//     Scale the texture so that the shorter side fits the bounding rectangle. The other
	//     side clips to the node's limits.
	KeepAspectCovered,
}

export enum LineWrappingMode {
	//
	// Summary:
	//     Line wrapping is disabled.
	None,
	//
	// Summary:
	//     Line wrapping occurs at the control boundary, beyond what would normally be visible.
	Boundary,
}
