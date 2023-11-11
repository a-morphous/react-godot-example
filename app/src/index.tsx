import React, { useState } from "react"
import { render } from "renderer"
import { LAYOUT_PRESET, SIZE_FLAGS } from "enums"

const App = () => {
	const [count, setCount] = useState(0)
	return (
		<div
			style={{
				anchorPreset: LAYOUT_PRESET.PRESET_FULL_RECT,
			}}
		>
			<hbox style={{
				expandBehaviorH: SIZE_FLAGS.SIZE_EXPAND_FILL,
				expandBehaviorV: SIZE_FLAGS.SIZE_EXPAND_FILL,
				separation: 20
			}}>
				<vbox style={{
					minWidth: 100,
					expandBehaviorV: SIZE_FLAGS.SIZE_EXPAND_FILL
				}}>
					<div
						style={{
							minHeight: 24,
						}}
					></div>
					<label>Sidebar</label>
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
				</vbox>
				<vbox style={{
					expandBehaviorH: SIZE_FLAGS.SIZE_EXPAND_FILL,
					expandBehaviorV: SIZE_FLAGS.SIZE_EXPAND_FILL
				}}>
					<label rich>You have increased the count [b]{count}[/b] times!</label>
					<div style={{minHeight: 24}}></div>
					<button onClick={() => {
						setCount(0)
					}}>Reset</button>
				</vbox>
			</hbox>
		</div>
	)
}

render(<App />)
