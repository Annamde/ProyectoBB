using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { set; get; }

    public Canvas instructionCanvas;

    static bool created = false;

    public bool allowHot = true;

    private void Awake()
    {
        if (!created)
        {
            DontDestroyOnLoad(gameObject);
            created = true;
            Instance = this;
        }

        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        if (instructionCanvas != null)
        {
            instructionCanvas.enabled = false;
        }
        
    }

    public void OnHotCheck()
    {
        allowHot = !allowHot;
        print(allowHot);
    }

    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }
}
