using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMAINSCORE : MonoBehaviour {
    public static int allscore = 1000;
    public Text scoretxt;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        scoretxt.text = allscore.ToString();
    }
    public void addscore(int x)
    {
        allscore += x;
    }
}
