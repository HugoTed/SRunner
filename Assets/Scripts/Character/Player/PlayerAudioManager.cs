using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioManager : MonoBehaviour {
    public AudioSource[] txmusic;
    public float txmusicVolume;
    public player player;
    public PlayerAttack playerAttack;
    // Use this for initialization
    void Start () {
        if (PlayerPrefs.GetInt("TogMusic") == 1)
        {
            txmusicVolume = PlayerPrefs.GetFloat("TXMusic");
        }
        else { txmusicVolume = 0.0f; }
        
    }
	
	// Update is called once per frame
	void Update () {
        //slide
        if(player.ifOnWall() && player.vspeed()>0)
        {
            if (!txmusic[0].isPlaying)
            {
                //播放音乐
                Debug.Log("wallmusic");
                txmusic[0].volume = txmusicVolume;
                txmusic[0].Play();
            }
        }
        else
        {
            if (txmusic[0].isPlaying)
            {
                //stop音乐
                txmusic[0].Stop();
            }
        }
        //jump-跳跃
        if (Input.GetButton("Jump"))
        {
            if (!txmusic[1].isPlaying)
            {
                //播放跳跃音乐
                txmusic[1].volume = txmusicVolume;
                txmusic[1].Play();
                //music[1].PlayDelayed(-1);
                Debug.Log("jumpmusic");
            }
        }
        else
        {
            if (txmusic[1].isPlaying)
            {
                //stop跳跃音乐
                txmusic[1].Stop();
            }
        }
        //attack-砍击
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (!txmusic[2].isPlaying)
            {
                //播放砍击音乐
                Debug.Log("attackmusic");
                txmusic[2].volume = txmusicVolume;
                txmusic[2].Play();
            }
        }

    }
}
