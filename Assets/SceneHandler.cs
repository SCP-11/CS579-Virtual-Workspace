/* SceneHandler.cs*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Valve.VR.Extras;
using uDesktopDuplication;
using System;
using System.Runtime.InteropServices;
using System.Drawing;


class MouseController {
    //This is a replacement for Cursor.Position in WinForms
    [System.Runtime.InteropServices.DllImport("user32.dll")]
    static extern bool SetCursorPos(int x, int y);

    [System.Runtime.InteropServices.DllImport("user32.dll")]
    public static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

    private const int MOUSEEVENTF_LEFTDOWN = 0x02;
    private const int MOUSEEVENTF_LEFTUP = 0x04;
    private const int MOUSEEVENTF_MIDDLEDOWN = 0x20;
    private const int MOUSEEVENTF_MIDDLEUP = 0x40;
    private const int MOUSEEVENTF_RIGHTDOWN = 0x08;
    private const int MOUSEEVENTF_RIGHTUP = 0x10;


    //This simulates a left mouse click
    public static void LeftMouseClick(int xpos, int ypos)
    {
        SetCursorPos(xpos, ypos);
        mouse_event(MOUSEEVENTF_LEFTDOWN, xpos, ypos, 0, 0);
        mouse_event(MOUSEEVENTF_LEFTUP, xpos, ypos, 0, 0);
    }

    public static void RightMouseClick(int xpos, int ypos)
    {
        SetCursorPos(xpos, ypos);
        mouse_event(MOUSEEVENTF_RIGHTDOWN, xpos, ypos, 0, 0);
        mouse_event(MOUSEEVENTF_RIGHTUP, xpos, ypos, 0, 0);
    }
}

public class SceneHandler : MonoBehaviour
{

    
    public SteamVR_LaserPointer leftLaserPointer;
    public SteamVR_LaserPointer rightLaserPointer;

    void Awake()
    {
        // laserPointer.PointerIn += PointerInside;
        // laserPointer.PointerOut += PointerOutside;
        leftLaserPointer.PointerClick += LeftPointerClick;
        rightLaserPointer.PointerClick += RightPointerClick;
    }

    public void LeftPointerClick(object sender, PointerEventArgs e)
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
        MouseController.LeftMouseClick(clickX, clickY);
    }

    public void RightPointerClick(object sender, PointerEventArgs e)
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
        MouseController.RightMouseClick(clickX, clickY);
    }
}