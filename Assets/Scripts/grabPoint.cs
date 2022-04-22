using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class grabPoint : MonoBehaviour
{
    private GameObject leftHand;
    private GameObject rightHand;
    public GameObject preParent;
    public GameObject workSpacePrefab;
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
                else if(this.transform.parent != null)
                {
                    this.transform.parent = null;
                }
            }
        }

        if(grip <= 0.5)
        {
            if (preParent != null)
            {
                //Debug.Log("setting parent to " + preParent);
                this.transform.parent = preParent.transform;

            }
            else if (this.transform.parent != null)
            {
                this.transform.parent = null;
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
        if (controller != null)
        {
            if ((controller.controllerNode == XRNode.RightHand))
            {
                GameObject travel = preParent;
                while (travel != null)
                {
                    travel.layer = 3;
                    travel = travel.GetComponent<grabPoint>().preParent;
                }


                rightHand = other.transform.gameObject;
                //Debug.Log("Enter");

            }

        }

    }

    private void OnTriggerExit(Collider other)
    {
        XRController controller = other.GetComponent<XRController>();
        //if the collider is a hand controller
        if (controller != null)
        {
            if ((controller.controllerNode == XRNode.RightHand))
            {
                rightHand = null;
                //Debug.Log("Exit");
                GameObject travel = preParent;
                while (travel != null)
                {
                    travel.layer = 0;
                    travel = travel.GetComponent<grabPoint>().preParent;
                }

            }

        }

        if (other.tag == "Player")//if the user camera is in the box
        {
            Debug.Log("outside" + this.gameObject);
            inside = false;

            if (this.transform.parent == null)
            {
                GameObject root = Instantiate(workSpacePrefab, other.transform.position, other.transform.rotation);
                preParent = root;
            }
        }

    }
}


