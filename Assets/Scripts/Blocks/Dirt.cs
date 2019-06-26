using System.Collections.Generic;
using UnityEngine;

public class Dirt : Block
{
	private bool[,,] blocks = new bool[3, 3, 3];
	
	public Dirt(Dictionary<Vector3Int, sbyte> world) : base(world)
	{
		mat = Resources.Load<Material>("Materials/Dirt");
	}
}
