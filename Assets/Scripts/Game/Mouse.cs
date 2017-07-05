using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : MonoBehaviour {

    [SerializeField]
    private LayerMask MassLayer;
    [SerializeField]
    private int PlayerNumber;
    public GameMaster gamemaster;
    // Update is called once per frame
    void Update()
    {
        if (gamemaster.Is_GameStart)
        {
            MouseAction();
        }


    }

    void MouseAction()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, MassLayer))
            {
                hit.collider.GetComponent<Mass>().SetMaterial(PlayerNumber);
            }
        }
    }
}
