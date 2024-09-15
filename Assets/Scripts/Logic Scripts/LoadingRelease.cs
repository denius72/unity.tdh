using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingRelease : MonoBehaviour
{
	
	private GameObject lCanvas;
	private LoadingScreen load;
	
    void Start()
    {
		lCanvas = GameObject.Find("Canvas_Loading");;
		
		LoadingScreen load = lCanvas.GetComponent<LoadingScreen>();
		
		StartCoroutine(load.fadeout());
        
    }

}
