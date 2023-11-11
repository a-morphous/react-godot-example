// CONTROL
interface ControlStyle {
	// position?: Vector2
	// rotation?: number
	// scale?: Vector2

	// focusMode?: FOCUS_MODE
	// focusNeighborBottom?: string
	// focusNeighborLeft?: string
	// focusNeighborRight?: string
	// focusNeighborTop?: string
	// focusNext?: string
	// focusPrevious?: string

	// mouseDefaultCursorShape?: CURSOR_SHAPE
	// mouseFilter?: MOUSE_FILTER
	// mouseForcePassScrollEvents?: boolean
	
	// offsetBottom?: number
	// offsetLeft?: number
	// offsetRight?: number
	// offsetTop?: number
	// pivotOffset?: Vector2

	modulate?: Color
	visible?: boolean
	zIndex?: number

	// anchorBottom?: number
	// anchorLeft?: number
	// anchorRight?: number
	// anchorTop?: number

	anchorPreset?: LAYOUT_PRESET

	// clipContents?: number
	
	// used to make up custom min size
	minWidth?: number
	minHeight?: number

	expandBehaviorH?: SIZE_FLAGS
	expandBehaviorV?: SIZE_FLAGS

	// growHorizontal?: GROW_DIRECTION
	// growVertical?: GROW_DIRECTION
	// layoutDirection?: LAYOUT_DIRECTION
}
interface ControlAttributes<T> extends DomAttributes<T> {
	children?: ReactNode | undefined
	theme?: string // path to a theme resource
	
	tooltip?: string
	onMouseEnter?: () => void
	onMouseExit?: () => void

	autoTranslate?: boolean

	// commented out stuff hasn't been implemented yet.
	style?: ControlStyle
}
interface ControlElement extends GlobalJSXElement {
	
}

interface ContainerAttributes<T> extends ControlAttributes<T> {

}

interface ButtonAttributes<T> extends ControlAttributes<T> {
	onClick?: () => void

	// disabled?: boolean
	// icon?: string // res:// path to the Texture2D for the icon
	// flat?: boolean
}

interface LabelAttributes <T> extends ContainerAttributes<T> {
	rich?: bool // makes it a rich text node
}

interface BoxAttributes <T> extends ContainerAttributes<T> {
	style?: ControlStyle & {
		separation?: number // number in pixels for how much to separate elements.
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
	}
}
