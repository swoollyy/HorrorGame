using UnityEngine;

public class CollideWithPlayer : MonoBehaviour
{

    float dmgNumberTimer;

    public DamageNumbers damageNumbers;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        print(dmgNumberTimer);
    }

    void OnTriggerStay(Collider col)
    {
        if(col.gameObject.tag == "Player")
        {
            damageNumbers.InstantiateDamageNumber(5, new Vector3(0f, 0f, 0f));
            col.gameObject.GetComponent<PlayerCombat>().DamageUnit(5f);
        }
    }

    void OnTriggerExit(Collider col)
    {
        dmgNumberTimer = 0;
    }

}
