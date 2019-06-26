using System.Collections.Generic;
using UnityEngine;

public class Concrete : Block
{
	public Concrete(Dictionary<Vector3Int, sbyte> world) : base(world)
	{
		mat = Resources.Load<Material>("Materials/Concrete");
	}
}
