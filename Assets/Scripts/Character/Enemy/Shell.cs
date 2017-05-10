using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnCollisionEnter(Collision enemy)
    {
        //碰到护盾直接销毁
        if (enemy.gameObject.tag == "Block")
        {
            Destroy(gameObject);
        }
        Destroy(gameObject, 5f);
    }
}
