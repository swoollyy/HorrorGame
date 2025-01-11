using UnityEngine;
using UnityEngine.AI;
using PrimeTween;

public class HomingBulletMovement : MonoBehaviour
{

    bool shakeCam;
    bool lowerCloseCallAudio;

    int damage = 10;

    float bulletTimer;

    float bulletSpeed = 3.5f;

    public GameController gc;

    GameObject player;
    NavMeshAgent agent;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gc = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        GetComponent<AudioSource>().pitch = Random.Range(1f, 1.3f);
        player = GameObject.FindWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion targetRotation = Quaternion.LookRotation(agent.velocity.normalized);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f); // Smooth rotation
        agent.SetDestination(player.transform.position);

        bulletTimer += Time.deltaTime;

        if (bulletTimer >= 10f)
            Destroy(this.gameObject);

        if (lowerCloseCallAudio)
        {
            transform.GetChild(0).GetComponent<AudioSource>().volume = Mathf.Lerp(transform.GetChild(0).GetComponent<AudioSource>().volume, 0f, 50 * Time.deltaTime);
        }

        if (transform.GetChild(0).GetComponent<AudioSource>().volume < .001f && lowerCloseCallAudio)
        {
            transform.GetChild(0).GetComponent<AudioSource>().Stop();
            GameObject.FindWithTag("Player").GetComponent<PlayerAudioManager>().PlayBulletDamage();
            lowerCloseCallAudio = false;
            Destroy(this.gameObject);
        }




    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Player")
        {
            if (gc.currentPlayerLevel == 1)
                col.gameObject.GetComponent<PlayerCombat>().DamageUnit(10f);
            else
                col.gameObject.GetComponent<PlayerCombat>().DamageUnit(damage);

            col.gameObject.GetComponent<PlayerCombat>().ParticleBleed();
            if (transform.GetChild(0).GetComponent<AudioSource>().isPlaying)
            {
                lowerCloseCallAudio = true;
            }
            else
                col.gameObject.GetComponent<PlayerAudioManager>().PlayBulletDamage();
            transform.GetChild(0).GetComponent<CloseCallDetection>().zoomCam = false;
            col.gameObject.transform.GetChild(0).GetComponent<DamageNumbers>().InstantiateDamageNumber(damage, new Vector3(0f, 0f, 0f));
            gc.FreezeEffect();
            gc.stopBulletZoom = false;
            if (!lowerCloseCallAudio)
                Destroy(this.gameObject);
        }
    }

    public void LevelUp()
    {
        damage += Mathf.RoundToInt(5 / Mathf.Pow(gc.currentPlayerLevel, .05f) + .3f);
    }

}
