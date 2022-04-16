using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class grabPoint : MonoBehaviour
{
    private GameObject leftHand;
    private GameObject rightHand;
    private GameObject preParent;
    private XRController handController;
    private float grip;
    private bool inside;

    // Start is called before the first frame update
    void Start()
    {
        if (this.transform.parent != null)
        {
            preParent = this.transform.parent.gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!inside)
        {
            if (rightHand != null)
            {
                rightHand.GetComponent<XRController>().inputDevice.TryGetFeatureValue(CommonUsages.grip, out grip);

                if (grip > 0.5)
                {
                    //preParent = this.transform.parent.gameObject;
                    this.transform.parent = rightHand.transform;

                }
                else
                {
                    if (preParent != null)
                    {
                        //Debug.Log("setting parent to " + preParent);
                        this.transform.parent = preParent.transform;
                    }
                    else
                    {
                        this.transform.parent = null;
                    }
                }
            }
        }

    }

    private void FixedUpdate()
    {
 
    }

    private void OnTriggerEnter(Collider other)
    {
        XRController controller = other.GetComponent<XRController>();
        //if the collider is a hand controller
        if (controller != null && (controller.controllerNode == XRNode.LeftHand))
        {
            leftHand = other.transform.gameObject;
            //Debug.Log("Enter");
        }

        if (controller != null && (controller.controllerNode == XRNode.RightHand))
        {
            rightHand = other.transform.gameObject;
            //Debug.Log("Enter");
        }

    }

    private void OnTriggerExit(Collider other)
    {
        XRController controller = other.GetComponent<XRController>();
        //if the collider is a hand controller
        if (controller != null && (controller.controllerNode == XRNode.LeftHand))
        {
            leftHand = null;
            //Debug.Log("Exit");
        }
        if (controller != null && (controller.controllerNode == XRNode.RightHand))
        {
            rightHand = null;
            //Debug.Log("Exit");
        }

    }

}
