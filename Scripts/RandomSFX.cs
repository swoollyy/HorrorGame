using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class RandomSFX : MonoBehaviour
{

    public List<GameObject> randomsfx = new List<GameObject>();
    public List<GameObject> randomAmbientSFX = new List<GameObject>();
    public List<GameObject> randomMassSFX = new List<GameObject>();

    public GameObject stepAudio;

    float timer;
    float massTimer;
    float massSFXTimer;
    float stepTimer;

    float randomAmbienceTimer;


    float rng;
    float massRNG;
    float stepRNG;

    public int stepCount;
    public int stepCountMax;


    public float rngMinSet;
    public float rngMaxSet;
    public float massrngMinSet;
    public float massrngMaxSet;
    public float steprngMinSet;
    public float steprngMaxSet;
    float rngMin;
    float rngMax;
    float massrngMin;
    float massrngMax;
    float steprngMin;
    float steprngMax;

    float rngMinLower = .5f;
    float rngMaxLower = 1.5f;
    float massrngMinLower = .25f;
    float massrngMaxLower = 1f;
    float steprngMinLower = 1f;
    float steprngMaxLower = 3.2f;


    float stepVicinity;

    public bool lowerValues = true;

    bool enableMassSFX;
    bool stopTimer;
    public bool resetSteps;

    public GameObject player;

    public CollectPapers papers;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        stepVicinity = 5f;

        rngMin = rngMinSet;
        rngMax = rngMaxSet;
        steprngMin = steprngMinSet;
        steprngMax = steprngMaxSet;
        rng = Random.Range(rngMin, rngMax);
        massRNG = Random.Range(massrngMinSet, massrngMaxSet);
        stepRNG = Random.Range(steprngMinSet, steprngMaxSet);
        enableMassSFX = false;

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        randomAmbienceTimer += Time.deltaTime;
        if (!stopTimer)
        stepTimer += Time.deltaTime;

        if(enableMassSFX)
        {
            massTimer += Time.deltaTime;
        }

        if(randomAmbienceTimer >= 15f)
        {
            int rng = Random.Range(1, 4);
            if(rng == 3)
            {
                Vector3 randomLoc = new Vector3(player.transform.position.x + Random.Range(-5f, 5f), player.transform.position.y, player.transform.position.z + Random.Range(-5f, 5f));
                GameObject instAudio = Instantiate(randomAmbientSFX[Random.Range(0, randomAmbientSFX.Count)], randomLoc, Quaternion.identity);
                rng = 0;
                randomAmbienceTimer = 0;
            }
        }

        if(timer >= rng)
        {
            Vector3 randomLoc = new Vector3(player.transform.position.x + Random.Range(-5f, 5f), player.transform.position.y, player.transform.position.z + Random.Range(-5f, 5f));
            GameObject instAudio = Instantiate(randomsfx[Random.Range(0, randomsfx.Count)], randomLoc, Quaternion.identity);
            rng = Random.Range(rngMin, rngMax);

            timer = 0;


        }

        if (massTimer >= massRNG)
        {
            massSFXTimer += Time.deltaTime;

            if(massSFXTimer < .75f)
            {
                int rngChance = Random.Range(1, 6);
                if(rngChance == 5)
                {
                    Vector3 randomLoc = new Vector3(player.transform.position.x + Random.Range(-10f, 10f), player.transform.position.y, player.transform.position.z + Random.Range(-10f, 10f));
                    GameObject instAudio = Instantiate(randomMassSFX[Random.Range(0, randomMassSFX.Count)], randomLoc, Quaternion.identity);
                }
            }
            if(massSFXTimer >= .75f)
            {
                massTimer = 0;
                massSFXTimer = 0;
                massRNG = Random.Range(massrngMin, massrngMax);
            }



        }

        if (stepTimer >= stepRNG)
        {
            stopTimer = true;
            stepCountMax = Random.Range(1, 6);
            Vector3 randomLoc = new Vector3(player.transform.position.x + Random.Range(-stepVicinity, stepVicinity), player.transform.position.y, player.transform.position.z + Random.Range(-stepVicinity, stepVicinity));
            GameObject instStepAudio = Instantiate(stepAudio, randomLoc, Quaternion.identity);
            stepTimer = 0;
        }

        if(resetSteps)
        {
            stepRNG = Random.Range(steprngMin, steprngMax);
            stopTimer = false;
            resetSteps = false;
        }


        switch(papers.collectedPapers)
        {
            case 0:
                break;
            case 1:
                rngMin = rngMinSet - rngMinLower;
                rngMax = rngMaxSet - rngMaxLower;
                steprngMin = steprngMinSet - steprngMinLower;
                steprngMax = steprngMaxSet - steprngMaxLower;
                break;
            case 2:
                if (lowerValues)
                {
                    rngMinLower = rngMinLower + .5f;
                    rngMaxLower = rngMaxLower + 1.5f;
                    steprngMinLower = steprngMinLower + 1f;
                    steprngMaxLower = steprngMaxLower + 3.2f;
                    lowerValues = false;
                }
                rngMin = rngMinSet - rngMinLower;
                rngMax = rngMaxSet - rngMaxLower;
                steprngMin = steprngMinSet - steprngMinLower;
                steprngMax = steprngMaxSet - steprngMaxLower;
                break;
            case 3:
                if (lowerValues)
                {
                    rngMinLower = rngMinLower + .5f;
                    rngMaxLower = rngMaxLower + 1.5f;
                    steprngMinLower = steprngMinLower + 1f;
                    steprngMaxLower = steprngMaxLower + 3.2f;
                    lowerValues = false;
                }
                rngMin = rngMinSet - rngMinLower;
                rngMax = rngMaxSet - rngMaxLower;
                steprngMin = steprngMinSet - steprngMinLower;
                steprngMax = steprngMaxSet - steprngMaxLower;
                break;
            case 4:
                if (lowerValues)
                {
                    rngMinLower = rngMinLower + .5f;
                    rngMaxLower = rngMaxLower + 1.5f;
                    steprngMinLower = steprngMinLower + 1f;
                    steprngMaxLower = steprngMaxLower + 3.2f;
                    lowerValues = false;
                }
                rngMin = rngMinSet - rngMinLower;
                rngMax = rngMaxSet - rngMaxLower;
                steprngMin = steprngMinSet - steprngMinLower;
                steprngMax = steprngMaxSet - steprngMaxLower;
                break;
            case 5:
                if (lowerValues)
                {
                    rngMinLower = rngMinLower + .5f;
                    rngMaxLower = rngMaxLower + 1.5f;
                    steprngMinLower = steprngMinLower + 1f;
                    steprngMaxLower = steprngMaxLower + 3.2f;
                    lowerValues = false;
                }
                rngMin = rngMinSet - rngMinLower;
                rngMax = rngMaxSet - rngMaxLower;
                steprngMin = steprngMinSet - steprngMinLower;
                steprngMax = steprngMaxSet - steprngMaxLower;
                break;
            case 6:
                if (lowerValues)
                {
                    rngMinLower = rngMinLower + .5f;
                    rngMaxLower = rngMaxLower + 1.5f;
                    steprngMinLower = steprngMinLower + 1f;
                    steprngMaxLower = steprngMaxLower + 3.2f;
                    lowerValues = false;
                }
                enableMassSFX = true;
                rngMin = rngMinSet - rngMinLower;
                rngMax = rngMaxSet - rngMaxLower;
                steprngMin = steprngMinSet - steprngMinLower;
                steprngMax = steprngMaxSet - steprngMaxLower;
                break;
            case 7:
                if (lowerValues)
                {
                    rngMinLower = rngMinLower + .5f;
                    rngMaxLower = rngMaxLower + 1.5f;
                    steprngMinLower = steprngMinLower + 1f;
                    steprngMaxLower = steprngMaxLower + 3.2f;
                    lowerValues = false;
                }
                rngMin = rngMinSet - rngMinLower;
                rngMax = rngMaxSet - rngMaxLower;
                massrngMin = massrngMinSet - massrngMinLower;
                massrngMax = massrngMaxSet - massrngMaxLower;
                steprngMin = steprngMinSet - steprngMinLower;
                steprngMax = steprngMaxSet - steprngMaxLower;
                break;
            case 8:
                if (lowerValues)
                {
                    rngMinLower = rngMinLower + 1.5f;
                    rngMaxLower = rngMaxLower + 2f;
                    steprngMinLower = steprngMinLower + 1f;
                    steprngMaxLower = steprngMaxLower + 3.2f;
                    lowerValues = false;
                }
                rngMin = rngMinSet - rngMinLower;
                rngMax = rngMaxSet - rngMaxLower;
                massrngMin = massrngMinSet - massrngMinLower;
                massrngMax = massrngMaxSet - massrngMaxLower;
                steprngMin = steprngMinSet - steprngMinLower;
                steprngMax = steprngMaxSet - steprngMaxLower;
                break;
            case 9:
                if (lowerValues)
                {
                    rngMinLower = rngMinLower + .5f;
                    rngMaxLower = rngMaxLower + 1.5f;
                    steprngMinLower = steprngMinLower + 1f;
                    steprngMaxLower = steprngMaxLower + 3.2f;
                    lowerValues = false;
                }
                rngMin = rngMinSet - rngMinLower;
                rngMax = rngMaxSet - rngMaxLower;
                massrngMin = massrngMinSet - massrngMinLower;
                massrngMax = massrngMaxSet - massrngMaxLower;
                steprngMin = steprngMinSet - steprngMinLower;
                steprngMax = steprngMaxSet - steprngMaxLower;
                break;
            case 10:
                if (lowerValues)
                {
                    rngMinLower = rngMinLower + .5f;
                    rngMaxLower = rngMaxLower + 1.5f;
                    steprngMinLower = steprngMinLower + 1f;
                    steprngMaxLower = steprngMaxLower + 3.2f;
                    lowerValues = false;
                }
                rngMin = rngMinSet - rngMinLower;
                rngMax = rngMaxSet - rngMaxLower;
                massrngMin = massrngMinSet - massrngMinLower;
                massrngMax = massrngMaxSet - massrngMaxLower;
                steprngMin = steprngMinSet - steprngMinLower;
                steprngMax = steprngMaxSet - steprngMaxLower;
                break;
            case 11:
                if (lowerValues)
                {
                    rngMinLower = rngMinLower + .5f;
                    rngMaxLower = rngMaxLower + 1.5f;
                    steprngMinLower = steprngMinLower + 1f;
                    steprngMaxLower = steprngMaxLower + 3.2f;
                    lowerValues = false;
                }
                rngMin = rngMinSet - rngMinLower;
                rngMax = rngMaxSet - rngMaxLower;
                massrngMin = massrngMinSet - massrngMinLower;
                massrngMax = massrngMaxSet - massrngMaxLower;
                steprngMin = steprngMinSet - steprngMinLower;
                steprngMax = steprngMaxSet - steprngMaxLower;
                break;
            case 12:
                if (lowerValues)
                {
                    rngMinLower = rngMinLower + .5f;
                    rngMaxLower = rngMaxLower + 1.5f;
                    steprngMinLower = steprngMinLower + 1f;
                    steprngMaxLower = steprngMaxLower + 3.2f;
                    lowerValues = false;
                }
                rngMin = rngMinSet - rngMinLower;
                rngMax = rngMaxSet - rngMaxLower;
                massrngMin = massrngMinSet - massrngMinLower;
                massrngMax = massrngMaxSet - massrngMaxLower;
                steprngMin = steprngMinSet - steprngMinLower;
                steprngMax = steprngMaxSet - steprngMaxLower;
                break;
        }
    }
}
