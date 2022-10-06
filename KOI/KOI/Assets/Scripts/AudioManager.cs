using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private GameObject fishy;
    
    public AudioSource damageSound;
    public AudioSource burstSound;
    public AudioSource deadSound;
    public AudioSource bgm;
    
    public AudioClip damagedS;
    public AudioClip burstS;
    public AudioClip bgmS;
    public AudioClip deadS;

    private bool check = true;
    
    void Start()
    {
        fishy = Movement.Instance.fishy;
    }
    
    void Update()
    {
        if (Movement.Instance.soundCheckCol && !damageSound.isPlaying)
        {
            damageSound.Play();
            Movement.Instance.soundCheckCol = false;
        }

        if (Movement.Instance.soundCheckLight && !burstSound.isPlaying)
        {
            burstSound.Play();
            Movement.Instance.soundCheckLight = false;
        }

        if (Movement.Instance.died || Movement.Instance.noLight && !deadSound.isPlaying)
        {
            bgm.Stop();
            if (check)
            {
                deadSound.Play();
                check = false;
            }
        }
    }
}
