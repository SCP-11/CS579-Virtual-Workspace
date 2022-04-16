using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class workSpace : MonoBehaviour
{
    public GameObject workSpacePrefab;
    private GameObject leftHand;
    private GameObject rightHand;
    private GameObject preParent;
    private XRController handController;
    private float grip;
    private bool inside;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void FixedUpdate()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")//if the user camera is in the box
        {
            Debug.Log("inside"+ this.gameObject);
            inside = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")//if the user camera is in the box
        {
            Debug.Log("outside" + this.gameObject);
            inside = false;
            if(this.transform.parent == null)
            {
                GameObject root = Instantiate(workSpacePrefab, null);
                this.transform.parent = root.transform;
            }
        }
    }
}
