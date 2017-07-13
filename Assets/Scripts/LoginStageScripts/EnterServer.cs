using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterServer : MonoBehaviour {

    public void OnClick()
    {
        GetComponent<Server2>().enabled = true;
    }

}
