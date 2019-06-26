using UnityEngine;

public class Voxel : MonoBehaviour
{
	protected MeshFilter mf;
	protected MeshRenderer mr;
	protected MeshCollider mc;

    void Awake()
    {
		mf = GetComponent<MeshFilter>();
		mr = GetComponent<MeshRenderer>();
        mc = GetComponent<MeshCollider>();
	}

	public void VoxelUpdate(Vector3Int pos, Mesh mesh, Material mat)
	{
		transform.position = pos;
		mf.mesh = mesh;
		mc.sharedMesh = mesh;
		mr.material = mat;
	}
}
