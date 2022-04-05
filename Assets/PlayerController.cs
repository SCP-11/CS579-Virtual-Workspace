using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class PlayerController : MonoBehaviour
{
    public SteamVR_Action_Vector2 input;
    public float speed = 2.0f;
    private CharacterController cc;

    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = Player.instance.hmdTransform.TransformDirection(new Vector3(input.axis.x, 0, input.axis.y));
        // gravity vector
        Vector3 gravity = new Vector3(0, -9.81f, 0);
        // direction to move
        Vector3 moveDirection = Vector3.ProjectOnPlane(direction, Vector3.up) * speed + gravity;
        cc.Move(moveDirection * Time.deltaTime);
    } 
}
