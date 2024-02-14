import { render } from "renderer"
import { App } from "./examples/kitchen-sink"
import React from "react"

root.setStyleSheet({
	"opacity-100": {
		modulate: "#FFFFFFFF",
		scaleX: 1,
	},
	"opacity-0": {
		modulate: "#FFFFFF00",
		scaleX: 0,
	},
	"opacity-50": {
		modulate: "#FFFFFF80",
	},
})

render(<App />)
