using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody))]
public class FauxGravityBody : MonoBehaviour {

	public FauxGravityAttractor attractor;

	void Start () {
		GetComponent<Rigidbody>().useGravity = false;
	}

	void FixedUpdate () {
		if (attractor){
			attractor.Attract(transform);
		}
	}
	
}
