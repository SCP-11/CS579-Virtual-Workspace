using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class Ray : MonoBehaviour
{
    public int range;
    public GameObject pointer;
    public LayerMask mask = new LayerMask();
    public float scale = 100; 

    private float distance;
    private Color rayColor;
    private GameObject targetInstance;
    private Vector2 joystick;
    private GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        targetInstance = Instantiate(pointer, this.transform.position + this.transform.forward * distance, new Quaternion(0,0,0, 0));
    }

    // Update is called once per frame
    void Update()
    {
        targetInstance.transform.localScale = new Vector3(distance / scale, distance / scale, distance / scale);

    }

    private void FixedUpdate()
    {
        var leftHandedControllers = new List<UnityEngine.XR.InputDevice>();
        var desiredCharacteristics = UnityEngine.XR.InputDeviceCharacteristics.HeldInHand | UnityEngine.XR.InputDeviceCharacteristics.Right | UnityEngine.XR.InputDeviceCharacteristics.Controller;
        UnityEngine.XR.InputDevices.GetDevicesWithCharacteristics(desiredCharacteristics, leftHandedControllers);

        //Debug.Log(string.Format("Device name '{0}' has characteristics '{1}'", leftHandedControllers[0].name, leftHandedControllers[0].characteristics.ToString()));
        if (leftHandedControllers.Count != 0)
        {
            leftHandedControllers[0].TryGetFeatureValue(CommonUsages.primary2DAxis, out joystick);
        }

        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
        {
            distance = hit.distance;
            rayColor = Color.yellow;
            targetInstance.transform.position = hit.point;
            target = hit.transform.gameObject;
            //Debug.Log("Did Hit");
        }
        else
        {
            distance = range;
            rayColor = Color.green;
            //Debug.Log("Did not Hit");
            target = null;
            targetInstance.transform.position = this.transform.position + this.transform.forward * distance;


        }
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * distance, rayColor);

        if (target != null) 
        {
            if(joystick.y > 0.5)
            {
                target.transform.localScale += new Vector3(1, 1, 1) * Time.deltaTime;
            }else if(joystick.y < -0.5)
            {
                if (target.transform.localScale.x > 0.05 && target.transform.localScale.y > 0.05 && target.transform.localScale.z > 0.05)
                {
                    target.transform.localScale -= new Vector3(1, 1, 1) * Time.deltaTime;
                }
            }
        }
    }
}
