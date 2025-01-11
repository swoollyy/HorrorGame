using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;

public class EnemyCombat : MonoBehaviour
{

    public float maxHealth = 100f;
    float currentHealth;

    public float damageTaken;

    int damage = 10;

    bool increaseHP;
    bool increaseOnce;

    AudioSource deathSound;

    public GameController gc;

    public SpawnEnemies enemySpawner;


    public ParticleSystem bloodParticle;
    public ParticleSystem xpParticle;
    public ParticleSystem deathParticle;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
        enemySpawner = GameObject.FindWithTag("GameController").GetComponent<SpawnEnemies>();
        gc = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        deathSound = GameObject.FindWithTag("EnemyDeathSFX").GetComponent<AudioSource>();

        GetComponent<NavMeshAgent>().speed = 2.1f;

    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.GetChild(0).GetComponent<HealthBar>().UpdateHP(maxHealth, currentHealth);

        if (currentHealth <= 0)
        {
            enemySpawner.RemoveFromList(this.gameObject);
            deathSound.Play();
            DropXP();
            Instantiate(deathParticle, transform.position, Quaternion.Euler(-90f, 0f, 0f));
            enemySpawner.closestEnemy = null;
            Destroy(this.gameObject);
        }

        if ((gc.currentPlayerLevel % 5 == 0))
        {
            if(!increaseOnce)
            increaseHP = true;
        }
        else increaseOnce = false;
        if (increaseHP)
        {
            maxHealth += 35f;
            increaseHP = false;
            increaseOnce = true;
        }




    }

    public void DamageUnit(float damage)
    {
        currentHealth -= damage;
    }

    public void BloodFX()
    {
        Instantiate(bloodParticle, transform.position, Quaternion.identity);
    }

    public void DropXP()
    {
        Instantiate(xpParticle, transform.position, Quaternion.identity);
    }

    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == "Player")
        {
            if(gc.currentPlayerLevel == 1)
            col.gameObject.GetComponent<PlayerCombat>().DamageUnit(10f);
            else
                col.gameObject.GetComponent<PlayerCombat>().DamageUnit(damage);

            col.gameObject.transform.GetChild(0).GetComponent<DamageNumbers>().InstantiateDamageNumber(damage, new Vector3(0f, 0f, 0f));
            col.gameObject.GetComponent<PlayerCombat>().ParticleBleed();
            col.gameObject.GetComponent<PlayerAudioManager>().PlayDamage();
            gc.FreezeEffect();
            gc.stopBulletZoom = false;
        }



    }

    public void LevelUp()
    {
        damage += Mathf.RoundToInt(5 / Mathf.Pow(gc.currentPlayerLevel, .05f) - .3f);
    }

}
