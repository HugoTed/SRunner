using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIoptionslidertextshow : MonoBehaviour {
    public Slider slider;
    public Text text;
    public int percentage;
    // Use this for initialization
    void Start () {
        percentage = (int)(slider.value * 100);
	}
	
	// Update is called once per frame
	void Update () {
        percentage = (int)(slider.value * 100);
        text.text = percentage.ToString();
    }
}
