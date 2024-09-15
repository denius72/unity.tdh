using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loadmainscene : MonoBehaviour
{
	
	public bool finished_loading = false;
	
	private GameObject lCanvas;
	private gamelogic game;
	public LoadingScreen load;
	
	void Start()
	{
		lCanvas = GameObject.Find("Canvas_Loading");;
		
		load = lCanvas.GetComponent<LoadingScreen>();

		StartCoroutine(waitforload());
    }
	
	
	IEnumerator waitforload()
	{
		while(!finished_loading)
		{
			yield return null;
		}
		Debug.Log("started loading main scene");
		//StartCoroutine(load.LoadSceneAsync("MainMenuScene"));
		StartCoroutine(load.LoadSceneAsync("Intro"));
		//StartCoroutine(load.LoadSceneAsync("MainScene"));
		//StartCoroutine(load.LoadSceneAsync("Test01"));
	}

}
