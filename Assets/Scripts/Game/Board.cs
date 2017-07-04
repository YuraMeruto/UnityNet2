using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour {

    [SerializeField]
    private GameObject Mass;

    void Start()
    {
        InstanceBoard();
    }
    public void InstanceBoard()
    {
        Vector3 InstancePos = Vector3.zero;
        for(int countx=0;countx<10; countx++)
        {
            for (int countz = 0; countz < 10; countz++)
            {
                Instantiate(Mass, InstancePos, Quaternion.identity);
                InstancePos.x += 1.1f;
            }
            InstancePos.x = 0;
            InstancePos.y+=1.1f;

        }
    }
}
