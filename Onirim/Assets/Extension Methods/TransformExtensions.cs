using UnityEngine;

/// <summary>
/// Transform extensions.
/// </summary>
public static class TransformExtensions
{
	/// <summary>
	/// Direction to another <c>Transform</c>.
	/// </summary>
	/// <returns>The direction vector to <c>destination</c>.</returns>
	/// <param name="destination">Destination.</param>
	public static Vector3 DirectionTo(this Transform source, Transform destination)
	{
		return source.position.DirectionTo(destination.position);
	}
}
