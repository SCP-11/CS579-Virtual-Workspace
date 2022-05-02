using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class sphereKeyboard : MonoBehaviour
{
    public GameObject hand;
    public GameObject player;

    private InputDevice handController;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = hand.transform.position;
        if (player.transform.position != hand.transform.position)
        {
            this.transform.rotation = Quaternion.LookRotation(player.transform.position - hand.transform.position, Vector3.up);
        }
        //keyboard.transform.localPosition = new Vector3(calcHMovement(), calcVMovement(), keyboard.transform.localPosition.z);
        //primary2DAxis


    }
}
