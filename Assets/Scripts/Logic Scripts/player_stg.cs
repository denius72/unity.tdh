using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static Player_Common_Data;

public class player_stg : MonoBehaviour
{
	public Player_Common_Data.PlayerState playerstate;
	public Player_Common_Data.PlayerState player1;
	public Player_Common_Data.PlayerState player2;
	public Player_Common_Data.PlayerState player3;
	public Player_Common_Data.PlayerState player4;

	//this is gonna get messy boys
	public GameObject sumireko_shot_prefab_1;
	private ObjectPool sumireko_shot_pool_1;

	/*
	//get shot prefab for player
	public GameObject[] player1_shot_prefab;
	private ObjectPool[] player1_shot_pool;
	public Player_Common_Data.PlayerState player1;

	public GameObject[] player2_shot_prefab;
	private ObjectPool[] player2_shot_pool;
	public Player_Common_Data.PlayerState player2;

	public GameObject[] player3_shot_prefab;
	private ObjectPool[] player3_shot_pool;
	public Player_Common_Data.PlayerState player3;

	public GameObject[] player4_shot_prefab;
	private ObjectPool[] player4_shot_pool;
	public Player_Common_Data.PlayerState player4;
	
	public Player_Common_Data.PlayerState selected_player;
	private GameObject[] selected_player_shot_prefab;
	private ObjectPool[] selected_player_shot_pool;*/

    private GameObject mCanvas;
	private GameObject lCanvas;
	private GameObject maincam;
	private SphereCollider collider;
	public GameObject playerobj;
	public GameObject player_visual_hitbox_obj;
	public GameObject player_visual_hitbox_obj2;
	public GameObject player_visual_hitbox_obj3;
	public GameObject stg_box;
	public MasterControls controls;
	private gamelogic game;
	private bool focus_performed = false;
	public uipausescript pausescript;

	//sumireko speeds
	public float player_speed = 0.0f;
	public float player_focus = 0.0f;
	public float player_shoot_cooldown = 0.08f;
	private float player_shoot_lastActionTime;

	public long time_since_stg = 0;
	public float floating_time_since_stg = 0;

	//TO DO
	//Equipped player specifics:
	//including: player speed, passive skills or active skills, player specific bullet types for focused and unfocused, bombs
	
	Vector2 movement = Vector2.zero;

    // Start is called before the first frame update
    void Start()
    {

        lCanvas = GameObject.Find("Canvas_Loading");;
		game = lCanvas.GetComponent<gamelogic>();
		mCanvas = GameObject.Find("Canvas");
		maincam = GameObject.Find("Main Camera");
		collider = playerobj.GetComponent<SphereCollider>();
		controls = new MasterControls();
		controls.Player.Enable();

		player_speed = player_set_speed(player1);
		player_focus = player_set_focus_speed(player1);
		
		player_shoot_cooldown = 0.08f;
		focus_performed = false;
		player_visual_hitbox_obj.GetComponent<MeshRenderer>().enabled = false;
		player_visual_hitbox_obj2.GetComponent<MeshRenderer>().enabled = false;
		player_visual_hitbox_obj3.GetComponent<MeshRenderer>().enabled = false;
		player1 = game.player_slot_1;
		player2 = game.player_slot_2;
		player3 = game.player_slot_3;
		player4 = game.player_slot_4;
		playerstate = player1;
		playerobj.AddComponent<Rigidbody>();
		playerobj.GetComponent<Rigidbody>().isKinematic = true;
		
		Instantiate_pools();

		/*
	    ObjectPool[] player1_shot_pool = new ObjectPool[player1_shot_prefab.Length];
		for(int i = 0; i < player1_shot_prefab.Length; i++)
		{
			GameObject player1_shot_pool_container = new GameObject("player1_shot_pool");
			player1_shot_pool[i] = player1_shot_pool_container.AddComponent<ObjectPool>();
			player1_shot_pool[i].prefab = player1_shot_prefab[i];
			player1_shot_pool[i].poolSize = 40;
		}

		ObjectPool[] player2_shot_pool = new ObjectPool[player2_shot_prefab.Length];
		for(int i = 0; i < player2_shot_prefab.Length; i++)
		{
			GameObject player2_shot_pool_container = new GameObject("player2_shot_pool");
			player2_shot_pool[i] = player2_shot_pool_container.AddComponent<ObjectPool>();
			player2_shot_pool[i].prefab = player2_shot_prefab[i];
			player2_shot_pool[i].poolSize = 40;
		}

		ObjectPool[] player3_shot_pool = new ObjectPool[player3_shot_prefab.Length];
		for(int i = 0; i < player3_shot_prefab.Length; i++)
		{
			GameObject player3_shot_pool_container = new GameObject("player3_shot_pool");
			player3_shot_pool[i] = player3_shot_pool_container.AddComponent<ObjectPool>();
			player3_shot_pool[i].prefab = player3_shot_prefab[i];
			player3_shot_pool[i].poolSize = 40;
		}

		ObjectPool[] player4_shot_pool = new ObjectPool[player4_shot_prefab.Length];
		for(int i = 0; i < player4_shot_prefab.Length; i++)
		{
			GameObject player4_shot_pool_container = new GameObject("player4_shot_pool");
			player4_shot_pool[i] = player4_shot_pool_container.AddComponent<ObjectPool>();
			player4_shot_pool[i].prefab = player4_shot_prefab[i];
			player4_shot_pool[i].poolSize = 40;
		}
		selected_player_shot_prefab = player1_shot_prefab;
		selected_player_shot_pool = player1_shot_pool;
		*/
    }

