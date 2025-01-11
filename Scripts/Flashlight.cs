using UnityEngine;
using System.Collections;

public class Flashlight : MonoBehaviour
{

    public Light flashlight;
    bool isFlashlightOn;

    AudioSource flashLightAudio;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        flashlight.enabled = false;
        flashLightAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("f"))
        {
            isFlashlightOn = !isFlashlightOn;
            flashLightAudio.pitch = Random.Range(.95f, 1.05f);
            flashLightAudio.volume = Random.Range(.4f, .45f);
            flashLightAudio.Play();
        }

        if (isFlashlightOn)
            RenderSettings.fogEndDistance = 20.4f;
        else RenderSettings.fogEndDistance = 4.20f;


        flashlight.enabled = isFlashlightOn;

        transform.rotation = Quaternion.Lerp(transform.rotation, Camera.main.transform.rotation, 5 * Time.deltaTime);
    }
}
