using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class World : MonoBehaviour
{
	private readonly sbyte AIR = -1;
	private readonly sbyte CONCRETE = 0;
	private readonly sbyte DIRT = 1;

	private readonly int POOL_MAX_SIZE = 10;

	public GameObject voxelPrefab;
	private ConcurrentBag<Voxel> voxelsPool = new ConcurrentBag<Voxel>();
	private Dictionary<Vector3Int, sbyte> world;
	private Dictionary<Vector3Int, Voxel> voxels;
	private Block[] blocks;

	void Start()
	{
		world = new Dictionary<Vector3Int, sbyte>();
		voxels = new Dictionary<Vector3Int, Voxel>();
		blocks = new Block[]
		{
			new Concrete(world),
			new Dirt(world)
		};
		SpawnPlatform();
	}

	public void SetBlock(Vector3Int pos, sbyte id)
	{
		if (id == AIR)
		{
			if (world.Count > 1)
			{
				world.Remove(pos);
				UpdateBlock(pos);
				UpdateAround(pos);
			}
		}
		else
		{
			world[pos] = id;
			UpdateBlock(pos);
			UpdateAround(pos);
		}
	}

	void UpdateBlock(Vector3Int pos)
	{
		Voxel voxel;
		if (world.TryGetValue(pos, out sbyte id))
		{
			if (voxels.TryGetValue(pos, out voxel))
			{
				if (ShouldBeRendered(pos))
				{
					blocks[id].UpdateBlock(pos, voxel);
				}
				else
				{
					voxels.Remove(pos);
					ReleaseVoxel(voxel);
				}
			}
			else if (ShouldBeRendered(pos))
			{
				voxel = GetVoxel();
				voxels[pos] = voxel;
				blocks[id].UpdateBlock(pos, voxel);
			}
		}
		else if(voxels.TryGetValue(pos, out voxel))
		{
			voxels.Remove(pos);
			ReleaseVoxel(voxel);
		}
	}

	void UpdateAround(Vector3Int pos)
	{
		Utilities.AROUND.ForEach(delta => UpdateBlock(pos + delta));
	}

	bool ShouldBeRendered(Vector3Int pos)
	{
		return Utilities.AROUND.Any(delta =>
		{
			return !world.ContainsKey(pos + delta);
		});
	}

	Voxel GetVoxel()
	{
		if (voxelsPool.TryTake(out Voxel voxel))
		{
			voxel.gameObject.SetActive(true);
			return voxel;
		}
		return Instantiate(voxelPrefab).GetComponent<Voxel>();
	}

	void ReleaseVoxel(Voxel voxel)
	{
		if(voxelsPool.Count >= POOL_MAX_SIZE)
		{
			Destroy(voxel.gameObject);
		}
		else
		{
			voxel.gameObject.SetActive(false);
			voxelsPool.Add(voxel);
		}
	}

	private void SpawnPlatform()
	{
		//SetBlock(new Vector3Int(0, 0, 0), CONCRETE);
		Fill(new Vector3Int(10, 0, 10), new Vector3Int(-10, -2, -10), CONCRETE);
	}

	public void Fill(Vector3Int p1, Vector3Int p2, sbyte id)
	{
		Vector3Int p = new Vector3Int();
		int tmp;

		if(p1.x > p2.x)
		{
			tmp = p1.x;
			p1.x = p2.x;
			p2.x = tmp;
		}
		if (p1.y > p2.y)
		{
			tmp = p1.y;
			p1.y = p2.y;
			p2.y = tmp;
		}
		if (p1.z > p2.z)
		{
			tmp = p1.z;
			p1.z = p2.z;
			p2.z = tmp;
		}

		for (int x = p1.x; x <= p2.x; x++)
			for (int y = p1.y; y <= p2.y; y++)
				for (int z = p1.z; z <= p2.z; z++)
				{
					p.x = x;
					p.y = y;
					p.z = z;
					if (id == AIR)
						world.Remove(p);
					else
						world[p] = id;
				}

		for (int x = p1.x; x <= p2.x; x++)
			for (int y = p1.y; y <= p2.y; y++)
				for (int z = p1.z; z <= p2.z; z++)
				{
					p.x = x;
					p.y = y;
					p.z = z;
					if (x == p1.x || x == p2.x || y == p1.y || y == p2.y || z == p1.z || z == p2.z)
						SetBlock(p, id);
					else
					{
						if (voxels.TryGetValue(p, out Voxel v))
						{
							ReleaseVoxel(v);
							voxels.Remove(p);
						}
					}
				}
	}
}
