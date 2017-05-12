using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UImain : MonoBehaviour {
    public Text textTime;
    private float time;
    //-------------------------------
    int hour=0;
    int minute=0;
    int second=0;
    int millisecond=0;
    //-------------------------------
    // Use this for initialization
    void Start () {
        time = 0.0f;
	}

	// Update is called once per frame
	void Update () {
        time += Time.deltaTime;
        totime(time);
    }

    public void totime(float timeSpend) {
        hour = (int)time / 3600;
        minute = ((int)timeSpend - hour * 3600) / 60;
        second = (int)timeSpend - hour * 3600 - minute * 60;
        textTime.text = string.Format("{0:D1}:{1:D2}:{2:D2}", hour, minute, second);
        //millisecond = (int)((timeSpend - (int)timeSpend) * 1000);
        //textTime.text = string.Format("{0:D2}:{1:D2}:{2:D2}.{3:D2}", hour, minute, second, millisecond);
        //return text;
    }
}
