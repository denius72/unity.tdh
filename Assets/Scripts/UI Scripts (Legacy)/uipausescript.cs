using System.Collections;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class uipausescript : MonoBehaviour
{
	
	public MainScript mainscr;
	public bool finished_loading = false;
	public bool fadein = false;
	public bool startfadecontrol = false;
	public MasterControls controls;
	public static bool gamePaused;
	public AudioClip clip;
	
	public Texture2D pausebg;
	private GameObject pausebgobj;
	private Sprite pausebgSp;
	private float pausebgobjoriginalposx = 0.0f;
	private float pausebgobjnewposx = 0.0f;
	
	private GameObject mCanvas;
	
	private GameObject lCanvas;
	private gamelogic game;
	
	void Start()
	{
		lCanvas = GameObject.Find("Canvas_Loading");;
		game = lCanvas.GetComponent<gamelogic>();
		
		double timer = Time.realtimeSinceStartup;
        mCanvas = GameObject.Find("Canvas");;
		pausebgobj = new GameObject();
		pausebgobj.name = "pausebg";
		pausebgobj.AddComponent<AudioSource>();
		
		controls = new MasterControls();
		controls.Menu.Enable();
		controls.Player.Disable();
		
		//------------------------------//
		
		pausebgobj.AddComponent<CanvasRenderer>();
		pausebgobj.AddComponent<RectTransform>();
		pausebgobj.AddComponent<Mask>();
		pausebgobj.AddComponent<Image>();
		pausebgobj.AddComponent<CanvasGroup>();

		//converter Texture2D para Sprite
		pausebgSp = Sprite.Create(pausebg, new Rect(0.0f, 0.0f, pausebg.width, pausebg.height), new Vector2(0.5f, 0.5f), 100.0f);

		//Pos + size
		//float minX = mCanvas.GetComponent<RectTransform>().position.x + mCanvas.GetComponent<RectTransform>().rect.xMin;
		//float minY = mCanvas.GetComponent<RectTransform>().position.y + mCanvas.GetComponent<RectTransform>().rect.yMin;
		float z = mCanvas.GetComponent<RectTransform>().position.z;
		
		pausebgobj.transform.position = new Vector3(mCanvas.transform.position.x*5.0f, mCanvas.transform.position.y, z);//pos
		//pausebgobj.transform.position = new Vector3(Screen.width*2.0f, Screen.height*0.5f, z);//pos
																							   //float cameraHeight = Camera.main.orthographicSize * 2;
																							   //float cameraWidth = cameraHeight * Screen.width / Screen.height; // cameraHeight * aspect ratio
																							   //txtpausebg.transform.localScale = Vector3.one * cameraHeight / 5.0f;
		pausebgobj.transform.SetParent(mCanvas.transform);

		//Rect do componente txtpausebg
		RectTransform rt = pausebgobj.GetComponent<RectTransform>();
		rt.sizeDelta = new Vector2(pausebg.width, pausebg.height);

		//Render + Listener
		pausebgobj.GetComponent<Image>().sprite = pausebgSp; //Override
		CanvasGroup transp1 = pausebgobj.GetComponent<CanvasGroup>();
		transp1.alpha = 0.9f;
		//txtpausebg.GetComponent<Button>().onClick.AddListener(Testmethod);
		
		pausebgobj.transform.localScale = new Vector3(1.3f, 1.3f, 1);
		pausebgobj.transform.SetAsLastSibling();
		
		updateposition();

		finished_loading = true;
		Debug.Log("uipausescript: "+(Time.realtimeSinceStartup - timer));
		StartCoroutine(coroutinecontrol());
		
	}
	
	public bool isPaused()
	{
		return gamePaused;
	}
	
	IEnumerator coroutinecontrol()
	{
		while(mainscr == null)
			yield return null;
			
		while(!mainscr.finishedloading)
			yield return null;
		
		StartCoroutine(FadeControl());
		StartCoroutine(backgroundanimation());
	}
	
    void Update()
    {
		
		pausebgobj.transform.SetAsLastSibling();
		if(controls.Menu.Pause.triggered)
		{
			gamePaused = !gamePaused;
			PauseGame();
		}
		//Debug.Log("pause frame: "+pausebgobj.transform.position.x);
    }
	/*
	private void OnApplicationFocus(bool hasFocus)
    {
        if (!hasFocus && (game.gamestate != gamelogic.GameState.MAIN_MENU))
        {
			gamePaused = true;
			PauseGame();
        }
    }*/
	
	IEnumerator backgroundanimation()
	{
		while(pausebgobj.activeSelf)
		{
			pausebgobj.transform.Rotate(0.0f, 0.0f, Time.unscaledDeltaTime* Mathf.Cos(Time.realtimeSinceStartup), Space.World);
			yield return null;
		}
		yield return null;
	}
	
	void PauseGame()
    {
		if(gamePaused)
		{
			Time.timeScale = 0f;
			AudioListener.pause = true;

			fadein = true;
			startfadecontrol = true;
			pausebgobj.GetComponent<AudioSource>().clip = clip;
			pausebgobj.GetComponent<AudioSource>().ignoreListenerPause=true;
			pausebgobj.GetComponent<AudioSource>().volume = game.SFXVolume;
			pausebgobj.GetComponent<AudioSource>().Play();
			
		}
		else if(!gamePaused)
		{
			
			controls.Player.Enable();
			Time.timeScale = 1;
			AudioListener.pause = false;
			
			fadein = false;
			startfadecontrol = true;
		}
    }
	
	bool fadeindone = true;
	bool fadeoutdone = true;
	IEnumerator FadeControl()
	{
		while(true)
		{
			if(startfadecontrol)
				if(fadein)
				{
					controls.Menu.Disable();
					startfadecontrol = false; //queues up in case it becomes true during execution
					if(fadeindone)
						StartCoroutine(FadeIn());
					while(!fadeindone)
						yield return null;
					controls.Menu.Enable();
					
				}
				else
				{
					startfadecontrol = false;
					if(fadeoutdone)
						StartCoroutine(FadeOut());
					while(!fadeoutdone)
						yield return null;
				}
			
			yield return null;
		}
	}
	
	//Do not call FadeOut or FadeIn directly
	IEnumerator FadeIn()
	{
		fadeindone = false;
		while (pausebgobj.transform.position.x > pausebgobjnewposx + 10)
		{
			pausebgobj.transform.position = Vector3.Lerp(pausebgobj.transform.position, new Vector3(pausebgobjnewposx, pausebgobj.transform.position.y, pausebgobj.transform.position.z), 8f * Time.unscaledDeltaTime);
			yield return null;
		}
		fadeindone = true;
		yield return null;
	}

	IEnumerator FadeOut()
	{
		fadeoutdone = false;
		while (pausebgobj.transform.position.x < pausebgobjoriginalposx - 10)
		{
			pausebgobj.transform.position = Vector3.Lerp(pausebgobj.transform.position, new Vector3(pausebgobjoriginalposx, pausebgobj.transform.position.y, pausebgobj.transform.position.z), 12f * Time.unscaledDeltaTime);
			yield return null;
		}
		fadeoutdone = true;
		yield return null;
	}

	void updateposition()
	{
		pausebgobjoriginalposx = pausebgobjoriginalposx = pausebgobj.transform.position.x;
		pausebgobj.transform.position = new Vector3(pausebgobjoriginalposx, pausebgobj.transform.position.y, pausebgobj.transform.position.z);
		
		pausebgobjnewposx = mCanvas.transform.position.x * 2.0f;
	}
}
