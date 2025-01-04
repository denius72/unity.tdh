using System.Collections;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainScript : MonoBehaviour
{
	
	private GameObject fpscounter;
	private GameObject mCanvas;
	public bool finishedloading = false;
	
	public uiplayerframe playerframe;
	public Scripo textbox;
	public uiplayerscript playerscript;
	public uiclockscript clockscript;
	public uibossframe bossframe;
	public Camera canvas_camera;
	public int fps = 60;
	
    // Start is called before the first frame update
    void Start()
    {
		mCanvas = GameObject.Find("Canvas");;
		//DontDestroyOnLoad(mCanvas);
		fpscounter = new GameObject();
		fpscounter.name = "fps";
		
		fpscounter.transform.SetParent(mCanvas.transform);
		fpscounter.transform.position = new Vector3(Screen.width* 0.99f, Screen.height * 0.05f, 0.0f);//pos
																								 //textobj.transform.position = new Vector3(-300.0f, 50.0f, z2);//pos
		
		fpscounter.AddComponent<TextMeshProUGUI>();
		fpscounter.GetComponent<TextMeshProUGUI>().fontSize = 30; //Default
		fpscounter.GetComponent<TextMeshProUGUI>().lineSpacing = 60; //Default
		fpscounter.GetComponent<TextMeshProUGUI>().color = new Color32(214, 214, 214, 255); //Default
		fpscounter.GetComponent<TextMeshProUGUI>().outlineWidth = 0.3f;
		fpscounter.GetComponent<TextMeshProUGUI>().outlineColor = new Color32(118, 55, 145, 255);
		fpscounter.GetComponent<TextMeshProUGUI>().richText = true; //for HTML markup
		fpscounter.GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Left;
		
        //To change framerate, must disable vsync
        QualitySettings.vSyncCount = 1;
        //Application.targetFrameRate = 60;
		Time.fixedDeltaTime = 0.016f;

        //Screen.SetResolution(640, 480, true);
		fpscounter.transform.localScale = new Vector3(1.3f, 1.3f, 1);
		StartCoroutine(fpsupdate());
		StartCoroutine(load_canvas());
    }

	IEnumerator fpsupdate()
	{
		while(true)
		{	
			fpscounter.GetComponent<TextMeshProUGUI>().SetText((int)(1f / Time.unscaledDeltaTime)+"fps"); //Default
			yield return new WaitForSecondsRealtime(1);
		}
	}
	
		
	IEnumerator load_canvas()
	{
		double timer = Time.realtimeSinceStartup;
		
		//fadein();
		
		while(true)
		{
			//yield return new WaitForSecondsRealtime(2);
			if(textbox.finished_loading && playerscript.finished_loading && clockscript.finished_loading && bossframe.finished_loading)
			{
				Debug.Log("Finished loading canvas in "+(Time.realtimeSinceStartup - timer));
				finishedloading = true;
				//yield return new WaitForSecondsRealtime(3);
				//yield return fadeout();
				canvas_camera_insert();
				break;
			}
			yield return null;
		}
		
	}

	void canvas_camera_insert()
	{
		//uses camera as Screen
		canvas_camera.orthographicSize = Screen.height/2;
		canvas_camera.transform.position = new Vector3(mCanvas.transform.position.x,mCanvas.transform.position.y,mCanvas.transform.position.z);
		this.GetComponent<Canvas>().worldCamera = canvas_camera;
		//mCanvas.transform.position = new Vector3(canvas_camera.transform.position.x, canvas_camera.transform.position.y, canvas_camera.transform.position.z);
	}
	
    // Update is called once per frame
    void Update()
    {
        //Application.targetFrameRate = fps;

    }
}
