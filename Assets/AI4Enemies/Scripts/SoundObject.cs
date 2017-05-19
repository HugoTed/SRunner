using UnityEngine;
using System.Collections;

[System.Serializable]
public class SoundObject
{
    //ShootSound    
    [SerializeField]
    private AudioClip soundfile;
    [SerializeField]
    private float soundVolume = 1;
    private AudioSource audioSource;
    [HideInInspector]
    public GameObject gameObject;
    public void Play()
    {
        if (soundfile != null)
        {
            audioSource.Play();
        }
    }

    public void Init()
    {
        if (soundfile != null)
        {
            //audioSource = gameObject.AddComponent("AudioSource") as AudioSource;
            audioSource = gameObject.AddComponent<AudioSource>() as AudioSource;
            audioSource.clip = soundfile;
            audioSource.loop = false;
            if (PlayerPrefs.GetInt("TogMusic") == 1) { audioSource.volume = PlayerPrefs.GetFloat("TXMusic"); }
            else { audioSource.volume = 0; }
            
            audioSource.playOnAwake = false;
            audioSource.rolloffMode = AudioRolloffMode.Linear;
        }
    }

}
