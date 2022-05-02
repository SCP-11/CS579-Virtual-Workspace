using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenFollow : MonoBehaviour
{
    public GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        this.transform.localScale += new Vector3(-2 * this.transform.localScale.z, 0,0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if(target != null)
        {
            transform.LookAt(target.transform);
        }
    }
}
