using System;

namespace UnityEngine.UIElements.Experimental
{
	public interface ITransitionAnimations
	{
		ValueAnimation<float> Start(float from, float to, int durationMs, Action<VisualElement, float> onValueChanged);

		ValueAnimation<Rect> Start(Rect from, Rect to, int durationMs, Action<VisualElement, Rect> onValueChanged);

		ValueAnimation<Color> Start(Color from, Color to, int durationMs, Action<VisualElement, Color> onValueChanged);

		ValueAnimation<Vector3> Start(Vector3 from, Vector3 to, int durationMs, Action<VisualElement, Vector3> onValueChanged);

		ValueAnimation<Vector2> Start(Vector2 from, Vector2 to, int durationMs, Action<VisualElement, Vector2> onValueChanged);

		ValueAnimation<Quaternion> Start(Quaternion from, Quaternion to, int durationMs, Action<VisualElement, Quaternion> onValueChanged);

		ValueAnimation<StyleValues> Start(StyleValues from, StyleValues to, int durationMs);

		ValueAnimation<StyleValues> Start(StyleValues to, int durationMs);

		ValueAnimation<float> Start(Func<VisualElement, float> fromValueGetter, float to, int durationMs, Action<VisualElement, float> onValueChanged);

		ValueAnimation<Rect> Start(Func<VisualElement, Rect> fromValueGetter, Rect to, int durationMs, Action<VisualElement, Rect> onValueChanged);

		ValueAnimation<Color> Start(Func<VisualElement, Color> fromValueGetter, Color to, int durationMs, Action<VisualElement, Color> onValueChanged);

		ValueAnimation<Vector3> Start(Func<VisualElement, Vector3> fromValueGetter, Vector3 to, int durationMs, Action<VisualElement, Vector3> onValueChanged);

		ValueAnimation<Vector2> Start(Func<VisualElement, Vector2> fromValueGetter, Vector2 to, int durationMs, Action<VisualElement, Vector2> onValueChanged);

		ValueAnimation<Quaternion> Start(Func<VisualElement, Quaternion> fromValueGetter, Quaternion to, int durationMs, Action<VisualElement, Quaternion> onValueChanged);

		ValueAnimation<Rect> Layout(Rect to, int durationMs);

		ValueAnimation<Vector2> TopLeft(Vector2 to, int durationMs);

		ValueAnimation<Vector2> Size(Vector2 to, int durationMs);

		ValueAnimation<float> Scale(float to, int duration);

		ValueAnimation<Vector3> Position(Vector3 to, int duration);

		ValueAnimation<Quaternion> Rotation(Quaternion to, int duration);
	}
}
