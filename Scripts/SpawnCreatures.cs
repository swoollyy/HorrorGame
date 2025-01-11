using UnityEngine;

public class SpawnCreatures : MonoBehaviour
{
    public GameObject puppetMan;
    public GameObject slender;

    public GameObject player;

    Vector3 nearPlayerPos;

    int rng;
    int slenderrng;

    bool stopSlenderTimer;
    bool stopPuppetTimer;
    bool enableSlender;

    public float rngMinSet;
    public float rngMaxSet;
    public float slenderrngMinSet;
    public float slenderrngMaxSet;

    float rngMin;
    float rngMax;
    float slenderrngMin;
    float slenderrngMax;

    float rngMinLower = .5f;
    float rngMaxLower = 1.5f;
    float slenderrngMinLower = 1.5f;
    float slenderrngMaxLower = 2f;

    public bool lowerValues = true;

    float timer;
    float slenderTimer;

    float rngTimer;
    float slenderRNGTimer;

    public ParticleSystem enemySpawnVFX;
    public GameObject enemySpawnSFX;
    public GameObject slenderSFX;
    public CollectPapers paperCollect;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rngMin = rngMinSet;
        rngMax = rngMaxSet;
        slenderrngMin = slenderrngMinSet;
        slenderrngMax = slenderrngMaxSet;
        rngTimer = Random.Range(rngMin, rngMax);
        slenderRNGTimer = Random.Range(slenderrngMin, slenderrngMax);
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray;
        RaycastHit hit;
        
        if(!stopPuppetTimer)
        timer += Time.deltaTime;

        if (!stopSlenderTimer && enableSlender)
            slenderTimer += Time.deltaTime;


        if (timer >= rngTimer)
        {
            rng = Random.Range(0, 6);
            rngTimer = Random.Range(rngMin, rngMax);
            timer = 0;
        }

        if (slenderTimer >= slenderRNGTimer)
        {
            slenderrng = Random.Range(0, 2);
            slenderRNGTimer = Random.Range(slenderrngMin, slenderrngMax);
            slenderTimer = 0;
        }

        if (rng == 1)
        {
            stopPuppetTimer = true;
            nearPlayerPos = new Vector3(player.transform.position.x + Random.Range(-9, 9f), player.transform.position.y, player.transform.position.z + Random.Range(-9f, 9f));

            if (Physics.Raycast(nearPlayerPos, -transform.up, out hit))
            {
                if (hit.transform.gameObject.tag == "Terrain")
                {
                    Instantiate(puppetMan, new Vector3(nearPlayerPos.x, hit.point.y, nearPlayerPos.z), Quaternion.identity);
                    Instantiate(enemySpawnSFX, new Vector3(nearPlayerPos.x, hit.point.y, nearPlayerPos.z), Quaternion.identity);
                    Instantiate(enemySpawnVFX, new Vector3(nearPlayerPos.x, hit.point.y, nearPlayerPos.z), Quaternion.Euler(-90f, 0f, 0f));
                }
            }
            stopPuppetTimer = false;
            rng = 0;
        }
        if (slenderrng == 1)
        {
            stopSlenderTimer = true;
            nearPlayerPos = new Vector3(player.transform.position.x + Random.Range(-15f, 15f), player.transform.position.y, player.transform.position.z + Random.Range(-15f, 15f));

            if (Physics.Raycast(nearPlayerPos, -transform.up, out hit))
            {
                if (hit.transform.gameObject.tag == "Terrain")
                {
                    GameObject instSlender = Instantiate(slender, new Vector3(nearPlayerPos.x, hit.point.y, nearPlayerPos.z), Quaternion.identity);
                    Instantiate(slenderSFX, new Vector3(nearPlayerPos.x, hit.point.y, nearPlayerPos.z), Quaternion.identity, instSlender.transform);
                }
            }
            stopSlenderTimer = false;
            slenderrng = 0;
        }

        print(rngMin);

