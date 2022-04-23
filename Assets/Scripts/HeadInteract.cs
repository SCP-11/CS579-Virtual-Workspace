using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadInteract : MonoBehaviour
{
    //private GameObject[] touchingObjects;
    public GameObject workSpacePrefab;
    public GameObject rightHand;

    private rightHand rightHandScript;
    // Start is called before the first frame update
    void Start()
    {
        rightHandScript = rightHand.GetComponent<rightHand>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "type1")
        {
            //for(int i = 0; i < touchingObjects.Count; i++)
            //{
            //    if(touchingObjects[i] == other.gameObject)
            //    {
            //    }
            //}
            //touchingObjects.(other.gameObject);
            //Debug.Log(touchingObjects.Count);

            if (other.gameObject.transform.parent == null)
            {
                GameObject root = Instantiate(workSpacePrefab, this.transform.position, this.transform.rotation);
                other.transform.parent = root.transform;
            }

        }

    }

}
