using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIoption : MonoBehaviour {

    public string lastscene;

    public Toggle toggleMusic;
    public Toggle toggleFullScreen;
    public Toggle toggleFPS;
    public Slider bgMusic;
    public Slider txMusic;
    public Slider tsMusic;

    public bool TogMusic;
    public bool TogFullScreen;
    public bool TogFPS;
    public float BGMusic;
    public float TXMusic;
    public float TSMusic;

    void Awake() {
        if(!PlayerPrefs.HasKey("FristPlay"))
        PlayerPrefs.SetInt("FristPlay",0);

        if (PlayerPrefs.GetInt("FristPlay") == 0)
        {
            PlayerPrefs.SetInt("TogMusic", 1);
            PlayerPrefs.SetInt("TogFullScreen", 0);
            PlayerPrefs.SetInt("TogFPS", 1);
            PlayerPrefs.SetFloat("BGMusic", 0.5f);
            PlayerPrefs.SetFloat("TXMusic", 0.5f);
            PlayerPrefs.SetFloat("TSMusic", 0.5f);
            PlayerPrefs.SetInt("FristPlay", 1);
        }
        else {
            if (PlayerPrefs.GetInt("TogMusic") == 1) TogMusic = true; else TogMusic = false;
            if (PlayerPrefs.GetInt("TogFullScreen") == 1) TogFullScreen = true; else TogFullScreen = false;
            if (PlayerPrefs.GetInt("TogFPS") == 1) TogFPS = true; else TogFPS = false;
            BGMusic = PlayerPrefs.GetFloat("BGMusic");
            TXMusic = PlayerPrefs.GetFloat("TXMusic");
            TSMusic = PlayerPrefs.GetFloat("TSMusic");
        }
        if (PlayerPrefs.GetInt("TogFullScreen") == 1)
        {
            if (!Screen.fullScreen) { Screen.SetResolution(Screen.width, Screen.height, true); }
        }
        else {
            if (Screen.fullScreen) { Screen.SetResolution(Screen.width, Screen.height, false); }
        }
    }
    // Use this for initialization
    void Start () {
        BGMusic = PlayerPrefs.GetFloat("BGMusic");
        TXMusic = PlayerPrefs.GetFloat("TXMusic");
        TSMusic = PlayerPrefs.GetFloat("TSMusic");
        if (PlayerPrefs.GetInt("TogMusic") == 1) TogMusic = true; else TogMusic = false;
        if (PlayerPrefs.GetInt("TogFullScreen") == 1) TogFullScreen = true; else TogFullScreen = false;
        if (PlayerPrefs.GetInt("TogFPS") == 1) TogFPS = true; else TogFPS = false;
        //界面显示
        toggleMusic.isOn = TogMusic;
        toggleFullScreen.isOn = TogFullScreen;
        toggleFPS.isOn = TogFPS;
        bgMusic.value = BGMusic;
        txMusic.value = TXMusic;
        tsMusic.value = TSMusic;
    }
	
	// Update is called once per frame
	void Update () {
        if (toggleMusic.isOn) TogMusic = true; else TogMusic = false;
        if (toggleFullScreen.isOn) TogFullScreen = true; else TogFullScreen = false;
        if (toggleFPS.isOn) TogFPS = true; else TogFPS = false;
        BGMusic = bgMusic.value;
        TXMusic = txMusic.value;
        TSMusic = tsMusic.value;

    }

    public void onokbutton() {
        if (TogMusic) PlayerPrefs.SetInt("TogMusic", 1); else PlayerPrefs.SetInt("TogMusic", 0);
        if (TogFullScreen) PlayerPrefs.SetInt("TogFullScreen", 1); else PlayerPrefs.SetInt("TogFullScreen", 0);
        if (TogFPS) PlayerPrefs.SetInt("TogFPS", 1); else PlayerPrefs.SetInt("TogFPS", 0);
        PlayerPrefs.SetFloat("BGMusic", BGMusic);
        PlayerPrefs.SetFloat("TXMusic", TXMusic);
        PlayerPrefs.SetFloat("TSMusic", TSMusic);

        if (PlayerPrefs.GetInt("TogFullScreen") == 1)
        {
            if (!Screen.fullScreen) { Screen.SetResolution(Screen.width, Screen.height, true); }
        }
        else
        {
            if (Screen.fullScreen) { Screen.SetResolution(Screen.width, Screen.height, false); }
        }

        SceneManager.LoadScene(lastscene);
    }
    public void onquitbutton() {
        SceneManager.LoadScene(lastscene);
    }
}
