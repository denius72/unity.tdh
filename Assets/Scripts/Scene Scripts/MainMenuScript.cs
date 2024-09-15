using System.Collections;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
	//Music
	public GameObject objToAttachSong;
	public AudioClip clip;
	private GameObject audioSelectObj;
	public AudioClip audioSelect;
	private GameObject audioCancelObj;
	public AudioClip audioCancel;
	private GameObject audioScrollObj;
	public AudioClip audioScroll;

	private gamelogic game;
	
	public TMP_FontAsset MenuFont;
	public MasterControls controls;
	public float movement_threshold = 0;
	
	public GameObject introCamera1;
	public GameObject introCamera2;
	public GameObject distortion;

	private GameObject lCanvas;
	public LoadingScreen load;
	
	//Main Menu
	public GameObject[] buttons;
	public GameObject particles;
	public int menu_selection = 0;
	public int menu_nested = 0;
	//menu_nested properties:
	//0 - main menu 
	//1 - clicked on new game, shows new game selections
	//2 - clicked on continue, shows save files to choose
	//3 - clicked on extras
	//4 - clicked on options, shows... options
	
	public Texture2D box;
	private GameObject boxobj;
	private Sprite boxSp;
	
	public Texture2D box2;
	private GameObject boxobj2;
	private Sprite boxSp2;
	
	private bool timeFlag = false;
	
	//Options
	
	//Canvas
	private GameObject mCanvas;
	
    // Start is called before the first frame update
    void Start()
    {
		
		lCanvas = GameObject.Find("Canvas_Loading");;
		load = lCanvas.GetComponent<LoadingScreen>();
		game = lCanvas.GetComponent<gamelogic>();
		
		controls = new MasterControls();
        mCanvas = GameObject.Find("Canvas");;
		//DontDestroyOnLoad(mCanvas);
		//introCamera1 = GameObject.Find("IntroCamera");
		//introCamera2 = GameObject.Find("CameraMenu");

		//buttons = new GameObject[5];
		
		boxobj = new GameObject();
		boxobj.name = "Logo Obj";
		boxobj.AddComponent<CanvasRenderer>();
		boxobj.AddComponent<RectTransform>();
		boxobj.AddComponent<Mask>();
		boxobj.AddComponent<Image>();
		boxobj.AddComponent<CanvasGroup>();
		boxobj.transform.position = new Vector3(Screen.width * 0.4f, Screen.height * 0.75f, 0.0f);//pos
		boxobj.transform.SetParent(mCanvas.transform);
		boxSp = Sprite.Create(box, new Rect(0.0f, 0.0f, box.width, box.height), new Vector2(0.5f, 0.5f));
		RectTransform rt = boxobj.GetComponent<RectTransform>();
		rt.sizeDelta = new Vector2(box.width, box.height);
		boxobj.GetComponent<Image>().sprite = boxSp;
		CanvasGroup transp1 = boxobj.GetComponent<CanvasGroup>();
		transp1.alpha = 1.0f;
		boxobj.transform.localScale = new Vector3(1.0f, 1.0f, 1);
		
		boxobj2 = new GameObject();
		boxobj2.name = "Logo Obj 2";
		boxobj2.AddComponent<CanvasRenderer>();
		boxobj2.AddComponent<RectTransform>();
		boxobj2.AddComponent<Mask>();
		boxobj2.AddComponent<Image>();
		boxobj2.AddComponent<CanvasGroup>();
		boxobj2.transform.position = new Vector3(Screen.width * 0.5f, Screen.height * 0.75f, 0.0f);//pos
		boxobj2.transform.SetParent(mCanvas.transform);
		boxSp2 = Sprite.Create(box2, new Rect(0.0f, 0.0f, box2.width, box2.height), new Vector2(0.5f, 0.5f));
		RectTransform rt2 = boxobj2.GetComponent<RectTransform>();
		rt2.sizeDelta = new Vector2(box.width, box.height);
		boxobj2.GetComponent<Image>().sprite = boxSp2;
		CanvasGroup transp12 = boxobj2.GetComponent<CanvasGroup>();
		transp12.alpha = 1.0f;
		boxobj2.transform.localScale = new Vector3(1.0f, 1.0f, 1);
		
		objToAttachSong.AddComponent<AudioSource>();
		objToAttachSong.GetComponent<AudioSource>().clip = clip;
		objToAttachSong.GetComponent<AudioSource>().ignoreListenerPause=true;
		objToAttachSong.GetComponent<AudioSource>().volume = game.BGMVolume;
		objToAttachSong.GetComponent<AudioSource>().loop = true;
		objToAttachSong.GetComponent<AudioSource>().Play();

		audioSelectObj = new GameObject();
		audioSelectObj.AddComponent<AudioSource>();
		audioSelectObj.GetComponent<AudioSource>().clip = audioSelect;
		audioSelectObj.GetComponent<AudioSource>().ignoreListenerPause=true;
		audioSelectObj.GetComponent<AudioSource>().volume = game.SFXVolume;

		audioCancelObj = new GameObject();
		audioCancelObj.AddComponent<AudioSource>();
		audioCancelObj.GetComponent<AudioSource>().clip = audioCancel;
		audioCancelObj.GetComponent<AudioSource>().ignoreListenerPause=true;
		audioCancelObj.GetComponent<AudioSource>().volume = game.SFXVolume;

		audioScrollObj = new GameObject();
		audioScrollObj.AddComponent<AudioSource>();
		audioScrollObj.GetComponent<AudioSource>().clip = audioScroll;
		audioScrollObj.GetComponent<AudioSource>().ignoreListenerPause=true;
		audioScrollObj.GetComponent<AudioSource>().volume = game.SFXVolume;


		StartCoroutine(intro());
		
		//introCamera1.SetActive(false);
		//introCamera2.SetActive(true);
		//StartCoroutine(fadecontrol(buttons));
		//StartCoroutine(menucontrol(buttons));
		//StartCoroutine(animatelogo1(boxobj));
		//StartCoroutine(animatelogo2(boxobj2));
		//StartCoroutine(waitForLoadAnimation());
    }
	
	IEnumerator intro()
	{
		//yield return new WaitForSeconds(2);
		
		yield return new WaitForSecondsRealtime(2);
		
		float journeyLength = Vector3.Distance(new Vector3(0.2f,4.4f,5.85f), new Vector3(-0.46f,3.86f,-63.06f));
		/*
		StartCoroutine(countdownRealTime(4));
		while(!timeFlag)
		{
			
			introCamera1.transform.position = Vector3.Lerp(introCamera1.transform.position, new Vector3(0.0f,4.341f,-1.778f), (Time.deltaTime * 50.0f) / journeyLength);
			yield return null;
		}
		StartCoroutine(countdownRealTime(5));
		while(!timeFlag)
		{
			
			introCamera1.transform.position = Vector3.Lerp(introCamera1.transform.position, new Vector3(-0.46f,3.86f,-63.06f), (Time.deltaTime * 100.0f) / journeyLength);
			yield return null;
		}
		StartCoroutine(countdownRealTime(5));
		while(!timeFlag)
		{
			introCamera1.transform.rotation = Quaternion.Slerp(introCamera1.transform.rotation, new Quaternion(0f,0f,0f,0f), (Time.deltaTime * 100.0f) / journeyLength);
			introCamera1.transform.position = Vector3.Lerp(introCamera1.transform.position, new Vector3(-4.71f,31.8f,-57.11f), (Time.deltaTime * 100.0f) / journeyLength);
			yield return null;
		}
		
		journeyLength = Vector3.Distance(new Vector3(-12.2f,33.9f,-57.11f), new Vector3(-4.71f,31.8f,-57.11f));
		StartCoroutine(countdownRealTime(1));
		while(!timeFlag)
		{
			//introCamera1.transform.rotation = Quaternion.Slerp(introCamera1.transform.rotation, new Quaternion(0f,0f,0f,0f), (Time.deltaTime * 100.0f) / journeyLength);
			introCamera2.transform.position = Vector3.Lerp(introCamera2.transform.position, new Vector3(-4.71f,33.48f,-57.11f), (Time.deltaTime * 20.0f) / journeyLength);
			yield return null;
		}
		*/
		//introCamera1.SetActive(false);
		//introCamera2.SetActive(true);
		
		StartCoroutine(fadecontrol(buttons));
		StartCoroutine(menucontrol(buttons));
		StartCoroutine(animatelogo1(boxobj));
		StartCoroutine(animatelogo2(boxobj2));
		StartCoroutine(waitForLoadAnimation());
		
		//introCamera1.transform.position = Vector3.Lerp(introCamera1.transform.position, new Vector3(-0.46f,3.86f,-63.06f), (Time.deltaTime * 20.0f) / journeyLength);
	}
	
	IEnumerator countdownRealTime(float time)
	{
		timeFlag = false;
		yield return new WaitForSecondsRealtime(time);
		timeFlag = true;
	}
	
	
	//wait for the transition to end before enabling controls
	IEnumerator waitForLoadAnimation()
	{
		while (!load.finished_animation)
			yield return null;
		
		controls.Menu.Enable();
		
	}
	
	IEnumerator menucontrol(GameObject[] obj)
	{
        float time = 0;
		float sineFrequency = 1.0f; // Frequency of the sine wave
		float sizeVariation = 5.0f; // Variation in font size
		float outlineWidthVariation = 0.1f; // Variation in outline width
		Color defaultOutlineColor = Color.black; // Default outline color
		Color selectedOutlineColor = Color.yellow; // Outline color for selected item
		float lerpSpeed = 15f; // Speed of the lerp
	
        while (obj[0] != null)
        {
            for (int i = 0; i < obj.Length; i++)
            {
                TextMeshProUGUI text = obj[i].GetComponent<TextMeshProUGUI>();
                if (menu_selection == i)
                {
                    // Calculate the sine wave offset for the selected menu item
                    float sineValue = Mathf.Sin(time * sineFrequency) * sizeVariation;
                    text.fontSize = 100 + sineValue; // Default size with sine wave variation
					// Animate outline width
                    float outlineWidth = 0.5f + Mathf.Sin(time * sineFrequency) * outlineWidthVariation;
                    text.outlineWidth = outlineWidth;

                    // Animate outline color
                    text.outlineColor = Color.Lerp(defaultOutlineColor, selectedOutlineColor, (Mathf.Sin(time * sineFrequency) + 1) / 2);
					
					RectTransform selectedTextRect = obj[i].GetComponent<RectTransform>();
					Vector3 newPosition = particles.transform.position;
					newPosition.y = selectedTextRect.position.y - 10f;
					particles.transform.position = Vector3.Lerp(particles.transform.position, newPosition, Time.deltaTime * lerpSpeed);
					
                }
                else
                {
                    text.fontSize = 80; // Not Default
                    text.outlineWidth = 0.5f;
                    text.outlineColor = defaultOutlineColor;
                }
            }

            time += Time.deltaTime * 2.0f; // Increment the elapsed time
            yield return null;
        }

	}
	
	IEnumerator fadecontrol(GameObject[] obj)
	{
		yield return new WaitForSecondsRealtime(0.3f);
        foreach (GameObject ob in obj)
        {
            StartCoroutine(fadein(ob));
            StartCoroutine(animate(ob));
			yield return new WaitForSecondsRealtime(0.3f);
        }
	}
	
	IEnumerator fadein(GameObject obj)
	{
		yield return new WaitForSeconds(1);
		float objfloat = obj.transform.rotation.y;
		
		while(obj.transform.rotation.y > 0.0f)
		{
			obj.transform.Rotate(0.0f, objfloat, 0.0f, Space.World);
			objfloat -= Time.unscaledDeltaTime*5.0f;
			yield return null;
		}
		
	}
	
	IEnumerator animate(GameObject obj)
	{
		float speed = 1.0f;
    	float radius = 0.1f;
    	float angle = 0.0f;
		while(obj != null)
		//while(true)
		{
			obj.transform.position = new Vector3(obj.transform.position.x + radius * Mathf.Cos(angle) * Time.deltaTime*40f, obj.transform.position.y + radius * Mathf.Sin(angle) * Time.deltaTime*35f, obj.transform.position.z);
			angle += speed * Time.unscaledDeltaTime;

			yield return null;
		}
	}
	
	IEnumerator animatelogo1 (GameObject obj)
	{
		float speed = 1.0f;
    	float radius = 0.02f;
    	float angle = 0.0f;
		while(obj != null)
		//while(true)
		{
			obj.transform.position = new Vector3(obj.transform.position.x + radius * Mathf.Cos(angle) * Time.deltaTime *200f, obj.transform.position.y + radius * Mathf.Sin(angle)* Time.deltaTime*200f, obj.transform.position.z);
			angle += speed * Time.unscaledDeltaTime;

			yield return null;
		}
	}
	
	IEnumerator animatelogo2 (GameObject obj)
	{
		float speed = 1.0f;
    	float radius = 0.02f;
    	float angle = 0.0f;
		while(obj != null)
		//while(true)
		{
			obj.transform.position = new Vector3(obj.transform.position.x - radius * Mathf.Cos(angle)* Time.deltaTime*200f, obj.transform.position.y - radius * Mathf.Sin(angle)* Time.deltaTime*200f, obj.transform.position.z);
			angle += speed * Time.unscaledDeltaTime;

			yield return null;
		}
	}
	
	
	void buttonConfirmPress()
	{

		//Confirm button action
		if(controls.Menu.Confirm.triggered)
		{
			audioSelectObj.GetComponent<AudioSource>().Play();
			switch(menu_selection)
			{
				case 0:
					Debug.Log("started loading main scene");
					//StartCoroutine(load.LoadSceneAsync("GensouHakurei"));
					//StartCoroutine(load.LoadSceneAsync("Test01"));
					StartCoroutine(load.LoadSceneAsync("Test04"));
					break;
				case 1:
					Debug.Log("2 was pressed");
					break;
				case 2:
					Debug.Log("3 was pressed");
					break;
				case 3:
					Debug.Log("4 was pressed");
					break;
				case 4:
					Application.Quit();
					Debug.Log("5 was pressed");
					break;
			}
		}
	}
	
    // Update is called once per frame
    void Update()
    {
		
		Vector2 input = controls.Menu.Movement.ReadValue<Vector2>();
		
		//Debug.Log(input + "|" + menu_selection);
		
		buttonConfirmPress();
		
		//movement down
		if(input.y < 0 && movement_threshold <= 0.0f)
		{
			menu_selection += 1;
			movement_threshold = 10.0f;
			audioScrollObj.GetComponent<AudioSource>().Play();
		}
		
		//movement up
		if(input.y > 0 && movement_threshold <= 0.0f)
		{
			menu_selection -= 1;
			movement_threshold = 10.0f;
			audioScrollObj.GetComponent<AudioSource>().Play();
		}
			
		//wrap around
		if(menu_selection > buttons.Length -1)
			menu_selection = 0;
		else if(menu_selection < 0)
			menu_selection = buttons.Length -1;
		
		//movement threshold
		if(movement_threshold > 0.0f)
			movement_threshold = movement_threshold - 60.0f * Time.unscaledDeltaTime;
		
    }
}
