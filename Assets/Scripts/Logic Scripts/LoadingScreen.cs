using System.Collections;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour
{
    public bool finished_loading = false;
    public bool finished_animation = false;

    public List<Texture2D> loadcharBundle1;
    public List<Texture2D> loadcharBundle2;
    public List<Texture2D> loadcharBundle3;
    private List<Texture2D> selectedBundle;
    private int currentBundleIndex = 0;
    private float cycleTime = 0.5f; // cycle every second
    private float elapsedTime = 0.0f;

    private GameObject loadcharobj;
    private Image loadcharImage;

    public Texture2D bg;
    private GameObject bgobj;
    private Sprite bgsp;

    private GameObject lCanvas;
    private gamelogic game;
    private Loadmainscene lms;
	
	private Vector3 initialLoadcharPosition;
    private float bobbingFrequency = 1.0f;
    private float bobbingAmplitude = 10.0f;

    void Start()
    {
        lCanvas = GameObject.Find("Canvas_Loading");
        game = lCanvas.GetComponent<gamelogic>();
        lms = lCanvas.GetComponent<Loadmainscene>();

        DontDestroyOnLoad(lCanvas);
        loadcharobj = new GameObject();
        loadcharobj.name = "char";
        bgobj = new GameObject();
        bgobj.name = "background";

        //----------------------------------

        // Initialize the first texture
        loadcharImage = loadcharobj.AddComponent<Image>();

        loadcharobj.AddComponent<CanvasRenderer>();
        loadcharobj.AddComponent<RectTransform>();
        loadcharobj.AddComponent<CanvasGroup>();

        // Pos + size
		initialLoadcharPosition = new Vector3(Screen.width * 0.92f, Screen.height * 0.2f, lCanvas.GetComponent<RectTransform>().position.z);
        loadcharobj.transform.position = initialLoadcharPosition; //pos
        loadcharobj.transform.SetParent(lCanvas.transform);

        // Rect do componente loadcharobj
        loadcharobj.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 0);

        // Render
        loadcharobj.GetComponent<CanvasGroup>().alpha = 0.0f;

        loadcharobj.transform.localScale = new Vector3(0.5f, 0.5f, 1);

        //----------------------------------

        // converter Texture2D para Sprite
        bgsp = Sprite.Create(bg, new Rect(0.0f, 0.0f, bg.width, bg.height), new Vector2(0.5f, 0.5f), 100.0f);
        bgobj.AddComponent<CanvasRenderer>();
        bgobj.AddComponent<RectTransform>();
        bgobj.AddComponent<Image>();
        bgobj.AddComponent<CanvasGroup>();

        // Pos + size
        bgobj.transform.position = new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, lCanvas.GetComponent<RectTransform>().position.z); //pos
        bgobj.transform.SetParent(lCanvas.transform);

        // Rect do componente bgobj
        bgobj.GetComponent<RectTransform>().sizeDelta = new Vector2(bg.width, bg.height);

        // Render
        bgobj.GetComponent<Image>().sprite = bgsp; //Override
        bgobj.GetComponent<CanvasGroup>().alpha = 0.0f;

        bgobj.transform.localScale = new Vector3(10.0f, 10.0f, 1);

        loadcharobj.transform.SetAsLastSibling();

        //----------------------------------
        lms.finished_loading = true;
        //StartCoroutine(load_canvas());
    }

    public void fadein()
    {
        // Randomly select one of the bundles
        List<Texture2D>[] bundles = new List<Texture2D>[] { loadcharBundle1, loadcharBundle2, loadcharBundle3 };
        selectedBundle = bundles[UnityEngine.Random.Range(0, bundles.Length)];
        currentBundleIndex = 0;
        elapsedTime = 0.0f;
        UpdateLoadcharSprite();

        finished_animation = false;
        loadcharobj.GetComponent<CanvasGroup>().alpha = 1.0f;
        bgobj.GetComponent<CanvasGroup>().alpha = 1.0f;
        bgobj.transform.localScale = new Vector3(10.0f, 10.0f, 1);
        //StartCoroutine(fadeInAnim());
    }

    public IEnumerator fadeInAnim()
    {
        bgobj.GetComponent<CanvasGroup>().alpha = 1.0f;
        bgobj.transform.localScale = new Vector3(0.0f, 0.0f, 1);
        float i = 0.0f;
        while (i < 10.0f)
        {
            bgobj.transform.localScale = new Vector3(i, i, 1);
            i += 20f * Time.unscaledDeltaTime;
            yield return null;
        }
    }

    public IEnumerator fadeout()
    {
        yield return new WaitForSecondsRealtime(3);
        float i = 10.0f;
        loadcharobj.GetComponent<CanvasGroup>().alpha = 0.0f;
        while (i > 0.0f)
        {
            bgobj.transform.localScale = new Vector3(i, i, 1);
            i -= 20f * Time.unscaledDeltaTime;
            //yield return new WaitForSecondsRealtime(0.01f);
            yield return null;
        }
        bgobj.GetComponent<CanvasGroup>().alpha = 0.0f;
        finished_animation = true;
    }

    public IEnumerator LoadSceneAsync(string sceneId)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneId);

        fadein();
        while (!operation.isDone)
        {
            yield return null;
        }

        fadeout();
    }

    // Update is called once per frame
    void Update()
    {
        // Update elapsed time and switch texture if necessary
        elapsedTime += Time.unscaledDeltaTime;
        if (elapsedTime >= cycleTime)
        {
            elapsedTime = 0.0f;
            currentBundleIndex = (currentBundleIndex + 1) % selectedBundle.Count;
            UpdateLoadcharSprite();
        }

		float newY = initialLoadcharPosition.y + Mathf.Sin(Time.unscaledTime * bobbingFrequency) * bobbingAmplitude;
        loadcharobj.transform.position = new Vector3(initialLoadcharPosition.x, newY, initialLoadcharPosition.z);
		
        bgobj.transform.Rotate(0.0f, 0.0f, Time.unscaledDeltaTime * 5.0f, Space.World);
    }

    void UpdateLoadcharSprite()
    {
        if (selectedBundle != null && selectedBundle.Count > 0)
        {
            Texture2D currentTexture = selectedBundle[currentBundleIndex];
            loadcharImage.sprite = Sprite.Create(currentTexture, new Rect(0.0f, 0.0f, currentTexture.width, currentTexture.height), new Vector2(0.5f, 0.5f), 100.0f);
            // Update size maintaining the aspect ratio
            float aspectRatio = (float)currentTexture.width / currentTexture.height;
            loadcharobj.GetComponent<RectTransform>().sizeDelta = new Vector2(currentTexture.width, currentTexture.height / aspectRatio);
        }
    }
}