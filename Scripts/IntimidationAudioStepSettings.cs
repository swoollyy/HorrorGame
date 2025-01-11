using UnityEngine;

public class IntimidationAudioStepSettings : MonoBehaviour
{

    AudioSource audio;

    RandomSFX sfxController;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audio = GetComponent<AudioSource>();
        audio.volume = Random.Range(.5f, .8f);

        sfxController = GameObject.FindWithTag("GameController").GetComponent<RandomSFX>();

    }

    // Update is called once per frame
    void Update()
    {
        if (!audio.isPlaying)
        {
            sfxController.stepCount++;
            if (sfxController.stepCount <= sfxController.stepCountMax && !sfxController.resetSteps)
            Instantiate(this.gameObject, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }

        if (sfxController.stepCount > sfxController.stepCountMax)
        {
            sfxController.stepCount = 0;
            sfxController.resetSteps = true;
        }
    }
}
