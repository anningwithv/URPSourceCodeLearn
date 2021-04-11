using System;

namespace UnityEngine.UIElements
{
	public interface IPointerEvent
	{
		int pointerId
		{
			get;
		}

		string pointerType
		{
			get;
		}

		bool isPrimary
		{
			get;
		}

		int button
		{
			get;
		}

		int pressedButtons
		{
			get;
		}

		Vector3 position
		{
			get;
		}

		Vector3 localPosition
		{
			get;
		}

		Vector3 deltaPosition
		{
			get;
		}

		float deltaTime
		{
			get;
		}

		int clickCount
		{
			get;
		}

		float pressure
		{
			get;
		}

		float tangentialPressure
		{
			get;
		}

		float altitudeAngle
		{
			get;
		}

		float azimuthAngle
		{
			get;
		}

		float twist
		{
			get;
		}

		Vector2 radius
		{
			get;
		}

		Vector2 radiusVariance
		{
			get;
		}

		EventModifiers modifiers
		{
			get;
		}

		bool shiftKey
		{
			get;
		}

		bool ctrlKey
		{
			get;
		}

		bool commandKey
		{
			get;
		}

		bool altKey
		{
			get;
		}

		bool actionKey
		{
			get;
		}
	}
}
