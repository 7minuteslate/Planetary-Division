using UnityEngine;
using System.Collections;

public class UserInput : MonoBehaviour {

	Pod spacecraftControl;



	// Use this for initialization
	void Start () 
	{
		spacecraftControl = GetComponent<Pod> ();
	}
	
	// Update is called once per frame
	void FixedUpdate()
	{
		float roll = Input.GetAxis ("Horizontal");
		float pitch = Input.GetAxis ("Vertical");
		bool airBrakes = Input.GetButton ("Fire1");
		float throttle = Input.GetAxis ("Throttle");

		spacecraftControl.Move (roll, pitch, 0, throttle, airBrakes);
	}
}
