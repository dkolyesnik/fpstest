using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundOnEnable : MonoBehaviour
{
    public AudioSource AudioSource;
    public List<AudioClip> Clips;

    private void OnEnable()
    {
        if (AudioSource != null && Clips.Count > 0)
        {
            var clip = Clips[Random.Range(0, Clips.Count)];
            AudioSource.PlayOneShot(clip);
        }
    }
}
