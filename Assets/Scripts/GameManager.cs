using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;
using UnityEngine.Analytics;
using Google.Play.Review;
//using GoogleMobileAds.Api;

public class GameManager : MonoBehaviour, IUnityAdsListener
{

    public static GameManager Instance { set; get; }

    static bool created = false;

    [Header("GameManager")]
    public bool allowHot = true;
    public bool withTime = true;
    public float hotLevel = 1;
    public bool anyCanvasActive = false;
    private Canvas activecanvas;
    List<Canvas> allCanvas = new List<Canvas>();
    public int counterAllModes;
    public GameObject noAdsPopup;


    [Header("AdsManager")]
    string gameID;
    public string iosGameID = "3760922";
    public string androidGameID = "3760923";
    public bool testMode;
    public bool removeAds;
    public bool ultrahotAvailable;

    [HideInInspector]
    public GameObject eob_playParent;
    public GameObject eob_watchAdsParent;
    public GameObject ultra_playParent;
    public GameObject ultra_buyParent;

    //private ReviewManager _reviewManager;
    private float IARcounter = 0;
    public float IARseconds;
    private bool IARshowed = false;
    private Canvas ratingCanvas;

    //AddNamesController
    [HideInInspector]
    public static List<string> nameList = new List<string>();

    private IAPManager iapManager;

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

        iapManager = GetComponent<IAPManager>();

        Advertisement.AddListener(this);

        if (Application.platform == RuntimePlatform.IPhonePlayer)
            gameID = iosGameID;
        else if (Application.platform == RuntimePlatform.Android)
            gameID = androidGameID;


        Advertisement.Initialize(gameID, testMode);


        eob_playParent = GameObject.FindGameObjectWithTag("playParent");
        eob_watchAdsParent = GameObject.FindGameObjectWithTag("watchAdsParent");
        noAdsPopup = GameObject.Find("NoAdCanvas");

        ultra_playParent = GameObject.Find("Ultra_Available");
        ultra_buyParent = GameObject.Find("Ultra_NotAvailable");

        removeAds = PlayerPrefs.GetInt("removeAds") == 1 ? true : false;
        ultrahotAvailable = PlayerPrefs.GetInt("ultraHot") == 1 ? true : false;

        if (!removeAds) //si no ha comprado eliminar anuncios
        {
            StartCoroutine(ShowBannerWhenInitialized());
            if(eob_watchAdsParent!=null) ChangeChildStatus(eob_watchAdsParent, true);
            if (eob_playParent != null) ChangeChildStatus(eob_playParent, false);

        }
        else
        {
            DisableRemoveAds();
            StopBanner();
            if (eob_watchAdsParent != null) ChangeChildStatus(eob_watchAdsParent, false);
            if (eob_playParent != null) ChangeChildStatus(eob_playParent, true);
        }

        //if (!ultrahotAvailable)
        //{
        //    if (ultra_buyParent != null) ChangeChildStatus(ultra_buyParent, true);
        //    if (ultra_playParent != null) ChangeChildStatus(ultra_playParent, false);
        //}
        //else
        //{
            if (ultra_buyParent != null) ChangeChildStatus(ultra_buyParent, false);
            if (ultra_playParent != null) ChangeChildStatus(ultra_playParent, true);
        //}

    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "ModesMenu_Arte")
        {
            SetAllCanvas();
        }

        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        //_reviewManager = new ReviewManager();

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

    public void ChangeChildStatus(GameObject parent, bool status)
    {
        foreach (Transform child in parent.transform)
        {
            child.gameObject.SetActive(status);
        }
    }

    public void BuyRemoveAds()
    {
        iapManager.BuyRemoveAds();
    }

    public void BuyUltraHot()
    {
        iapManager.BuyUltraHot();
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

    public void ShowRewardedVideo()
    {
        if (!removeAds)
        {
            if (Advertisement.IsReady("Rewarded_EoB"))
            {
                Advertisement.Show("Rewarded_EoB");
            }
            else
            {
                noAdsPopup.GetComponent<Canvas>().enabled = true;
            }
        }
    }

    // Implement IUnityAdsListener interface methods:
    public void OnUnityAdsDidFinish(string surfacingId, ShowResult showResult)
    {
        // Define conditional logic for each ad completion status:
        if (showResult == ShowResult.Finished && surfacingId == "Rewarded_EoB")
        {
            //Recompensa
            SceneManager.LoadScene("EnviaoBebe_Arte");
            Analytics.CustomEvent("start_eob");
        }
        else if (showResult == ShowResult.Skipped)
        {
            // Do not reward the user for skipping the ad.
        }
        else if (showResult == ShowResult.Failed)
        {
            Debug.LogWarning("The ad did not finish due to an error.");
        }
    }

    public void OnUnityAdsReady(string surfacingId)
    {
        // If the ready Ad Unit or legacy Placement is rewarded, show the ad:
        if (surfacingId == "Rewarded_EoB")
        {
            // Optional actions to take when theAd Unit or legacy Placement becomes ready (for example, enable the rewarded ads button)
        }
    }

    public void OnUnityAdsDidError(string message)
    {
        // Log the error.
    }

    public void OnUnityAdsDidStart(string surfacingId)
    {
        // Optional actions to take when the end-users triggers an ad.
    }

    // When the object that subscribes to ad events is destroyed, remove the listener:
    public void OnDestroy()
    {
        Advertisement.RemoveListener(this);
    }

    //---------------------------------------IN APP REVIEW----------------------------------------
    //public IEnumerator RequestReview()
    //{
    //    var requestFlowOperation = _reviewManager.RequestReviewFlow();
    //    yield return requestFlowOperation;
    //    if (requestFlowOperation.Error != ReviewErrorCode.NoError)
    //    {
    //        // Log error. For example, using requestFlowOperation.Error.ToString().
    //        yield break;
    //    }
    //    var _playReviewInfo = requestFlowOperation.GetResult();

    //    var launchFlowOperation = _reviewManager.LaunchReviewFlow(_playReviewInfo);
    //    yield return launchFlowOperation;
    //    _playReviewInfo = null; // Reset the object
    //    if (launchFlowOperation.Error != ReviewErrorCode.NoError)
    //    {
    //        // Log error. For example, using requestFlowOperation.Error.ToString().
    //        yield break;
    //    }
    //    // The flow has finished. The API does not indicate whether the user
    //    // reviewed or not, or even whether the review dialog was shown. Thus, no
    //    // matter the result, we continue our app flow.
    //}

    //public void ShowReview()
    //{
    //    StartCoroutine(RequestReview());
    //}

    public void ShowReview()
    {
        ratingCanvas = GameObject.Find("RatingCanvas").GetComponent<Canvas>();
        ratingCanvas.enabled = true;
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
