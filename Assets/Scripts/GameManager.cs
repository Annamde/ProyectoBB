﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { set; get; }

    static bool created = false;

    public bool allowHot = true;

    public bool withTime = true;

    //public bool switching = true;
    //estaria guay poner en el manager los tiempos que le daremos a cada modo, ya que cada uno será diferente (aunq por escena se podrá cambiar)

    public bool anyCanvasActive = false;
    


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


    private void Update()
    {
        if(Application.platform == RuntimePlatform.Android)
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                if (!anyCanvasActive)
                {
                    if (SceneManager.GetActiveScene().name == "ModesMenu_Arte")
                    {
                        Application.Quit();
                    }
                    else
                    {
                        SceneManager.LoadScene("ModesMenu_Arte");
                    }
                }
            }
        }

    }

    public void IsAnyCanvasActive()
    {
        anyCanvasActive = !anyCanvasActive;
    }

}
