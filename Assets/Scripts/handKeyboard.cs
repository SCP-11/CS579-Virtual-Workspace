using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class handKeyboard : MonoBehaviour
{
    public GameObject hand;
    public GameObject keyboard;
    public GameObject player;
    public float horizontalLength; //keyboard
    public float VerticalLength; //keyboard

    public int maxAngle; //degree

    private InputDevice handController;
    // Start is called before the first frame update
    void Start()
    {
        handController = hand.GetComponent<XRController>().inputDevice;
        Debug.Log(handController.name);
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = hand.transform.position;
        if (player.transform.position != hand.transform.position)
        {
            this.transform.rotation = Quaternion.LookRotation(player.transform.position - hand.transform.position, Vector3.up);
        }
        keyboard.transform.localPosition = new Vector3(calcHMovement(), calcVMovement(), keyboard.transform.localPosition.z);
        //primary2DAxis


    }

    private void FixedUpdate()
    {
    }

    private float calcHMovement() {
        float upForward = Vector3.Angle(Vector3.up, hand.transform.forward);
        Vector3 adjustedUp = Quaternion.AngleAxis(90 - upForward, hand.transform.right) * Vector3.up;
        return (Vector3.Angle(adjustedUp, -hand.transform.right) - Vector3.Angle(adjustedUp, hand.transform.right)) * horizontalLength / 2 / maxAngle;
    }

    private float calcVMovement()
    {
        Vector2 joystick;
        float Vposition = 0;
        if (handController != null)
        {
            handController.TryGetFeatureValue(CommonUsages.primary2DAxis, out joystick);
            if (joystick == null)
            {
                Debug.Log("joystick is Null");
            }
            else
            {
                Debug.Log(joystick.y);

                if (joystick.y > 0.8)
                {
                    Vposition = VerticalLength / 3;
                }else if(joystick.y < -0.8)
                {
                    Vposition = -VerticalLength / 3;
                }
                else {
                    Vposition = 0;

                }
            }
            return Vposition;
        }
        return Vposition;
    }
}
