using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadInteract : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "type1" && other.transform.parent
            )
        {
            //for(int i = 0; i < touchingObjects.Count; i++)
            //{
            //    if(touchingObjects[i] == other.gameObject)
            //    {
            //    }
            //}
            touchingObjects.Remove(other.gameObject);
            Debug.Log(touchingObjects.Count);
        }

    }

}
