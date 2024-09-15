using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;

public class SampleScript : MonoBehaviour
{
	
	// Canvas Scripts
	public MainScript mainscr;
	//public uiplayerframe playerframe;
	public Scripo textbox;
	public uiplayerscript playerscript;
	public uiclockscript clockscript;
	public uibossframe bossframe;
	public GameObject mCanvas;
	public GameObject bulletparent;

	// Controls 
	public MasterControls controls;

	// Objects that survive load
	public LoadingScreen load;
	public gamelogic game;

	// Scene Camera
	public Camera camera; 

	// Post-processing (BACKGROUND)
	public PostProcessVolume postProcessVolume;

	// STG objects in the scene
	public GameObject stg_box;
	public GameObject stg_background;
	public GameObject directional_light;
	public GameObject enemy_boss_01;

	//background elements to repeat
	public GameObject left_wall;
	public GameObject right_wall;

	//used to control a transition in the electric heritage sample (slow it down)
	public bool in_script_transition_1 = false;
	
	private GameObject lCanvas;
	
    // Start is called before the first frame update
    void Start()
    {
		lCanvas = GameObject.Find("Canvas_Loading");;
		load = lCanvas.GetComponent<LoadingScreen>();
		game = lCanvas.GetComponent<gamelogic>();
		mCanvas = GameObject.Find("Canvas");
		directional_light = GameObject.Find("Directional Light");
		StartCoroutine(samplescript());
    }

	IEnumerator samplescript() //sequential script of STG Scene, calls events and actors with timings
	{
		//Must load all stuff
		//yield return new WaitForSecondsRealtime(5);
		while(mainscr == null)
			yield return new WaitForFixedUpdate();
			
		while(!mainscr.finishedloading)
			yield return new WaitForFixedUpdate();
		
		//yield return new WaitForSeconds(3);
		
		playerscript.fadein = false;
		clockscript.fadein = false;
		StopCoroutine(clockscript.countdown());
		bossframe.fadein = false;
		
		//InputAction.CallbackContext context;
		//Will listen to pause and control the text;
		controls = new MasterControls();
		controls.Menu.Enable();

		//for the cool fog effect
		RenderSettings.fogDensity = 0.1f;

		// REMOVE THIS COMMENT TO HAVE THE THING PLAY
		/* 
		yield return new WaitForSeconds(10);
		*/

		//Render the player related stuff in UI
		playerscript.startfadecontrol = true;
		playerscript.fadein = true;
		
		StartCoroutine(stg_background_control(26f));
		StartCoroutine(stg_background_transition_01());
		StartCoroutine(stg_background_transition_01_b());
		
		// REMOVE THIS COMMENT TO HAVE THE THING PLAY
		/*
		yield return new WaitForSeconds(26);
		*/

		in_script_transition_1 = true;
		
		// REMOVE THIS COMMENT TO HAVE THE THING PLAY
		
		//Hide the UI for the dialogue
		playerscript.startfadecontrol = true;
		playerscript.fadein = false;
		
		StartCoroutine(stg_enemy_boss_movement_01());
		enemy_boss_01.gameObject.GetComponent<enemy_health>().hp = 99999;
		game.bossmaxhp = enemy_boss_01.gameObject.GetComponent<enemy_health>().hp;
		// REMOVE THIS COMMENT TO HAVE THE THING PLAY
		
		yield return textbox.FadeInWithTxt("<b><sp=0.001>???</b><size=30><sp=0.002>\nHalt!");
		yield return textwaithandler();
		yield return StartCoroutine(textbox.char2_control(0,true));
		yield return textbox.FadeInWithTxt("<b><color=#D3BE55><sp=0.001>Mayumi</color></b><size=30><sp=0.002>\nThis is as far as you go. Turn back at once and I might spare you.");
		yield return textwaithandler();
		yield return StartCoroutine(textbox.char1_control(0,true));
		yield return textbox.FadeInWithTxt("<b><color=#BA55D3><sp=0.001>Sumireko</color></b><size=30><sp=0.002>\nMove, please.");
		yield return textwaithandler();
		yield return textbox.FadeInWithTxt("<b><color=#D3BE55><sp=0.001>Mayumi</color></b><size=30><sp=0.002>\nTo think that you're so stubborn as well.");
		yield return textwaithandler();
		yield return textbox.FadeInWithTxt("<b><color=#D3BE55><sp=0.001>Mayumi</color></b><size=30><sp=0.002>\nAs a being entrusted with life, I must fulfill my duties!");
		yield return textwaithandler();
		yield return textbox.FadeInWithTxt("<b><color=#BA55D3><sp=0.001>Sumireko</color></b><size=30><sp=0.002>\nSigh...");
		yield return textwaithandler();
		yield return textbox.FadeInWithTxt("<b><color=#BA55D3><sp=0.001>Sumireko</color></b><size=30><sp=0.002>\nIt can't be helped.");
		yield return textwaithandler();
		yield return textbox.FadeOut();
		
		playerscript.startfadecontrol = true;
		playerscript.fadein = true;
		

		clockscript.startfadecontrol = true;
		clockscript.fadein = true;
		clockscript.time = 999;
		StartCoroutine(clockscript.countdown());
		bossframe.startfadecontrol = true;
		bossframe.fadein = true;
		StartCoroutine(stg_enemy_boss_shot_01());

		while (clockscript.time > 0)
		{
			yield return new WaitForFixedUpdate();
		}
		
		//playerframe.startfadecontrol = true;
		//playerframe.fadein = false;
		playerscript.startfadecontrol = true;
		playerscript.fadein = false;
		clockscript.startfadecontrol = true;
		clockscript.fadein = false;
		StartCoroutine(clockscript.countdown());
		bossframe.startfadecontrol = true;
		bossframe.fadein = false;
		
		//USE THIS TO DISABLE STUFF
		//mCanvas.GetComponent<uiplayerframe>().enabled = false;
		
		yield return textbox.FadeInWithTxt("<b><color=#BA55D3><sp=0.01>Sumireko</color></b><size=30><sp=0.05>\nTime's up.");
		yield return textwaithandler();	
		yield return textbox.FadeOut();
		
		yield return new WaitForSecondsRealtime(1);
		StartCoroutine(load.LoadSceneAsync("Test01"));
		
		yield return new WaitForFixedUpdate();
	}

