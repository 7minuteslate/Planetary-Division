using UnityEngine;
using System.Collections;

public class DropShipFlying : MonoBehaviour {
	Animator animator;
	public Rigidbody pod;
	public float speed = 0;
	public float descentspeed = 3;
	public bool ls;
	// Use this for initialization
	void Start () 
	{
		animator = GetComponent<Animator>();
		pod = GetComponent<Rigidbody> ();
	}
	void FixedUpdate()
	{
		Vector3 forward = Camera.main.transform.forward;
		forward.y = 0;
		forward.Normalize();
		forward.y = pod.velocity.y / speed;
		if (Input.GetKeyDown(KeyCode.LeftShift))
		{
			pod.AddForce (0, 50, 0);
			animator.SetBool("FlyUp",true);

		}
		if (Input.GetKeyUp(KeyCode.LeftShift))
		{
			pod.AddForce (0, 0, 0);
			animator.SetBool("FlyUp",false);
		}
		if (Input.GetKeyDown(KeyCode.LeftControl))
		{
			pod.AddForce (0, -50 , 0);
			animator.SetBool("FlyUp",true);

		}
		if (Input.GetKeyUp(KeyCode.LeftControl))
		{
			pod.AddForce (0, 0, 0);
			animator.SetBool("FlyUp",false);

		}
		if (Input.GetKey(KeyCode.W))
		{
			speed += Time.deltaTime;
			{
				pod.velocity += forward * speed;
				if (speed >= 1f) 
				{
					speed = 1f;
				}
			}


		}
		if (Input.GetKeyUp(KeyCode.W))
		{
			speed -= Time.deltaTime;
			{
				pod.velocity += forward * speed;
				if (speed >= 0f)
				{
					pod.AddForce (0, -1 * descentspeed, 0);
				}
					if (speed <= 0f)
					{
						speed = 0f;
						pod.AddForce (0, 0, 0);
					}
			}


		}
	}


	// Update is called once per frame
	void Update () 
	{

	}
}
