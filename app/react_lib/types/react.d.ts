// GENERIC STYLES

interface ControlStyle {
	// global position. For absolutely positioned controls only.
	x?: number
	y?: number

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

	modulate?: ColorType
	visible?: boolean
	zIndex?: number

	// anchorBottom?: number
	// anchorLeft?: number
	// anchorRight?: number
	// anchorTop?: number

	anchorPreset?: LayoutPreset

	// clipContents?: number

	// used to make up custom min size
	minWidth?: number
	minHeight?: number

	// used to set size
	width?: number
	height?: number

	expandBehaviorH?: SizeFlags
	expandBehaviorV?: SizeFlags

	growHorizontal?: GrowDirection
	growVertical?: GrowDirection

	layoutDirection?: LayoutDirectionEnum
}

interface FontStyle {
	font?: string // as a res:// string
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

// CONTROL
interface ControlAttributes<T> extends DomAttributes<T> {
	children?: ReactNode | undefined
	theme?: string // path to a theme resource
	name?: string

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
	icon?: string // res:// path to the Texture2D for the icon
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
			disabledStyle?: string // res string for now
			focusStyle?: string
			hoverStyle?: string
			normalStyle?: string
			pressedStyle?: string
		}
}

interface LabelAttributes<T> extends ContainerAttributes<T> {
	rich?: bool // makes it a rich text node

	style?: ControlStyle &
		FontStyle & FontShadowStyle & {
			uppercase?: bool

			// theme styles
			lineSpacing?: number
			normalStyle?: string // res:// string for StyleBox
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
		marginBottom?: number,
		marginLeft?: number,
		marginTop?: number, 
		marginBottom?: number,
	}
}

declare namespace JSX {
	interface IntrinsicElements {
		control: React.DetailedHTMLProps<ControlAttributes<ControlElement>, ControlElement>
		div: React.DetailedHTMLProps<ContainerAttributes<ControlElement>, ControlElement>
		button: React.DetailedHTMLProps<ButtonAttributes<ControlElement>, ControlElement>
		label: React.DetailedHTMLProps<LabelAttributes<ControlElement>, ControlElement>
		hbox: React.DetailedHTMLProps<BoxAttributes<ControlElement>, ControlElement>
		vbox: React.DetailedHTMLProps<BoxAttributes<ControlElement>, ControlElement>
		flow: React.DetailedHTMLProps<FlowAttributes<ControlElement>, ControlElement>
		margin: React.DetailedHTMLProps<MarginAttributes<ControlElement>, ControlElement>
	}
}
