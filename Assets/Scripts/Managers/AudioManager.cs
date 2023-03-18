using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        if (instance == null) instance = this;
        else Destroy(this.gameObject);

        audioSource = GetComponent<AudioSource>();
    }

    public void TurnOffMusic()
    {
        audioSource.Stop();
    }

    public void PlayOneShot(AudioClip audio)
    {
        audioSource.PlayOneShot(audio);
    }
}
