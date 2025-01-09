using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test03 : MonoBehaviour
{

    public GameObject objToAttachSong;
    public AudioClip clip;
    public gamelogic game;
    public UITransition[] uiTransitions;
    public bool isOrtographicScene;
    private GameObject lCanvas;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        //find game state singleton
        lCanvas = GameObject.Find("Canvas_Loading");;
        game = lCanvas.GetComponent<gamelogic>();

        //attach scene music to obj and play
        audioSource = objToAttachSong.AddComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.ignoreListenerPause = true;
        audioSource.volume = game.BGMVolume;
        audioSource.loop = true;
        audioSource.Play();

        // makes the player walk diagonally.
        if (isOrtographicScene)
        {
            game.gamestate = gamelogic.GameState.EXPLORATION_MODE_ORTOGRAPHIC;
        }
        else //makes the player walk straight.
        {
            game.gamestate = gamelogic.GameState.EXPLORATION_MODE;
        }

        //show UI
        for (int i = 0; i < uiTransitions.Length; i++)
        {
            if (uiTransitions[i] != null)
            {
                StartCoroutine(uiTransitions[i].TransitionUI(true, 3.0f));
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        // if game paused
        if (Time.timeScale == 0f)
        {
            audioSource.Pause();
        }
        else if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }
}

