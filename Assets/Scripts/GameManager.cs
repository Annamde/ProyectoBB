using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;
using UnityEngine.Analytics;
using Google.Play.Review;
//using GoogleMobileAds.Api;

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
    public string iosGameID = "3760922";
    public string androidGameID = "3760923";
    public bool testMode;
    public bool removeAds;

    private ReviewManager _reviewManager;
    private float IARcounter = 0;
    public float IARseconds;
    private bool IARshowed = false;

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

#if UNITY_ANDROID
        Advertisement.Initialize(androidGameID, testMode);
#endif
#if UNITY_IOS
        Advertisement.Initialize(iosGameID, testMode);
#endif

        removeAds = PlayerPrefs.GetInt("removeAds") == 1 ? true : false;
        if (!removeAds) StartCoroutine(ShowBannerWhenInitialized());
        else
        {
            DisableRemoveAds();
            StopBanner();
        }
    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "ModesMenu_Arte")
        {
            SetAllCanvas();
        }

        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        _reviewManager = new ReviewManager();

        //por si no le ha dado tiempo a procesar que está comprado, que lo vuelva a comprobar
        removeAds = PlayerPrefs.GetInt("removeAds") == 1 ? true : false;
        if (removeAds)
        {
            DisableRemoveAds();
            StopBanner();
        }
    }


    private void Update()
    {

        if (allCanvas.Count < counterAllModes)
        {
            print("NO HAY Y LOS INSTANCIO");
            SetAllCanvas();
        }


        //-----------------IMPORTANTISIMO-----------------------

        //DESCOMENTAR PARA LA BUILD

        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
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
                        TextManager tm = FindObjectOfType<TextManager>();
                        int clicks = tm.clicks;

                        Dictionary<string, object> dictionary = new Dictionary<string, object>();
                        dictionary.Add("clicks", clicks);

                        switch (SceneManager.GetActiveScene().name)
                        {
                            case "Cita3_Arte":
                                Analytics.CustomEvent("quit_cita3", dictionary);
                                break;
                            case "Letras_Arte":
                                Analytics.CustomEvent("quit_letras", dictionary);
                                break;
                            case "Mimica_Arte":
                                Analytics.CustomEvent("quit_mimica", dictionary);
                                break;
                            case "Quien_Arte":
                                Analytics.CustomEvent("quit_quien", dictionary);
                                break;
                            case "Retos_Arte":
                                Analytics.CustomEvent("quit_retos", dictionary);
                                break;
                            case "Tabu_Arte":
                                Analytics.CustomEvent("quit_tabu", dictionary);
                                break;
                            case "YoNunca_Arte":
                                Analytics.CustomEvent("quit_yonunca", dictionary);
                                break;
                        }
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

#if UNITY_ANDROID
        if (!IARshowed)
        {
            if (IARcounter < IARseconds)
                IARcounter += Time.deltaTime;
            else
            {
                ShowReview();
                IARshowed = true;
            }
        }
#endif

#if UNITY_IOS
        if (!IARshowed)
        {
            if (IARcounter < IARseconds)
                IARcounter += Time.deltaTime;
            else
            {
                NativeReviewRequest.RequestReview()
                IARshowed = true;
            }
        }
#endif

    }

    public void ResetPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
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

    //--------------------------------------------Unity Ads-----------------------------------------

    public void ShowInterstitialAd()
    {
        if (!removeAds)
        {
            // Check if UnityAds ready before calling Show method:
            if (Advertisement.IsReady("Intersticial_menu"))
            {
                Advertisement.Show("Intersticial_menu");
            }
            else
            {
                Debug.Log("Interstitial ad not ready at the moment! Please try again later!");
            }
        }
    }

    IEnumerator ShowBannerWhenInitialized()
    {
        if (!removeAds)
        {
            while (!Advertisement.isInitialized)
            {
                yield return new WaitForSeconds(0.5f);
            }

            Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
            Advertisement.Banner.Show("Banner_abajo");
        }

        else StopBanner();
    }

    public void StopBanner()
    {
        Advertisement.Banner.Hide();
    }

    public void DisableRemoveAds()
    {
        GameObject button;
        button = GameObject.FindGameObjectWithTag("RemoveAds");
        if (button != null) button.SetActive(false);
        StopBanner();
    }

    //---------------------------------------IN APP REVIEW----------------------------------------
    public IEnumerator RequestReview()
    {
        var requestFlowOperation = _reviewManager.RequestReviewFlow();
        yield return requestFlowOperation;
        if (requestFlowOperation.Error != ReviewErrorCode.NoError)
        {
            // Log error. For example, using requestFlowOperation.Error.ToString().
            yield break;
        }
        var _playReviewInfo = requestFlowOperation.GetResult();

        var launchFlowOperation = _reviewManager.LaunchReviewFlow(_playReviewInfo);
        yield return launchFlowOperation;
        _playReviewInfo = null; // Reset the object
        if (launchFlowOperation.Error != ReviewErrorCode.NoError)
        {
            // Log error. For example, using requestFlowOperation.Error.ToString().
            yield break;
        }
        // The flow has finished. The API does not indicate whether the user
        // reviewed or not, or even whether the review dialog was shown. Thus, no
        // matter the result, we continue our app flow.
    }

    public void ShowReview()
    {
        StartCoroutine(RequestReview());
    }

    //--------------------------------------------ADMOB-----------------------------------------

    /*
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
*/


}
