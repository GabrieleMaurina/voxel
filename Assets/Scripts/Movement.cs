using UnityEngine;

public class Movement : MonoBehaviour
{
	private readonly KeyCode FORWARD = KeyCode.W;
	private readonly KeyCode BACK = KeyCode.S;
	private readonly KeyCode LEFT = KeyCode.A;
	private readonly KeyCode RIGHT = KeyCode.D;
	private readonly KeyCode UP = KeyCode.Space;
	private readonly KeyCode DOWN = KeyCode.LeftShift;

	private readonly float SPEED = 3f;
	private readonly float ROTATION_SPEED = 3f;

	private Quaternion rot;

	void Start()
	{
		Cursor.lockState = CursorLockMode.Locked;
	}

	void Update()
    {
		if (Input.anyKey)
		{
			rot = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
		}

        if(Input.GetKey(RIGHT) && !Input.GetKey(LEFT))
		{
			transform.position += rot * Vector3.right * SPEED * Time.deltaTime;
		}
		else if (Input.GetKey(LEFT) && !Input.GetKey(RIGHT))
		{
			transform.position += rot * Vector3.left * SPEED * Time.deltaTime;
		}

		if (Input.GetKey(FORWARD) && !Input.GetKey(BACK))
		{
			transform.position += rot * Vector3.forward * SPEED * Time.deltaTime;
		}
		else if (Input.GetKey(BACK) && !Input.GetKey(FORWARD))
		{
			transform.position += rot * Vector3.back * SPEED * Time.deltaTime;
		}

		if (Input.GetKey(UP) && !Input.GetKey(DOWN))
		{
			transform.position +=  Vector3.up * SPEED * Time.deltaTime;
		}
		else if (Input.GetKey(DOWN) && !Input.GetKey(UP))
		{
			transform.position += Vector3.down * SPEED * Time.deltaTime;
		}

		float xRotation = transform.rotation.eulerAngles.x - Input.GetAxis("Mouse Y") * ROTATION_SPEED;
		float yRotation = transform.rotation.eulerAngles.y + Input.GetAxis("Mouse X") * ROTATION_SPEED;
		
		if (xRotation > 90f && xRotation < 180f)
		{
			xRotation = 90f;
		}
		else if (xRotation < 270f && xRotation > 180f)
		{
			xRotation = 270f;
		}

		transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
	}
}
