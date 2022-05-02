using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Pen : MonoBehaviour
{
    public bool activate;
    public SteamVR_Action_Boolean buttonB;
    public Transform penTip;
    public GameObject ink;
    public GameObject linePrefab;
    public GameObject drawPrefab;

    private List<GameObject> touchInks;
    private List<GameObject> touchObject;
    private GameObject inkBox = null;
    private GameObject[] points;
    private GameObject point;
    private GameObject line;

    // Start is called before the first frame update
    void Start()
    {
        points = new GameObject[2];
        touchInks = new List<GameObject>();
        touchObject = new List<GameObject>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (activate && buttonB.state && touchInks.Count <= 0)
        {
            if(inkBox == null && touchObject.Count != 0)
            {
                points[0] = touchObject[touchObject.Count - 1];
                inkBox = Instantiate(drawPrefab/*new GameObject("ink box")*/, penTip.position, new Quaternion(0, 0, 0, 0));
                Debug.Log("stateDown");
            }
            point = Instantiate(ink, penTip.position, new Quaternion(0, 0, 0, 0));
            point.transform.parent = inkBox.transform;
        } else if(inkBox != null && touchInks.Count <= 0)
        {
            points[1] = touchObject[touchObject.Count - 1];
            if (points[0] != points[1])
            {
                Object.Destroy(inkBox);
                line = Instantiate(linePrefab, penTip.position, new Quaternion(0, 0, 0, 0));
                line.GetComponent<line>().start = points[0];
                line.GetComponent<line>().end = points[1];
            }
            inkBox = null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "ink")
        {
            touchInks.Add(other.gameObject);
        }else if (other.tag == "type1")
        {
            touchObject.Add(other.gameObject);
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "ink")
        {
            touchInks.Remove(other.gameObject);
        }else if (other.tag == "type1")
        {
            touchObject.Remove(other.gameObject);
        }

    }
}
