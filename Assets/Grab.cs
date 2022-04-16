using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;


public class Grab : MonoBehaviour
{

    private XRController thisController;
    private bool left;
    private InputDevice input;
    // Start is called before the first frame update
    void Start()
    {
            thisController = this.GetComponent<XRController>();
            if (thisController.controllerNode == XRNode.RightHand)
            {
                left = false;
            }
            else if (thisController.controllerNode == XRNode.LeftHand)
            {
                left = true;
            }
            else
            {
                Debug.LogError("Grab script not on the hand controller");
            }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (true) {
            if (other.tag == "Grab")
            {

            }
        }
    }

}
