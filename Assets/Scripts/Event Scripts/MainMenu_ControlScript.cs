using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu_ControlScript : MonoBehaviour
{
	public GameObject sakura;
	
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
		sakura.transform.Rotate(0.0f, -30.0f * Time.deltaTime, 0.0f, Space.World);
    }
}
