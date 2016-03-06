using UnityEngine;
using System.Collections;

public class CollectResources : MonoBehaviour {
//
 //   GameObject canvas;
	// Use this for initialization
	void Start () {

 //       canvas = GameObject.Find("Canvas");
	}
	
	// Update is called once per frame
	void Update () {

        RaycastHit hit;

        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        if (Physics.Raycast(transform.position, fwd, out hit, 3f))
        {
            if (hit.collider.gameObject.tag.Equals("Rock"))
            {
                print(hit.collider.gameObject.name);
                Destroy(hit.collider.gameObject);
            }
        }
    }
}
