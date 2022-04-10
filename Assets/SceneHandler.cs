/* SceneHandler.cs*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Valve.VR.Extras;
using uDesktopDuplication;

public class SceneHandler : MonoBehaviour
{
    public SteamVR_LaserPointer laserPointer;

    void Awake()
    {
        // laserPointer.PointerIn += PointerInside;
        // laserPointer.PointerOut += PointerOutside;
        laserPointer.PointerClick += PointerClick;
    }

    public void PointerClick(object sender, PointerEventArgs e)
    {   
        // Target
        var target = e.target.gameObject;

        if(target.name != "Monitor Board")
            return;
            
        // Dimensions of target
        Vector2 size = target.GetComponent<MeshRenderer>().bounds.size;

        // Location of the target
        Vector2 targetLocation = e.target.transform.position;
        
        // Calculate the origin
        Vector2 origin = new Vector3(targetLocation.x - size.x / 2, targetLocation.y - size.y / 2);

        // Calculate the click point relative to the origin
        Vector2 clickPoint = e.point;
        Vector2 actualClickPoint = clickPoint - origin;

        // Need to invert the y axis
        actualClickPoint.y = size.y - actualClickPoint.y;
        
        uDesktopDuplication.Texture uddTexture = GameObject.FindObjectsOfType<uDesktopDuplication.Texture>()[0];
        var monitor = uddTexture.monitor;
        var clickX = (int)(actualClickPoint.x/size.x * monitor.width);
        var clickY = (int)(actualClickPoint.y/size.y * monitor.height);
        
        var clickLocationStr = "(x:" + clickX + ", y:" + clickY + ")";
        var mouseLocationStr = "(x:" + monitor.cursorX + ", y:" + monitor.cursorY  + ")";
        
        Debug.Log("Clicked on " + target.name + " at: " + clickLocationStr);
        Debug.Log("Mouse Location: " + mouseLocationStr);
    }

    // public void PointerInside(object sender, PointerEventArgs e)
    // {
    //     if (e.target.name == "Cube")
    //     {
    //         Debug.Log("Cube was entered");
    //     }
    //     else if (e.target.name == "Button")
    //     {
    //         Debug.Log("Button was entered");
    //     }
    // }

    // public void PointerOutside(object sender, PointerEventArgs e)
    // {
    //     if (e.target.name == "Cube")
    //     {
    //         Debug.Log("Cube was exited");
    //     }
    //     else if (e.target.name == "Button")
    //     {
    //         Debug.Log("Button was exited");
    //     }
    // }
}