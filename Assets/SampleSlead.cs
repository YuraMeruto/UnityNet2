using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using System.Threading;
public class SampleSlead : MonoBehaviour {

    Thread thre;
    int count =0;
	// Use this for initialization
	void Start () {
        thre = new Thread(Hoge);
        thre.Start();
        for (int count = 0; count <= 100; count++)
        {
            Debug.Log("これはメインの方" + count);
        }
    }
	
	// Update is called once per frame
	void Update () {
 

    }

    void Hoge()
    {
        for (int count=0;count<=100;count++)
        {
            Debug.Log("これはマルチの方"+count);
        }
    }
}
