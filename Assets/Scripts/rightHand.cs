using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using Valve.VR;
using Valve.VR.InteractionSystem;


public class rightHand : MonoBehaviour
{
    private List<GameObject> touchingObjects;

    public SteamVR_Action_Boolean input;
    private XRController xrController;
    private float grip;
    public Transform preParent;
    private GameObject holdingObject;
    // Start is called before the first frame update
    void Start()
    {
        /**
        xrController = GetComponent<XRController>();
        */
        touchingObjects = new List<GameObject>();
    }

    void Update()
    {
        /**XR
         * 
        xrController.inputDevice.TryGetFeatureValue(CommonUsages.grip, out grip);

        if (grip > 0.5)
        {
            //preParent = this.transform.parent.gameObject;
            if (holdingObject == null && touchingObjects.Count != 0)
            {
                holdingObject = touchingObjects[touchingObjects.Count-1];
                preParent = touchingObjects[touchingObjects.Count-1].transform.parent;
                touchingObjects[touchingObjects.Count-1].transform.parent = this.transform;
                Debug.Log("Grabing");
            }
        }
        else
        {
            if (holdingObject != null && touchingObjects.Count != 0)
            {
                holdingObject.transform.parent = preParent;
                preParent = null;
                holdingObject = null;
                Debug.Log("released");
            }
        }
        */

        if (input.state)
        {
            //preParent = this.transform.parent.gameObject;
            if (holdingObject == null && touchingObjects.Count != 0)
            {
                holdingObject = touchingObjects[touchingObjects.Count - 1];
                preParent = touchingObjects[touchingObjects.Count - 1].transform.parent;
                touchingObjects[touchingObjects.Count - 1].transform.parent = this.transform;
                Debug.Log("Grabing");
            }
        }
        else
        {
            if (holdingObject != null && touchingObjects.Count != 0)
            {
                holdingObject.transform.parent = preParent;
                preParent = null;
                holdingObject = null;
                Debug.Log("released");
            }
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        //if the collider is a hand controller
        if (other.tag == "type1")
        {
            touchingObjects.Add(other.gameObject);
            Debug.Log(touchingObjects.Count);
        }

    }

    private void OnTriggerExit(Collider other)
    {
        //if the collider is a hand controller
        if (other.tag == "type1")
        {
            //for(int i = 0; i < touchingObjects.Count; i++)
            //{
            //    if(touchingObjects[i] == other.gameObject)
            //    {
            //    }
            //}
            touchingObjects.Remove(other.gameObject);
            Debug.Log(touchingObjects.Count);
        }
    }

}
