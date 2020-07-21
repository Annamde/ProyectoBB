using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StaticFunctions : MonoBehaviour
{
    public void LoadScene(string name)
    {
        GameManager.Instance.ShowIntersticial();
        SceneManager.LoadScene(name);
    }
}
