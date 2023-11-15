// GENERIC STYLES

/**
 * Used to define animations.
 * All the elements are arrays, and each index in the array points to the same transition style.
 * So if you want to translate width and height by 200ms and 400ms respectively,
 * your transitions array should be
 * [AnimatableNode.Width, AnimatableNode.Height]
 * and your transitionTimeMS array should be
 * [200, 400]
 */
interface AnimationStyle {
	transitions?: AnimatableNode[]
	transitionTimeMS?: number[]
	transitionTypes?: TransitionType[]
	transitionEasing?: EaseType[]
}

interface NodeStyle {
	// global position. For Sprite2D, it's local, for Control, it's global.
	x?: number
	y?: number

	modulate?: ColorType
	modulateSelf?: ColorType
	visible?: boolean
	zIndex?: number

	scaleX?: number
	scaleY?: number
}

interface ControlStyle extends NodeStyle, AnimationStyle {
	// rotation?: number
	// scale?: Vector2

	focusMode?: FOCUS_MODE
	// focusNeighborBottom?: string
	// focusNeighborLeft?: string
	// focusNeighborRight?: string
	// focusNeighborTop?: string
	// focusNext?: string
	// focusPrevious?: string

	mouseDefaultCursorShape?: CursorShape
	mouseFilter?: MouseFilterEnum
	mouseForcePassScrollEvents?: boolean

	// offsetBottom?: number
	// offsetLeft?: number
	// offsetRight?: number
	// offsetTop?: number
	// pivotOffset?: Vector2

	// anchorBottom?: number
	// anchorLeft?: number
	// anchorRight?: number
	// anchorTop?: number

	anchorPreset?: LayoutPreset

	// clipContents?: number

	// used to set size
	width?: number
	height?: number

	// used to make up custom min size
	minWidth?: number
	minHeight?: number

	expandBehaviorH?: SizeFlags
	expandBehaviorV?: SizeFlags

	growHorizontal?: GrowDirection
	growVertical?: GrowDirection

	layoutDirection?: LayoutDirectionEnum
}

interface FontStyle {
	font?: FontType // as a res:// string
	fontSize?: number
	fontColor?: ColorType
	fontOutlineColor?: ColorType
	fontOutlineSize?: number
}

interface FontShadowStyle {
	fontShadowColor?: ColorType
	shadowOffsetX?: number
	shadowOffsetY?: number
	shadowOutlineSize?: number
}

interface CanvasItemAttributes<T> extends DomAttributes<T> {
	children?: ReactNode | undefined
	name?: string

	onTransitionRun?: () => void
	onTransitionEnd?: () => void
}

// CONTROL
interface ControlAttributes<T> extends CanvasItemAttributes<T> {
	class?: string

	theme?: ThemeType // path to a theme resource

	tooltip?: string
	onMouseEnter?: () => void
	onMouseExit?: () => void

	autoTranslate?: boolean

	// commented out stuff hasn't been implemented yet.
	style?: ControlStyle
}
interface ControlElement extends GlobalJSXElement {}

interface ContainerAttributes<T> extends ControlAttributes<T> {}

interface ButtonAttributes<T> extends ControlAttributes<T> {
	onClick?: () => void

	clipText?: boolean
	disabled?: boolean
	expandIcon?: boolean
	icon?: Texture2DType // res:// path to the Texture2D for the icon
	flat?: boolean

	style?: ControlStyle &
		FontStyle & {
			// theme styles
			fontDisabledColor?: ColorType
			fontFocusColor?: ColorType
			fontHoverColor?: ColorType
			fontPressedColor?: ColorType
			fontHoverPressedColor?: ColorType
			iconColor?: ColorType
			iconDisabledColor?: ColorType
			iconFocusColor?: ColorType
			iconHoverColor?: ColorType
			iconPressedColor?: ColorType
			iconHoverPressedColor?: ColorType
			hSeparation?: number
			iconMaxWidth?: number

			// TODO: Maybe allow making styleboxes in js
			disabledStyle?: StyleBoxType // res string for now
			focusStyle?: StyleBoxType
			hoverStyle?: StyleBoxType
			normalStyle?: StyleBoxType
			pressedStyle?: StyleBoxType
		}
}

