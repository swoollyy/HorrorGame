using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;

public class GammaRayBlast : MonoBehaviour
{

    float timer;
    float warningFlashTimer;
    float gammaTimer;


    int rng;
    int rngPosition;
    int shooterPosition;

    float gammaCooldown = 12f;

    GameController gc;

    public Light light;
    public GameObject gammaRay;

    AudioSource gammaAudio;
    public AudioSource warningAudio;

    public AudioMixerSnapshot defaultMixer;
    public AudioMixerSnapshot gammaMixer;

    bool beginGammaBurst;
    bool rollForPoint;
    bool chargeUpGamma;
    bool hasLockedOn;
    bool enableBurst;
    bool shakeCam;

    public List<GameObject> warningSpots = new List<GameObject>(4);

    public List<GameObject> gammaRaySpots = new List<GameObject>(8);
    public List<GameObject> gammaShooterSpots = new List<GameObject>(8);
    public List<GameObject> rayLocation = new List<GameObject>(8);


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        light.intensity = 2400f;
        light.enabled = false;

        gammaAudio = GetComponent<AudioSource>();

        gammaRay.SetActive(false);
        for (int i = 0; i < warningSpots.Count; i++)
        {
            warningSpots[i].SetActive(false);
        }

        for (int i = 0; i < gammaRaySpots.Count; i++)
        {
            gammaRaySpots[i].SetActive(false);
        }

        gc = GameObject.FindWithTag("GameController").GetComponent<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!hasLockedOn)
        timer += Time.deltaTime;

        if(gc.currentPlayerLevel == 3)
        {
            if (timer > 12f)
            {
                rng = Random.Range(0, 4);
                timer = 0;
            }
        }
        else
        if (timer > gammaCooldown)
        {
            rng = Random.Range(0, 4);
            timer = 0;
        }

        if (rng == 1)
        {
            beginGammaBurst = true;
            hasLockedOn = true;
        }
        if (beginGammaBurst)
        {
            rng = 0;
            rngPosition = Random.Range(0, 4);

            if (rngPosition == 0)
            {
                shooterPosition = Random.Range(0, 2);
            }
            else if (rngPosition == 1)
            {
                shooterPosition = Random.Range(2, 4);
            }
            else if (rngPosition == 2)
            {
                shooterPosition = Random.Range(4, 6);
            }
            else
            {
                shooterPosition = Random.Range(6, 8);
            }

            gammaRay.SetActive(true);
            gammaRay.transform.position = new Vector3(rayLocation[shooterPosition].transform.position.x, 0f, rayLocation[shooterPosition].transform.position.z);
            gammaRay.transform.rotation = rayLocation[shooterPosition].transform.rotation;

            rollForPoint = true;

        }
        if (rollForPoint)
        {
            AlertPlayer(rngPosition);
            beginGammaBurst = false;
            gammaMixer.TransitionTo(2.5f);
        }

        if (chargeUpGamma)
        {
            light.enabled = true;
            light.intensity = Mathf.Lerp(light.intensity, 100000f, 5 * Time.deltaTime);
            light.transform.position = gammaShooterSpots[shooterPosition].transform.position;
            light.transform.rotation = gammaShooterSpots[shooterPosition].transform.rotation;

            if (!gammaAudio.isPlaying)
            {
                gammaAudio.pitch = Random.Range(.95f, 1.1f);
                gammaAudio.Play();
            }

            if (light.intensity > 97000f)
            {
                enableBurst = true;
                GetComponent<GameController>().ShakeCamera(5f, 4f, 100);
            }
        }

        if(enableBurst)
        {
            light.intensity = 96000f;
            gammaTimer += Time.deltaTime;
            gammaRaySpots[shooterPosition].SetActive(true);
            if(gammaTimer >= 2.5f)
                defaultMixer.TransitionTo(1.5f);
            if (gammaTimer >= 4f)
            {
            gammaRaySpots[shooterPosition].SetActive(false);
                enableBurst = false;
                chargeUpGamma = false;
                light.enabled = false;
                light.intensity = 2400f;
                gammaRay.SetActive(false);
                hasLockedOn = false;
                gammaTimer = 0f;
            }
        }
    }

    void AlertPlayer(int location)
    {
        if (warningSpots[location].activeSelf && !warningAudio.isPlaying)
        {
            warningAudio.Play();
        }
        print("location " + location);
        warningSpots[location].SetActive(true);
        warningFlashTimer += Time.deltaTime;
        if(warningFlashTimer >= .5f && warningFlashTimer < 1f)
            warningSpots[location].SetActive(false);
        if (warningFlashTimer >= 1f && warningFlashTimer < 1.5f)
            warningSpots[location].SetActive(true);
        if (warningFlashTimer >= 1.5f && warningFlashTimer < 2f)
            warningSpots[location].SetActive(false);
        if (warningFlashTimer >= 2f && warningFlashTimer < 2.5f)
            warningSpots[location].SetActive(true);
        if (warningFlashTimer >= 2.5f && warningFlashTimer < 3f)
            warningSpots[location].SetActive(false);
        if (warningFlashTimer >= 3f && warningFlashTimer < 3.5f)
        {
            warningSpots[location].SetActive(true);
            chargeUpGamma = true;
        }
        if (warningFlashTimer >= 3.5f && warningFlashTimer < 4f)
        {
            warningSpots[location].SetActive(false);
        }
        if (warningFlashTimer >= 4f)
        {
            warningFlashTimer = 0;
            warningSpots[location].SetActive(false);
            rollForPoint = false;
        }


    }

    public void LevelUp()
    {
        gammaCooldown -= 4 / (Mathf.Pow(gc.currentPlayerLevel, .95f));
    }

}
