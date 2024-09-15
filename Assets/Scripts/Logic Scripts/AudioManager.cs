using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
	
	public AudioSource audio_src1;
	
	void Start()
	{
		DontDestroyOnLoad(this.gameObject);
	}

    public void playSound1()
	{
		audio_src1.Play();
	}
}
