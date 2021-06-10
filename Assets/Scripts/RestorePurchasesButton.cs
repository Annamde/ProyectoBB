using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RestorePurchasesButton : MonoBehaviour
{
    IAPManager IAPManager;
    Button thisButton;

    void Start()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            this.gameObject.SetActive(false);
        }

        else
        {
            IAPManager = GameObject.FindWithTag("GameManager").GetComponent<IAPManager>();
            thisButton = GetComponent<Button>();
            thisButton.onClick.AddListener(IAPManager.RestorePurchases);
        }
    }
}
