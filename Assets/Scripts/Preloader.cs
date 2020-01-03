using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Preloader : MonoBehaviour
{

    private CanvasGroup fadeGroup;
    public float logoTime = 3.0f;
    public float fadingTime;
    bool showed = false;
    public GameObject buttonsContainer;

    private void Start()
    {
        fadeGroup = FindObjectOfType<CanvasGroup>();

        fadeGroup.alpha = 1.0f;
    }

    private void Update()
    {
        if (!showed)
            StartCoroutine(ShowLogo());

        else
        {
            fadeGroup.alpha -= 1 / fadingTime * Time.deltaTime;

            if (fadeGroup.alpha <= 0)
            {
                buttonsContainer.SetActive(true);
            }

        }
    }

    IEnumerator ShowLogo()
    {
        yield return new WaitForSeconds(logoTime);
        showed = true;
    }
}
