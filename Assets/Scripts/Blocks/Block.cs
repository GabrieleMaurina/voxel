using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Block
{
	protected Dictionary<Vector3Int, sbyte> world;
	protected Material mat;
	protected Mesh mesh;

	protected bool[] faces = new bool[6];
	protected int facesNumber;
	protected Vector3[] vertices;
	protected int[] triangles;

	public Block(Dictionary<Vector3Int, sbyte> world)
	{
		this.world = world;
		mat = Resources.Load<Material>("Materials/Default");
	}

	public void UpdateBlock(Vector3Int pos, Voxel voxel)
	{
		mesh = new Mesh();

		facesNumber = 0;
		for(int i = 0; i < 6; i++)
		{
			if (!world.ContainsKey(Utilities.AROUND[i] + pos))
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
					vertices[facesNumber * 4 + j] = Utilities.CUBE_VERTICES[Utilities.FACE_VERTICES[i * 4 + j]];
				}
				for (int j = 0; j < 6; j++)
				{
					triangles[facesNumber * 6 + j] = facesNumber * 4 + Utilities.TRIANGLES[j];
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
