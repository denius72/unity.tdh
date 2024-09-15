using System.Collections;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Scripo : MonoBehaviour
{
	public bool finished_loading = false;
	
	public bool fadein;
	public bool startfadecontrol = true;
	public uiplayerframe playerframe;
	public uiplayerscript playerscript;
	public uipausescript pausescript;
	public MainScript mainscr;
	
	public Material material;
	public CustomRenderTexture custom;
	public Texture2D[] char1;
	public Texture2D[] char2;
	public Texture2D box;
	public MasterControls controls;
	public Texture2D mask1;
	public Texture2D mask2;
	public Texture2D prompt;
	public int distortion;

	private Sprite boxSp;
	private Sprite mask1Sp;
	private Sprite mask2Sp;
	private Sprite promptSp;
	private Sprite[] char1Sp;
	private Sprite[] char2Sp;
	private GameObject mCanvas;
	private GameObject txtbox;
	private GameObject mask1obj;
	private GameObject mask2obj;
	private GameObject textobj;
	private GameObject promptobj;
	private GameObject[] char1obj;
	private GameObject[] char2obj;
	public CanvasGroup transpprompt; //To change alpha anywhere
	public bool isReady = false; //Used to see if the txtlogic function is ready to move on
								//original and new Y position values
	private float textobjoriginalposy = 0.0f;
	private float textobjnewposy = 0.0f;
	private float txtboxoriginalposy = 0.0f;
	private float txtboxnewposy = 0.0f;
	private float mask1objoriginalposy = 0.0f;
	private float mask1objnewposy = 0.0f;
	private float mask2objoriginalposy = 0.0f;
	private float mask2objnewposy = 0.0f;

	private float char1objoriginalposx = 0.0f;
	private float char1objnewposx = 0.0f;
	private float char2objoriginalposx = 0.0f;
	private float char2objnewposx = 0.0f;

	public bool txtkeypressed = false;
	public bool drawall = false;
	float screenfactor = (Screen.width * 5) / 1920;
	//

	// Start is called before the first frame update
	private GameObject lCanvas;
	private gamelogic game;
	
	void Start()
	{
		lCanvas = GameObject.Find("Canvas_Loading");;
		game = lCanvas.GetComponent<gamelogic>();
		
		double timer = Time.realtimeSinceStartup;
		//QualitySettings.vSyncCount = 0;
		//Application.targetFrameRate = 100;
		
		controls = new MasterControls();

		//inicializar
		mCanvas = GameObject.Find("Canvas");
		txtbox = new GameObject();
		txtbox.name = "Text Box";
		mask1obj = new GameObject();
		mask1obj.name = "Mask 1";
		mask2obj = new GameObject();
		mask2obj.name = "Mask 2";
		textobj = new GameObject();
		textobj.name = "Text";
		promptobj = new GameObject();
		promptobj.name = "Prompt";
		char1obj = new GameObject[char1.Length];
		char1Sp = new Sprite[char1.Length];
		for (int i = 0; i < char1.Length; i++)
		{
			char1obj[i] = new GameObject();
			char1obj[i].name = "Char1 Sprite "+i;
		}
		char2obj = new GameObject[char2.Length];
		char2Sp = new Sprite[char2.Length];
		for (int i = 0; i < char2.Length; i++)
		{
			char2obj[i] = new GameObject();
			char2obj[i].name = "Char2 Sprite "+i;
		}

		//mask3obj = new GameObject();
		int rand = UnityEngine.Random.Range(1, 255);

		//-------------------------------------------------------------------//

		//CHARACTER SPRITES

		//converter Texture2D para Sprite
		for (int i = 0; i < char1.Length; i++)
		{
			char1Sp[i] = Sprite.Create(char1[i], new Rect(0.0f, 0.0f, char1[i].width, char1[i].height), new Vector2(0.5f, 0.5f), 100.0f);
			char1obj[i].AddComponent<CanvasRenderer>();
			char1obj[i].AddComponent<RectTransform>();
			char1obj[i].AddComponent<Image>();
			char1obj[i].AddComponent<CanvasGroup>();

			//Pos + size
			char1obj[i].transform.position = new Vector3(Screen.width * - 0.4f, Screen.height*0.5f, mCanvas.GetComponent<RectTransform>().position.z);//pos
			char1obj[i].transform.SetParent(mCanvas.transform);

			//Rect do componente char1obj
			RectTransform rtc1 = char1obj[i].GetComponent<RectTransform>();
			rtc1.sizeDelta = new Vector2(char1[i].width, char1[i].height);

			//Render
			char1obj[i].GetComponent<Image>().sprite = char1Sp[i]; //Override

			CanvasGroup char1transp = char1obj[i].GetComponent<CanvasGroup>();
			//transpprompt.alpha = 0.7f;
			char1transp.alpha = 0.0f;
		}
		for (int i = 0; i < char2.Length; i++)
		{
			char2Sp[i] = Sprite.Create(char2[i], new Rect(0.0f, 0.0f, char2[i].width, char2[i].height), new Vector2(0.5f, 0.5f), 100.0f);
			char2obj[i].AddComponent<CanvasRenderer>();
			char2obj[i].AddComponent<RectTransform>();
			char2obj[i].AddComponent<Image>();
			char2obj[i].AddComponent<CanvasGroup>();

			//Pos + size
			char2obj[i].transform.position = new Vector3(Screen.width * 1.4f, Screen.height*0.5f, mCanvas.GetComponent<RectTransform>().position.z);//pos
			char2obj[i].transform.SetParent(mCanvas.transform);

			//Rect do componente char1obj
			RectTransform rtc1 = char2obj[i].GetComponent<RectTransform>();
			rtc1.sizeDelta = new Vector2(char2[i].width, char2[i].height);

			//Render
			char2obj[i].GetComponent<Image>().sprite = char2Sp[i]; //Override

			CanvasGroup char2transp = char2obj[i].GetComponent<CanvasGroup>();
			//transpprompt.alpha = 0.7f;
			char2transp.alpha = 0.0f;
		}


		//-------------------------------------------------------------------//

		txtbox.AddComponent<CanvasRenderer>();
		txtbox.AddComponent<RectTransform>();
		txtbox.AddComponent<Mask>();
		txtbox.AddComponent<Image>();
		txtbox.AddComponent<CanvasGroup>();

		//converter Texture2D para Sprite
		boxSp = Sprite.Create(box, new Rect(0.0f, 0.0f, box.width, box.height), new Vector2(0.5f, 0.5f), 100.0f);

		//Pos + size
		//float minX = mCanvas.GetComponent<RectTransform>().position.x + mCanvas.GetComponent<RectTransform>().rect.xMin;
		//float minY = mCanvas.GetComponent<RectTransform>().position.y + mCanvas.GetComponent<RectTransform>().rect.yMin;
		float z = mCanvas.GetComponent<RectTransform>().position.z;
		txtbox.transform.position = new Vector3(Screen.width * 0.5f, Screen.height * -0.2f, z);//pos
																							   //float cameraHeight = Camera.main.orthographicSize * 2;
																							   //float cameraWidth = cameraHeight * Screen.width / Screen.height; // cameraHeight * aspect ratio
																							   //txtbox.transform.localScale = Vector3.one * cameraHeight / 5.0f;
		txtbox.transform.SetParent(mCanvas.transform);

		//Rect do componente txtbox
		RectTransform rt = txtbox.GetComponent<RectTransform>();
		rt.sizeDelta = new Vector2(box.width, box.height);

		//Render + Listener
		txtbox.GetComponent<Image>().sprite = boxSp; //Override
		CanvasGroup transp1 = txtbox.GetComponent<CanvasGroup>();
		transp1.alpha = 0.7f;
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
		mask1obj.transform.SetParent(txtbox.transform);

		//Rect do componente mask1obj
		RectTransform rt2 = mask1obj.GetComponent<RectTransform>();
		rt2.sizeDelta = new Vector2(mask1.width, mask1.height);

		//Render
		mask1obj.GetComponent<Image>().sprite = mask1Sp; //Override
		//mask1obj.GetComponent<Image>().material = (Material)Resources.Load("textboxmaterial.mat", typeof(Material));
		//mask1obj.GetComponent<Image>().material = material;
		CanvasGroup transp = mask1obj.GetComponent<CanvasGroup>();
		transp.alpha = 0.3f;
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
		mask2obj.transform.SetParent(txtbox.transform);

		//Rect do componente mask2obj
		RectTransform rt3 = mask2obj.GetComponent<RectTransform>();
		rt3.sizeDelta = new Vector2(mask2.width, mask2.height);

		//Render
		mask2obj.GetComponent<Image>().sprite = mask2Sp; //Override
		//mask2obj.GetComponent<Image>().material = material;
		CanvasGroup transp3 = mask2obj.GetComponent<CanvasGroup>();
		transp3.alpha = 0.3f;

		mask2obj.transform.Rotate(0.0f, 0.0f, 128.0f + (float)rand, Space.World);

		//-------------------------------------------------------------------//

		
		//converter Texture2D para Sprite
		promptSp = Sprite.Create(prompt, new Rect(0.0f, 0.0f, prompt.width, prompt.height), new Vector2(0.5f, 0.5f), 100.0f);
		promptobj.AddComponent<CanvasRenderer>();
		promptobj.AddComponent<RectTransform>();
		promptobj.AddComponent<Image>();
		promptobj.AddComponent<CanvasGroup>();

		//Pos + size
		float z4 = mCanvas.GetComponent<RectTransform>().position.z;
		promptobj.transform.position = new Vector3(Screen.width * 0.87f, Screen.height * 0.15f, z4);//pos
		promptobj.transform.SetParent(mCanvas.transform);

		//Rect do componente promptobj
		RectTransform rt4 = promptobj.GetComponent<RectTransform>();
		rt4.sizeDelta = new Vector2(prompt.width, prompt.height);

		//Render
		promptobj.GetComponent<Image>().sprite = promptSp; //Override
		transpprompt = promptobj.GetComponent<CanvasGroup>();
		//transpprompt.alpha = 0.7f;
		transpprompt.alpha = 0.0f;

		//-------------------------------------------------------------------//

		//Showing the text
		textobj.transform.SetParent(mCanvas.transform);
		textobj.transform.position = new Vector3(Screen.width * 0.2f, Screen.height * -0.1f, z2);//pos
																								 //textobj.transform.position = new Vector3(-300.0f, 50.0f, z2);//pos
		textobj.AddComponent<TextMeshProUGUI>();
		textobj.GetComponent<TextMeshProUGUI>().fontSize = 26; //Default
		textobj.GetComponent<TextMeshProUGUI>().lineSpacing = 60; //Default
		textobj.GetComponent<TextMeshProUGUI>().color = Color.white; //Default
		textobj.GetComponent<TextMeshProUGUI>().SetText(""); //Default
		textobj.GetComponent<TextMeshProUGUI>().richText = true; //for HTML markup
		textobj.GetComponent<TextMeshProUGUI>().margin = new Vector4(0.0f, 0.0f, -720.0f, -140.0f);

		//-------------------------------------------------------------------//

		//Messing with scale, this might have some problems
		textobj.transform.localScale = new Vector3(1.3f, 1.3f, 1);
		txtbox.transform.localScale = new Vector3(1.3f, 1.3f, 1);
		mask1obj.transform.localScale = new Vector3(1.6f, 1.6f, 1);
		mask2obj.transform.localScale = new Vector3(1.6f, 1.6f, 1);
		promptobj.transform.localScale = new Vector3(1.0f, 1.0f, 1);
		for (int i = 0; i < char1.Length; i++)
		{
			char1obj[i].transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
		}		
		for (int i = 0; i < char2.Length; i++)
		{
			char2obj[i].transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
		}

		//-------------------------------------------------------------------//

		//memorize good stuff

		updateposition();

		//start those damn routines;
		finished_loading = true;
		Debug.Log("Scripo: "+(Time.realtimeSinceStartup - timer));
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

	//id tells what sprite will draw
	//type 0 is instantaneous
	//type 1 has exchange animation
	public IEnumerator char1_control(int id, bool type)
	{
		if (type)
		{
			
			for (int i = 0; i < char1.Length; i++)
			{
				CanvasGroup char1transp = char1obj[i].GetComponent<CanvasGroup>();
				char1transp.alpha = 0.0f;
				if(i == id)
				{
					char1obj[i].transform.position = new Vector3(char1objoriginalposx, char1obj[i].transform.position.y, char1obj[i].transform.position.z);
					char1transp.alpha = 1.0f;	
					StartCoroutine(Portrait_FadeIn(i));
				}
			}
		}
		else
		{
			for (int i = 0; i < char1.Length; i++)
			{
				CanvasGroup char1transp = char1obj[i].GetComponent<CanvasGroup>();
				char1transp.alpha = 0.0f;
				if(i == id)
					char1transp.alpha = 1.0f;	
			}		
		}
		yield return null;
	}
	public IEnumerator char2_control(int id, bool type)
	{
		if (type)
		{
			for (int i = 0; i < char2.Length; i++)
			{
				CanvasGroup char2transp = char2obj[i].GetComponent<CanvasGroup>();
				char2transp.alpha = 0.0f;
				if(i == id)
				{
					char2obj[i].transform.position = new Vector3(char2objoriginalposx, char2obj[i].transform.position.y, char2obj[i].transform.position.z);
					char2transp.alpha = 1.0f;	
					StartCoroutine(Portrait_Enemy_FadeIn(i));
				}
			}
		}
		else
		{
			for (int i = 0; i < char2.Length; i++)
			{
				CanvasGroup char2transp = char2obj[i].GetComponent<CanvasGroup>();
				char2transp.alpha = 0.0f;
				if(i == id)
					char2transp.alpha = 1.0f;	
			}		
		}
		yield return null;
	}
	// Update is called once per frame
	void Update()
	{
		if (!pausescript.isPaused() && !drawall)
			txtkeypressed = controls.Menu.Confirm.triggered;
		//float screenfactor = (Screen.width * 5) / 1920;
	}

	IEnumerator backgroundanimation()
	{
		while(mask1obj.activeSelf && mask2obj.activeSelf)
		{
			mask1obj.transform.Rotate(0.0f, 0.0f, 10f * Time.deltaTime, Space.World);
			mask2obj.transform.Rotate(0.0f, 0.0f, -10f * Time.deltaTime, Space.World);
			promptobj.transform.position = new Vector3(promptobj.transform.position.x, promptobj.transform.position.y + Mathf.Sin(Time.time*5) * Time.deltaTime * 20.0f, promptobj.transform.position.z);//pos
			yield return null;
		}
		yield return null;
	}

	//Recognizes standard Unity richtext format markup like:
	//<color><b><i><size><material><quadmaterial>
	//There is a new speed tag;
	//Example: <sp=0.01> this is the new speed for the text (in ms) until it finds another <sp> tag
	//The speed tag can also be used for brief pauses if you use it correctly.
	IEnumerator txtlogic(string textdraw)
	{
		isReady = false;
		string nexttodraw = "";
		string buffer = ""; //used to keep track of the sp tag
		int txtlen = textdraw.Length;
		int count = 0;
		float speed = 0.01f;
		while (txtlen > count)
		{
			if (pausescript.isPaused())
				Debug.Log("Game is paused, text will wait.");
			
			while(pausescript.isPaused())
				yield return null;

			//Debug.Log(txtkeypressed);
			//Logic to "ignore" HTML tags in drawing
			if (Char.Equals(textdraw[count], '<'))

				//using do, because we need this to iterate one last time if the char is '>'
				do
				{
					buffer = buffer + textdraw[count];
					nexttodraw = nexttodraw + textdraw[count];
					count += 1;

				} while (!Char.Equals(textdraw[count], '>'));

			//Checks to see if speed tag is found
			//If the buffer is not empty
			if (!String.Equals(buffer, ""))
			{
				string tempbuffer = "";
				//Extract the first four char
				if (buffer.Length > 3)
					for (int i = 0; i < 4; i++)
					{
						tempbuffer = tempbuffer + buffer[i];
					}
				//Check to see if we can find <sp= in tempbuffer
				if (String.Equals(tempbuffer, "<sp="))
				{
					//empties tempbuffer to use it again
					tempbuffer = "";
					//gets the number and sets it 
					for (int i = 4; i < buffer.Length; i++)
					{
						tempbuffer += buffer[i];
					}
					speed = float.Parse(tempbuffer);

					//remove the tag so it doesn't draw on the screen and add 1 to counter to get rid of the '>'.
					nexttodraw = nexttodraw.Remove(nexttodraw.Length - (4 + tempbuffer.Length));
					count += 1;
				}
				//empties the buffer;
				buffer = "";
				tempbuffer = "";

			}

			nexttodraw = nexttodraw + textdraw[count];
			textobj.GetComponent<TextMeshProUGUI>().SetText(nexttodraw);
			//textobj.GetComponent<TextMeshProUGUI>().SetText((Time.deltaTime).ToString() + " " + txtbox.transform.position.y);
			count += 1;
			
			if(!txtkeypressed && !drawall)
			{}
			else
				drawall = true;
			
			if(drawall)
			{}
			else
				yield return new WaitForSecondsRealtime(0.001f);
				//yield return new WaitForSecondsRealtime(speed*0.02f);
				
		}
		//StartCoroutine(FadeOut());
		txtkeypressed = false;
		drawall = false;
		isReady = true;
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

	//It won't update the position if txtbox is already up, so can be called directly.
	public IEnumerator FadeInWithTxt(string textdraw)
	{
		
		controls.Menu.Enable();
		fadeindone = false;
		isReady = false;
		textobj.GetComponent<TextMeshProUGUI>().SetText("");
		StartCoroutine(txtlogic(textdraw));
		
		while (txtbox.transform.position.y < txtboxnewposy-10)
		{
			txtbox.transform.position = Vector3.Lerp(txtbox.transform.position, new Vector3(txtbox.transform.position.x, txtboxnewposy, txtbox.transform.position.z), 8f * Time.deltaTime);
			textobj.transform.position = Vector3.Lerp(textobj.transform.position, new Vector3(textobj.transform.position.x, textobjnewposy, textobj.transform.position.z), 8f * Time.deltaTime);
			mask1obj.transform.position = Vector3.Lerp(mask1obj.transform.position, new Vector3(mask1obj.transform.position.x, mask1objnewposy, mask1obj.transform.position.z), 8f * Time.deltaTime);
			mask2obj.transform.position = Vector3.Lerp(mask2obj.transform.position, new Vector3(mask2obj.transform.position.x, mask2objnewposy, mask2obj.transform.position.z), 8f * Time.deltaTime);

			for (int i = 0; i < char1.Length; i++)
			{
				char1obj[i].transform.position = Vector3.Lerp(char1obj[i].transform.position, new Vector3(char1objnewposx, char1obj[i].transform.position.y, char1obj[i].transform.position.z), 8f * Time.deltaTime);
			}
			for (int i = 0; i < char2.Length; i++)
			{
				char2obj[i].transform.position = Vector3.Lerp(char2obj[i].transform.position, new Vector3(char2objnewposx, char2obj[i].transform.position.y, char2obj[i].transform.position.z), 8f * Time.deltaTime);
			}
			
			//txtbox.transform.Translate(Vector3.down* 150 * Time.deltaTime);
			yield return null;
		}
		fadeindone = true;
		yield return null;
	}

	//Without calling text logic
	public IEnumerator FadeIn()
	{
		controls.Menu.Enable();
		fadeindone = false;
		while (txtbox.transform.position.y < txtboxnewposy - 10)
		{
			txtbox.transform.position = Vector3.Lerp(txtbox.transform.position, new Vector3(txtbox.transform.position.x, txtboxnewposy, txtbox.transform.position.z), 8f * Time.deltaTime);
			textobj.transform.position = Vector3.Lerp(textobj.transform.position, new Vector3(textobj.transform.position.x, textobjnewposy, textobj.transform.position.z), 8f * Time.deltaTime);
			mask1obj.transform.position = Vector3.Lerp(mask1obj.transform.position, new Vector3(mask1obj.transform.position.x, mask1objnewposy, mask1obj.transform.position.z), 8f * Time.deltaTime);
			mask2obj.transform.position = Vector3.Lerp(mask2obj.transform.position, new Vector3(mask2obj.transform.position.x, mask2objnewposy, mask2obj.transform.position.z), 8f * Time.deltaTime);
			
			for (int i = 0; i < char1.Length; i++)
			{
				char1obj[i].transform.position = Vector3.Lerp(char1obj[i].transform.position, new Vector3(char1objnewposx, char1obj[i].transform.position.y, char1obj[i].transform.position.z), 8f * Time.deltaTime);
			}
			for (int i = 0; i < char2.Length; i++)
			{
				char2obj[i].transform.position = Vector3.Lerp(char2obj[i].transform.position, new Vector3(char2objnewposx, char2obj[i].transform.position.y, char2obj[i].transform.position.z), 8f * Time.deltaTime);
			}
			//txtbox.transform.Translate(Vector3.down* 150 * Time.deltaTime);
			yield return null;
		}
		fadeindone = true;
		yield return null;
	}

	public IEnumerator Portrait_FadeIn(int i)
	{
		fadeindone = false;
		while (char1obj[i].transform.position.x < char1objnewposx - 10)
		{
			char1obj[i].transform.position = Vector3.Lerp(char1obj[i].transform.position, new Vector3(char1objnewposx, char1obj[i].transform.position.y, char1obj[i].transform.position.z), 8f * Time.deltaTime);
			yield return null;
		}
		fadeindone = true;
		yield return null;
	}

	public IEnumerator Portrait_Enemy_FadeIn(int i)
	{
		fadeindone = false;
		while (char2obj[i].transform.position.x > char2objnewposx + 10)
		{
			char2obj[i].transform.position = Vector3.Lerp(char2obj[i].transform.position, new Vector3(char2objnewposx, char2obj[i].transform.position.y, char2obj[i].transform.position.z), 8f * Time.deltaTime);
			yield return null;
		}
		fadeindone = true;
		yield return null;
	}

	public IEnumerator FadeOut()
	{
		//Only call this if FadeIn() has been called before and it is finished, or else good luck.
		controls.Menu.Disable();
		fadeoutdone = false;
		while (txtbox.transform.position.y > txtboxoriginalposy+10)
		{
			txtbox.transform.position = Vector3.Lerp(txtbox.transform.position, new Vector3(txtbox.transform.position.x, txtboxoriginalposy, txtbox.transform.position.z), 8f * Time.deltaTime);
			textobj.transform.position = Vector3.Lerp(textobj.transform.position, new Vector3(textobj.transform.position.x, textobjoriginalposy, textobj.transform.position.z), 8f * Time.deltaTime);
			mask1obj.transform.position = Vector3.Lerp(mask1obj.transform.position, new Vector3(mask1obj.transform.position.x, mask1objoriginalposy, mask1obj.transform.position.z), 8f * Time.deltaTime);
			mask2obj.transform.position = Vector3.Lerp(mask2obj.transform.position, new Vector3(mask2obj.transform.position.x, mask2objoriginalposy, mask2obj.transform.position.z), 8f * Time.deltaTime);
			for (int i = 0; i < char1.Length; i++)
			{
				char1obj[i].transform.position = Vector3.Lerp(char1obj[i].transform.position, new Vector3(char1objoriginalposx, char1obj[i].transform.position.y, char1obj[i].transform.position.z), 8f * Time.deltaTime);
			}
			for (int i = 0; i < char2.Length; i++)
			{
				char2obj[i].transform.position = Vector3.Lerp(char2obj[i].transform.position, new Vector3(char2objoriginalposx, char2obj[i].transform.position.y, char2obj[i].transform.position.z), 8f * Time.deltaTime);
			}
			yield return null;
		}
		fadeoutdone = true;
	}


	void updateposition()
	{
		txtboxoriginalposy = Screen.height * - 0.2f;
		txtbox.transform.position = new Vector3(txtbox.transform.position.x, txtboxoriginalposy, txtbox.transform.position.z);
		textobjoriginalposy = Screen.height * - 0.2f;
		textobj.transform.position = new Vector3(textobj.transform.position.x*1.25f, textobjoriginalposy, textobj.transform.position.z);
		mask1objoriginalposy = Screen.height * - 0.2f;
		mask1obj.transform.position = new Vector3(mask1obj.transform.position.x, mask1objoriginalposy, mask1obj.transform.position.z);
		mask2objoriginalposy = Screen.height * - 0.2f;
		mask2obj.transform.position = new Vector3(mask2obj.transform.position.x, mask2objoriginalposy, mask2obj.transform.position.z);

		for (int i = 0; i < char1.Length; i++)
		{
			char1objoriginalposx = char1obj[i].transform.position.x;
		}
		for (int i = 0; i < char2.Length; i++)
		{
			char2objoriginalposx = char2obj[i].transform.position.x;
		}
		//char2objoriginalposx = char2obj.transform.position.x;

		//Update Y
		txtboxnewposy = Screen.height * 0.22f;
		textobjnewposy = Screen.height * 0.3f;
		mask1objnewposy = Screen.height * 0.3f;
		mask2objnewposy = Screen.height * 0.3f;

		//Update X
		char1objnewposx = Screen.width * 0.2f;
		char2objnewposx = Screen.width * 0.8f;
	}

}
