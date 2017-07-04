using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnterButton : MonoBehaviour {


    public void OnClick()
    {
        GetComponent<Client>().enabled = true;
    }
}
