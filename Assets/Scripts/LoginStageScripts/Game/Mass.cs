using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mass : MonoBehaviour {

    [SerializeField]
    private List<Material> mass_material= new List<Material>();
	// Use this for initialization
	    
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetMaterial(int num)
    {
       gameObject.GetComponent<Renderer>().material = mass_material[num];
    }
}
