using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StaticFunctions : MonoBehaviour
{

    public GameObject errorText;

    private void Start()
    {
        errorText = GameObject.Find("ErrorText");
        errorText.SetActive(false);
    }

    public void LoadScene(string name)
    {
        if (name == "ModesMenu_Arte")
        {
            //GameManager.Instance.ShowIntersticial();
            GameManager.Instance.ShowInterstitialAd();
            SceneManager.LoadScene(name);
        }

        else if(name == "Mix_Arte")
        {
            if (GameManager.Instance.includeYN || GameManager.Instance.includeVoR || GameManager.Instance.includeQuien)
                SceneManager.LoadScene(name);
            else
                errorText.SetActive(true);
        }
    }
}
