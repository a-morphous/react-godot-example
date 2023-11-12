import React, { useRef, useState } from "react"
import { render } from "renderer"
import { LayoutPreset, MouseFilterEnum, SizeFlags } from "gd"
import { AnimatableNode } from "enums"

const App = () => {
	const [count, setCount] = useState(0)
	const texture = useRef<Texture2D>(GD.Load<Texture2D>("res://assets/boxNormal.png"))
	const position = useRef<Vector2>(new Vector2(400, 300))
	return (
		<>
			<margin
				style={{
					anchorPreset: LayoutPreset.FullRect,
					margin: 24,
				}}
			>
				<div
					style={{
						backgroundStyle: "res://assets/panel.tres",
						modulateSelf: count > 2 ? "#FFFFFFFF" : "#FFFFFF00",
						transitions: [AnimatableNode.ModulateSelf],
						transitionTimeMS: [100],
					}}
				>
					<hbox
						style={{
							expandBehaviorH: SizeFlags.ExpandFill,
							expandBehaviorV: SizeFlags.ExpandFill,
							separation: 20,
						}}
					>
						<vbox
							style={{
								minWidth: 100,
								expandBehaviorV: SizeFlags.ExpandFill,
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
								expandBehaviorH: SizeFlags.ExpandFill,
								expandBehaviorV: SizeFlags.ExpandFill,
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
						<flow
							style={{
								expandBehaviorH: SizeFlags.ExpandFill,
								expandBehaviorV: SizeFlags.ExpandFill,
							}}
						>
							<label>Testing!</label>
							<label>Testing!</label>
							<label>Testing!</label>
							<label>Testing!</label>
						</flow>
					</hbox>
					{/** Absolutely positioned elements need to have a control parent, and preferably are
					 * top-level nodes. Global positioning might not update totally correctly otherwise.
					 */}
					<control
						style={{
							zIndex: 1,
							mouseFilter: MouseFilterEnum.Ignore,
						}}
					>
						<button
							onClick={() => {
								setCount(15)
							}}
							style={{
								x: 300,
								y: 200,
								height: 48,
								font: GD.Load<Font>("res://assets/AlegreyaSC-Regular.ttf"),
								fontSize: 24,
								fontOutlineColor: "#FF0000",
								fontOutlineSize: 3,
							}}
						>
							Absolutely positioned! We need panels haha
						</button>
					</control>
				</div>
			</margin>

			<raw
				name="RawNode!"
				type="Sprite2D"
				raw={{
					position: position.current,
					texture: texture.current
				}}
			></raw>
		</>
	)
}

render(<App />)
