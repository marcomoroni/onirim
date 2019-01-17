using System.Collections.Generic;
using System;

/// <summary>
/// IList extensions.
/// </summary>
public static class IListExtensions
{
	/// <summary>
	/// Shuffle the list using the Fisher-Yates method.
	/// </summary>
	public static void Shuffle<T>(this IList<T> list)
	{
		Random rng = new Random();
		int n = list.Count;
		while (n > 1)
		{
			n--;
			int k = rng.Next(n + 1);
			T value = list[k];
			list[k] = list[n];
			list[n] = value;
		}
	}

	/// <summary>
	/// Returns a random item from the list.
	/// Sampling with replacement.
	/// </summary>
	/// <returns>A random item from the list.</returns>
	public static T RandomItem<T>(this IList<T> list)
	{
		if (list.Count == 0) throw new System.IndexOutOfRangeException("Cannot select a random item from an empty list.");
		return list[UnityEngine.Random.Range(0, list.Count)];
	}

	/// <summary>
	/// Removes a random item from the list, returning that item.
	/// Sampling without replacement.
	/// </summary>
	/// <returns>The item removed.</returns>
	public static T RemoveRandom<T>(this IList<T> list)
	{
		if (list.Count == 0) throw new System.IndexOutOfRangeException("Cannot remove a random item from an empty list.");
		int index = UnityEngine.Random.Range(0, list.Count);
		T item = list[index];
		list.RemoveAt(index);
		return item;
	}

	public static T GetLastItem<T>(this IList<T> list)
	{
		if (list.Count == 0) throw new System.IndexOutOfRangeException("Cannot get item from an empty list.");
		return list[list.Count - 1];
	}

	public static T GetFirstItem<T>(this IList<T> list)
	{
		if (list.Count == 0) throw new System.IndexOutOfRangeException("Cannot get item from an empty list.");
		return list[0];
	}
}
