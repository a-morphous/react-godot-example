import React, { createContext, useContext, useEffect, useRef, useState } from "react"

export interface TransitionBaseProps {
    class?: string
    children?: any
    style?: ControlStyle
    enter?: string
    enterFrom?: string
    enterTo?: string
    leave?: string
    leaveFrom?: string
    leaveTo?: string
}

export interface TransitionProps extends TransitionBaseProps {
    show: boolean
    appear?: boolean
}

export interface TransitionChildProps extends TransitionBaseProps {

}

const TransitionContext = createContext({} as any)

/**
 * Setting ref.current.style.display should be the more performance approach, but
 * right now there seems to be an UI Toolkit bug that causes issues when animating 
 * opacity and setting display values at the same time. 
 */

const TransitionBase = ({ class: classProp, children, enter, enterFrom, enterTo, leave, leaveFrom, leaveTo, style }: TransitionBaseProps) => {
    const ref = useRef<IDom>()
    const firstRender = useRef(true)
    const transCount = useRef(0)

    const { showing, appear } = useContext(TransitionContext)
    const [reallyShowing, setReallyShowing] = useState(showing as boolean)

    useEffect(() => {
		if (firstRender.current) {
            // ref.current.style.display = showing && appear ? "Flex" : "None"
			ref.current.setStyle({
				visible: showing && appear ?  true : false
			})
            ref.current.setClass(`${classProp} ${!showing ? enter : leave} ${!showing ? enterFrom : leaveFrom}`)
            if (showing && appear) {
                ref.current.setClass(`${classProp} ${enter} ${enterTo}`)
            }
            firstRender.current = false
        } else {
            transCount.current = 0
            ref.current.setClass(`${classProp} ${showing ? enter : leave} ${showing ? enterTo : leaveTo}`)
            if (!reallyShowing && showing) {
                ref.current.setStyle({
					visible: true
				})
                setReallyShowing(showing)
            }
        }
    }, [showing])

    const onTransitionRun = () => {
		transCount.current++
    }

    const onTransitionEnd = () => {
        transCount.current--
		if (firstRender.current || transCount.current > 0) return
        ref.current.setClass(`${classProp} ${showing ? leave : enter} ${showing ? leaveFrom : enterFrom}`)
        if (!showing) {
            ref.current.setStyle({
				visible: false
			})
            setReallyShowing(showing)
        }
    }

    const classStr = showing && appear && firstRender.current ? `${classProp} ${enter} ${enterFrom}` : classProp

    return <control name="Transition" ref={ref} class={`${classStr}`} style={style} onTransitionRun={onTransitionRun} onTransitionEnd={onTransitionEnd}>
        {/*reallyShowing ? children : null*/}
		{children}
    </control>
}

export const Transition = ({ class: classProp, children, show, appear, enter, enterFrom, enterTo, leave, leaveFrom, leaveTo, style }: TransitionProps) => {
    const firstRun = useRef(false)
    const [showing, setShowing] = useState(show)

    useEffect(() => {
		setShowing(show)
    }, [show])

    firstRun.current = true

    return <TransitionContext.Provider value={{ showing, appear }}>
        {<TransitionBase key="transition" class={classProp} style={style} enter={enter} enterFrom={enterFrom} enterTo={enterTo} leave={leave} leaveFrom={leaveFrom} leaveTo={leaveTo}>{children}</TransitionBase>}
    </TransitionContext.Provider>
}
