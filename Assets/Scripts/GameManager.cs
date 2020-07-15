using System;
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
    
    //estaria guay poner en el manager los tiempos que le daremos a cada modo, ya que cada uno será diferente (aunq por escena se podrá cambiar)

    public bool anyCanvasActive = false;

    public Canvas yonunca, tabu, retos, quienesmas, letras, mimica;

    private Canvas activecanvas;

    List<Canvas> allCanvas = new List<Canvas>();


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
        if (SceneManager.GetActiveScene().name == "ModesMenu_Arte")
        {
            //falta
            if (GameObject.Find("YoNuncaInstructions"))
            {
                //allCanvas.Add(Canvas.Find("YoNuncaInstructions"));
            }

            allCanvas.Add(yonunca);
            allCanvas.Add(tabu);
            allCanvas.Add(retos);
            allCanvas.Add(quienesmas);
            allCanvas.Add(letras);
            allCanvas.Add(mimica);
        }
      

        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        
    }


    private void Update()
    {
        print(anyCanvasActive);

        if (SceneManager.GetActiveScene().name == "ModesMenu_Arte")
        {
            for (int i = 0; i < allCanvas.Count; i++)
            {
                if (allCanvas[i].isActiveAndEnabled)
                {
                    activecanvas = allCanvas[i];

                    anyCanvasActive = true;
                    return;
                }
                else
                {
                    activecanvas = null;
                }
            }
        }
        //-----------------IMPORTANTISIMO-----------------------

        //DESCOMENTAR PARA LA BUILD

        //if (Application.platform == RuntimePlatform.Android)
        //{
        //    if(Input.GetKeyDown(KeyCode.Escape))
        //    {
        //        if (!anyCanvasActive)
        //        {
        //            if (SceneManager.GetActiveScene().name == "ModesMenu_Arte")
        //            {
        //                Application.Quit();
        //            }
        //            else
        //            {
        //                SceneManager.LoadScene("ModesMenu_Arte");
        //            }
        //        }
        //        else
        //        {
        //            print("SOY UNA GENIA");
        //            anyCanvasActive = false;
        //            activecanvas.enabled = false;
        //        }
        //    }
        //}

        //BORRAR PARA LA BUILD
        //ANNA DEL FUTURO RECUERDA QUE AL ACTIVAR EL CANVAS NO SABEMOS PQ NO VA LO DE APRETAR UN BOTÓN

        if (Input.GetKeyDown(KeyCode.A))
        {
            print("ESCAPEEEEEEEEE");
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
            else
            {
                print("SOY UNA GENIA");
                anyCanvasActive = false;
                activecanvas.enabled = false;
            }
        }



        print(activecanvas);

    }

    void CanvasSwitch() //no sirve en algun mom lo borrare
    {
        switch(activecanvas.name)
        {
            case "yonunca":

                break;
            case "tabu":

                break;
            case "retos":

                break;
            case "quienesmas":

                break;
            case "letras":

                break;
            case "mimca":

                break;
        }

    }


    public void IsAnyCanvasActive()
    {
        anyCanvasActive = !anyCanvasActive;
    }

}
