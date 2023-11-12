using System;
using Godot;
using Microsoft.ClearScript;

namespace Spectral.React
{
	public class FlowNode : DomNode<FlowContainer>
	{
		protected override void updatePropsImpl(ScriptObject newProps)
		{
			base.updatePropsImpl(newProps);

			if (C.TryGetProps(newProps, "vertical", out object vertical))
			{
				_instance.Vertical = (bool)vertical;
			}

			if (C.TryGetProps(newProps, "alignment", out object alignment))
			{
				_instance.Alignment = (FlowContainer.AlignmentMode)Convert.ToInt64(alignment);
			}

			if (
				C.TryGetStyleProps(newProps, "hSeparation", out object hSeparation)
			)
			{
				_instance.AddThemeConstantOverride("h_separation", (int)hSeparation);
			}
			if (
				C.TryGetStyleProps(newProps, "vSeparation", out object vSeparation)
			)
			{
				_instance.AddThemeConstantOverride("v_separation", (int)vSeparation);
			}
		}
	}
}
