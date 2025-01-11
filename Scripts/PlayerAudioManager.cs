using UnityEngine;

public class PlayerAudioManager : MonoBehaviour
{

    public AudioClip meleeAudio;
    public AudioClip bulletAudio;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayDamage()
    {
        GetComponent<AudioSource>().clip = meleeAudio;
        GetComponent<AudioSource>().pitch = Random.Range(.8f, 1.2f);
        GetComponent<AudioSource>().Play();
    }

    public void PlayBulletDamage()
    {
        GetComponent<AudioSource>().clip = bulletAudio;
        GetComponent<AudioSource>().pitch = Random.Range(.8f, 1.2f);
        GetComponent<AudioSource>().Play();
    }
}
