﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System;

public class TextManager_Ultra : MonoBehaviour
{
    [Header("Text options")]
    public string level1DocName;
    public string level2DocName;
    public string level3DocName;
    public Canvas instructionsCanvas;
    public Canvas levelEndedCanvas;
    public bool startQuestion;
    public GameObject tapButton;

    List<int> usedRandom = new List<int>();
    List<int> freeRandom = new List<int>();
    List<string> names = new List<string>();

    TextAsset level1TextFile, level2TextFile, level3TextFile;
    public Text screenText;
    string[] level1QuestionsList, level2QuestionsList, level3QuestionsList, questionsList;
    int i;
    int turn;

    public int clicks;

    public float levelEnabled = 1;

    string name1 = "";
    string name2 = "";

    private void Start()
    {
        names.Add("Carla");
        names.Add("Alicia");
        names.Add("Pau");
        names.Add("Alex");

        i = 0;
        turn = 0;

        level1TextFile = Resources.Load<TextAsset>(level1DocName);
        level2TextFile = Resources.Load<TextAsset>(level2DocName);
        level3TextFile = Resources.Load<TextAsset>(level3DocName);


        level1QuestionsList = level1TextFile.text.Split('\n');
        print("level1: " + level1QuestionsList.Length);

        level2QuestionsList = level2TextFile.text.Split('\n');
        print("level2: " + level2QuestionsList.Length);

        level3QuestionsList = level3TextFile.text.Split('\n');
        print("level3: " + level3QuestionsList.Length);


        levelEnabled = GameManager.Instance.hotLevel;

        switch (levelEnabled)
        {
            case 1:
                questionsList = level1QuestionsList;

                foreach (string s in level1QuestionsList)
                {
                    freeRandom.Add(i);
                    i++;
                }
                break;
            case 2:
                questionsList = level2QuestionsList;

                foreach (string s in level2QuestionsList)
                {
                    freeRandom.Add(i);
                    i++;
                }
                break;
            case 3:
                questionsList = level3QuestionsList;

                foreach (string s in level3QuestionsList)
                {
                    freeRandom.Add(i);
                    i++;
                }
                break;
            default:
                questionsList = level1QuestionsList;

                foreach (string s in level1QuestionsList)
                {
                    freeRandom.Add(i);
                    i++;
                }
                break;
        }

        print(levelEnabled);
        ChangeText();

        clicks = 0;
    }

    public void OnScreenTap()
    {
        if (!instructionsCanvas.enabled & !levelEndedCanvas.enabled)
        {
            if (usedRandom.Count >= questionsList.Length) //si se acaban las preguntas
            {
                i = 0;

                if (levelEnabled == 1 || levelEnabled == 2)
                {
                    levelEndedCanvas.enabled = true;
                    tapButton.SetActive(false);
                }

                if(levelEnabled == 3)
                {
                    foreach (string s in questionsList)
                    {
                        freeRandom.Add(i);
                        i++;
                    }

                    usedRandom.Clear();
                }

                //questionsList = level2QuestionsList;

                //foreach (string s in questionsList)
                //{
                //    freeRandom.Add(i);
                //    i++;
                //}

                //usedRandom.Clear();
            }

            else
            {
                clicks++;
                ChangeText();
                print("free: " + freeRandom.Count + " used: " + usedRandom.Count);
            }
        }
    }

    int RandomQuestion()
    {
        int generatedRandom = freeRandom[UnityEngine.Random.Range(0, freeRandom.Count)];

        usedRandom.Add(generatedRandom);
        freeRandom.Remove(generatedRandom);

        return generatedRandom;
    }

    void FrasesNames(out string name1, out string name2)
    {
        int generatedRandom2 = UnityEngine.Random.Range(0, names.Count);

        while (turn == generatedRandom2)
        {
            generatedRandom2 = UnityEngine.Random.Range(0, names.Count);
        }

        name1 = names[turn];
        name2 = names[generatedRandom2];

        if (turn == names.Count-1)
        {
            turn = 0;
        }
        else
            turn++;
    }

    void ChangeText ()
    {
        FrasesNames(out name1, out name2);
        screenText.text = String.Format(questionsList[RandomQuestion()], name1, name2);
    }

    public void RepeatButton()
    {
        print("repeat");

        foreach (string s in questionsList)
        {
            freeRandom.Add(i);
            i++;
        }

        usedRandom.Clear();

        levelEndedCanvas.enabled = false;
        tapButton.SetActive(true);
        ChangeText();
    }

    public void NextLevelButton()
    {

        print("next");

        if (levelEnabled == 1)
            questionsList = level2QuestionsList;

        if (levelEnabled == 2)
            questionsList = level3QuestionsList;


        levelEnabled++;

        foreach (string s in questionsList)
        {
            freeRandom.Add(i);
            i++;
        }

        usedRandom.Clear();

        levelEndedCanvas.enabled = false;
        tapButton.SetActive(true);
        ChangeText();
    }
}