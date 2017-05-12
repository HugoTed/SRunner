using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class reload_Scenes : MonoBehaviour {
    public string levelname;
    public string lastname;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(levelname);
            //Application.LoadLevel(levelname);
        }
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            SceneManager.LoadScene(lastname);
            Cursor.visible = true;//鼠标
            //Application.LoadLevel(lastname);
        }
    }
}
