import { LayoutPreset, SizeFlags } from "gd"
import React, { useRef } from "react"

const store = {
	text: "",
}

export const App = () => {
	const ref = useRef<IDom>()
	return (
		<div
			style={{
				anchorPreset: LayoutPreset.FullRect,
			}}
		>
			<vbox
				style={{
					expandBehaviorH: SizeFlags.ExpandFill,
					expandBehaviorV: SizeFlags.ExpandFill,
				}}
			>
				{/* toolbar */}
				<flow style={{ expandBehaviorH: SizeFlags.ExpandFill }}>
					<button
						onClick={() => {
							GD.Print(store.text)
						}}
					>
						Print Text
					</button>
					<button>Edit</button>
					<button>These do nothing right now!</button>
				</flow>
				<hbox
					style={{
						expandBehaviorH: SizeFlags.ExpandFill,
						expandBehaviorV: SizeFlags.ExpandFill,
					}}
				>
					{/* sidebar */}
					<vbox
						style={{
							expandBehaviorV: SizeFlags.ExpandFill,
							minWidth: 240,
						}}
					>
						<div
							style={{
								backgroundStyle: GD.Load<StyleBox>("res://assets/panel.tres"),
							}}
						>
							<vbox>
								<button
									onClick={() => {
										GD.Print(ref.current.getNode())
										ref.current.getNode().Set("text", "")
									}}
								>
									Clear Textedit
								</button>
								<control
									style={{
										minHeight: 24,
									}}
								></control>
								<label rich>
									This is a sample textedit app to show how React GD works! In an
									actual app, you can also attach additional C# instances and
									functions to the `root` object and then call those functions
									from JS
								</label>
							</vbox>
						</div>
					</vbox>
					<textedit
						placeholder="Write something here..."
						minimap={true}
						style={{
							expandBehaviorH: SizeFlags.ExpandFill,
							expandBehaviorV: SizeFlags.ExpandFill,
						}}
						ref={ref}
						onTextChanged={(text: string) => {
							store.text = text
						}}
					></textedit>
				</hbox>
			</vbox>
		</div>
	)
}
