using System.Collections;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class UI_stg_border : MonoBehaviour
{
	public Texture2D box;
	private GameObject boxobj;
	private Sprite boxSp;
	
	private GameObject mCanvas;
	
    // Start is called before the first frame update
    void Start()
    {
        mCanvas = GameObject.Find("Canvas");;
		boxobj = new GameObject();
		boxobj.name = "STG Frame";
		
		boxobj.AddComponent<CanvasRenderer>();
		boxobj.AddComponent<RectTransform>();
		boxobj.AddComponent<Image>();
		boxobj.AddComponent<CanvasGroup>();
		
		boxSp = Sprite.Create(box, new Rect(0.0f, 0.0f, box.width, box.height), new Vector2(0.5f, 0.5f));
		
		float z = mCanvas.GetComponent<RectTransform>().position.z;
		boxobj.transform.position = new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, z);//pos
		boxobj.transform.SetParent(mCanvas.transform);
		
		RectTransform rt = boxobj.GetComponent<RectTransform>();
		rt.sizeDelta = new Vector2(box.width, box.height);

		//Render + Listener
		boxobj.GetComponent<Image>().sprite = boxSp; //Override
		CanvasGroup transp1 = boxobj.GetComponent<CanvasGroup>();
		transp1.alpha = 0.4f;
		
		boxobj.transform.localScale = new Vector3(1.0f, 1.06f, 1);
		boxobj.transform.SetAsFirstSibling();
		
		
    }

    // Update is called once per frame
    void Update()
    {
        
		//boxobj.transform.SetAsLastSibling();
    }
}
