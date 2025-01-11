using UnityEngine;
using TMPro;


public class DamageNumbers : MonoBehaviour
{

    public GameObject damageText;

    Camera cam;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InstantiateDamageNumber(int damage, Vector3 location)
    {
        GameObject newText = Instantiate(damageText, location, Quaternion.Euler(0f, 0f, 0f), this.transform);

        newText.GetComponent<TMP_Text>().text = damage.ToString();

        newText.transform.localScale = new Vector3(1f, 1f, 1f);
    }

}
