﻿using System.Collections;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using static Player_Common_Data;

public class uiplayerscript : MonoBehaviour
{
	
	public bool finished_loading = false;
	public MainScript mainscr;

	public GameObject character_text;
	public TMP_FontAsset MenuFont;

	public bool fadein;
	public bool startfadecontrol = true;
	public Texture2D char1;
	private GameObject char1obj;
	private Sprite char1Sp;
	private float char1objoriginalposx = 0.0f;
	private float char1objnewposx = 0.0f;

	public Texture2D box;
	private GameObject boxobj;
	private Sprite boxSp;
	private float boxobjoriginalposy = 0.0f;
	private float boxobjnewposy = 0.0f;

	public Texture2D mask1;
	private GameObject mask1obj;
	private Sprite mask1Sp;
	private float mask1objoriginalposy = 0.0f;
	private float mask1objnewposy = 0.0f;

	public Texture2D mask2;
	private GameObject mask2obj;
	private Sprite mask2Sp;
	private float mask2objoriginalposy = 0.0f;
	private float mask2objnewposy = 0.0f;

	public Texture2D hpbar;
	private GameObject hpbarobj;
	private Sprite hpbarSp;
	private float hpbarobjoriginalposy = 0.0f;
	
	private GameObject mCanvas;
	float screenfactor = (Screen.width * 5) / 1920;

	private GameObject lCanvas;
	private gamelogic game;
	
