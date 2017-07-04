using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Master : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.S))
        {
            gameObject.GetComponent<Server>().enabled = true;
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            gameObject.GetComponent<Client>().enabled = true;
        }
    }
}
