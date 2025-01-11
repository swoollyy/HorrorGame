using UnityEngine;
using UnityEngine.UI;

public class AllyXPBar : MonoBehaviour
{

    public Image xpBar;
    public Image xpBarHolder;


    public float particleXP = 0f;

    float target;
    public float reduceSpeed;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        xpBar.fillAmount = Mathf.MoveTowards(xpBar.fillAmount, target, reduceSpeed * Time.deltaTime);
    }

    public void UpdateXP(float currentXP, float maxXP)
    {
        target = currentXP / maxXP;
    }

}
