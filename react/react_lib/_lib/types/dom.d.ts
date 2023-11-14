declare class IDom {
	setDocument(doc: Document): void
	getDocument(): Document
	updateProps(newProps: ComponentProps<any>)
	clearChildren(): void
	appendChild(node: IDom): void
	removeChild(node: IDom): void
	style: Record<string, any>
	setStyle(newStyle: Record<string, any>)
	setClass(className: string)
}

declare class Document {
	clearChildren(): void
	appendChild(node: IDom): void
	removeChild(node: IDom): void
	static createElement(type: string, props: ComponentProps<any>, rootContainer: Document): IDom

	setStyleSheet(stylesheet: Record<string, ControlStyle>)
}