using UnityEngine;

public class LevelUp : MonoBehaviour
{

    public XPBar xpScript;
    public ReceiveXP receiveXP;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(xpScript.xpBar.fillAmount == 1)
        {
            xpScript.xpBar.fillAmount = 0;
            receiveXP.currentXP = receiveXP.extraXP;
        }
    }
}
