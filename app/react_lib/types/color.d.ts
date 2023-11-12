// https://docs.godotengine.org/en/stable/classes/class_color.html
declare class Color {
	A: number
	A8: number
	B: number
	B8: number
	G: number
	G8: number
	H: number
	R: number
	R8: number
	S: number
	V: number
	constructor()
	constructor(from: Color, alpha: number)
	constructor(from: Color)
	constructor(code: string)
	constructor(code: string, alpha: number)
	constructor(r: number, g: number, b: number)
	constructor(r: number, g: number, b: number, a: number)

	static FromString(str: string, def: Color)

	// TODO: this doesn't seem to work properly! I'll have to figure that out...
	// static FromHtml(str: string)
}

declare var ColorType = Color | string