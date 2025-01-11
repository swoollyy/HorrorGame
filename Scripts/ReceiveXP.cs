using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ReceiveXP : MonoBehaviour
{

    public float currentXP;
    float maxXP = 50f;

    public float extraXP;

    public XPBar xpScript;
    public AllyXPBar allyXPScript;
    public CollisionArea colArea;
    public TMP_Text receiveXPtext;

    public TMP_Text xpText;
    public TMP_Text levelText;


    public AllyState ally;

    public EnemyCombat enemy;
    public ShootBullet bulletShootersX;
    public ShootBullet bulletShootersZ;
    public SpawnEnemies enemySpawner;
    public BulletMovement bulletMovement;
    public HomingBulletMovement homingBulletMovement;
    public GammaRayBlast gammaRay;

    public AudioSource gainXPAudio;

    public AudioSource levelUpAudio;
    public AudioClip levelUp1;
    public AudioClip levelUp2;

    public int level = 1;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(receiveXPtext.enabled)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                CollectXP(colArea.currentXP);
                colArea.currentXP = 0;
                allyXPScript.particleXP = 0;
                allyXPScript.UpdateXP(colArea.currentXP, colArea.currentXP);
                gainXPAudio.Play();
            }
        }
        
        if(currentXP >= maxXP)
        {
            xpScript.fillToMax = true;
            if(xpScript.canLevelUp)
            {
                int pickAudio = 0;
                pickAudio = UnityEngine.Random.Range(1, 3);
                if (pickAudio == 1)
                {
                    levelUpAudio.clip = levelUp1;
                    levelUpAudio.Play();
                }
                else
                {
                    levelUpAudio.clip = levelUp2;
                    levelUpAudio.Play();
                }

                LevelUp();
            }
        }

        if(!xpScript.fillToMax)
        xpScript.UpdateXP(currentXP, maxXP);

        print("max XP: "+maxXP);
        print("current XP: " + currentXP);
        print("level: " + level);

        xpText.text = "XP: " + currentXP + "/ " + maxXP;
        levelText.text = "Level " + level;


    }


    void CollectXP(float xp)
    {
        print("collected xp : "+xp);
        currentXP += xp;

        if(currentXP > maxXP)
        {
            extraXP = currentXP - maxXP;
        }
    }

    void LevelUp()
    {
        xpScript.canLevelUp = false;
        xpScript.fillToMax = false;
        xpScript.xpBar.fillAmount = 0;
        if(extraXP > 0)
            currentXP = extraXP;
        else
        currentXP = 0;
        level++;
        maxXP += Mathf.RoundToInt(Mathf.Exp(1.61f + level));

        enemySpawner.LevelUp();
        ally.LevelUp();
        bulletMovement.LevelUp();
        homingBulletMovement.LevelUp();
        enemy.LevelUp();
        bulletShootersX.LevelUp();
        bulletShootersZ.LevelUp();
        gammaRay.LevelUp();
    }

}
