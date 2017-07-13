using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mass : MonoBehaviour {

    [SerializeField]
    private List<Material> mass_material= new List<Material>();
    private int MassNumber;

    public void SetMaterial(int num)
    {
       gameObject.GetComponent<Renderer>().material = mass_material[num];
    }


    public void SetMassNumber(int num)
    {
        MassNumber = num;
    }
    public int GetMassNumber()
    {
        return MassNumber;
    }
}
