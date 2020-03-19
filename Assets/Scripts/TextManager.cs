﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class TextManager : MonoBehaviour
{
    [Header("Text options")]
    public string hotDocName;
    public string lightDocName;
    public char separator;
    public Canvas instructionsCanvas;

    bool allowHot;
    bool withTime;

    
    
    List<int> usedRandom = new List<int>();
    List<int> freeRandom = new List<int>();
    TextAsset lightTextFile, hotTextFile;
    public Text screenText;
    string[] lightQuestionsList, hotQuestionsList, questionsList;
    int i;

    [Header("Time options")]
    public bool sceeneWithTime = false;
    public float maxTime = 30.0f;
    float counterTime = 0.0f;
    public Text counterText; 
    public Button tabButton;

    private void Start()
    {
        counterTime = maxTime;

        i = 0;

        lightTextFile = Resources.Load<TextAsset>(lightDocName);
        hotTextFile = Resources.Load<TextAsset>(hotDocName);

        if (lightTextFile != null)
        {
            if(separator != '-')
                lightQuestionsList = lightTextFile.text.Split(separator);
            else
                lightQuestionsList = lightTextFile.text.Split('\n');

            print("lights: " + lightQuestionsList.Length);
            foreach (string s in lightQuestionsList)
            {
                freeRandom.Add(i);
                i++;
            }
        }

        else
            screenText.text = "No text file found";
        
        allowHot = GameManager.Instance.allowHot;

        withTime = GameManager.Instance.withTime;

        if (allowHot && hotTextFile != null)
        {
            if(separator != '-')
                hotQuestionsList = hotTextFile.text.Split(separator);
            else
                hotQuestionsList = hotTextFile.text.Split('\n');
            
            print("hots: " + hotQuestionsList.Length);
            questionsList = new string[lightQuestionsList.Length + hotQuestionsList.Length];
            lightQuestionsList.CopyTo(questionsList, 0);
            hotQuestionsList.CopyTo(questionsList, lightQuestionsList.Length);

            foreach (string s in hotQuestionsList)
            {
                freeRandom.Add(i);
                i++;
            }
        }

        else
            questionsList = lightQuestionsList;

        print("total: " + questionsList.Length);
        screenText.text = questionsList[RandomQuestion()];
        print(questionsList.Length);

        print("WITH TIME IN SCENE " + withTime);
        if (sceeneWithTime)
        {
            if (withTime)
            {
                tabButton.enabled = false;
                counterText.enabled = true;
            }
            else
            {
                tabButton.enabled = true;
                counterText.enabled = false;
            }
        }
    }

    private void Update()
    {
        if (sceeneWithTime)
        {
            if (withTime)
            {
                counterTime -= Time.deltaTime;
                if (counterTime <= 0.0f)
                {
                    GameModeWithTime();
                    counterTime = maxTime;
                }

                counterText.text = Mathf.FloorToInt(counterTime).ToString();
            }
        }
    }

    public void OnScreenTap()
    {
        if (!instructionsCanvas.enabled)
        {
           if (usedRandom.Count >= questionsList.Length)
           {
               i = 0;

               foreach (string s in questionsList)
               {
                    freeRandom.Add(i);
                    i++;
               }

               usedRandom.Clear();
           }

         screenText.text = questionsList[RandomQuestion()];
        }
    }


    public void GameModeWithTime()
    {
        if (!instructionsCanvas.enabled)
        {
            OnScreenTap();
        }
    }


    int RandomQuestion()
    {
        int generatedRandom = freeRandom[Random.Range(0, freeRandom.Count)];

        usedRandom.Add(generatedRandom);
        freeRandom.Remove(generatedRandom);

        return generatedRandom;
    }
}
