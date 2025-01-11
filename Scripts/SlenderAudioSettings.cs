using UnityEngine;

public class SlenderAudioSettings : MonoBehaviour
{

    AudioSource audio;

    public float minPitchRange;
    public float maxPitchRange;
    public float minVolRange;
    public float maxVolRange;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audio = GetComponent<AudioSource>();
        audio.pitch = Random.Range(minPitchRange, maxPitchRange);
        audio.volume = Random.Range(minVolRange, maxVolRange);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
