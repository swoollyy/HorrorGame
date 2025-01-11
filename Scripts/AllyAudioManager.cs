using UnityEngine;

public class AllyAudioManager : MonoBehaviour
{

    public AudioClip windUp;
    public AudioClip impact;

    public AllyState ally;

    public bool hasWindedUp;


    AudioSource audio;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audio = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        if(ally.attackTarget)
        {
            if (!hasWindedUp)
            {
                audio.clip = windUp;
                if (!audio.isPlaying)
                {
                    audio.volume = 1f;
                    audio.pitch = Random.Range(.75f, 1.25f);
                    audio.Play();
                    hasWindedUp = true;
                }
            }

        }

        if(ally.hasHitTarget)
        {
            if (audio.isPlaying)
            {
                print("whew");
                audio.Stop();
                audio.clip = null;
            }
            audio.clip = impact;
            audio.pitch = Random.Range(.9f, 1.1f);
            audio.volume = 1f;
            audio.Play();
            ally.hasHitTarget = false;
        }


    }
}
