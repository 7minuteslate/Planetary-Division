using UnityEngine;
using System.Collections;

public class UserInput : MonoBehaviour {

	Pod spacecraftControl;
	float verticalTranslate;
	float yaw;



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
		float verticalTranslate = Input.GetAxis("StationaryVertical");
		float yaw = Input.GetAxis ("Yaw");

			spacecraftControl.Move (roll, pitch, yaw, throttle, verticalTranslate, airBrakes);
	}
}
