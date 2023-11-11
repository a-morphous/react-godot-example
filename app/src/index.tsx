import React, { useState } from "react"
import { render } from "renderer"
import { LAYOUT_PRESET, SIZE_FLAGS } from "enums"

const App = () => {
	const [count, setCount] = useState(0)
	return (
		<>
			<div
				style={{
					anchorPreset: LAYOUT_PRESET.PRESET_FULL_RECT,
				}}
			>
				<hbox
					style={{
						expandBehaviorH: SIZE_FLAGS.SIZE_EXPAND_FILL,
						expandBehaviorV: SIZE_FLAGS.SIZE_EXPAND_FILL,
						separation: 20,
					}}
				>
					<vbox
						style={{
							minWidth: 100,
							expandBehaviorV: SIZE_FLAGS.SIZE_EXPAND_FILL,
						}}
					>
						<div
							style={{
								minHeight: 24,
							}}
						></div>
						<label>Sidebar Label</label>
						<div
							style={{
								minHeight: 24,
							}}
						></div>
						<button
							onClick={() => {
								setCount(count + 1)
							}}
						>
							Add to the count!
						</button>
						<button
							onClick={() => {
								setCount(count - 1)
							}}
						>
							Subtract the count!
						</button>
					</vbox>
					<vbox
						style={{
							expandBehaviorH: SIZE_FLAGS.SIZE_EXPAND_FILL,
							expandBehaviorV: SIZE_FLAGS.SIZE_EXPAND_FILL,
						}}
					>
						<label rich>You have increased the count [b]{count}[/b] times!</label>
						<div style={{ minHeight: 24 }}></div>
						<button
							onClick={() => {
								setCount(0)
							}}
						>
							Reset
						</button>
					</vbox>
				</hbox>
				{/** Absolutely positioned elements need to have a control parent, and preferably are
				 * top-level nodes. Global positioning might not update totally correctly otherwise.
				 */}
				<control
					style={{
						zIndex: 1,
					}}
				>
					<button
						onClick={() => {
							setCount(15)
						}}
						style={{
							x: 300,
							y: 200,
						}}
					>
						Absolutely positioned! We need panels haha
					</button>
				</control>
			</div>
		</>
	)
}

render(<App />)
