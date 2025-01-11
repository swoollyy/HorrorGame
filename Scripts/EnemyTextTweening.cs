using UnityEngine;
using PrimeTween;
using TMPro;

public class EnemyTextTweening : MonoBehaviour
{
    TMP_Text text;

    Color textColor;
    float textAlpha;

    bool stopIt;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        text = GetComponent<TMP_Text>();
        textColor = Color.red;
        textAlpha = 255f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!stopIt)
        {
            Sequence.Create()
.Group(Tween.PositionY(transform, endValue: 3f, duration: .5f))
.Group(Tween.Scale(transform, endValue: 1.5f, duration: .2f))
.Group(Tween.Custom(text.color, Color.black, duration: 1f, onValueChange: newVal => textColor = newVal))
.Group(Tween.Custom(text.alpha, 0f, duration: .5f, onValueChange: newVal => textAlpha = newVal))
            .OnComplete(() =>
    stopIt = true);
        }


        if (text.alpha == 0 && stopIt)
        {
            stopIt = false;
            Destroy(this.gameObject);
        }

        text.color = textColor;
        text.alpha = textAlpha;
    }

}
