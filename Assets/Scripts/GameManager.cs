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

    public bool anyCanvasActive = false;

    //public Canvas yonunca, tabu, retos, quienesmas, letras, mimica;

    private Canvas activecanvas;

    List<Canvas> allCanvas = new List<Canvas>();

    public int counterAllModes;


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

            SetAllCanvas();

            //allCanvas.Add(yonunca);
            //allCanvas.Add(tabu);
            //allCanvas.Add(retos);
            //allCanvas.Add(quienesmas);
            //allCanvas.Add(letras);
            //allCanvas.Add(mimica);
        }
      

        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        
    }


    private void Update()
    {
        print(allCanvas.Count);


        if (allCanvas.Count < counterAllModes)
        {
            print("NO HAY Y LOS INSRANCIO");
            SetAllCanvas();
        }


        //-----------------IMPORTANTISIMO-----------------------

        //DESCOMENTAR PARA LA BUILD

        //if (Application.platform == RuntimePlatform.Android)
        //{
        //    if (Input.GetKeyDown(KeyCode.Escape))
        //    {
        //        StartCoroutine(DesactiveCanvas());
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
        //            anyCanvasActive = false;
        //            activecanvas.enabled = false;
        //        }
        //    }
        //}

        //BORRAR PARA LA BUILD


        if (Input.GetKeyDown(KeyCode.A))
        {
            StartCoroutine(DesactiveCanvas());
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
                anyCanvasActive = false;
                activecanvas.enabled = false;
            }
        }
        
    }


    IEnumerator DesactiveCanvas()
    {
        if (SceneManager.GetActiveScene().name == "ModesMenu_Arte")
        {
            for (int i = 0; i < allCanvas.Count; i++)
            {
                if (allCanvas[i].isActiveAndEnabled)
                {
                    activecanvas = allCanvas[i];

                    anyCanvasActive = true;
                    yield return anyCanvasActive = true;
                }
                else
                {
                    activecanvas = null;
                }
            }
        }
        yield return null;
    }

    public void SetAllCanvas()
    {
        print("pon los canvas plisss");
        allCanvas.Add(GameObject.Find("YoNuncaInstructions").GetComponent<Canvas>());
        allCanvas.Add(GameObject.Find("TabuInstructions").GetComponent<Canvas>());
        allCanvas.Add(GameObject.Find("RetosInstructions").GetComponent<Canvas>());
        allCanvas.Add(GameObject.Find("QuienInstructions").GetComponent<Canvas>());
        allCanvas.Add(GameObject.Find("LetrasInstructions").GetComponent<Canvas>());
        allCanvas.Add(GameObject.Find("MimicaInstructions").GetComponent<Canvas>());
    }

    public void ResetListOfCanvas()
    {
        allCanvas.Clear();
    }

    public void IsAnyCanvasActive()
    {
        anyCanvasActive = !anyCanvasActive;
    }

}
