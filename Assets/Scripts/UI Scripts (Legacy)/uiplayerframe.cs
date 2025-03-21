using System.Collections;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class uiplayerframe : MonoBehaviour
{
	//public uiplayerframe originalframe;
	public bool finished_loading = false;
	public MainScript mainscr;
	
	public GameObject fpscounter;
	public GameObject difficultycounter;
	public bool fadein;
	public bool startfadecontrol = true;
	//state 0 = static
	//state 1 = hp bar
	//state 2 = mp bar
	public int state = 0;
	public int maxwidth = 370;
	public int minwidth = 0;
	public int actualwidth = 370;

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
	
	private GameObject mCanvas;
	float screenfactor = (Screen.width * 5) / 1920;

	private GameObject lCanvas;
	public gamelogic game;
	
	void Start()
	{		
		lCanvas = GameObject.Find("Canvas_Loading");;
		game = lCanvas.GetComponent<gamelogic>();
		
		double timer = Time.realtimeSinceStartup;
        mCanvas = GameObject.Find("Canvas");;
		boxobj = new GameObject();
		if(state == 0)
		boxobj.name = "Frame Obj";
		if(state == 1)
		boxobj.name = "HP frame Obj";
		if(state == 2)
		boxobj.name = "MP frame Obj";

		mask1obj = new GameObject();
		mask1obj.name = "Mask 1";
		mask2obj = new GameObject();
		mask2obj.name = "Mask 2";
		//mask3obj = new GameObject();
		int rand = UnityEngine.Random.Range(1, 255);

		//-------------------------------------------------------------------//

		boxobj.AddComponent<CanvasRenderer>();
		boxobj.AddComponent<RectTransform>();
		boxobj.AddComponent<Mask>();
		boxobj.AddComponent<Image>();
		boxobj.AddComponent<CanvasGroup>();

		//converter Texture2D para Sprite
		//boxSp = Sprite.Create(box, new Rect(0.0f, 0.0f, box.width, box.height), new Vector2(0.5f, 0.5f), 100.0f);

		boxSp = Sprite.Create(box, new Rect(0.0f, 0.0f, box.width, box.height), new Vector2(0.5f, 0.5f));

		//Pos + size
		//float minX = mCanvas.GetComponent<RectTransform>().position.x + mCanvas.GetComponent<RectTransform>().rect.xMin;
		//float minY = mCanvas.GetComponent<RectTransform>().position.y + mCanvas.GetComponent<RectTransform>().rect.yMin;
		float z = mCanvas.GetComponent<RectTransform>().position.z;
																							   //float cameraHeight = Camera.main.orthographicSize * 2;
																							   //float cameraWidth = cameraHeight * Screen.width / Screen.height; // cameraHeight * aspect ratio
																							   //txtbox.transform.localScale = Vector3.one * cameraHeight / 5.0f;
		boxobj.transform.position = new Vector3(Screen.width * 0.5f, Screen.height * -0.35f, z);//pos
		
		boxobj.transform.SetParent(mCanvas.transform);
		


		//Rect do componente txtbox
		RectTransform rt = boxobj.GetComponent<RectTransform>();
		rt.sizeDelta = new Vector2(box.width, box.height);

		//Render + Listener
		boxobj.GetComponent<Image>().sprite = boxSp; //Override
		CanvasGroup transp1 = boxobj.GetComponent<CanvasGroup>();
		transp1.alpha = 0.8f;
		//txtbox.GetComponent<Button>().onClick.AddListener(Testmethod);
		
		//if(state == 1)
		//	boxobj.transform.position = new Vector3(Screen.width * 0.1265f, boxobj.transform.position.y, boxobj.transform.position.z);//pos
		
		//if(state == 2)
		//	boxobj.transform.position = new Vector3(Screen.width * 0.127f, boxobj.transform.position.y, boxobj.transform.position.z);//pos
		//boxobj.transform.position = new Vector3(boxobj.transform.position.x - 587f, boxobj.transform.position.y, boxobj.transform.position.z);//pos

		//-------------------------------------------------------------------//


		//converter Texture2D para Sprite
		mask1Sp = Sprite.Create(mask1, new Rect(0.0f, 0.0f, mask1.width, mask1.height), new Vector2(0.66f, 0.5f), 100.0f);
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
		transp.alpha = 0.0f; //0.2
		//custom.

		mask1obj.transform.Rotate(0.0f, 0.0f, (float)rand, Space.World);

		//-------------------------------------------------------------------//


		//converter Texture2D para Sprite
		mask2Sp = Sprite.Create(mask2, new Rect(0.0f, 0.0f, mask2.width, mask2.height), new Vector2(0.66f, 0.5f), 100.0f);
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
		transp3.alpha = 0.0f;

		mask2obj.transform.Rotate(0.0f, 0.0f, 128.0f + (float)rand, Space.World);

		//-------------------------------------------------------------------//

		//Messing with scale, this might have some problems
		boxobj.transform.localScale = new Vector3(1.0f, 1.0f, 1);
		mask1obj.transform.localScale = new Vector3(1.6f, 1.6f, 1);
		mask2obj.transform.localScale = new Vector3(1.6f, 1.6f, 1);


		boxobj.transform.SetAsLastSibling();
		updateposition();
		
		difficultycounter = new GameObject();
		difficultycounter.name = "difficulty";
		
		difficultycounter.transform.SetParent(mCanvas.transform);
		difficultycounter.transform.position = new Vector3(Screen.width* 0.9f, Screen.height * 0.945f, 0.0f);//pos
		
		difficultycounter.AddComponent<TextMeshProUGUI>();
		difficultycounter.GetComponent<TextMeshProUGUI>().fontSize = 25; //Default
		difficultycounter.GetComponent<TextMeshProUGUI>().lineSpacing = 60; //Default,
		
		//difficultycounter.GetComponent<TextMeshProUGUI>().color = new Color32(0, 0, 0, 0); //Default
		difficultycounter.GetComponent<TextMeshProUGUI>().alpha = 0;
		difficultycounter.GetComponent<TextMeshProUGUI>().outlineWidth = 0.5f;
		//difficultycounter.GetComponent<TextMeshProUGUI>().outlineColor = new Color32(0, 0, 0, 0);
		difficultycounter.GetComponent<TextMeshProUGUI>().richText = true; //for HTML markup
		difficultycounter.GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Center;

		difficultycounter.transform.localScale = new Vector3(1.3f, 1.3f, 1);
		
		finished_loading = true;
		Debug.Log("uiplayerframe: "+ (Time.realtimeSinceStartup - timer));
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

    // Update is called once per frame
    void Update()
    {
		if(state == 1)
		{
			fpscounter.GetComponent<TextMeshProUGUI>().SetText(game.player_life+"/"+game.maxlife); //Default
		}
		else if(state == 2)
		{
			fpscounter.GetComponent<TextMeshProUGUI>().SetText(game.player_mp+"/"+game.maxmp); //Default
		}
		
		if (state == 0)
		{
			game.playerframefadein = fadein;
			if(fadein)
				startfadecontrol = true;
		}
		
		/*
        if (state == 1)
		{
			fadein = game.playerframefadein;
			startfadecontrol = true;
			int barsize = game.player_life * 370 / game.maxlife;
			RectTransform rt = boxobj.GetComponent<RectTransform>();
			if(actualwidth < barsize)
			{
				actualwidth+= Mathf.CeilToInt(500*Time.deltaTime);
				rt.sizeDelta = new Vector2(actualwidth, 256);
			}
			else if(actualwidth > barsize)
			{
				actualwidth-= Mathf.FloorToInt(500*Time.deltaTime);
				rt.sizeDelta = new Vector2(actualwidth, 256);
			}
		}

		if (state == 2)
		{
			fadein = game.playerframefadein;
			startfadecontrol = true;
			int barsize = game.player_mp * 370 / game.maxmp;
			RectTransform rt = boxobj.GetComponent<RectTransform>();
			if(actualwidth < barsize)
			{
				actualwidth+= Mathf.CeilToInt(500*Time.deltaTime);
				rt.sizeDelta = new Vector2(actualwidth, 256);
			}
			else if(actualwidth > barsize)
			{
				actualwidth-= Mathf.FloorToInt(500*Time.deltaTime);
				rt.sizeDelta = new Vector2(actualwidth, 256);
			}
		}
		*/
    }

	IEnumerator backgroundanimation()
	{
		while(mask1obj.activeSelf && mask2obj.activeSelf)
		{
			mask1obj.transform.Rotate(0.0f, 0.0f, 2f * Time.deltaTime, Space.World);
			mask2obj.transform.Rotate(0.0f, 0.0f, -2f * Time.deltaTime, Space.World);
			//boxobj.transform.Rotate(0.0f, 0.0f, -10f * Time.deltaTime, Space.World);
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
					
					StartCoroutine(FadeTxt(1));
					
				}
				else
				{
					startfadecontrol = false;
					if(fadeoutdone)
						StartCoroutine(FadeOut());
					while(!fadeoutdone)
						yield return null;
					
					StartCoroutine(FadeTxt(0));
				}
			
			yield return null;
		}
	}
	
	IEnumerator FadeTxt(int intstate)
	{
		if(intstate == 0)
		{
			yield return new WaitForSeconds(1.5f);
			int alpha = 0;
			while (alpha < 255)
			{
				alpha += 1;
				
				if(state == 1)
					fpscounter.GetComponent<TextMeshProUGUI>().color = new Color32(207, 52, 235, (byte)alpha); //Default
				else if(state == 2)
					fpscounter.GetComponent<TextMeshProUGUI>().color = new Color32(76, 235, 52, (byte)alpha); //Default
				else if(state == 0)
				{
					/*
					if(game.overdrive)
					{
						difficultycounter.GetComponent<TextMeshProUGUI>().color = new Color32(230, 230, 230, (byte)alpha); //Default
						difficultycounter.GetComponent<TextMeshProUGUI>().outlineColor = new Color32(166, 61, 61, (byte)alpha);
						difficultycounter.GetComponent<TextMeshProUGUI>().SetText("OVERDRIVE"); //Default
					}
					else
					{
						if(game.difficulty == 0)
						{
							difficultycounter.GetComponent<TextMeshProUGUI>().color = new Color32(230, 230, 230, (byte)alpha); //Default
							difficultycounter.GetComponent<TextMeshProUGUI>().outlineColor = new Color32(61, 166, 79, (byte)alpha);
							difficultycounter.GetComponent<TextMeshProUGUI>().SetText("EASY"); //Default
						}
						else if(game.difficulty == 1)
						{
							difficultycounter.GetComponent<TextMeshProUGUI>().color = new Color32(230, 230, 230, (byte)alpha); //Default
							difficultycounter.GetComponent<TextMeshProUGUI>().outlineColor = new Color32(61, 152, 166, (byte)alpha);
							difficultycounter.GetComponent<TextMeshProUGUI>().SetText("NORMAL"); //Default
						}
						else if(game.difficulty == 2)
						{
							difficultycounter.GetComponent<TextMeshProUGUI>().color = new Color32(230, 230, 230, (byte)alpha); //Default
							difficultycounter.GetComponent<TextMeshProUGUI>().outlineColor = new Color32(61, 80, 166, (byte)alpha);
							difficultycounter.GetComponent<TextMeshProUGUI>().SetText("HARD"); //Default
						}
						else if(game.difficulty == 3)
						{
							difficultycounter.GetComponent<TextMeshProUGUI>().color = new Color32(230, 230, 230, (byte)alpha); //Default
							difficultycounter.GetComponent<TextMeshProUGUI>().outlineColor = new Color32(156, 61, 166, (byte)alpha);
							difficultycounter.GetComponent<TextMeshProUGUI>().SetText("LUNATIC"); //Default
						}
						else if(game.difficulty == 4)
						{
							difficultycounter.GetComponent<TextMeshProUGUI>().color = new Color32(230, 230, 230, (byte)alpha); //Default
							difficultycounter.GetComponent<TextMeshProUGUI>().outlineColor = new Color32(166, 61, 89, (byte)alpha);
							difficultycounter.GetComponent<TextMeshProUGUI>().SetText("ULTRA"); //Default
						}
					}
					*/
				}
				yield return null;
			}
		}
		else if(intstate == 1)		
		{
			int alpha = 255;
			while (alpha > 0)
			{
				alpha -= 1;
				
				if(state == 1)
					fpscounter.GetComponent<TextMeshProUGUI>().color = new Color32(207, 52, 235, (byte)alpha); //Default
				else if(state == 2)
					fpscounter.GetComponent<TextMeshProUGUI>().color = new Color32(76, 235, 52, (byte)alpha); //Default
				else if(state == 0)
				{
					/*
					if(game.overdrive)
					{
						difficultycounter.GetComponent<TextMeshProUGUI>().color = new Color32(230, 230, 230, (byte)alpha); //Default
						difficultycounter.GetComponent<TextMeshProUGUI>().outlineColor = new Color32(166, 61, 61, (byte)alpha);
						difficultycounter.GetComponent<TextMeshProUGUI>().SetText("OVERDRIVE"); //Default
					}
					else
					{
						if(game.difficulty == 0)
						{
							difficultycounter.GetComponent<TextMeshProUGUI>().color = new Color32(230, 230, 230, (byte)alpha); //Default
							difficultycounter.GetComponent<TextMeshProUGUI>().outlineColor = new Color32(61, 166, 79, (byte)alpha);
							difficultycounter.GetComponent<TextMeshProUGUI>().SetText("EASY"); //Default
						}
						else if(game.difficulty == 1)
						{
							difficultycounter.GetComponent<TextMeshProUGUI>().color = new Color32(230, 230, 230, (byte)alpha); //Default
							difficultycounter.GetComponent<TextMeshProUGUI>().outlineColor = new Color32(61, 152, 166, (byte)alpha);
							difficultycounter.GetComponent<TextMeshProUGUI>().SetText("NORMAL"); //Default
						}
						else if(game.difficulty == 2)
						{
							difficultycounter.GetComponent<TextMeshProUGUI>().color = new Color32(230, 230, 230, (byte)alpha); //Default
							difficultycounter.GetComponent<TextMeshProUGUI>().outlineColor = new Color32(61, 80, 166, (byte)alpha);
							difficultycounter.GetComponent<TextMeshProUGUI>().SetText("HARD"); //Default
						}
						else if(game.difficulty == 3)
						{
							difficultycounter.GetComponent<TextMeshProUGUI>().color = new Color32(230, 230, 230, (byte)alpha); //Default
							difficultycounter.GetComponent<TextMeshProUGUI>().outlineColor = new Color32(156, 61, 166, (byte)alpha);
							difficultycounter.GetComponent<TextMeshProUGUI>().SetText("LUNATIC"); //Default
						}
						else if(game.difficulty == 4)
						{
							difficultycounter.GetComponent<TextMeshProUGUI>().color = new Color32(230, 230, 230, (byte)alpha); //Default
							difficultycounter.GetComponent<TextMeshProUGUI>().outlineColor = new Color32(166, 61, 89, (byte)alpha);
							difficultycounter.GetComponent<TextMeshProUGUI>().SetText("ULTRA"); //Default
						}
					}
					*/
				}
				yield return null;
			}
		}
	}

	IEnumerator FadeIn()
	{
		fadeindone = false;
		while (boxobj.transform.position.y < boxobjnewposy - 10)
		{
			boxobj.transform.position = Vector3.Lerp(boxobj.transform.position, new Vector3(boxobj.transform.position.x, boxobjnewposy, boxobj.transform.position.z), 2f * Time.deltaTime);
			mask1obj.transform.position = Vector3.Lerp(mask1obj.transform.position, new Vector3(mask1obj.transform.position.x, mask1objnewposy, mask1obj.transform.position.z), 2f * Time.deltaTime);
			mask2obj.transform.position = Vector3.Lerp(mask2obj.transform.position, new Vector3(mask2obj.transform.position.x, mask2objnewposy, mask2obj.transform.position.z), 2f * Time.deltaTime);

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
			boxobj.transform.position = Vector3.Lerp(boxobj.transform.position, new Vector3(boxobj.transform.position.x, boxobjoriginalposy, boxobj.transform.position.z), 2f * Time.deltaTime);
			mask1obj.transform.position = Vector3.Lerp(mask1obj.transform.position, new Vector3(mask1obj.transform.position.x, mask1objoriginalposy, mask1obj.transform.position.z), 2f * Time.deltaTime);
			mask2obj.transform.position = Vector3.Lerp(mask2obj.transform.position, new Vector3(mask2obj.transform.position.x, mask2objoriginalposy, mask2obj.transform.position.z), 2f * Time.deltaTime);

			yield return null;
		}
		fadeoutdone = true;
	}

	void updateposition()
	{
		boxobjoriginalposy = boxobj.transform.position.y - 200 * screenfactor;
		boxobj.transform.position = new Vector3(35f, boxobjoriginalposy, boxobj.transform.position.z);
		mask1objoriginalposy = mask1obj.transform.position.y - 200 * screenfactor;
		mask1obj.transform.position = new Vector3(mask1obj.transform.position.x, mask1objoriginalposy, mask1obj.transform.position.z);
		mask2objoriginalposy = mask2obj.transform.position.y - 200 * screenfactor;
		mask2obj.transform.position = new Vector3(mask2obj.transform.position.x, mask2objoriginalposy, mask2obj.transform.position.z);

		//Update Y
		if(state == 0)
			boxobjnewposy = Screen.height * 0.12f;
		else if(state == 1)
			boxobjnewposy = Screen.height * -0.005f;
		else if (state == 2)
			boxobjnewposy = Screen.height * -0.005f;
		
		mask1objnewposy = Screen.height * 0.12f;
		mask2objnewposy = Screen.height * 0.12f;

	}
}