	IEnumerator stg_background_control(float first_timer)
	{
		//float timer = first_timer;
		//Material left_wall_material = left_wall.GetComponent<Renderer>().material;
		//Material right_wall_material = right_wall.GetComponent<Renderer>().material;
		float speed = 5.0f; 
		float offset = 0.0f;
		//float repeat_threshold = 15.5f;
		float camera_threshold = 16.7f;
		float camera_last_pos = 0.0f;
		float camera_new_pos = 0.0f;
		int last_repeat = 0;
		
		Bloom bloom;
		ColorGrading colorgrading;
		postProcessVolume.profile.TryGetSettings(out bloom);
		postProcessVolume.profile.TryGetSettings(out colorgrading);
		//StartCoroutine(stg_background_transition_01());
		while(true)
		{

			offset+= (Time.deltaTime*speed)/10.0f;
			left_wall.GetComponent<MeshRenderer>().material.mainTextureOffset = new Vector2(-offset,0);
			right_wall.GetComponent<MeshRenderer>().material.mainTextureOffset = new Vector2(offset,0);

			bloom.intensity.value = Mathf.Sin(Time.time)*4f;
			//colorgrading.red.value = Mathf.Cos(Time.time)*2.5f;
			colorgrading.mixerRedOutRedIn.value = Mathf.Cos(Time.time)*150f;
        	left_wall.transform.position = new Vector3(left_wall.transform.position.x + Mathf.Sin(offset)*0.2f*speed*Time.deltaTime, left_wall.transform.position.y, left_wall.transform.position.z );
        	right_wall.transform.position = new Vector3(right_wall.transform.position.x + Mathf.Sin(offset)*0.2f*speed*Time.deltaTime, right_wall.transform.position.y, right_wall.transform.position.z );

			if (in_script_transition_1 && speed > 2.0f)
				speed -= 0.1f;
			else if(!in_script_transition_1)
				stg_background.transform.rotation = Quaternion.Euler(new Vector3(0,Mathf.Sin(offset)*200f*Time.fixedDeltaTime,0));

			//b.intensity.value = Mathf.Sin(offset);
			//bloom control
			yield return new WaitForFixedUpdate();
		}
	}

