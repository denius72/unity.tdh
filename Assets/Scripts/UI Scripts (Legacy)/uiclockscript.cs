using System.Collections;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class uiclockscript : MonoBehaviour
{
	
	
	public MainScript mainscr;
	public bool finished_loading = false;
	
	public bool fadein;
	public bool startfadecontrol = true;
	public AudioClip clip;
	
	public float time;
	private GameObject textobj;
	private float textobjoriginalposy = 0.0f;
	private float textobjnewposy = 0.0f;
	public CustomRenderTexture custom;
	
	public Texture2D clock3;
	private GameObject clock3obj;
	private Sprite clock3Sp;
	private float clock3objoriginalposy = 0.0f;
	private float clock3objnewposy = 0.0f;

	public Texture2D clock1;
	private GameObject clock1obj;
	private Sprite clock1Sp;
	private float clock1objoriginalposy = 0.0f;
	private float clock1objnewposy = 0.0f;

	public Texture2D clock2;
	private GameObject clock2obj;
	private Sprite clock2Sp;
	private float clock2objoriginalposy = 0.0f;
	private float clock2objnewposy = 0.0f;
	
	private GameObject mCanvas;

	
	private GameObject lCanvas;
	private gamelogic game;
	
	
	void Start()
	{
		lCanvas = GameObject.Find("Canvas_Loading");;
		game = lCanvas.GetComponent<gamelogic>();
		double timer = Time.realtimeSinceStartup;
		
        mCanvas = GameObject.Find("Canvas");;
		textobj = new GameObject();
		textobj.name = "clock text";
		clock1obj = new GameObject();
		clock1obj.name = "clock1";
		clock2obj = new GameObject();
		clock2obj.name = "clock2";
		clock3obj = new GameObject();
		clock3obj.name = "clock3";
		int rand = UnityEngine.Random.Range(1, 255);
		
		//choose an object to have the audiosource;
		textobj.AddComponent<AudioSource>();

		//-------------------------------------------------------------------//

		clock3obj.AddComponent<CanvasRenderer>();
		clock3obj.AddComponent<RectTransform>();
		clock3obj.AddComponent<Mask>();
		clock3obj.AddComponent<Image>();
		clock3obj.AddComponent<CanvasGroup>();

		//converter Texture2D para Sprite
		clock3Sp = Sprite.Create(clock3, new Rect(0.0f, 0.0f, clock3.width, clock3.height), new Vector2(0.5f, 0.5f), 100.0f);

		//Pos + size
		//float minX = mCanvas.GetComponent<RectTransform>().position.x + mCanvas.GetComponent<RectTransform>().rect.xMin;
		//float minY = mCanvas.GetComponent<RectTransform>().position.y + mCanvas.GetComponent<RectTransform>().rect.yMin;
		float z = mCanvas.GetComponent<RectTransform>().position.z;
		clock3obj.transform.position = new Vector3(Screen.width * 0.08f, Screen.height * -0.35f, z);//pos
																							   //float cameraHeight = Camera.main.orthographicSize * 2;
																							   //float cameraWidth = cameraHeight * Screen.width / Screen.height; // cameraHeight * aspect ratio
																							   //txtclock3.transform.localScale = Vector3.one * cameraHeight / 5.0f;
		clock3obj.transform.SetParent(mCanvas.transform);
		clock3obj.transform.SetAsFirstSibling();

		//Rect do componente txtclock3
		RectTransform rt = clock3obj.GetComponent<RectTransform>();
		rt.sizeDelta = new Vector2(clock3.width, clock3.height);

		//Render + Listener
		clock3obj.GetComponent<Image>().sprite = clock3Sp; //Override
		CanvasGroup transp1 = clock3obj.GetComponent<CanvasGroup>();
		transp1.alpha = 0.8f;
		//txtclock3.GetComponent<Button>().onClick.AddListener(Testmethod);

		clock3obj.transform.Rotate(25.0f, 0.0f, 128.0f + (float)rand, Space.World);
		
		//-------------------------------------------------------------------//


		//converter Texture2D para Sprite
		clock1Sp = Sprite.Create(clock1, new Rect(0.0f, 0.0f, clock1.width, clock1.height), new Vector2(0.5f, 0.5f), 100.0f);
		clock1obj.AddComponent<CanvasRenderer>();
		clock1obj.AddComponent<RectTransform>();
		clock1obj.AddComponent<Image>();
		clock1obj.AddComponent<CanvasGroup>();

		//Pos + size
		float z2 = mCanvas.GetComponent<RectTransform>().position.z;
		clock1obj.transform.position = new Vector3(Screen.width * 0.08f, Screen.height * 0.2f, z2);//pos
		clock1obj.transform.SetParent(mCanvas.transform);

		//Rect do componente clock1obj
		RectTransform rt2 = clock1obj.GetComponent<RectTransform>();
		rt2.sizeDelta = new Vector2(clock1.width, clock1.height);

		//Render
		clock1obj.GetComponent<Image>().sprite = clock1Sp; //Override
		CanvasGroup transp = clock1obj.GetComponent<CanvasGroup>();
		transp.alpha = 0.5f;
		//custom.

		clock1obj.transform.Rotate(25.0f, 0.0f, (float)rand, Space.World);

		//-------------------------------------------------------------------//


		//converter Texture2D para Sprite
		clock2Sp = Sprite.Create(clock2, new Rect(0.0f, 0.0f, clock2.width, clock2.height), new Vector2(0.5f, 0.5f), 100.0f);
		clock2obj.AddComponent<CanvasRenderer>();
		clock2obj.AddComponent<RectTransform>();
		clock2obj.AddComponent<Image>();
		clock2obj.AddComponent<CanvasGroup>();

		//Pos + size
		float z3 = mCanvas.GetComponent<RectTransform>().position.z;
		clock2obj.transform.position = new Vector3(Screen.width * 0.08f, Screen.height * 0.2f, z3);//pos
		clock2obj.transform.SetParent(mCanvas.transform);

		//Rect do componente clock2obj
		RectTransform rt3 = clock2obj.GetComponent<RectTransform>();
		rt3.sizeDelta = new Vector2(clock2.width, clock2.height);

		//Render
		clock2obj.GetComponent<Image>().sprite = clock2Sp; //Override
		CanvasGroup transp3 = clock2obj.GetComponent<CanvasGroup>();
		transp3.alpha = 0.5f;

		clock2obj.transform.Rotate(25.0f, 0.0f, 128.0f + (float)rand, Space.World);

		//-------------------------------------------------------------------//
		
		//1366 = 55 //154
		//1920 = 15 //30
		//2560 = 0 //1
		//3840 = -29 //-19
		
		textobj.transform.SetParent(mCanvas.transform);
		textobj.transform.position = new Vector3(Screen.width * 0.1f, Screen.height * -0.1f, z2);//pos
																								 //textobj.transform.position = new Vector3(-300.0f, 50.0f, z2);//pos
		
		textobj.AddComponent<TextMeshProUGUI>();
		textobj.GetComponent<TextMeshProUGUI>().fontSize = 70; //Default
		textobj.GetComponent<TextMeshProUGUI>().lineSpacing = 60; //Default
		textobj.GetComponent<TextMeshProUGUI>().color = new Color32(214, 214, 214, 255); //Default
		textobj.GetComponent<TextMeshProUGUI>().outlineWidth = 0.3f;
		textobj.GetComponent<TextMeshProUGUI>().outlineColor = new Color32(118, 55, 145, 255);
		textobj.GetComponent<TextMeshProUGUI>().SetText(""+time); //Default
		textobj.GetComponent<TextMeshProUGUI>().richText = true; //for HTML markup
		textobj.GetComponent<TextMeshProUGUI>().margin = new Vector4(0.0f, 0.0f, -720.0f, 0.0f);
		textobj.GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Left;

		//-------------------------------------------------------------------//
		
		//Messing with scale, this might have some problems
		clock3obj.transform.localScale = new Vector3(0.7f, 0.7f, 1);
		clock1obj.transform.localScale = new Vector3(0.7f, 0.7f, 1);
		textobj.transform.localScale = new Vector3(1.0f, 1.0f, 1);
		clock2obj.transform.localScale = new Vector3(0.7f, 0.7f, 1);
		
		updateposition();
		
		finished_loading = true;
		Debug.Log("uiclockscript: "+(Time.realtimeSinceStartup - timer));
		StartCoroutine(coroutinecontrol());
    }
	
	IEnumerator coroutinecontrol()
	{
		while(mainscr == null)
			yield return null;
			
		while(!mainscr.finishedloading)
			yield return null;
		
		StartCoroutine(FadeControl());
		//StartCoroutine(countdown());
		StartCoroutine(backgroundanimation());
	}

    // Update is called once per frame
    void Update()
    {
       
    }
	
	public IEnumerator countdown()
	{
		//time = Timer;
		float showtime;
		string lastnum = "";
		string front = "";
		string back = "";
		
		while (true)
		{
			if(time > 0)
				time -= Time.deltaTime;
			
			//to never show above 99 sec
			if (time > 99.99f)
				showtime = 99.99f;
			else
				showtime = time;
			
			if (showtime>0)
			{
				
				back = showtime.ToString("00.00");
				//front = back;
				back = back.Substring(back.Length-2);
				if (back == "01" || back == "02")
					back = "00";
				
				front = Math.Truncate(showtime)+"";
				if (Math.Truncate(showtime) < 10)
					front = "0"+front;
				
				textobj.transform.SetAsLastSibling();
				//float screenfactor1 = (Screen.width * 70) / 1920;
				//float screenfactor2 = (Screen.width * 40) / 1920;
				//float screenfactor1 = Screen.width/40;
				//float screenfactor2 = Screen.width/60;
				
				textobj.GetComponent<TextMeshProUGUI>().SetText("<size="+48+">"+front+".<size="+32+">"+back);
			
			}
			
			//to play sound
			if(lastnum != front && showtime < 10)
			{
				lastnum = front;
				//audioManagerGameObj.GetComponent<AudioManager>().playSound1();
				
				textobj.GetComponent<AudioSource>().clip = clip;
				textobj.GetComponent<AudioSource>().volume = game.SFXVolume;
				textobj.GetComponent<AudioSource>().Play();
				//audio_src1.Play();
			}
			
			//change color based on time
			if(showtime < 10)
			{
				
				textobj.GetComponent<TextMeshProUGUI>().color = new Color32(235, 49, 94, 255); //Default
			}
			else
			{
				textobj.GetComponent<TextMeshProUGUI>().color = new Color32(214, 214, 214, 255); //Default
			}
			
			yield return null;
		}
		
		//fadein = false;
		//startfadecontrol = true;
		
		yield return null;
	}

	IEnumerator backgroundanimation()
	{
		while(clock1obj.activeSelf && clock2obj.activeSelf)
		{
			clock1obj.transform.Rotate(0.0f, 55.0f* Time.deltaTime, -15f * Time.deltaTime, Space.World);
			clock2obj.transform.Rotate(0.0f, -55.0f* Time.deltaTime, 25f * Time.deltaTime, Space.World);
			clock3obj.transform.Rotate(0.0f, 55.0f* Time.deltaTime, -35f * Time.deltaTime, Space.World);
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
	
	//Do not call FadeOut or FadeIn directly
	IEnumerator FadeIn()
	{
		fadeindone = false;
		while (clock3obj.transform.position.y < clock3objnewposy - 10)
		{
			clock3obj.transform.position = Vector3.Lerp(clock3obj.transform.position, new Vector3(clock3obj.transform.position.x, clock3objnewposy, clock3obj.transform.position.z), 2f * Time.deltaTime);
			clock1obj.transform.position = Vector3.Lerp(clock1obj.transform.position, new Vector3(clock1obj.transform.position.x, clock1objnewposy, clock1obj.transform.position.z), 2f * Time.deltaTime);
			clock2obj.transform.position = Vector3.Lerp(clock2obj.transform.position, new Vector3(clock2obj.transform.position.x, clock2objnewposy, clock2obj.transform.position.z), 2f * Time.deltaTime);
			textobj.transform.position = Vector3.Lerp(textobj.transform.position, new Vector3(textobj.transform.position.x, textobjnewposy, textobj.transform.position.z), 2f * Time.deltaTime);
			//txtclock3.transform.Translate(Vector3.down* 150 * Time.deltaTime);
			yield return null;
		}
		fadeindone = true;
		yield return null;
	}

	IEnumerator FadeOut()
	{
		fadeoutdone = false;
		while (clock3obj.transform.position.y > clock3objoriginalposy+10)
		{
			clock3obj.transform.position = Vector3.Lerp(clock3obj.transform.position, new Vector3(clock3obj.transform.position.x, clock3objoriginalposy, clock3obj.transform.position.z), 2f * Time.deltaTime);
			clock1obj.transform.position = Vector3.Lerp(clock1obj.transform.position, new Vector3(clock1obj.transform.position.x, clock1objoriginalposy, clock1obj.transform.position.z), 2f * Time.deltaTime);
			clock2obj.transform.position = Vector3.Lerp(clock2obj.transform.position, new Vector3(clock2obj.transform.position.x, clock2objoriginalposy, clock2obj.transform.position.z), 2f * Time.deltaTime);
			textobj.transform.position = Vector3.Lerp(textobj.transform.position, new Vector3(textobj.transform.position.x, textobjoriginalposy, textobj.transform.position.z), 2f * Time.deltaTime);
			yield return null;
		}
		fadeoutdone = true;
		yield return null;
	}

	void updateposition()
	{
		clock3objoriginalposy = Screen.height * - 0.2f;
		clock3obj.transform.position = new Vector3(clock3obj.transform.position.x, clock3objoriginalposy, clock3obj.transform.position.z);
		clock1objoriginalposy = Screen.height * - 0.2f;
		clock1obj.transform.position = new Vector3(clock1obj.transform.position.x, clock1objoriginalposy, clock1obj.transform.position.z);
		clock2objoriginalposy = Screen.height * - 0.2f;
		clock2obj.transform.position = new Vector3(clock2obj.transform.position.x, clock2objoriginalposy, clock2obj.transform.position.z);
		textobjoriginalposy = Screen.height * - 0.2f;
		textobj.transform.position = new Vector3(textobj.transform.position.x, textobjoriginalposy, textobj.transform.position.z);


		//Update Y
		clock3objnewposy = Screen.height * 0.95f;
		clock1objnewposy = Screen.height * 0.95f;
		clock2objnewposy = Screen.height * 0.95f;
		textobjnewposy = Screen.height * 0.95f;

	}


}
