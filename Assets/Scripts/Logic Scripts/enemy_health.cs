using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_health : MonoBehaviour
{
	public gamelogic game;
    public int hp;
    public bool isBoss = false;
	private GameObject lCanvas;

    // Start is called before the first frame update
    void Start()
    {
        lCanvas = GameObject.Find("Canvas_Loading");;
		game = lCanvas.GetComponent<gamelogic>();

        if(isBoss)
            game.bossmaxhp = game.bosshp = hp;
    }

    // Update is called once per frame
    void Update()
    {
        game.bosshp = hp;
        if(hp <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
