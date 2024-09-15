using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_sumireko_bullet_1 : MonoBehaviour
{
    //public ObjectPool bulletPool;
    public bool hit_target = false;

    void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Enemy"))  {
            //Debug.Log("Touched Enemy");
            other.gameObject.GetComponent<enemy_health>().hp -= 5;
            //bulletPool.ReturnObjectToPool(this.gameObject);
            hit_target = true;
        }
    }
}
