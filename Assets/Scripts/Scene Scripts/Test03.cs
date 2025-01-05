using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test03 : MonoBehaviour
{

    public GameObject objToAttachSong;
    public AudioClip clip;
    public gamelogic game;
    private GameObject lCanvas;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        lCanvas = GameObject.Find("Canvas_Loading");;
        game = lCanvas.GetComponent<gamelogic>();
        audioSource = objToAttachSong.AddComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.ignoreListenerPause = true;
        audioSource.volume = game.BGMVolume;
        audioSource.loop = true;
        audioSource.Play();
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

