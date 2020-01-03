using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaderScript : MonoBehaviour
{
    private CanvasGroup fadeGroup;
    public float fadingTime;

    private void Start()
    {
        fadeGroup = FindObjectOfType<CanvasGroup>();

        fadeGroup.alpha = 1.0f;
    }

    private void Update()
    {
        fadeGroup.alpha -= 1 / fadingTime * Time.deltaTime;
    }
}
