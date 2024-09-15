using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Test01 : MonoBehaviour
{
	
	public MainScript mainscr;
	//public uiplayerframe playerframe;
	public Scripo textbox;
	public uiplayerscript playerscript;
	public uiclockscript clockscript;
	public uibossframe bossframe;
	public GameObject mCanvas;
	public MasterControls controls;
	public LoadingScreen load;
	public gamelogic game;
	
	private GameObject lCanvas;
	
    // Start is called before the first frame update
    void Start()
    {
		lCanvas = GameObject.Find("Canvas_Loading");;
		load = lCanvas.GetComponent<LoadingScreen>();
		game = lCanvas.GetComponent<gamelogic>();
        mCanvas = GameObject.Find("Canvas");
		StartCoroutine(samplescript());
    }

	IEnumerator samplescript()
	{
		//Must load all stuff
		//yield return new WaitForSecondsRealtime(5);
		while(mainscr == null)
			yield return null;
			
		while(!mainscr.finishedloading)
			yield return null;
		
		if(load == null)
			Debug.Log("ERROR: CANNOT LOAD TRANSITION SCRIPT, GAME WILL CRASH");
		yield return new WaitForSeconds(3);
		
		
		playerscript.fadein = false;
		clockscript.fadein = false;
		StopCoroutine(clockscript.countdown());
		bossframe.fadein = false;
		
		//InputAction.CallbackContext context;
		controls = new MasterControls();
		controls.Menu.Enable();
		
		/*
		StartCoroutine(textbox.FadeInWithTxt("<b><color=#BA55D3><sp=0.01>Sumireko</color></b><size=30><sp=0.02>\nDid it work?"));

		while (!textbox.isReady)
			yield return null;
		textbox.transpprompt.alpha = 0.7f;
		while (!(controls.Menu.Confirm.triggered))
			yield return null;
		textbox.transpprompt.alpha = 0.0f;

		StartCoroutine(textbox.FadeOut());
		*/
		
		//playerframe.startfadecontrol = true;
		//playerframe.fadein = true;
		playerscript.startfadecontrol = true;
		playerscript.fadein = true;
		
		/*
		clockscript.startfadecontrol = true;
		clockscript.fadein = true;
		clockscript.time = 20;
		StartCoroutine(clockscript.countdown());
		bossframe.startfadecontrol = true;
		bossframe.fadein = true;
		*/
		
		while (clockscript.time > 0)
		{
			yield return null;
		}
		
		//playerframe.startfadecontrol = true;
		//playerframe.fadein = false;
		playerscript.startfadecontrol = true;
		playerscript.fadein = false;
		clockscript.startfadecontrol = true;
		clockscript.fadein = false;
		StartCoroutine(clockscript.countdown());
		bossframe.startfadecontrol = true;
		bossframe.fadein = false;
		
		//USE THIS TO DISABLE STUFF
		//mCanvas.GetComponent<uiplayerframe>().enabled = false;
		
		yield return new WaitForSeconds(3);
		StartCoroutine(textbox.FadeInWithTxt("<b><color=#BA55D3><sp=0.01>Sumireko</color></b><size=30><sp=0.05>\nTime's up."));

		while (!textbox.isReady)
			yield return null;
		textbox.transpprompt.alpha = 0.7f;
		while (!(controls.Menu.Confirm.triggered))
			yield return null;
		textbox.transpprompt.alpha = 0.0f;

		StartCoroutine(textbox.FadeOut());
		
		StartCoroutine(load.LoadSceneAsync("Test01"));
		//StartCoroutine(load.LoadSceneAsync("MainScene"));
		
		yield return null;
	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
