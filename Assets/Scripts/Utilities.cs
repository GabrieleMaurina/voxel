using System;
using System.Collections.Generic;
using UnityEngine;

public static class Utilities
{
	public static readonly sbyte AIR = -1;
	public static readonly sbyte CONCRETE = 0;
	public static readonly sbyte DIRT = 1;

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

	public static readonly int[] CUBE_FACES =
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

	public static readonly int[] CUBE_TRIANGLES = { 0, 2, 1, 1, 2, 3 };

	public static readonly int[] DIAMOND_FACES =
	{
		0, 5, 3,
		3, 5, 1,
		0, 2, 5,
		5, 2, 1,
		0, 3, 4,
		4, 3, 1,
		0, 4, 2,
		2, 4, 1
	};

	public static readonly Vector3[] DIAMOND_VERTICES =
	{
		new Vector3(.5f, 0, .5f),
		new Vector3(.5f, 1, .5f),
		new Vector3(0, .5f, .5f),
		new Vector3(1, .5f, .5f),
		new Vector3(.5f, .5f, 1),
		new Vector3(.5f, .5f, 0)
	};

	public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
	{
		foreach (T element in source) action(element);
	}

	public static string Print<T>(this IEnumerable<T> source)
	{
		return string.Join(",", source);
	}
}
