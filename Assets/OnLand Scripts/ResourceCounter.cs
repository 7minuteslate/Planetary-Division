using UnityEngine;
using System.Collections;

public class ResourceCounter : MonoBehaviour {

    private GameObject[] resources;

	// Use this for initialization
	void Start () {

        resources = new GameObject[transform.childCount];
	
        for (int i = 0; i <transform.childCount; i++)
        {
            resources[i] = transform.GetChild(i).gameObject;
            
        }


	}
	
	// Update is called once per frame
	void Update () {

    }
}
