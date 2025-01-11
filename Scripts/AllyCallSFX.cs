using UnityEngine;

public class AllyCallSFX : MonoBehaviour
{

    public AllyState ally;

    AudioSource audio;

    public AudioClip attackClip;
    public AudioClip followClip;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ally.playAttackModeAudio)
        {
            audio.clip = attackClip;
            audio.Play();
            ally.playAttackModeAudio = false;
        }

        if (ally.playFollowModeAudio)
        {
            audio.clip = followClip;
            audio.Play();
            ally.playFollowModeAudio = false;
        }
    }
}
