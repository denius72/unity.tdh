using System.Collections;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
 
 public class UISpritesAnimation : MonoBehaviour
 {
     public float duration = 0.8f;
 
     [SerializeField] public Texture2D[] images;
     [SerializeField] public Sprite[] sprites;
     
     public int heartnum = 0;
     private float heartoffset = 0.0f;
     private gamelogic gl;

     private GameObject heartobj;
     private int index = 0;
     private float timer = 0;

	 private float heartobjoriginalposy = 0.0f;
	 private float heartobjnewposy = 0.0f; 

     private GameObject mCanvas;
	 float screenfactor = (Screen.width * 5) / 1920;
     //float screenfactor = Screen.width;

	private GameObject lCanvas;
	//private gamelogic game;
	
	void Start()
	{
		lCanvas = GameObject.Find("Canvas_Loading");;
		gl = lCanvas.GetComponent<gamelogic>();
        mCanvas = GameObject.Find("Canvas");;
		heartobj = new GameObject();
		
		
		Debug.Log("UISpritesAnimation : "+images.Length);
        for (int i = 0; i < images.Length; i++)
        {
			Debug.Log("UISpritesAnimation : "+i);
            sprites[i] = Sprite.Create(images[i], new Rect(0.0f, 0.0f, images[i].width, images[i].height), new Vector2(0.5f, 0.5f), 100.0f);
			Debug.Log("UISpritesAnimation : "+i);
        }

        heartobj.AddComponent<CanvasRenderer>();
		heartobj.AddComponent<RectTransform>();
		heartobj.AddComponent<Image>();
		heartobj.AddComponent<CanvasGroup>();

        //Pos + size
        for (int i = 0; i < heartnum; i++)
        {
            heartoffset += 70f;
        }
		heartobj.transform.position = new Vector3(Screen.width * 0.16f + heartoffset, Screen.height * -1.33f, mCanvas.GetComponent<RectTransform>().position.z);//pos
		heartobj.transform.SetParent(mCanvas.transform);
        
		heartobj.transform.localScale = new Vector3(0.3f, 0.3f, 1);

        //Rect do componente char1obj
		RectTransform rtc1 = heartobj.GetComponent<RectTransform>();
		rtc1.sizeDelta = new Vector2(images[0].width, images[0].height);

		//Render
		heartobj.GetComponent<Image>().sprite = sprites[0];
        heartobjnewposy = heartobj.transform.position.y + screenfactor * 300;
		heartobjoriginalposy = heartobj.transform.position.y - screenfactor;
        
		StartCoroutine(lifecheck());
     }

     private void Update()
     {
         if((timer+=Time.deltaTime) >= (duration / sprites.Length))
         {
             timer = 0;
             heartobj.GetComponent<Image>().sprite = sprites[index];
             index = (index + 1) % sprites.Length;
         }
     }
    
     IEnumerator lifecheck()
     {
		  while(true)
		  {
			  if(heartnum < gl.player_life)
			  {
				  StartCoroutine(FadeIn());
			  }
			  else if (heartnum >= gl.player_life)
			  {
				  StartCoroutine(FadeOut());
			  }
			  yield return null;
		  }
     }

    IEnumerator FadeIn()
	{
		while (heartobj.transform.position.y < heartobjnewposy - 10)
		{
			heartobj.transform.position = Vector3.Lerp(heartobj.transform.position, new Vector3(heartobj.transform.position.x, heartobjnewposy, heartobj.transform.position.z), 0.1f * Time.deltaTime);
			yield return null;
		}

	}
    
	IEnumerator FadeOut()
	{
		//Only call this if FadeIn() has been called before and it is finished, or else good luck.
		
		while (heartobj.transform.position.y > heartobjoriginalposy+10)
		{
			heartobj.transform.position = Vector3.Lerp(heartobj.transform.position, new Vector3(heartobj.transform.position.x, heartobjoriginalposy, heartobj.transform.position.z), 0.1f* Time.deltaTime);
			yield return null;
		}
	}
 }