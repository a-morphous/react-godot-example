import {GrowDirection, LayoutPreset, SizeFlags} from "gd"
import React, {useState} from "react"

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
        <control
            theme={GD.Load<Theme>("res://themes/dialogue.tres")}
            style={{
                anchorPreset: LayoutPreset.FullRect,
                expandBehaviorV: SizeFlags.ExpandFill,
            }}
        >
            {/*textbox*/}
            <control
                style={{
                    growVertical: GrowDirection.Begin,
                    anchorLeft: 0,
                    anchorRight: 1,
                    anchorTop: 0.8, // this puts it at the bottom!
                    anchorBottom: 1,

                    offsetRight: -100, // 100 pixels inset
                    offsetLeft: 100, // 100 pixels inset
                    // offsetTop: -200, // since by default the anchor top and bottom went to the same place,
                    // this effectively moves the element 'up' by 200px and makes a 200px tall box

                    minHeight: 150,
                }}
            >
                <div style={{
                    backgroundStyle: GD.Load<StyleBox>("res://assets/panel.tres"),
                    anchorPreset: LayoutPreset.FullRect,
                    expandBehaviorV: SizeFlags.ExpandFill,
                    expandBehaviorH: SizeFlags.ExpandFill,
                    minHeight: 72
                }}>
                    <label rich>{lines[currentLine]}</label>
                </div>
                
            </control>
            <control style={{
                growVertical: GrowDirection.Begin,
                growHorizontal: GrowDirection.Begin,
                anchorLeft: 1, // puts it at the right
                anchorRight: 1,
                anchorTop: 1, // this puts it at the bottom!
                anchorBottom: 1,

                minWidth: 96,
                minHeight: 64,
            }}>
                <button
                    style={{
                        expandBehaviorV: SizeFlags.ExpandFill,
                        expandBehaviorH: SizeFlags.ExpandFill,
                    }}
                    onClick={() => {
                        setCurrentLine((currentLine + 1) % lines.length)
                    }}
                >
                    Continue
                </button>
            </control>

        </control>
    )
}
