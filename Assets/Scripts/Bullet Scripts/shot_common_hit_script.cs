using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shot_common_hit_script : MonoBehaviour
{
    //public ObjectPool bulletPool;
    //public bool hit_target = false;

    void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player"))  {
            //Debug.Log("Touched Enemy");
            //other.gameObject.GetComponent<enemy_health>().hp -= 5;
            //other.gameObject.transform.parent.gameObject.SetActive(false);
            //other.gameObject.gameObject.SetActive(false); //do not deactivate player from scene like this or else player_stg script will freeze. That would be kinda cringe.
            other.gameObject.GetComponent<player_stg>().DeathLogic(); //do this instead.
            Destroy(this.gameObject); //TO DO! - deathlogic for bullet (fade animation + return to pool or whatever)
            //bulletPool.ReturnObjectToPool(this.gameObject);
            //hit_target = true;
        }
    }
    void FixedUpdate()
    {
    	if (this.gameObject.transform.position.x < this.gameObject.transform.parent.transform.position.x - 15f)
			Destroy(this.gameObject);
		if (this.gameObject.transform.position.x > this.gameObject.transform.parent.transform.position.x + 15f)
			Destroy(this.gameObject);
		if (this.gameObject.transform.position.y < this.gameObject.transform.parent.transform.position.y - 8f) 
			Destroy(this.gameObject);
		if (this.gameObject.transform.position.y > this.gameObject.transform.parent.transform.position.y + 8f)
			Destroy(this.gameObject);
    }

}
