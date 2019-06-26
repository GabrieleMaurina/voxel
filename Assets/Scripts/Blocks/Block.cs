using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Block
{
	protected World world;
	protected Material mat;
	protected Mesh mesh;

	protected bool[] faces = new bool[6];
	protected int facesNumber;
	protected Vector3[] vertices;
	protected int[] triangles;

	public bool full = true;

	public Block(World world)
	{
		mat = Resources.Load<Material>("Materials/Default");
		this.world = world;
	}

	public virtual void UpdateBlock(Vector3Int pos, Voxel voxel)
	{
		mesh = new Mesh();

		facesNumber = 0;
		for(int i = 0; i < 6; i++)
		{
			if (!world.world.TryGetValue(Utilities.AROUND[i] + pos, out sbyte id) || !world.blocks[id].full)
			{
				faces[i] = true;
				facesNumber++;
			}
			else
			{
				faces[i] = false;
			}
		}

		vertices = new Vector3[facesNumber * 4];
		triangles = new int[facesNumber * 6];

		facesNumber = 0;
		for(int i = 0; i < 6; i++)
		{
			if (faces[i])
			{
				for (int j = 0; j < 4; j++)
				{
					vertices[facesNumber * 4 + j] = Utilities.CUBE_VERTICES[Utilities.CUBE_FACES[i * 4 + j]];
				}
				for (int j = 0; j < 6; j++)
				{
					triangles[facesNumber * 6 + j] = facesNumber * 4 + Utilities.CUBE_TRIANGLES[j];
				}
				facesNumber++;
			}
		}

		mesh.vertices = vertices;
		mesh.triangles = triangles;
		mesh.RecalculateNormals();

		voxel.VoxelUpdate(pos, mesh, mat);
	}
}