	IEnumerator stg_enemy_boss_shot_01()
	{
		while(true)
		{
			if (Time.timeScale != 0)
    		{
				GameObject shot01Prefab = Resources.Load("shot_small_circle_01") as GameObject;
				GameObject player = GameObject.FindWithTag("Player");

				if (shot01Prefab != null && player != null)
				{
					GameObject shot01 = Instantiate(shot01Prefab, enemy_boss_01.gameObject.transform.position, Quaternion.identity);
					shot01.transform.SetParent(bulletparent.transform, true); // true is for maintain world position
					shot01.AddComponent<Rigidbody>();
					shot01.GetComponent<Rigidbody>().isKinematic = true;

					StartCoroutine(stg_enemy_boss_shot_01_control(shot01, player));
				}
				yield return new WaitForSecondsRealtime(0.1f);
			}
			yield return null;
		}
	}

	IEnumerator stg_enemy_boss_shot_01_control(GameObject bullet, GameObject player)
	{
		Vector3 direction = new Vector3(0f,0f,0f);
		bool bullet_get_player_angle = false;

		if(bullet != null && player != null)
			direction = (player.transform.position - bullet.transform.position).normalized;
		while(bullet != null && player != null)
		{
			//bullet.transform.position = Vector3.Lerp(bullet.transform.position, new Vector3(bullet.transform.position.x, bullet.transform.position.y-5, bullet.transform.position.z), Time.fixedDeltaTime);

			float speed = 5.0f;
			if(!bullet_get_player_angle)
			{
				direction = (player.transform.position - bullet.transform.position).normalized;
				bullet.transform.rotation = Quaternion.Euler(0f, 0f, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
				bullet_get_player_angle = true;
			}

			Vector3 newPosition = Vector3.Lerp(bullet.transform.position, bullet.transform.position + direction, speed * Time.fixedDeltaTime);
			bullet.GetComponent<Rigidbody>().MovePosition(newPosition);
			yield return new WaitForFixedUpdate();
		}
	}

	IEnumerator stg_enemy_boss_movement_01()
	{
		while(enemy_boss_01.transform.position.x < -0.1f)
		{
			enemy_boss_01.transform.position = Vector3.Lerp(enemy_boss_01.transform.position, new Vector3(0f, enemy_boss_01.transform.position.y, enemy_boss_01.transform.position.z), 3.0f*Time.deltaTime);
			yield return new WaitForFixedUpdate();
		}
	}

	IEnumerator stg_background_transition_01()
	{
		//x = 190.846 //y = 165.545 //z = -135.431
		//x = 0 to -10
		float i = 0.1f;
		while (i > 0.009f)
		{
			RenderSettings.fogDensity = i;
			i -= 0.05f * Time.deltaTime;
			yield return new WaitForFixedUpdate();
		}
	}

	IEnumerator stg_background_transition_01_b()
	{
		//x = 190.846 //y = 165.545 //z = -135.431
		//x = 0 to -10
		float i = -20f;
		while (i < -11f)
		{
			directional_light.transform.rotation = Quaternion.Euler(i, 0, 0); 
			i += 4.0f * Time.deltaTime;
			//Debug.Log(directional_light.transform.rotation.x);
			yield return new WaitForFixedUpdate();
		}
	}
	
	IEnumerator textwaithandler()
	{
		while (!textbox.isReady)
			yield return new WaitForFixedUpdate();
		textbox.transpprompt.alpha = 0.7f;
		yield return new WaitForSecondsRealtime(0.1f);
		while (!(controls.Menu.Confirm.triggered))
			yield return new WaitForFixedUpdate();
		textbox.transpprompt.alpha = 0.0f;
		textbox.txtkeypressed = false;
		textbox.drawall = false;
	}
	
    // Update is called once per frame
    void Update()
    {
        //camera.transform.position = new Vector3(stg_box.transform.position.x, stg_box.transform.position.y, stg_box.transform.position.z);
    }
}