	void Start()
	{
		lCanvas = GameObject.Find("Canvas_Loading");;
		game = lCanvas.GetComponent<gamelogic>();
		double timer = Time.realtimeSinceStartup;
        mCanvas = GameObject.Find("Canvas");;
		boxobj = new GameObject();
		boxobj.name = "Box Obj";
		mask1obj = new GameObject();
		mask1obj.name = "Mask 1";
		mask2obj = new GameObject();
		mask2obj.name = "Mask 2";
		char1obj = new GameObject();
		char1obj.name = "Char Sprite 1";
		//mask3obj = new GameObject();
		int rand = UnityEngine.Random.Range(1, 255);

		//-------------------------------------------------------------------//

		//CHARACTER SPRITES

		//converter Texture2D para Sprite
		char1Sp = Sprite.Create(char1, new Rect(0.0f, 0.0f, char1.width, char1.height), new Vector2(0.5f, 0.5f), 100.0f);
		char1obj.AddComponent<CanvasRenderer>();
		char1obj.AddComponent<RectTransform>();
		char1obj.AddComponent<Image>();
		char1obj.AddComponent<CanvasGroup>();

		//Pos + size
		char1obj.transform.position = new Vector3(Screen.width * - 0.2f, Screen.height*0.4f, mCanvas.GetComponent<RectTransform>().position.z);//pos
		char1obj.transform.SetParent(mCanvas.transform);

		//Rect do componente char1obj
		RectTransform rtc1 = char1obj.GetComponent<RectTransform>();
		rtc1.sizeDelta = new Vector2(char1.width, char1.height);

		//Render 
		char1obj.GetComponent<Image>().sprite = char1Sp; //Override

		CanvasGroup char1transp = char1obj.GetComponent<CanvasGroup>();
		//transpprompt.alpha = 0.7f;
		
		
		//MAKE SUMIREKO DISAPPEAR FROM THE VIEW
		char1transp.alpha = 0.0f;

		//-------------------------------------------------------------------//

		boxobj.AddComponent<CanvasRenderer>();
		boxobj.AddComponent<RectTransform>();
		boxobj.AddComponent<Mask>();
		boxobj.AddComponent<Image>();
		boxobj.AddComponent<CanvasGroup>();

		//converter Texture2D para Sprite
		boxSp = Sprite.Create(box, new Rect(0.0f, 0.0f, box.width, box.height), new Vector2(0.5f, 0.5f), 100.0f);

		//Pos + size
		//float minX = mCanvas.GetComponent<RectTransform>().position.x + mCanvas.GetComponent<RectTransform>().rect.xMin;
		//float minY = mCanvas.GetComponent<RectTransform>().position.y + mCanvas.GetComponent<RectTransform>().rect.yMin;
		float z = mCanvas.GetComponent<RectTransform>().position.z;
		boxobj.transform.position = new Vector3(Screen.width * 0.08f, Screen.height * -0.35f, z);//pos
																							   //float cameraHeight = Camera.main.orthographicSize * 2;
																							   //float cameraWidth = cameraHeight * Screen.width / Screen.height; // cameraHeight * aspect ratio
																							   //txtbox.transform.localScale = Vector3.one * cameraHeight / 5.0f;
		boxobj.transform.SetParent(mCanvas.transform);
		boxobj.transform.SetAsFirstSibling();

		//Rect do componente txtbox
		RectTransform rt = boxobj.GetComponent<RectTransform>();
		rt.sizeDelta = new Vector2(box.width, box.height);

		//Render + Listener
		boxobj.GetComponent<Image>().sprite = boxSp; //Override
		CanvasGroup transp1 = boxobj.GetComponent<CanvasGroup>();
		transp1.alpha = 0.8f;
		//txtbox.GetComponent<Button>().onClick.AddListener(Testmethod);

		//-------------------------------------------------------------------//


		//converter Texture2D para Sprite
		mask1Sp = Sprite.Create(mask1, new Rect(0.0f, 0.0f, mask1.width, mask1.height), new Vector2(0.5f, 0.5f), 100.0f);
		mask1obj.AddComponent<CanvasRenderer>();
		mask1obj.AddComponent<RectTransform>();
		mask1obj.AddComponent<Image>();
		mask1obj.AddComponent<CanvasGroup>();

		//Pos + size
		float z2 = mCanvas.GetComponent<RectTransform>().position.z;
		mask1obj.transform.position = new Vector3(Screen.width * 0.5f, Screen.height * 0.2f, z2);//pos
		mask1obj.transform.SetParent(boxobj.transform);

		//Rect do componente mask1obj
		RectTransform rt2 = mask1obj.GetComponent<RectTransform>();
		rt2.sizeDelta = new Vector2(mask1.width, mask1.height);

		//Render
		mask1obj.GetComponent<Image>().sprite = mask1Sp; //Override
		CanvasGroup transp = mask1obj.GetComponent<CanvasGroup>();
		transp.alpha = 0.5f;
		//custom.

		mask1obj.transform.Rotate(0.0f, 0.0f, (float)rand, Space.World);

		//-------------------------------------------------------------------//


		//converter Texture2D para Sprite
		mask2Sp = Sprite.Create(mask2, new Rect(0.0f, 0.0f, mask2.width, mask2.height), new Vector2(0.5f, 0.5f), 100.0f);
		mask2obj.AddComponent<CanvasRenderer>();
		mask2obj.AddComponent<RectTransform>();
		mask2obj.AddComponent<Image>();
		mask2obj.AddComponent<CanvasGroup>();

		//Pos + size
		float z3 = mCanvas.GetComponent<RectTransform>().position.z;
		mask2obj.transform.position = new Vector3(Screen.width * 0.5f, Screen.height * 0.2f, z3);//pos
		mask2obj.transform.SetParent(boxobj.transform);

		//Rect do componente mask2obj
		RectTransform rt3 = mask2obj.GetComponent<RectTransform>();
		rt3.sizeDelta = new Vector2(mask2.width, mask2.height);

		//Render
		mask2obj.GetComponent<Image>().sprite = mask2Sp; //Override
		CanvasGroup transp3 = mask2obj.GetComponent<CanvasGroup>();
		transp3.alpha = 0.5f;

		mask2obj.transform.Rotate(0.0f, 0.0f, 128.0f + (float)rand, Space.World);

		//-------------------------------------------------------------------//

		//Messing with scale, this might have some problems
		boxobj.transform.localScale = new Vector3(0.3f, 0.3f, 1);
		mask1obj.transform.localScale = new Vector3(1.6f, 1.6f, 1);
		mask2obj.transform.localScale = new Vector3(1.6f, 1.6f, 1);
		char1obj.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
		
		char1obj.transform.SetAsFirstSibling(); //para mostrar char atrás da UI
		updateposition();

		//if player slot 2, 3 and 4 are empty, define text inside of circle as 'empty'
		if(game.player_slot_2 == Player_Common_Data.PlayerState.EMPTY && game.player_slot_3 == Player_Common_Data.PlayerState.EMPTY && game.player_slot_4 == Player_Common_Data.PlayerState.EMPTY)
			Start_character_text("EMPTY");
		else
			Start_character_text("");

		finished_loading = true;
		Debug.Log("uiplayerscript: "+(Time.realtimeSinceStartup - timer));
		StartCoroutine(coroutinecontrol());
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

	void Start_character_text(String str)
	{
		character_text = new GameObject();
		character_text.name = str;
		
		character_text.transform.SetParent(mCanvas.transform);
		character_text.transform.position = new Vector3(Screen.width* 0.99f, Screen.height * 2f, 0.0f);//pos
																								 //textobj.transform.position = new Vector3(-300.0f, 50.0f, z2);//pos
		character_text.AddComponent<TextMeshProUGUI>();
		character_text.GetComponent<TextMeshProUGUI>().fontSize = 30; //Default
		character_text.GetComponent<TextMeshProUGUI>().lineSpacing = 60; //Default
		character_text.GetComponent<TextMeshProUGUI>().color = new Color32(200, 200, 200, 255); //Default
		//character_text.GetComponent<TextMeshProUGUI>().outlineWidth = 0.3f;
		//character_text.GetComponent<TextMeshProUGUI>().outlineColor = new Color32(114, 47, 55, 255);
		character_text.GetComponent<TextMeshProUGUI>().richText = true; //for HTML markup
		character_text.GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Center;
		character_text.GetComponent<TextMeshProUGUI>().SetText(str);
		character_text.GetComponent<TextMeshProUGUI>().font = MenuFont;
		character_text.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
	}

    // Update is called once per frame
    void Update()
    {
        
    }

	IEnumerator backgroundanimation()
	{
		while(mask1obj.activeSelf && mask2obj.activeSelf)
		{
			mask1obj.transform.Rotate(0.0f, 0.0f, 2f * Time.deltaTime, Space.World);
			mask2obj.transform.Rotate(0.0f, 0.0f, -2f * Time.deltaTime, Space.World);
			boxobj.transform.Rotate(0.0f, 0.0f, -10f * Time.deltaTime, Space.World);
			yield return null;
		}
		yield return null;
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
					startfadecontrol = false; //queues up in case it becomes true during execution
					if(fadeindone)
						StartCoroutine(FadeIn());
					while(!fadeindone)
						yield return null;
					
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
	
	IEnumerator FadeIn()
	{
		fadeindone = false;
		while (boxobj.transform.position.y < boxobjnewposy - 10)
		{
			character_text.transform.position = Vector3.Lerp(boxobj.transform.position, new Vector3(boxobj.transform.position.x, boxobjnewposy, boxobj.transform.position.z), 2f * Time.deltaTime);
			boxobj.transform.position = Vector3.Lerp(boxobj.transform.position, new Vector3(boxobj.transform.position.x, boxobjnewposy, boxobj.transform.position.z), 2f * Time.deltaTime);
			mask1obj.transform.position = Vector3.Lerp(mask1obj.transform.position, new Vector3(mask1obj.transform.position.x, mask1objnewposy, mask1obj.transform.position.z), 2f * Time.deltaTime);
			mask2obj.transform.position = Vector3.Lerp(mask2obj.transform.position, new Vector3(mask2obj.transform.position.x, mask2objnewposy, mask2obj.transform.position.z), 2f * Time.deltaTime);
			char1obj.transform.position = Vector3.Lerp(char1obj.transform.position, new Vector3(char1objnewposx, char1obj.transform.position.y, char1obj.transform.position.z), 2f * Time.deltaTime);
			//txtbox.transform.Translate(Vector3.down* 150 * Time.deltaTime);
			yield return null;
		}
		fadeindone = true;

		yield return null;
	}

	IEnumerator FadeOut()
	{
		//Only call this if FadeIn() has been called before and it is finished, or else good luck.
		fadeoutdone = false;
		while (boxobj.transform.position.y > boxobjoriginalposy+10)
		{
			character_text.transform.position = Vector3.Lerp(boxobj.transform.position, new Vector3(boxobj.transform.position.x, boxobjoriginalposy, boxobj.transform.position.z), 2f * Time.deltaTime);
			boxobj.transform.position = Vector3.Lerp(boxobj.transform.position, new Vector3(boxobj.transform.position.x, boxobjoriginalposy, boxobj.transform.position.z), 2f * Time.deltaTime);
			mask1obj.transform.position = Vector3.Lerp(mask1obj.transform.position, new Vector3(mask1obj.transform.position.x, mask1objoriginalposy, mask1obj.transform.position.z), 2f * Time.deltaTime);
			mask2obj.transform.position = Vector3.Lerp(mask2obj.transform.position, new Vector3(mask2obj.transform.position.x, mask2objoriginalposy, mask2obj.transform.position.z), 2f * Time.deltaTime);
			char1obj.transform.position = Vector3.Lerp(char1obj.transform.position, new Vector3(char1objoriginalposx, char1obj.transform.position.y, char1obj.transform.position.z), 2f * Time.deltaTime);
			yield return null;
		}
		fadeoutdone = true;
	}

	void updateposition()
	{
		boxobjoriginalposy = boxobj.transform.position.y - 200 * screenfactor;
		boxobj.transform.position = new Vector3(boxobj.transform.position.x, boxobjoriginalposy, boxobj.transform.position.z);
		mask1objoriginalposy = mask1obj.transform.position.y - 200 * screenfactor;
		mask1obj.transform.position = new Vector3(mask1obj.transform.position.x, mask1objoriginalposy, mask1obj.transform.position.z);
		mask2objoriginalposy = mask2obj.transform.position.y - 200 * screenfactor;
		mask2obj.transform.position = new Vector3(mask2obj.transform.position.x, mask2objoriginalposy, mask2obj.transform.position.z);

		char1objoriginalposx = char1obj.transform.position.x;
		//char2objoriginalposx = char2obj.transform.position.x;

		//Update Y
		boxobjnewposy = Screen.height * 0.12f;
		mask1objnewposy = Screen.height * 0.12f;
		mask2objnewposy = Screen.height * 0.12f;

		//Update X
		char1objnewposx = Screen.width * 0.1f;
	}
}
