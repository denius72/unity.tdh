using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Player_Common_Data;

public class gamelogic : MonoBehaviour
{
    //This must be a singleton, as it stores global variables for multiple systems.
    //this is created in memory by loading the starting scene.
    //By exiting to main menu, destroy Canvas_loading and AudioManager, and call the starting scene to refresh these values.
    //A checkpoint system will preserve these values temporarily in case of Retry in all gamestates besides arcade mode
    //By loading a game, will change these values below to the loaded ones.
    //There will be TWO save types: user save and system save. 
    //User save is for the progress, what level they're in and most of these variables below. 
    //System save is for saving and loading configurations like sound volume.

    //level sheet
    //1 - (4,5)
    //2 - (5,6)
    //3 - (5,6)
    //4 - (6,7)
    //5 - (7,7)
    //6 - (7,8)
    //7 - (8,8)
    //8 - (8,9)
    //9 - (9,9)

    public int player_level = 1;
    private int maxlevel = 99;

	public int player_graze = 0;

    public int player_life = 3;
    public int maxlife = 4;

    public int player_mp = 2;
    public int maxmp = 5;

    // perfect possession gauge
    public int player_transform = 0;
    public int maxtransform = 100;

    //extra lives and bombs will be converted into money at the end of a level, except during ARCADE_MODE
	public int player_money = 0;
	public int maxmoney = 99999;

    // 0 - Empty
    // 1 - Sumireko
    // 2 - Reimu
    // 3 - Marisa
    // 4 - Sanae
    // 5 - Youmu
    //This one is always sumireko
	public Player_Common_Data.PlayerState player_slot_1 = Player_Common_Data.PlayerState.SUMIREKO;
    //Variable ones
	public Player_Common_Data.PlayerState player_slot_2 = Player_Common_Data.PlayerState.EMPTY;
	public Player_Common_Data.PlayerState player_slot_3 = Player_Common_Data.PlayerState.EMPTY;
	public Player_Common_Data.PlayerState player_slot_4 = Player_Common_Data.PlayerState.EMPTY;
    
	public int bossmaxhp = 1000;
	public int bosshp = 1000;
	public int bossSpellCounter = 9;
    //public String bossname = ""
	
    public int tookdamage = 0;
    public int gainedlife = 0;
    public int exp_threshold = 100;

    public enum GameDifficulty{
        EASY,
        NORMAL,
        HARD,
        LUNATIC,
        OVERDRIVE,
        EXTRA
    }
	
	public float timer= 10.0f;

    public enum GameState
    {
        MAIN_MENU,
        STG_MODE,
        DIALOGUE_MODE,
        CHALLENGE,
        ARCADE_MODE,
        EXPLORATION_MODE
    }
	//0 = main menu
		// pause menu shoudn't open
	//1 = Story STG mode
		// pause menu options are Resume, Retry, Options, Exit
	//2 = Dialogue mode
		// pause menu options are Resume, Skip, Exit
	//3 = Challenge
		// pause menu options are Resume, Retry, Options, Exit
		// difficulty will be set to "Overdrive"
    //4 = Arcade Mode
        // pause menu options are Resume, Restart, Options, Exit
        // level will be 9, but won't show on HUD
        // progress will carry between levels, meaning health and spell won't reset to default 
        // the extra mode will use this state
        // money will be renamed as points
    //5 = Exploration mode
        // used in story mode to walk around in the 3D world.
    public GameState gamestate = GameState.MAIN_MENU;
	
	public float SFXVolume = 1.0f;
	public float BGMVolume = 1.0f;
	
	public bool playerframefadein;

    [SerializeField] private Texture2D[] images;
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private GameObject[] heartcollection;

    // Start is called before the first frame update
    void Start()
    {
        update_level();
    }

    // Update is called once per frame
    void Update()
    {

        if (tookdamage >= player_life)
        {
            //die
        }
        
        if(tookdamage > 0)
        {
            player_life-=tookdamage;
            tookdamage = 0;
        }
        else if(gainedlife > 0)
        {
            /*for (int i = player_life; i < player_life+gainedlife; i++)
            {
                GameObject heartcol = new GameObject();
                UISpritesAnimation u = heartcol.AddComponent<UISpritesAnimation>();
                u.images = images;
                u.sprites = sprites;
                u.heartnum = i;
                heartcollection[i] = heartcol;
            }*/
            player_life+=gainedlife;
            gainedlife = 0;
        }
        //yield return null;
    }

    public void update_level()
    {
        maxlife = 4 + (int)Mathf.Floor(0.6f*player_level);
        maxmp = 5 + (int)Mathf.Floor(0.5f*player_level);
    }
}
