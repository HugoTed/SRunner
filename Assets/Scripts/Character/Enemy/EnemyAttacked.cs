using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttacked : MonoBehaviour {

    public UIMAINSCORE uiscore;
    public AIController aiEnemy;
    public player player;
    // Use this for initialization
    void Start () {
        aiEnemy = gameObject.GetComponent<AIController>();

	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnTriggerEnter(Collider enemy)
    {
        if(enemy.tag=="Player")
        {

            //aiEnemy.ApplyDamage(player.damage);
            //aiEnemy.SendMessage("ApplyDamage", player.damage);
            uiscore.addscore(100);
            Debug.Log("00000000000000000000000000000");
            Destroy(this.gameObject);
        }
    }
}
