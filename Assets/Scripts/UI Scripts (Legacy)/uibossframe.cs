using System.Collections;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class uibossframe : MonoBehaviour
{
	
	public MainScript mainscr;
	public bool finished_loading = false;
	
	public bool fadein;
	public bool startfadecontrol = true;
	//state 0 = static
	//state 1 = hp bar
	//state 2 = mp bar
	public int state = 0;
	public int maxwidth = 2048;
	public int minwidth = 0;
	public int actualwidth = 0;

	public Texture2D box;
	private GameObject boxobj;
	private Sprite boxSp;
	private float boxobjoriginalposy = 0.0f;
	private float boxobjnewposy = 0.0f;

	public Texture2D bar;
	private GameObject barobj;
	private Sprite barSp;
	private float barobjoriginalposy = 0.0f;
	private float barobjnewposy = 0.0f;
	
	public Texture2D star;
    [SerializeField] public GameObject[] starobj;
	private Sprite starSp;
	private float starobjoriginalposy = 0.0f;
	private float starobjnewposy = 0.0f;
	
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
		if(state == 0)
		boxobj.name = "HP Boss Frame Obj";

		barobj = new GameObject();
		barobj.name = "Boss bar";
		//mask3obj = new GameObject();
		int rand = UnityEngine.Random.Range(1, 255);

		//-------------------------------------------------------------------//
		
		
		boxSp = Sprite.Create(box, new Rect(0.0f, 0.0f, box.width, box.height), new Vector2(0.5f, 0.5f), 100.0f);
		boxobj.AddComponent<CanvasRenderer>();
		boxobj.AddComponent<RectTransform>();
		boxobj.AddComponent<Image>();
		boxobj.AddComponent<CanvasGroup>();

		//converter Texture2D para Sprite

		/*
		if(state == 0)
			boxSp = Sprite.Create(box, new Rect(0.0f, 0.0f, box.width, box.height), new Vector2(0.5f, 0.5f), 100.0f);
		else
		{
			boxSp = Sprite.Create(box, new Rect(250.0f, 0.0f, box.width-250.0f-1425.0f, box.height), new Vector2(0.5f, 0.5f));
		}
		*/
		//Pos + size
		//float minX = mCanvas.GetComponent<RectTransform>().position.x + mCanvas.GetComponent<RectTransform>().rect.xMin;
		//float minY = mCanvas.GetComponent<RectTransform>().position.y + mCanvas.GetComponent<RectTransform>().rect.yMin;
		float z = mCanvas.GetComponent<RectTransform>().position.z;
		boxobj.transform.position = new Vector3(Screen.width * 0.5f, Screen.height * -0.35f, z);//pos
																							   //float cameraHeight = Camera.main.orthographicSize * 2;
																							   //float cameraWidth = cameraHeight * Screen.width / Screen.height; // cameraHeight * aspect ratio
																							   //txtbox.transform.localScale = Vector3.one * cameraHeight / 5.0f;
		boxobj.transform.SetParent(mCanvas.transform);

		//Rect do componente txtbox
		RectTransform rt = boxobj.GetComponent<RectTransform>();
		rt.sizeDelta = new Vector2(box.width, box.height);

		//Render + Listener
		boxobj.GetComponent<Image>().sprite = boxSp; //Override
		CanvasGroup transp1 = boxobj.GetComponent<CanvasGroup>();
		transp1.alpha = 1f;
		//txtbox.GetComponent<Button>().onClick.AddListener(Testmethod);
		
		//boxobj.transform.position = new Vector3(Screen.width * 0.195f, boxobj.transform.position.y, boxobj.transform.position.z);//pos
		//boxobj.transform.position = new Vector3(boxobj.transform.position.x - 587f, boxobj.transform.position.y, boxobj.transform.position.z);//pos

		//-------------------------------------------------------------------//


		//converter Texture2D para Sprite
		barSp = Sprite.Create(bar, new Rect(0.0f, 0.0f, bar.width, bar.height), new Vector2(0.5f, 0.5f), 100.0f);
		barobj.AddComponent<CanvasRenderer>();
		barobj.AddComponent<RectTransform>();
		barobj.AddComponent<Image>();
		barobj.AddComponent<CanvasGroup>();

		//Pos + size
		float z2 = mCanvas.GetComponent<RectTransform>().position.z;
		barobj.transform.position = new Vector3((Screen.width * 0.5f), Screen.height * -0.35f, z2);//
		//barobj.transform.position = new Vector3(barobj.transform.position.x - 437f, barobj.transform.position.y, barobj.transform.position.z);//pos
		//barobj.transform.position = new Vector3(Screen.width * 0.197f, Screen.height * -0.35f, z2);//pos
		barobj.transform.SetParent(mCanvas.transform);

		//Rect do componente barobj
		RectTransform rt2 = barobj.GetComponent<RectTransform>();
		rt2.sizeDelta = new Vector2(bar.width, bar.height);
		rt2.pivot = new Vector2(0.0f, 0.5f); 

		//Render
		barobj.GetComponent<Image>().sprite = barSp; //Override
		CanvasGroup transp = barobj.GetComponent<CanvasGroup>();
		transp.alpha = 1.0f;
		//custom.

		//-------------------------------------------------------------------//

		//Messing with scale, this might have some problems
		boxobj.transform.localScale = new Vector3(0.7f, 0.7f, 1);
		barobj.transform.localScale = new Vector3(0.7f, 0.7f, 1);

		//boxobj.transform.SetAsLastSibling();
		updateposition();
		
		finished_loading = true;
		Debug.Log("uibossframe: "+(Time.realtimeSinceStartup - timer));
		StartCoroutine(coroutinecontrol());
		StartCoroutine(boss_lifebar_control());
    }
	
	IEnumerator coroutinecontrol()
	{
		while(mainscr == null)
			yield return null;
			
		while(!mainscr.finishedloading)
			yield return null;
		
		StartCoroutine(FadeControl());
	}

	IEnumerator boss_lifebar_control()
	{
		while(true)
		{
			if(fadein)
			{
				int barsize = game.bosshp * 1240 / game.bossmaxhp;
				RectTransform rt = barobj.GetComponent<RectTransform>();
				rt.localPosition = new Vector3(-437f, rt.localPosition.y, rt.localPosition.z);
				while(actualwidth < barsize)
				{
					actualwidth+= Mathf.FloorToInt(500*Time.deltaTime);
					rt.sizeDelta = new Vector2(actualwidth, 57);
					if(actualwidth >= barsize)
					{
						rt.sizeDelta = new Vector2(barsize, 57);
						break;
					}

					yield return null;
				}
				while(actualwidth > barsize)
				{
					actualwidth -= Mathf.FloorToInt(500*Time.deltaTime);
					rt.sizeDelta = new Vector2(actualwidth, 57);
					if(actualwidth <= barsize)
					{
						rt.sizeDelta = new Vector2(barsize, 57);
						break;
					}
					yield return null;
				}
				//Debug.Log("barsize: "+actualwidth);
			}
			yield return null;
		}
	}
    // Update is called once per frame
    void Update()
    {
		
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
			boxobj.transform.position = Vector3.Lerp(boxobj.transform.position, new Vector3(boxobj.transform.position.x, boxobjnewposy, boxobj.transform.position.z), 2f * Time.deltaTime);
			barobj.transform.position = Vector3.Lerp(barobj.transform.position, new Vector3(barobj.transform.position.x, barobjnewposy, barobj.transform.position.z), 2f * Time.deltaTime);

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
			barobj.transform.position = Vector3.Lerp(barobj.transform.position, new Vector3(barobj.transform.position.x, barobjoriginalposy, barobj.transform.position.z), 2f * Time.deltaTime);

			yield return null;
		}		
		fadeoutdone = true;
	}

	void updateposition()
	{
		boxobjoriginalposy = boxobj.transform.position.y - 200 * screenfactor;
		boxobj.transform.position = new Vector3(boxobj.transform.position.x, boxobjoriginalposy, boxobj.transform.position.z);
		barobjoriginalposy = barobj.transform.position.y - 200 * screenfactor;
		barobj.transform.position = new Vector3(barobj.transform.position.x, barobjoriginalposy, barobj.transform.position.z);

		//Update Y
		boxobjnewposy = Screen.height * 0.65f;
		barobjnewposy = Screen.height * 0.953f;

	}
}
