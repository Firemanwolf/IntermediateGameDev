using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
   public AudioClip movingClip, ClickClip,hurtclip;
   public AudioSource  BgmSource, soundeffectSource;
    // Start is called before the first frame update
    void Start()
    {
    }


    public void PlayAudio_Effect(AudioClip newAudio)
    {
        soundeffectSource.clip = newAudio;
       if(!soundeffectSource.isPlaying)soundeffectSource.Play();
        soundeffectSource.loop = false;
    }

}
