using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    bool timeEnded = false;
    public float maxTime = 5.0f;
    float counterTime = 0.0f;
    public Text topText;
    public Text centerText;
    public Text bottomText;
    public Text counterText;
    public Text outlineText;
    public Image outlineTextImage;

    void Start()
    {
        counterTime = maxTime;

        if(!GameManager.Instance.withTime)
        {
            this.enabled = false;
        }
        else
        {
            this.enabled = true;
        }

        if (outlineTextImage)
        {
            outlineTextImage.enabled = true;
        }
    }

    void Update()
    {
        if (counterTime <= 0.1f)
        {
            timeEnded = true;
            outlineTextImage.enabled = false;
            outlineText.text = "";
            topText.text = "";
            centerText.text = "¡TIEMPO!";
            bottomText.text = "";
            
            if (outlineTextImage)
            {
                outlineTextImage.enabled = false;
            }
            Handheld.Vibrate();
        }

        else
            counterTime -= Time.deltaTime;

        counterText.text = Mathf.FloorToInt(counterTime).ToString();
    }

    public void OnScreenTap()
    {
        counterTime = maxTime;
        timeEnded = false;
        if (outlineTextImage)
        {
            outlineTextImage.enabled = true;
        }
    }
}
