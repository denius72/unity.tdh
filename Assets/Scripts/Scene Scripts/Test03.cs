using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test03 : MonoBehaviour
{

    public GameObject objToAttachSong;
    public AudioClip clip;
    public gamelogic game;
    private GameObject lCanvas;

    // Start is called before the first frame update
    void Start()
    {
        lCanvas = GameObject.Find("Canvas_Loading");;
        game = lCanvas.GetComponent<gamelogic>();
        objToAttachSong.AddComponent<AudioSource>();
        objToAttachSong.GetComponent<AudioSource>().clip = clip;
        objToAttachSong.GetComponent<AudioSource>().ignoreListenerPause=true;
        objToAttachSong.GetComponent<AudioSource>().volume = game.BGMVolume;
        objToAttachSong.GetComponent<AudioSource>().loop = true;
        objToAttachSong.GetComponent<AudioSource>().Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
