using System;
using Godot;
using Microsoft.ClearScript;

namespace Spectral.React
{
	public class FlowNode : DomNode<FlowContainer>
	{
		protected override void updatePropsImpl(ScriptObject newProps)
		{
			ControlPropHelpers.InjectProps(this, _instance, _previousProps, newProps);

			if (C.TryGetProps(newProps, "vertical", out object vertical))
			{
				_instance.Vertical = (bool)vertical;
			}

			if (C.TryGetProps(newProps, "alignment", out object alignment))
			{
				_instance.Alignment = (FlowContainer.AlignmentMode)Convert.ToInt64(alignment);
			}

			ThemePropHelpers.InjectSeparationProps(_instance, _previousProps, newProps);
		}
	}
}
