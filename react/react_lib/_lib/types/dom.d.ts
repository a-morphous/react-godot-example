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

	/**
	 * Call this function *before* rendering the app to set the 'global' stylesheet for the app.
	 * This is a replacement for CSS, given that we are not in an HTML DOM environment, and
	 * allows for setting more fields than a Theme would.
	 * 
	 * A stylesheet is in the form 
	 * {
	 * 		class: { <list of styles associated with that class> }
	 * }
	 * Then, any component can set their `class` prop to one of the fields in the stylesheet
	 * to get the styles in that field's value. Note that like with React-dom, the custom-defined
	 * styles in a component will override a class's value.
	 * @param stylesheet 
	 */
	setStyleSheet(stylesheet: Record<string, ControlStyle>)
}