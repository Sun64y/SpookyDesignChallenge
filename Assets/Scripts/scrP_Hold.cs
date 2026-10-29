using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrP_Hold : MonoBehaviour
{

    public static scrP_Hold single;


    public float reach;
    public bool isHolding = false;
    public GameObject heldItem;
    Transform holdSpot;



    private void Awake()
    {
        if (single== null) { single = this; }
    }


    void Start()
    {

        //find things
        holdSpot = transform.Find("holdSpot");
    }

    void Update()
    {

        //pick up items-------------------------------------------------------------------------
        if (scrP_Movement.single.canWalk && !isHolding && Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, reach, 1 << 6))
            {
                Debug.Log(hit.transform.name);

                isHolding = true;
                heldItem = hit.transform.gameObject;

                //held item position
                heldItem.transform.parent = holdSpot;
                heldItem.transform.localPosition = Vector3.zero;
                heldItem.transform.localEulerAngles = Vector3.zero;

                //diable held item
                heldItem.GetComponent<Rigidbody>().isKinematic = true;
                heldItem.GetComponent<Collider>().enabled = false;

            }
        }

        //quick drop items--------------------------------------------------------------------
        if (scrP_Movement.single.canWalk && isHolding && Input.GetMouseButtonDown(1))
        {
            //drop item
            heldItem.transform.parent = null;

            //enable held item
            heldItem.GetComponent<Rigidbody>().isKinematic = false;
            heldItem.GetComponent<Collider>().enabled = true;

            //stop holding
            heldItem = null;
            isHolding = false;
        }

    }
}
