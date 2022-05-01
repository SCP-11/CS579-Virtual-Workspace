/* SceneHandler.cs*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Valve.VR.Extras;
using System;
using System.Runtime.InteropServices;
using System.Drawing;
using uWindowCapture;


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

    // Top Most 
    [DllImport("user32.dll")] 
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

    private static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
    private const UInt32 SWP_NOSIZE = 0x0001;
    private const UInt32 SWP_NOMOVE = 0x0002;
    private const UInt32 TOPMOST_FLAGS = SWP_NOMOVE | SWP_NOSIZE;


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

    public static void MakeTopMost(IntPtr hWnd)
    {
        SetWindowPos(hWnd, HWND_TOPMOST, 0, 0, 0, 0, TOPMOST_FLAGS);
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

        // get uWindowCapture object
        var uWindowTexture = target.GetComponent<UwcWindowTexture>();
        if (uWindowTexture == null)
        {
            Debug.Log("No uWindowTexture component found on " + target.name);
            return;
        }

        var uWindow = uWindowTexture.window;

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
        
        // uDesktopDuplication.Texture uddTexture = GameObject.FindObjectsOfType<uDesktopDuplication.Texture>()[0];
        // var monitor = uddTexture.monitor;
        var clickX = (int)(actualClickPoint.x/size.x * uWindow.width) + uWindow.x;
        var clickY = (int)(actualClickPoint.y/size.y * uWindow.height) + uWindow.y;
        var clickLocationStr = "(x:" + clickX + ", y:" + clickY + ")";
        
        Debug.Log("Left Pointer Clicked on " + uWindow.title + " at: " + clickLocationStr);

        Debug.Log("Window handle: " + uWindow.handle);
        MouseController.MakeTopMost(uWindow.handle);
        MouseController.LeftMouseClick(clickX, clickY);
    }

    public void RightPointerClick(object sender, PointerEventArgs e)
    {   
        // Target
        var target = e.target.gameObject;

        // get uWindowCapture object
        var uWindowTexture = target.GetComponent<UwcWindowTexture>();
        if (uWindowTexture == null)
        {
            Debug.Log("No uWindowTexture component found on " + target.name);
            return;
        }

        var uWindow = uWindowTexture.window;

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
        
        // uDesktopDuplication.Texture uddTexture = GameObject.FindObjectsOfType<uDesktopDuplication.Texture>()[0];
        // var monitor = uddTexture.monitor;
        var clickX = (int)(actualClickPoint.x/size.x * uWindow.width) + uWindow.x;
        var clickY = (int)(actualClickPoint.y/size.y * uWindow.height) + uWindow.y;
        var clickLocationStr = "(x:" + clickX + ", y:" + clickY + ")";
        
        Debug.Log("Right Pointer Clicked on " + uWindow.title + " at: " + clickLocationStr);

        MouseController.MakeTopMost(uWindow.handle);
        MouseController.RightMouseClick(clickX, clickY);
    }
}