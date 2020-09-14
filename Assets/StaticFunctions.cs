using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StaticFunctions : MonoBehaviour
{
    public void LoadScene(string name)
    {
        if (name == "ModesMenu_Arte")
        {
            //GameManager.Instance.ShowIntersticial();
            GameManager.Instance.ShowInterstitialAd();
        }

        SceneManager.LoadScene(name);
    }
}
