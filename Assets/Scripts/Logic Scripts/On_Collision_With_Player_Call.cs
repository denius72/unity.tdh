using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class On_Collision_With_Player_Call : MonoBehaviour
{
    public int wallid = 0;
    public player_stg player;

    void OnTriggerEnter(Collider other) {
        if(other.gameObject.name == "Player")  {
            //Debug.Log("Tocou o player");
            //player.WallCollideCancel(wallid);
        }
    }

    void OnTriggerExit(Collider other){
        if(other.gameObject.name == "Player")  {
            //Debug.Log("Tocou o player");
            //player.WallCollideContinue(wallid);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
