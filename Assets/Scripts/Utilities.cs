using System;
using System.Collections.Generic;
using UnityEngine;

public static class Utilities
{
	public static Vector3Int toVector3Int(this Vector3 v)
	{
		return new Vector3Int(Mathf.FloorToInt(v.x), Mathf.FloorToInt(v.y), Mathf.FloorToInt(v.z));
	}

	public readonly static Vector3Int[] AROUND =
	{
		Vector3Int.down,
		Vector3Int.up,
		Vector3Int.left,
		Vector3Int.right,
		new Vector3Int(0, 0, 1),
		new Vector3Int(0, 0, -1)
	};

	public static readonly int[] FACE_VERTICES =
	{
		0, 1, 2, 3,
		7, 5, 6, 4,
		5, 1, 4, 0,
		2, 3, 6, 7,
		7, 3, 5, 1,
		0, 2, 4, 6
	};

	public static readonly Vector3[] CUBE_VERTICES =
	{
		new Vector3(0, 0, 0),
		new Vector3(0, 0, 1),
		new Vector3(1, 0, 0),
		new Vector3(1, 0, 1),
		new Vector3(0, 1, 0),
		new Vector3(0, 1, 1),
		new Vector3(1, 1, 0),
		new Vector3(1, 1, 1)
	};

	public static readonly int[] TRIANGLES = { 0, 2, 1, 1, 2, 3};

	public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
	{
		foreach (T element in source) action(element);
	}

	public static string Print<T>(this IEnumerable<T> source)
	{
		return string.Join(",", source);
	}
}
