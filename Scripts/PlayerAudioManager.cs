using UnityEngine;

public class PlayerAudioManager : MonoBehaviour
{

    public PlayerMovement movement;
    AudioSource audio;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(movement.rb.linearVelocity.magnitude > 0 && movement.currentState == PlayerMovement.MovementState.walking)
        {
            if(!audio.isPlaying)
            {
                audio.pitch = Random.Range(.95f, 1.05f);
                audio.volume = Random.Range(.25f, .35f);
                audio.Play();
            }
        }
        else audio.Stop();
    }
}
