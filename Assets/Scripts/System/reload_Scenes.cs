using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class reload_Scenes : MonoBehaviour {
    public string levelname = "demo1";
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.LoadLevel(levelname);
        }
		
	}
}