interface LabelAttributes<T> extends ContainerAttributes<T> {
	rich?: bool // makes it a rich text node

	// for rich box only
	contextMenuEnabled?: boolean
	deselectOnFocusLoss?: boolean

	style?: ControlStyle &
		FontStyle &
		FontShadowStyle & {
			uppercase?: bool
			autowrapMode?: AutowrapMode

			// theme styles
			lineSpacing?: number
			normalStyle?: StyleBoxType // res:// string for StyleBox

			// for rich box only
			fitContent?: boolean
			scrollActive?: boolean
			scrollFollowing?: boolean
			selectionEnabled?: boolean

			// rich theme styles
			// normal font uses regular font styles
			boldFont?: FontType
			boldItalicFont?: FontType
			italicFont?: FontType
			monoFont?: FontType
			boldFontSize?: number
			boldItalicFontSize?: number
			italicFontSize?: number
			monoFontSize?: number

			focusStyle?: StyleBoxType
		}
}

interface BoxAttributes<T> extends ContainerAttributes<T> {
	style?: ControlStyle & {
		separation?: number // number in pixels for how much to separate elements.
	}
}

interface FlowAttributes<T> extends ContainerAttributes<T> {
	alignment?: AlignmentMode
	vertical?: boolean
	style?: ControlStyle & {
		hSeparation?: number // number in pixels for how much to separate elements.
		vSeparation?: number
	}
}

interface MarginAttributes<T> extends ContainerAttributes<T> {
	style?: ControlStyle & {
		marginBottom?: number
		marginLeft?: number
		marginTop?: number
		marginBottom?: number
		margin?: number
	}
}

interface PanelAttributes<T> extends ContainerAttributes<T> {
	style?: ControlStyle & {
		backgroundStyle?: StyleBoxType // res:// string pointing to a stylebox
	}
}

interface TextureRectAttributes<T> extends ControlAttributes<T> {
	texture: TextureType
	style?: ControlStyle & {
		flipH?: boolean
		flipV?: boolean
		expandMode?: ExpandModeEnum
		stretchMode?: StretchModeEnum
	}
}

interface SpriteAttributes<T> extends Omit<CanvasItemAttributes<T>, "children"> {
	texture: TextureType
	frame?: number
	style?: NodeStyle & {
		rotation?: number
		scaleX?: number
		scaleY?: number
	}
}

interface TextEditAttributes<T> extends ControlAttributes<T> {
	text?: string
	placeholder?: string
	minimap?: boolean,
	editable?: boolean,
	wrapMode?: LineWrappingMode,
	onTextChanged?: (text: string) => void
}

interface RawNodeAttributes<T> extends CanvasItemAttributes<T> {
	type?: string
	raw?: Record<string, any>
}

declare namespace JSX {
	interface IntrinsicElements {
		control: React.DetailedHTMLProps<ControlAttributes<ControlElement>, ControlElement>
		div: React.DetailedHTMLProps<PanelAttributes<ControlElement>, ControlElement>
		button: React.DetailedHTMLProps<ButtonAttributes<ControlElement>, ControlElement>
		label: React.DetailedHTMLProps<LabelAttributes<ControlElement>, ControlElement>
		hbox: React.DetailedHTMLProps<BoxAttributes<ControlElement>, ControlElement>
		vbox: React.DetailedHTMLProps<BoxAttributes<ControlElement>, ControlElement>
		flow: React.DetailedHTMLProps<FlowAttributes<ControlElement>, ControlElement>
		margin: React.DetailedHTMLProps<MarginAttributes<ControlElement>, ControlElement>
		texture: React.DetailedHTMLProps<TextureRectAttributes<ControlElement>, ControlElement>
		image: React.DetailedHTMLProps<SpriteAttributes<ControlElement>, ControlElement>
		textedit: React.DetailedHTMLProps<TextEditAttributes<ControlElement>, ControlElement>

		// used when the actual node is not available.
		/**
		 * @deprecated This is only useful for debugging, because it is slow! Used when a node hasn't
		 * been defined in other contexts.
		 */
		raw: React.DetailedHTMLProps<RawNodeAttributes<GlobalJSXElement>, GlobalJSXElement>
	}
}
