using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour
{
	Rigidbody rb;
	public float moveSpeed = 5;
	public float rotationSpeed = 2;

	// Use this for initialization
	void Start ()
	{
		rb = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		MovePlayer (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"));
	}

	void MovePlayer (float horizontal, float vertical)
	{
		rb.AddForce (transform.forward * rb.mass * horizontal * moveSpeed);
	}
}