	void Update()
	{
		if(controls.Player.Shoot.ReadValue<float>() == 1.0f && !pausescript.isPaused())
		{
			if (Time.realtimeSinceStartup - player_shoot_lastActionTime >= player_shoot_cooldown)
			{
				switch(focus_performed)
				{
					case true:
						player_focused_bullet_subcall(playerstate);
						break;
					case false:
						player_unfocused_bullet_subcall(playerstate);
						break;
				}
				Debug.Log(Time.realtimeSinceStartup - player_shoot_lastActionTime);
				player_shoot_lastActionTime = Time.realtimeSinceStartup;
			}
		}
	}
	
    // Update is called once per frame
    void FixedUpdate()
    {
		time_since_stg += 1;
		floating_time_since_stg += Time.fixedDeltaTime;
		movement = controls.Player.Movement.ReadValue<Vector2>();

		//playerobj.transform.position = Vector3.Lerp(playerobj.transform.position, new Vector3(playerobj.transform.position.x + movement.x * player_speed, playerobj.transform.position.y + movement.y * player_speed, playerobj.transform.position.z), Time.fixedDeltaTime);

		Vector3 newPosition = Vector3.Lerp(playerobj.transform.position, new Vector3(playerobj.transform.position.x + movement.x * player_speed, playerobj.transform.position.y + movement.y * player_speed, playerobj.transform.position.z), Time.fixedDeltaTime);

		newPosition.x = Mathf.Clamp(newPosition.x, playerobj.transform.parent.transform.position.x - 5.15f, playerobj.transform.parent.transform.position.x + 5.15f);
		newPosition.y = Mathf.Clamp(newPosition.y, playerobj.transform.parent.transform.position.y - 5.45f, playerobj.transform.parent.transform.position.y + 5.45f);

		playerobj.GetComponent<Rigidbody>().MovePosition(newPosition);

		//controls.Player.Focus.performed += ctx  => player_speed = 3.0f;
		controls.Player.Focus.performed += ctx  => focus_performed = true;
		//controls.Player.Focus.canceled += ctx  => player_speed = 7.0f;
		controls.Player.Focus.canceled += ctx  => focus_performed = false;
		player_visual_hitbox_obj2.transform.rotation = Quaternion.Euler(new Vector3(time_since_stg*Time.fixedDeltaTime*100.0f,-90,90));
		player_visual_hitbox_obj3.transform.rotation = Quaternion.Euler(new Vector3(-time_since_stg*Time.fixedDeltaTime*100.0f,-90,90));
		//Debug.Log("Movement: "+movement);
		/*
		if (player_shoot_cooldown > 0.0f)
			player_shoot_cooldown -= 20.0f * Time.deltaTime ;
		else if(player_shoot_cooldown <= 0.0f)
		{
			//controls.Player.Shoot.performed += ctx => StartCoroutine(player_shoot_bullet());
			if(controls.Player.Shoot.ReadValue<float>() == 1.0f && focus_performed)
				StartCoroutine(player_focused_bullet_subcall(playerstate));
			else if (controls.Player.Shoot.ReadValue<float>() == 1.0f && !focus_performed)
				StartCoroutine(player_unfocused_bullet_subcall(playerstate));
			player_shoot_cooldown = 1.0f;
		}
		*/

		//Debug.Log(20.0f * Time.deltaTime);

		//controls.Player.Shoot.performed += ctx => Debug.Log(Time.time+" shoot pressed");
		//Debug.Log(player_shoot_cooldown);
		if(focus_performed && !pausescript.isPaused())
		{
			player_visual_hitbox_obj.GetComponent<MeshRenderer>().enabled = true;
			player_visual_hitbox_obj2.GetComponent<MeshRenderer>().enabled = true;
			player_visual_hitbox_obj3.GetComponent<MeshRenderer>().enabled = true;
			player_speed = player_set_focus_speed(playerstate);
		}
		else
		{
			player_visual_hitbox_obj.GetComponent<MeshRenderer>().enabled = false;
			player_visual_hitbox_obj2.GetComponent<MeshRenderer>().enabled = false;
			player_visual_hitbox_obj3.GetComponent<MeshRenderer>().enabled = false;
			player_speed = player_set_speed(playerstate);
		}
    }

