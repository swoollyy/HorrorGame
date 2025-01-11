using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CollectXP : MonoBehaviour
{

    Vector3 reference = new Vector3(0f, 0f, 0f);

    public List<ParticleSystem.Particle> particles = new List<ParticleSystem.Particle>();


    CollisionArea colArea;
    GameObject ally;

    public AudioSource xpAudio;

    AudioSource instantiatedAudio;

    ParticleSystem ps;

    bool collectxp;
    int numberOfParticles;
    int audioCount;


    AllyXPBar allyXPScript;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ally = GameObject.FindWithTag("AllyCollection");
        colArea = ally.GetComponent<CollisionArea>();
        allyXPScript = GameObject.FindWithTag("AllyXPHolder").transform.GetChild(0).GetComponent<AllyXPBar>();
        ps = GetComponent<ParticleSystem>();
        ps.trigger.AddCollider(ally.GetComponent<Collider>());
    }

    // Update is called once per frame
    void Update()
    {


    }

    void OnParticleTrigger()
    {
        int triggeredParticles = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, particles);

        for (int i = 0; i < triggeredParticles; i++)
        {
            audioCount++;
            allyXPScript.particleXP += 2f;
            ParticleSystem.Particle p = particles[i];
            colArea.lightUp = true;
            colArea.AddXP(2f);
            instantiatedAudio = Instantiate(xpAudio, p.position, Quaternion.identity);
            instantiatedAudio.pitch = Random.Range(.9f, 1.1f);
            instantiatedAudio.volume = Random.Range(.018f, .091f);
            allyXPScript.UpdateXP(allyXPScript.particleXP, 200f);
            p.remainingLifetime = 0;
            particles[i] = p;
        }


        ps.SetTriggerParticles(ParticleSystemTriggerEventType.Enter, particles);

    }

}
