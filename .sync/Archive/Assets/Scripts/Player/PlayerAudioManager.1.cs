using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioManager : MonoBehaviour {
    public AudioSource[] music;
    public float musicVolume;
    public player player;
    public PlayerAttack playerAttack;
    // Use this for initialization
    void Start () {
        musicVolume = 1F;
        
    }
	
	// Update is called once per frame
	void Update () {
        //slide
        if(player.ifOnWall() && player.vspeed()>0)
        {
            if (!music[0].isPlaying)
            {
                //播放音乐
                Debug.Log("wallmusic");
                music[0].Play();
            }
        }
        else
        {
            if (music[0].isPlaying)
            {
                //stop音乐
                music[0].Stop();
            }
        }
        //jump-跳跃
        if (Input.GetButton("Jump"))
        {
            if (!music[1].isPlaying)
            {
                //播放跳跃音乐

                music[1].Play();
                //music[1].PlayDelayed(-1);
                Debug.Log("jumpmusic");
            }
        }
        else
        {
            if (music[1].isPlaying)
            {
                //stop跳跃音乐
                music[1].Stop();
            }
        }
        //attack-砍击
        if (playerAttack.attack)
        {
            if (!music[2].isPlaying)
            {
                //播放砍击音乐
                Debug.Log("attackmusic");
                music[2].Play();
            }
        }
        else
        {
            if (music[2].isplaying)
            {
                //stop砍击音乐
                music[2].stop();
            }
        }

    }
}