	void player_unfocused_bullet_subcall(Player_Common_Data.PlayerState playerstate)
	{
		switch (playerstate){
			case Player_Common_Data.PlayerState.SUMIREKO: StartCoroutine(player_sumireko_shoot_bullet_unfocused(0,0, sumireko_shot_pool_1)); break;
			case Player_Common_Data.PlayerState.REIMU: break;
			case Player_Common_Data.PlayerState.MARISA: break;
			case Player_Common_Data.PlayerState.SANAE: break;
			case Player_Common_Data.PlayerState.YOUMU: break;
			default: break;
		}
	}

	void player_focused_bullet_subcall(Player_Common_Data.PlayerState playerstate)
	{
		switch (playerstate){
			case Player_Common_Data.PlayerState.SUMIREKO: StartCoroutine(player_sumireko_shoot_bullet_focused(0,0, sumireko_shot_pool_1)); break;
			case Player_Common_Data.PlayerState.REIMU: break;
			case Player_Common_Data.PlayerState.MARISA: break;
			case Player_Common_Data.PlayerState.SANAE: break;
			case Player_Common_Data.PlayerState.YOUMU: break;
			default: break;
		}
	}

	float player_set_speed(Player_Common_Data.PlayerState playerstate)
	{
		switch (playerstate){
			case Player_Common_Data.PlayerState.SUMIREKO: return 7.0f;
			case Player_Common_Data.PlayerState.REIMU: return 7.0f;
			case Player_Common_Data.PlayerState.MARISA: return 9.0f;
			case Player_Common_Data.PlayerState.SANAE: return 6.0f;
			case Player_Common_Data.PlayerState.YOUMU: return 8.0f;
			default: return 7.0f;
		}
	}

	float player_set_focus_speed(Player_Common_Data.PlayerState playerstate)
	{
		switch (playerstate){
			case Player_Common_Data.PlayerState.SUMIREKO: return 3.0f;
			case Player_Common_Data.PlayerState.REIMU: return 3.5f;
			case Player_Common_Data.PlayerState.MARISA: return 4.0f;
			case Player_Common_Data.PlayerState.SANAE: return 3.0f;
			case Player_Common_Data.PlayerState.YOUMU: return 4.0f;
			default: return 3.5f;
		}
	}
	
	IEnumerator player_sumireko_shoot_bullet_unfocused(int nest, float deviation, ObjectPool pool)
	{
		if(nest == 0)
		{
			StartCoroutine(player_sumireko_shoot_bullet_unfocused(nest+1, -2, pool));
			StartCoroutine(player_sumireko_shoot_bullet_unfocused(nest+1, 2, pool));
		}

		//GameObject bullet = Instantiate(player1_shot_prefab, playerobj.transform.position, Quaternion.identity);
		GameObject bullet = pool.GetObjectFromPool();
		//bullet.GetComponent<player_sumireko_bullet_1>().bulletPool = pool;
		bullet.transform.position = playerobj.transform.position;
		bullet.transform.rotation = Quaternion.identity;
		bullet.SetActive(true);
		float elapsedTime = 0f;
		float destroyDelay = 3f;

		//bullet = Instantiate(player1_shot_prefab, playerobj.transform.position, Quaternion.identity);
		while(bullet.activeInHierarchy) //All logic regarding 'ReturnObjectToPool' must be done ONLY in this loop, or else things will break bruh moment.
		{
			elapsedTime += Time.fixedDeltaTime;
			//bullet.transform.position = Vector3.Lerp(bullet.transform.position, new Vector3(bullet.transform.position.x + deviation, bullet.transform.position.y+10, bullet.transform.position.z), Time.fixedDeltaTime);
			
			Vector3 newPosition = Vector3.Lerp(bullet.transform.position, new Vector3(bullet.transform.position.x + deviation, bullet.transform.position.y+10, bullet.transform.position.z), Time.fixedDeltaTime);
			bullet.GetComponent<Rigidbody>().MovePosition(newPosition);
			//if (bullet.transform.position.x < -6f || bullet.transform.position.x > 6f || bullet.transform.position.y < -6f || bullet.transform.position.y > 6f)
			if (elapsedTime >= destroyDelay)
			{
				//Destroy(bullet);
				//bullet.SetActive(false);
				pool.ReturnObjectToPool(bullet);
				break;
			}
			else if(bullet.GetComponent<player_sumireko_bullet_1>().hit_target)
			{
				bullet.GetComponent<player_sumireko_bullet_1>().hit_target = false; //setting to false so it can shoot again
				pool.ReturnObjectToPool(bullet);
				break;
			}

			yield return new WaitForFixedUpdate();
		}
		pool.ReturnObjectToPool(bullet);
		yield return new WaitForFixedUpdate();
	}

