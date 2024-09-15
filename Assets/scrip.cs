using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrip : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Vector3 boxSize = GetComponent<Collider>().bounds.size;
        Debug.Log("X: "+boxSize.x+" Y: "+boxSize.y+" Z: "+boxSize.z);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
