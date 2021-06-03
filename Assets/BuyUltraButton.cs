using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyUltraButton : MonoBehaviour
{
    GameManager gameManager;
    Button thisButton;

    void Start()
    {
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        thisButton = GetComponent<Button>();
        thisButton.onClick.AddListener(gameManager.BuyUltraHot);
    }
}