	IEnumerator player_sumireko_shoot_bullet_focused(int nest, float deviation, ObjectPool pool)
	{
		if(nest == 0)
		{
			StartCoroutine(player_sumireko_shoot_bullet_unfocused(nest+1, -1, pool));
			StartCoroutine(player_sumireko_shoot_bullet_unfocused(nest+1, 1, pool));
		}
		
		//GameObject bullet = Instantiate(player1_shot_prefab, playerobj.transform.position, Quaternion.identity);
		GameObject bullet = pool.GetObjectFromPool();
		//bullet.GetComponent<player_sumireko_bullet_1>().bulletPool = pool;
		bullet.transform.position = playerobj.transform.position;
		bullet.transform.rotation = Quaternion.identity;
		bullet.SetActive(true);
		float elapsedTime = 0f;
		float destroyDelay = 3f;
		//bullet.SetActive(true);
		//bullet = Instantiate(player1_shot_prefab, playerobj.transform.position, Quaternion.identity);
		while(bullet.activeInHierarchy) //All logic regarding 'ReturnObjectToPool' must be done ONLY in this loop, or else things will break bruh moment.
		{
			elapsedTime += Time.fixedDeltaTime;
			//bullet.transform.position = Vector3.Lerp(bullet.transform.position, new Vector3(bullet.transform.position.x + deviation, bullet.transform.position.y+10, bullet.transform.position.z), Time.fixedDeltaTime);
			
			Vector3 newPosition = Vector3.Lerp(bullet.transform.position, new Vector3(bullet.transform.position.x + deviation, bullet.transform.position.y+10, bullet.transform.position.z), Time.fixedDeltaTime);
			bullet.GetComponent<Rigidbody>().MovePosition(newPosition);

			//if (bullet.transform.position.x < -6f || bullet.transform.position.x > 6f || bullet.transform.position.y < -6f || bullet.transform.position.y > 6f)
			if (elapsedTime >= destroyDelay)
			{
				pool.ReturnObjectToPool(bullet);
				break;
			}
			else if(bullet.GetComponent<player_sumireko_bullet_1>().hit_target)
			{
				bullet.GetComponent<player_sumireko_bullet_1>().hit_target = false; //setting to false so it can shoot again
				pool.ReturnObjectToPool(bullet);
				break;
			}

			yield return new WaitForFixedUpdate();
		}
		pool.ReturnObjectToPool(bullet);
		yield return new WaitForFixedUpdate();
	}

	void Instantiate_pools()
	{
		GameObject sumireko_shot_pool_1_container = new GameObject("sumireko_shot_pool_1");
		sumireko_shot_pool_1_container.transform.SetParent(stg_box.transform);
		sumireko_shot_pool_1 = sumireko_shot_pool_1_container.AddComponent<ObjectPool>();
		sumireko_shot_pool_1.prefab = sumireko_shot_prefab_1;
		sumireko_shot_pool_1.poolSize = 40;
	}

	public void DeathLogic()
	{
		Debug.Log("Touched Enemy");
		this.GetComponent<MeshRenderer>().enabled = false;
		this.GetComponent<SphereCollider>().enabled = false;
		controls.Player.Disable();
		//disable player controls (maybe not menu controls?)
		//play death animation
		//play res animation and re-enable player controls if player can still play
	}
}
