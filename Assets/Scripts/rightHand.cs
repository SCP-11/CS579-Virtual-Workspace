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
    private GameObject container;
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
                preParent = holdingObject.transform.parent;
                holdingObject.transform.parent = this.transform;
                Debug.Log("Grabing");
            }
        }
        else
        {
            if (holdingObject != null)
            {
                if (container != null && !Object.ReferenceEquals(holdingObject, container.transform.parent.gameObject))
                {
                    //Vector3 containerSize = container.GetComponent<Collider>().bounds.size;
                    //Vector3 holdingSize = holdingObject.GetComponent<Collider>().bounds.size;

                    //float relativeSize = containerSize.magnitude / holdingSize.magnitude;
                    holdingObject.transform.parent = container.transform;
                    holdingObject.transform.localRotation = new Quaternion(0, 0, 0, 0);
                    //holdingObject.transform.localScale = new Vector3(relativeSize, relativeSize, relativeSize); 
                    Debug.Log("contained");

                }
                else
                {
                    Debug.Log("holdingObject: " + holdingObject.ToString());
                    //Debug.Log("preParent: "+preParent);
                    holdingObject.transform.parent = preParent;
                    Debug.Log("released");
                }
                //container = null;
                preParent = null;
                holdingObject = null;
            }
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        //if the collider is a hand controller
        if (other.tag == "type1" && holdingObject == null)
        {
            touchingObjects.Add(other.gameObject);
            Debug.Log(touchingObjects.Count);
        }
        if(other.GetComponent<Container>() != null) 
        {
            container = other.gameObject;
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
        if (other.GetComponent<Container>() != null)
        {
            container = null;
        }


    }

}
