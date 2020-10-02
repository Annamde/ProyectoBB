using System.Collections;
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
    public bool startQuestion;

    bool allowHot;
    bool withTime;
    bool timeEnded = false;


    List<int> usedRandom = new List<int>();
    List<int> freeRandom = new List<int>();

    TextAsset lightTextFile, hotTextFile;
    public Text screenText;
    string[] lightQuestionsList, hotQuestionsList, questionsList;
    int i;

    [Header("Time options")]
    public bool sceneWithTime = false;
    public float maxTime = 30.0f;
    float counterTime = 0.0f;
    public Text counterText;
    public Image outlineText;

    public int clicks;

    private void Start()
    {
        counterTime = maxTime;

        if (outlineText)
        {
            outlineText.enabled = true;
        }

        i = 0;

        lightTextFile = Resources.Load<TextAsset>(lightDocName);
        hotTextFile = Resources.Load<TextAsset>(hotDocName);

        if (lightTextFile != null)
        {
            if (separator != '-')
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
            if (separator != '-')
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

        if (startQuestion)
        {
            screenText.text = questionsList[RandomQuestion()];
        }
        print(questionsList.Length);

        print("WITH TIME IN SCENE " + withTime);

        if (withTime)
        {
            if (counterText != null)
            {
                counterText.enabled = true;
            }
        }
        else
            if (counterText != null) counterText.enabled = false;

        clicks = 0;
    }

    private void Update()
    {
        if (sceneWithTime)
        {
            if (withTime & !timeEnded)
            {
                if (counterTime <= 0.1f)
                {
                    timeEnded = true;
                    screenText.text = "¡TIEMPO!";
                    if (outlineText)
                    {
                        outlineText.enabled = false;
                    }
                    Handheld.Vibrate();
                }

                else
                    counterTime -= Time.deltaTime;

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

            clicks++;
            screenText.text = questionsList[RandomQuestion()];

            if (sceneWithTime)
            {
                if (withTime)
                {
                    counterTime = maxTime;
                    timeEnded = false;
                    if (outlineText)
                    {
                        outlineText.enabled = true;
                    }

                }
            }
        }
    }

    public void OnVerdadClick()
    {
        if (!instructionsCanvas.enabled)
        {
            clicks++;
            screenText.text = lightQuestionsList[Random.Range(0, lightQuestionsList.Length)]; //light = verdad, hot = reto

        }
    }
    public void OnRetoClick()
    {
        if (!instructionsCanvas.enabled)
        {
            clicks++;
            screenText.text = hotQuestionsList[Random.Range(0, hotQuestionsList.Length)];
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
