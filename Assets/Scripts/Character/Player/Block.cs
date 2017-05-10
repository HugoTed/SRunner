﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour {
    public EnemyShoot enemyShoot;
    public Transform shootPoint;
    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnCollisionEnter(Collision enemy)
    {
        if (enemy.gameObject.tag == "Shell")
        {
            GameObject newShell = Instantiate(enemyShoot.shell, shootPoint.position, shootPoint.rotation);
            Rigidbody r = newShell.GetComponent<Rigidbody>();
            r.velocity = shootPoint.forward * enemyShoot.shootPower;
        }
    }
}