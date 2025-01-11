using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{

    GameObject playerLocator;
    AudioSource audio;

    public PlayerMovement player;

    int audioCount = 0;
    int rng = 0;

    float cushionTimer;

    int onlyOnce = 0;

    float chaseRNGTime;
    float despawnTimer;

    public bool hasBeenStaredAt;

    public ParticleSystem enemySpawnVFX;
    public GameObject enemySpawnSFX;
    public GameObject enemyFleeSFX;
    public GameObject enemyAttackSFX;
    public GameObject enemyAttackRunSFX;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerLocator = GameObject.FindWithTag("PlayerLocatorForPuppetMan");
        player = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
        audio = GetComponent<AudioSource>();
        rng = Random.Range(2, 6);
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(new Vector3(playerLocator.transform.position.x, transform.position.y, playerLocator.transform.position.z));

        cushionTimer += Time.deltaTime;

        if(!audio.isPlaying && audioCount <= rng)
        {
            audioCount++;
            audio.Play();
        }
        if(audioCount > rng && !audio.isPlaying)
        {
            audio.Stop();
            if (hasBeenStaredAt && onlyOnce < 1)
            {
                Instantiate(enemySpawnVFX, transform.position, Quaternion.Euler(-90f, 0f, 0f));
                Instantiate(enemySpawnSFX, transform.position, Quaternion.identity);
                Instantiate(enemyFleeSFX, transform.position, Quaternion.identity);
                Destroy(this.gameObject);
                audioCount = 0;
            }
            else
            {
                despawnTimer += Time.deltaTime;
                onlyOnce++;
                if(onlyOnce == 1)
                {
                    chaseRNGTime = Random.Range(4f, 6f);
                    Instantiate(enemyAttackSFX, transform.position, Quaternion.identity);
                    Instantiate(enemyAttackRunSFX, transform.position, Quaternion.identity, this.transform);
                }
                transform.position = Vector3.MoveTowards(transform.position, playerLocator.transform.position, 10 * Time.deltaTime);

                if (despawnTimer >= chaseRNGTime)
                {
                    Instantiate(enemySpawnVFX, transform.position, Quaternion.Euler(-90f, 0f, 0f));
                    Instantiate(enemySpawnSFX, transform.position, Quaternion.identity);
                    Instantiate(enemyFleeSFX, transform.position, Quaternion.identity);
                    Destroy(this.gameObject);
                    audioCount = 0;
                }

            }
        }

        if(player.currentState != PlayerMovement.MovementState.idle && cushionTimer >= 1f)
        {
            despawnTimer += Time.deltaTime;
            audio.volume = Mathf.Lerp(audio.volume, 0f, 25 * Time.deltaTime);
            onlyOnce++;
            if(onlyOnce == 1)
            {
                chaseRNGTime = Random.Range(4f, 6f);
                Instantiate(enemyAttackSFX, transform.position, Quaternion.identity);
                Instantiate(enemyAttackRunSFX, transform.position, Quaternion.identity, this.transform);
            }

            transform.position = Vector3.MoveTowards(transform.position, playerLocator.transform.position, 10 * Time.deltaTime);

            if(despawnTimer >= chaseRNGTime)
            {
                Instantiate(enemySpawnVFX, transform.position, Quaternion.Euler(-90f, 0f, 0f));
                Instantiate(enemySpawnSFX, transform.position, Quaternion.identity);
                Instantiate(enemyFleeSFX, transform.position, Quaternion.identity);
                Destroy(this.gameObject);
            }

        }


    }

}
