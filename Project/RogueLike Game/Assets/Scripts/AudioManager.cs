using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
   public AudioClip movingClip, ClickClip;
   public AudioSource  BgmSource, soundeffectSource;
    // Start is called before the first frame update
    void Start()
    {
        //PlayAudio_Bgm(BgmClip);
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)&& !soundeffectSource.isPlaying)
        {
            PlayAudio_Effect(movingClip);
        }
        if (Input.GetMouseButtonDown(0))
        {
            PlayAudio_Effect(ClickClip);
        }
    }

    public void PlayAudio_Effect(AudioClip newAudio)
    {
        soundeffectSource.clip = newAudio;
        soundeffectSource.Play();
        soundeffectSource.loop = false;
        if (!soundeffectSource.isPlaying) Destroy(this.gameObject);
    }

}
