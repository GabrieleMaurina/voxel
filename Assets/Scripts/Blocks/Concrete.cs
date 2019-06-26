using System.Collections.Generic;
using UnityEngine;

public class Concrete : Block
{
	public Concrete(World world) : base(world)
	{
		mat = Resources.Load<Material>("Materials/Concrete");
	}
}
