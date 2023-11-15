import React, { useRef, useState } from "react"
import { render } from "renderer"
import { ExpandModeEnum, LayoutPreset, MouseFilterEnum, SizeFlags } from "gd"
import { AnimatableNode } from "enums"
import { Transition } from "./components/Transition"

const App = () => {
	const [count, setCount] = useState(0)
	const ref = useRef<IDom>()
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
						transitionTimeMS: [500],
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
						<vbox
							style={{
								expandBehaviorH: SizeFlags.ExpandFill,
								expandBehaviorV: SizeFlags.ExpandFill,
							}}
						>
							<flow
								style={{
									expandBehaviorH: SizeFlags.ExpandFill,
									expandBehaviorV: SizeFlags.Expand,
								}}
							>
								<label>Testing!</label>
								<label class="opacity-50">Testing!</label>
								<label>Testing!</label>
								<control>
									<Transition
										show={count > 0}
										appear={false}
										style={{
											transitions: [
												AnimatableNode.Modulate,
												AnimatableNode.ScaleX,
											],
											transitionTimeMS: [100, 100],
										}}
										enterFrom="opacity-0"
										enterTo="opacity-100"
										leaveFrom="opacity-100"
										leaveTo="opacity-0"
									>
										<>
											<label>Testing!</label>
											<texture
												texture={GD.Load<Texture2D>(
													"res://assets/fish_icon.png"
												)}
												style={{
													minWidth: 64,
													minHeight: 64,
													width: 64,
													height: 64,
												}}
											></texture>
										</>
									</Transition>
								</control>
								<control>
									{/*<image
								texture={GD.Load<Texture2D>("res://assets/fish_icon.png")}
								style={{
									scaleX: 0.1,
									scaleY: 0.1,
								}}
							></image>*/}
								</control>
							</flow>
							<textedit
								minimap={true}
								style={{
									expandBehaviorH: SizeFlags.ExpandFill,
									expandBehaviorV: SizeFlags.ExpandFill,
								}}
							></textedit>
						</vbox>
					</hbox>

					{/** Absolutely positioned elements need to have a control parent, and preferably are
					 * top-level nodes. Global positioning might not update totally correctly otherwise.
					 */}
					<control
						ref={ref}
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
					texture: texture.current,
				}}
			></raw>
		</>
	)
}

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
