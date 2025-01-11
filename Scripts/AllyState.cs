using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using PrimeTween;

public class AllyState : MonoBehaviour
{

    public GameObject player;

    public SpawnEnemies enemySpawner;
    public GameController gc;
    public AllyAudioManager audioManager;

    NavMeshAgent agent;

    bool oneShot;

    int damage = 34;

    bool decreaseAtkCooldown;
    bool decreaseOnce;

    public Color matColor;
    public Renderer rend;

    float cooldownTimer = 0f;
    public float cooldown = 3f;

    public bool attackTarget;
    public bool hasHitTarget;

    public bool playAttackModeAudio;
    public bool playFollowModeAudio;

    public bool playAttackParticles;
    public bool playFollowParticles;

    public ParticleSystem attackParticle;
    public ParticleSystem followParticle;

    public CollisionArea colArea;

    public DamageNumbers canvasDamage;


    public enum States
    {
        follow,
        attack
    }

    public States currentState;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentState = States.follow;
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        rend.material.color = matColor;

        cooldownTimer -= Time.deltaTime;
        if(cooldownTimer <= 0f)
            cooldownTimer = 0f;

        if(currentState == States.follow)
        {
            transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z));
            agent.SetDestination(player.transform.position);

            if (!agent.pathPending)
                if (agent.remainingDistance <= agent.stoppingDistance && colArea.isHoldingXP)
            {
                    transform.GetChild(0).GetComponent<FaceCamera>().text.enabled = true;
            }
                else
                    transform.GetChild(0).GetComponent<FaceCamera>().text.enabled = false;



        }

        if ((gc.currentPlayerLevel % 5 == 0))
        {
            if (!decreaseOnce)
                decreaseAtkCooldown = true;
        }
        else decreaseOnce = false;
        if (decreaseAtkCooldown)
        {
            DecreaseAtkCooldown();
            decreaseAtkCooldown = false;
            decreaseOnce = true;
        }

        if (currentState == States.attack)
        {
            if(enemySpawner.closestEnemy != null)
            {
                transform.LookAt(new Vector3(enemySpawner.closestEnemy.transform.position.x, transform.position.y, enemySpawner.closestEnemy.transform.position.z));
                agent.SetDestination(enemySpawner.closestEnemy.transform.position);
            }

            if(!agent.pathPending)
            if (agent.stoppingDistance - agent.remainingDistance >= 0)
            {
                attackTarget = true;
            }


            if (attackTarget && cooldownTimer <= 0f)
            {
                Vector3 target = new Vector3(0f, 0f, 0f);
                if (enemySpawner.closestEnemy != null)
                    target = enemySpawner.closestEnemy.transform.position;
                else attackTarget = false;
                transform.position = Vector3.Lerp(transform.position, target, 5 * Time.deltaTime);

                if(!oneShot)
                {
                    audioManager.hasWindedUp = false;
                    oneShot = true;
                }

            }

        }


        if (Input.GetKeyDown(KeyCode.Space))
        {
            enemySpawner.FindClosestEnemyToAlly(this.gameObject);
            if(enemySpawner.closestEnemy != null)
            currentState = States.attack;
            playAttackModeAudio = true;
            playAttackParticles = true;
            Tween.Scale(transform, endValue: .1f, duration: .05f).OnComplete(() =>
Tween.Scale(transform, endValue: .5f, duration: .1f).OnComplete(() =>
Tween.Scale(transform, endValue: .3f, duration: .1f)));
            Tween.Custom(matColor, Color.red, duration: .2f, onValueChange: newVal => matColor = newVal);

        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            currentState = States.follow;
            playFollowModeAudio = true;
            playFollowParticles = true;
            Tween.Scale(transform, endValue: .1f, duration: .05f).OnComplete(() =>
Tween.Scale(transform, endValue: .5f, duration: .1f).OnComplete(() =>
Tween.Scale(transform, endValue: .3f, duration: .1f)));
            Tween.Custom(matColor, Color.green, duration: .2f, onValueChange: newVal => matColor = newVal);

        }


        if (playAttackParticles)
        {
            Instantiate(attackParticle, transform.position, Quaternion.Euler(-90f, 0f, 0f));
            Instantiate(attackParticle, player.transform.position, Quaternion.Euler(-90f, 0f, 0f));
            playAttackParticles = false;
        }

        if (playFollowParticles)
        {
            Instantiate(followParticle, transform.position, Quaternion.Euler(-90f, 0f, 0f));
            Instantiate(followParticle, player.transform.position, Quaternion.Euler(-90f, 0f, 0f));
            playFollowParticles = false;
        }


        if (enemySpawner.closestEnemy == null)
            enemySpawner.FindClosestEnemyToAlly(this.gameObject);

    }

    void OnCollisionStay(Collision col)
    {
        if(col.gameObject == enemySpawner.closestEnemy && cooldownTimer <= 0f && currentState == States.attack)
        {
            hasHitTarget = true;
            attackTarget = false;
            cooldownTimer = cooldown;
            oneShot = false;
            if(gc.firstLevel)
            col.gameObject.GetComponent<EnemyCombat>().DamageUnit(34f);
            else
                col.gameObject.GetComponent<EnemyCombat>().DamageUnit(damage);
            col.gameObject.GetComponent<EnemyCombat>().BloodFX();

            canvasDamage.InstantiateDamageNumber(damage, new Vector3(0f, 0f, 0f));

            gc.FreezeEffect();
        }
    }


    public void LevelUp()
    {
        damage += Mathf.RoundToInt(10 / Mathf.Sqrt(Mathf.Pow(gc.currentPlayerLevel, .95f)));
    }

    public void DecreaseAtkCooldown()
    {
        cooldown -= 2 / Mathf.Pow(5, .65f);
    }

}
