using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Container : MonoBehaviour
{
    public float range = 1;
    private Transform[] allChildren;
    private int childNum;
    // Start is called before the first frame update
    void Start()
    {
        childNum = this.transform.childCount;
        Debug.Log("child count: " + childNum);


    }

    // Update is called once per frame
    void Update()
    {
        if(childNum != this.transform.childCount)
        {
            childNum = this.transform.childCount;
            Debug.Log("child count: " + childNum);

            updateChildPosition();
        }
    }

    private void updateChildPosition()
    {
        int sideLength = Mathf.CeilToInt(Mathf.Pow(childNum, 1f / 3f));
        Debug.Log("side length: " + sideLength);
        float gap = range / sideLength;
        float x = 0;
        float y = 0;
        float z = 0;
        for (int i = 0; i < childNum; i++)
        {
            Transform child = this.transform.GetChild(i);
            if (x < 2*range)
            {
                child.localPosition = new Vector3(-range + gap + x, -range + gap, -range + gap);
                x = x + 2 * gap;
            }
            else if (y < 2*range)
            {
                child.localPosition = new Vector3(-range + gap, -range + gap + y, -range + gap);
                y = y + 2 * gap;
            }
            else if (z < 2*range)
            {
                child.localPosition = new Vector3(-range + gap, -range + gap, -range + gap  + z);
                z = z + 2 * gap;
            }
            float containerSize = this.GetComponent<Collider>().bounds.size.magnitude;
            float childSize = child.GetComponent<Collider>().bounds.size.magnitude;
            Debug.Log("containerSize: " + containerSize);
            Debug.Log("childSize: " + childSize);
            float relativeSize = containerSize / childSize;
            child.localScale = child.localScale * relativeSize / sideLength;

        }

    }
}
