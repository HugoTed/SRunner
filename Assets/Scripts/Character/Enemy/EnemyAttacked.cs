using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttacked : MonoBehaviour {

    public AIEnemy aiEnemy;
    public player player;
    // Use this for initialization
    void Start () {
        aiEnemy = gameObject.GetComponent<AIEnemy>();

	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnTriggerEnter(Collider enemy)
    {
        if(enemy.tag=="Player")
        {
            aiEnemy.getDamage(player.damage);
            //Destroy(this.gameObject);
        }
    }
}
