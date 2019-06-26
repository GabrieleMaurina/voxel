using System.Collections.Generic;
using UnityEngine;

public class Dirt : Block
{	
	public Dirt(World world) : base(world)
	{
		mat = Resources.Load<Material>("Materials/Dirt");
		full = false;
	}

	public override void UpdateBlock(Vector3Int pos, Voxel voxel)
	{
		mesh = new Mesh();

		vertices = new Vector3[24];
		triangles = new int[24];

		for (int i = 0; i < 8; i++)
		{
			for (int j = 0; j < 3; j++)
			{
				vertices[i * 3 + j] = Utilities.DIAMOND_VERTICES[Utilities.DIAMOND_FACES[i * 3 + j]];
				triangles[i * 3 + j] = i * 3 + j;
			}
		}

		mesh.vertices = vertices;
		mesh.triangles = triangles;
		mesh.RecalculateNormals();

		voxel.VoxelUpdate(pos, mesh, mat);
	}
}
