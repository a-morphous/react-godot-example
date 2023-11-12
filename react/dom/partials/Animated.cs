using Godot;

namespace Spectral.React {
	public struct TransitionProperties {
		public float duration;
		public Tween.TransitionType trans;
		public Tween.EaseType ease;

		public TransitionProperties(float duration, Tween.TransitionType trans, Tween.EaseType ease) {
			this.duration = duration;
			this.trans = trans;
			this.ease = ease;
		}
	}
    public interface IAnimatedDom : IDom {
        public Tween getTween(string property);
		public bool hasTransitionProperties(string property);
        public TransitionProperties getTransitionProperties(string property);
		public void setTransitionProperties(string property, TransitionProperties props);
		public void removeTransitionProperties(string property);
    }

}