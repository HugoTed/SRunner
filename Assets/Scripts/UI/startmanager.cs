using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class startmanager : MonoBehaviour {

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void onstartgame(string startscene) {
        SceneManager.LoadScene(startscene);
    }
    public void onsavegame(string savescene)
    {
        SceneManager.LoadScene(savescene);
    }
    public void onoptiongame(string optionscene)
    {
        SceneManager.LoadScene(optionscene);
    }
    public void onquitgame()
    {
        Application.Quit();
    }
}
