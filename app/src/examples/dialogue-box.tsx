import { GrowDirection, LayoutPreset, SizeFlags } from "gd"
import React, { useState } from "react"

const lines = [
	"This is a dialogue box!",
	"Use the button to advance.",
	"In a real game, you'd probably want to integrate something more sophisticated than an array of strings...",
	"But if you're using rich text, BBCode [b]styling[/b] will also work out of the box.",
	"Also, this will loop once you get to the end.",
]

export const App = () => {
	const [currentLine, setCurrentLine] = useState(0)
	return (
		<div
			theme={GD.Load<Theme>("res://themes/dialogue.tres")}
			style={{
				anchorPreset: LayoutPreset.FullRect,
				expandBehaviorV: SizeFlags.ExpandFill,
			}}
		>
			<vbox
				style={{
					expandBehaviorH: SizeFlags.ExpandFill,
					expandBehaviorV: SizeFlags.ExpandFill,
					growVertical: GrowDirection.End,
				}}
			>
				<div
					style={{
						expandBehaviorH: SizeFlags.ExpandFill,
						expandBehaviorV: SizeFlags.ExpandFill,
					}}
				></div>

				{/*textbox*/}
				<margin style={{
					margin: 12,
					expandBehaviorV: SizeFlags.ShrinkEnd,
					minHeight: 72
				}}>
					<label rich>{lines[currentLine]}</label>
				</margin>

				<control>
					<button
						style={{
							x: 1000,
							y: 600,
						}}
						onClick={() => {
							setCurrentLine((currentLine + 1) % lines.length)
						}}
					>
						Continue
					</button>
				</control>
			</vbox>
		</div>
	)
}
