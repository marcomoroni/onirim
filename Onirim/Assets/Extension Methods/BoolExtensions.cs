/// <summary>
/// Boolean extensions.
/// </summary>
public static class BoolExtensions 
{
	/// <summary>
	/// Toggle this instance.
	/// </summary>
	/// <returns>The toggled value.</returns>
	public static bool Toggle(this bool item)
	{
		return !item;
	}
}
