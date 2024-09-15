using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox_Ztest_Shader : MonoBehaviour
{

    public Material myMaterial;

    // Start is called before the first frame update
    void Start()
    {
        myMaterial.SetInt("_ZTest", (int)UnityEngine.Rendering.CompareFunction.Always);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