        switch (paperCollect.collectedPapers)
        {
            case 4:
                    rngMin = rngMinSet - rngMinLower;
                    rngMax = rngMaxSet - rngMaxLower;
                enableSlender = true;
                break;
            case 5:
                if (lowerValues)
                {
                    rngMinLower = rngMinLower + .5f;
                    rngMaxLower = rngMaxLower + 1.5f;
                    lowerValues = false;
                }
                rngMin = rngMinSet - rngMinLower;
                rngMax = rngMaxSet - rngMaxLower;
                break;
            case 6:
                if (lowerValues)
                {
                    rngMinLower = rngMinLower + .5f;
                    rngMaxLower = rngMaxLower + 1.5f;
                    slenderrngMinLower = slenderrngMinLower + .5f;
                    slenderrngMaxLower = slenderrngMaxLower + 1.5f;
                    lowerValues = false;
                }
                rngMin = rngMinSet - rngMinLower;
                rngMax = rngMaxSet - rngMaxLower;
                slenderrngMin = slenderrngMinSet - slenderrngMinLower;
                slenderrngMax = slenderrngMaxSet - slenderrngMaxLower;
                break;
            case 7:
                if (lowerValues)
                {
                    rngMinLower = rngMinLower + .5f;
                    rngMaxLower = rngMaxLower + 1.5f;
                    slenderrngMinLower = slenderrngMinLower + .5f;
                    slenderrngMaxLower = slenderrngMaxLower + 1.5f;
                    lowerValues = false;
                }
                rngMin = rngMinSet - rngMinLower;
                rngMax = rngMaxSet - rngMaxLower;
                slenderrngMin = slenderrngMinSet - slenderrngMinLower;
                slenderrngMax = slenderrngMaxSet - slenderrngMaxLower;
                break;
            case 8:
                if (lowerValues)
                {
                    rngMinLower = rngMinLower + 1f;
                    rngMaxLower = rngMaxLower + 2f;
                    slenderrngMinLower = slenderrngMinLower + .5f;
                    slenderrngMaxLower = slenderrngMaxLower + 1.5f;
                    lowerValues = false;
                }
                rngMin = rngMinSet - rngMinLower;
                rngMax = rngMaxSet - rngMaxLower;
                slenderrngMin = slenderrngMinSet - slenderrngMinLower;
                slenderrngMax = slenderrngMaxSet - slenderrngMaxLower;
                break;
            case 9:
                if (lowerValues)
                {
                    rngMinLower = rngMinLower + 1f;
                    rngMaxLower = rngMaxLower + 2f;
                    slenderrngMinLower = slenderrngMinLower + .5f;
                    slenderrngMaxLower = slenderrngMaxLower + 1.5f;
                    lowerValues = false;
                }
                rngMin = rngMinSet - rngMinLower;
                rngMax = rngMaxSet - rngMaxLower;
                slenderrngMin = slenderrngMinSet - slenderrngMinLower;
                slenderrngMax = slenderrngMaxSet - slenderrngMaxLower;
                break;
            case 10:
                if (lowerValues)
                {
                    rngMinLower = rngMinLower + 1f;
                    rngMaxLower = rngMaxLower + 2f;
                    slenderrngMinLower = slenderrngMinLower + .5f;
                    slenderrngMaxLower = slenderrngMaxLower + 1.5f;
                    lowerValues = false;
                }
                rngMin = rngMinSet - rngMinLower;
                rngMax = rngMaxSet - rngMaxLower;
                slenderrngMin = slenderrngMinSet - slenderrngMinLower;
                slenderrngMax = slenderrngMaxSet - slenderrngMaxLower;
                break;
            case 11:
                if (lowerValues)
                {
                    rngMinLower = rngMinLower + 1f;
                    rngMaxLower = rngMaxLower + 2f;
                    slenderrngMinLower = slenderrngMinLower + .5f;
                    slenderrngMaxLower = slenderrngMaxLower + 1.5f;
                    lowerValues = false;
                }
                rngMin = rngMinSet - rngMinLower;
                rngMax = rngMaxSet - rngMaxLower;
                slenderrngMin = slenderrngMinSet - slenderrngMinLower;
                slenderrngMax = slenderrngMaxSet - slenderrngMaxLower;
                break;
            case 12:
                if (lowerValues)
                {
                    rngMinLower = rngMinLower + 1f;
                    rngMaxLower = rngMaxLower + 2f;
                    slenderrngMinLower = slenderrngMinLower + .5f;
                    slenderrngMaxLower = slenderrngMaxLower + 1.5f;
                    lowerValues = false;
                }
                rngMin = rngMinSet - rngMinLower;
                rngMax = rngMaxSet - rngMaxLower;
                slenderrngMin = slenderrngMinSet - slenderrngMinLower;
                slenderrngMax = slenderrngMaxSet - slenderrngMaxLower;
                break;
        }

    }
}
