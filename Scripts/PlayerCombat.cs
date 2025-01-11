using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerCombat : MonoBehaviour
{

    public float maxHealth = 100f;
    float currentHealth;

    public HealthBar playerHealth;

    public ReceiveXP playerLevel;

    public TMP_Text healthText;

    public float damageTaken;

    public ParticleSystem bloodParticle;

    bool increaseHP;
    bool increaseOnce;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = "Health: " + currentHealth +"/ " + maxHealth;

        playerHealth.UpdateHP(maxHealth, currentHealth);

        if (currentHealth <= 0)
            SceneManager.LoadScene("MainScene");


        if((playerLevel.level % 5 == 0))
        {
            if(!increaseOnce)
            increaseHP = true;
        }
        else increaseOnce = false;
        if(increaseHP)
        {
            maxHealth += 25f;
            increaseHP = false;
            increaseOnce = true;
        }


    }

    public void DamageUnit(float damage)
    {
        currentHealth -= damage;
    }

    public void ParticleBleed()
    {
        Instantiate(bloodParticle, transform.position, Quaternion.identity);
    }
}
