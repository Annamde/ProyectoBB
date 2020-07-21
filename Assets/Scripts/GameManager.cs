using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using GoogleMobileAds.Api;

public class GameManager : MonoBehaviour
{
    
    public static GameManager Instance { set; get; }

    static bool created = false;

    [Header("GameManager")]
    public bool allowHot = true;
    public bool withTime = true;
    public bool anyCanvasActive = false;
    private Canvas activecanvas;
    List<Canvas> allCanvas = new List<Canvas>();
    public int counterAllModes;

    [Header("AdsManager")]
    [SerializeField] private string appID = "";
    [SerializeField] private string bannerID = "";
    [SerializeField] private string intersticialID = "";

    private BannerView bannerAd;
    private InterstitialAd interstitialAd;

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

        MobileAds.Initialize(initStatus => { });
        RequestIntersticial();
        RequestBanner();
    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "ModesMenu_Arte")
        {

            SetAllCanvas();
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

        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
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

        //BORRAR PARA LA BUILD


        //if (Input.GetKeyDown(KeyCode.A))
        //{
        //    StartCoroutine(DesactiveCanvas());
        //    if (!anyCanvasActive)
        //    {
        //        if (SceneManager.GetActiveScene().name == "ModesMenu_Arte")
        //        {
        //            Application.Quit();
        //        }
        //        else
        //        {
        //            SceneManager.LoadScene("ModesMenu_Arte");
        //        }
        //    }
        //    else
        //    {
        //        anyCanvasActive = false;
        //        activecanvas.enabled = false;
        //    }
        //}
        
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

    //--------------------------------------------ADS-----------------------------------------

    public void RequestBanner()
    {
        bannerAd = new BannerView(bannerID, AdSize.Banner, AdPosition.Bottom);
        AdRequest request = new AdRequest.Builder().Build();
        bannerAd.LoadAd(request);
    }

    public void RequestIntersticial()
    {
        interstitialAd = new InterstitialAd(intersticialID);
        AdRequest request = new AdRequest.Builder().Build();
        interstitialAd.LoadAd(request);
    }

    public void ShowIntersticial()
    {
        interstitialAd.Show();
        interstitialAd.Destroy();
        RequestIntersticial();
    }

}
