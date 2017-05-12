using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UILoad : MonoBehaviour {
    public string startscene;
    public AsyncOperation async;
    public int progress=0;
    public Text loadtext;
    public Image loadimg; 
	// Use this for initialization
	void Start () {
        startScene(startscene);
	}
	
	// Update is called once per frame
	void Update () {
        progress = (int)(async.progress * 100);
        loadtext.text = "加载"+progress + "%";
        loadimg.fillAmount = async.progress;
	}
    public void startScene(string sceneName)
    {
        StartCoroutine("LoadScene", sceneName);
    }
    IEnumerator LoadScene(string sceneName) {
        async = SceneManager.LoadSceneAsync(sceneName);
        async.allowSceneActivation = true;
        yield return new WaitForEndOfFrame();
        yield return async;
    }

}
