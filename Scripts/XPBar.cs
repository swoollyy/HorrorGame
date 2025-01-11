using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class XPBar : MonoBehaviour
{

    public Image xpBar;
    public Image xpBarHolder;

    public bool fillToMax;
    public bool canLevelUp;


    float target;
    public float reduceSpeed;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (!fillToMax)
            xpBar.fillAmount = Mathf.MoveTowards(xpBar.fillAmount, target, reduceSpeed * Time.deltaTime);
        else
        {
            xpBar.fillAmount = Mathf.MoveTowards(xpBar.fillAmount, .999f, reduceSpeed * Time.deltaTime);
            if(xpBar.fillAmount >= .999f)
            {
                canLevelUp = true;
            }
        }

    }

    public void UpdateXP(float currentXP, float maxXP)
    {
        target = currentXP / maxXP;
    }

}
