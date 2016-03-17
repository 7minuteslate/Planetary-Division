using UnityEngine;
using System.Collections;

public class CameraRotationOnGround : MonoBehaviour {

	Rigidbody rb;
	public float rotationSpeed = 4f;
	// Use this for initialization
	void Start () 
	{
		rb = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		rb.rotation *= Quaternion.AngleAxis (Input.GetAxis ("Mouse X") * rotationSpeed, transform.up);
		rb.rotation *= Quaternion.AngleAxis (Input.GetAxis ("Mouse Y") * rotationSpeed, transform.right);
	}
}
