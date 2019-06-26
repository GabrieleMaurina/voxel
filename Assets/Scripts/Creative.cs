using System;
using System.Linq;
using UnityEngine;

public class Creative : MonoBehaviour
{
	private readonly int DESTROY = 2;

	private readonly Vector3 CROSSHAIR = new Vector3(0.5F, 0.5F, 0);

	private Ray ray;
	private RaycastHit raycastHit;
	private World world;

	void Start()
	{
		world = FindObjectOfType<World>();
	}

	void Update()
    {
		try
		{
			Click((sbyte)Enumerable.Range(0, 3).First(Input.GetMouseButtonDown));
		}
		catch (InvalidOperationException) { }
	}

	void Click(sbyte action)
	{
		ray = Camera.main.ViewportPointToRay(CROSSHAIR);
		if (Physics.Raycast(ray, out raycastHit))
		{
			Vector3Int pos = raycastHit.transform.position.toVector3Int();
			if(action == DESTROY)
			{
				action = -1;
			}
			else
			{
				Vector3 delta = raycastHit.point - raycastHit.transform.position - Vector3.one / 2f;
				if (Mathf.Abs(delta.x) > Mathf.Abs(delta.y) && Mathf.Abs(delta.x) > Mathf.Abs(delta.z))
				{
					if (delta.x > 0)
					{
						pos += Vector3Int.right;
					}
					else
					{
						pos += Vector3Int.left;
					}
				}
				else if (Mathf.Abs(delta.y) > Mathf.Abs(delta.x) && Mathf.Abs(delta.y) > Mathf.Abs(delta.z))
				{
					if (delta.y > 0)
					{
						pos += Vector3Int.up;
					}
					else
					{
						pos += Vector3Int.down;
					}
				}
				else
				{
					if (delta.z > 0)
					{
						pos += new Vector3Int(0, 0, 1);
					}
					else
					{
						pos += new Vector3Int(0, 0, -1);
					}
				}
			}
			
			world.SetBlock(pos, action);
		}
	}
}
