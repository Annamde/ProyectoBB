using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class TextManager_Mix : MonoBehaviour
{
    [Header("Text options")]
    public string ynDocName;
    public string vorDocName;
    public string quienDocName;
    public Canvas instructionsCanvas;
    public bool startQuestion;

    bool includeYN;
    bool includeVoR;
    bool includeQuien;

    List<string> usedRandom_yn = new List<string>();
    List<string> freeRandom_yn = new List<string>();

    List<string> usedRandom_vor = new List<string>();
    List<string> freeRandom_vor = new List<string>();

    List<string> usedRandom_quien = new List<string>();
    List<string> freeRandom_quien = new List<string>();

    TextAsset ynTextFile, vorTextFile, quienTextFile;
    public Text screenText, modeText;
    string[] ynQuestionsList, vorQuestionsList, quienQuestionsList;
    int i;

    public int clicks;

    private void Start()
    {
        i = 0;

        bool includeYN = GameManager.Instance.includeYN;

        if(includeYN)
        {
            ynTextFile = Resources.Load<TextAsset>(ynDocName);

            if (ynTextFile != null)
            {
                ynQuestionsList = ynTextFile.text.Split('\n');

                print("yn: " + ynQuestionsList.Length);

                foreach (string s in ynQuestionsList)
                {
                    freeRandom_yn.Add(s);
                }
            }

            else
                screenText.text = "No Yo Nunca text file found";
        }


        bool includeVoR = GameManager.Instance.includeVoR;

        if(includeVoR)
        {
            vorTextFile = Resources.Load<TextAsset>(vorDocName);

            if (vorTextFile != null)
            {
                vorQuestionsList = vorTextFile.text.Split('\n');

                print("vor: " + vorQuestionsList.Length);

                foreach (string s in vorQuestionsList)
                {
                    freeRandom_vor.Add(s);
                }
            }
        }

        bool includeQuien = GameManager.Instance.includeQuien;

        quienTextFile = Resources.Load<TextAsset>(quienDocName);

        if (includeQuien)
        {
            quienQuestionsList = quienTextFile.text.Split('\n');

            print("quien: " + quienQuestionsList.Length);

            foreach (string s in quienQuestionsList)
            {
                freeRandom_quien.Add(s);
            }
        }

        if (startQuestion)
        {
            screenText.text = RandomQuestion();
        }

        clicks = 0;
    }

    public void OnScreenTap()
    {
        if (!instructionsCanvas.enabled)
        {
            clicks++;
            screenText.text = RandomQuestion();
        }
    }

    string RandomQuestion()
    {
        int free_yn = freeRandom_yn.Count;
        int free_vor = freeRandom_vor.Count;
        int free_quien = freeRandom_quien.Count;
        int free_total = free_yn + free_vor + free_quien;

        int randomTopic = Random.Range(0, free_total);

        int randomQuestionIndex;

        string randomQuestion = "";

        print("Tema aleatorio: " + randomTopic);
        print("YN: " + free_yn + "VOR: " + (free_yn + free_vor) + "QUIEN: " + (free_total));

        if (randomTopic >= 0 && randomTopic < free_yn) //yn
        {

            modeText.text = "YO NUNCA";

            if (usedRandom_yn.Count >= ynQuestionsList.Length)
            {
                foreach (string s in ynQuestionsList)
                {
                    freeRandom_yn.Add(s);
                }

                usedRandom_yn.Clear();
            }

            randomQuestionIndex = Random.Range(0, freeRandom_yn.Count);

            randomQuestion = freeRandom_yn[randomQuestionIndex];

            usedRandom_yn.Add(freeRandom_yn[randomQuestionIndex]);
            freeRandom_yn.Remove(freeRandom_yn[randomQuestionIndex]);
        }

        else if(randomTopic >= free_yn && randomTopic < (free_yn + free_vor)) //vor
        {
            modeText.text = "VERDAD O RETO";

            if (usedRandom_vor.Count >= vorQuestionsList.Length)
            {
                foreach (string s in vorQuestionsList)
                {
                    freeRandom_vor.Add(s);
                }

                usedRandom_vor.Clear();
            }

            randomQuestionIndex = Random.Range(0, freeRandom_vor.Count);

            randomQuestion = freeRandom_vor[randomQuestionIndex];

            usedRandom_vor.Add(freeRandom_vor[randomQuestionIndex]);
            freeRandom_vor.Remove(freeRandom_vor[randomQuestionIndex]);
        }

        else //quien
        {
            modeText.text = "QUIÉN ES MÁS";

            if (usedRandom_quien.Count >= quienQuestionsList.Length)
            {
                foreach (string s in quienQuestionsList)
                {
                    freeRandom_quien.Add(s);
                }

                usedRandom_quien.Clear();
            }

            randomQuestionIndex = Random.Range(0, freeRandom_quien.Count);

            randomQuestion = freeRandom_quien[randomQuestionIndex];

            usedRandom_quien.Add(freeRandom_quien[randomQuestionIndex]);
            freeRandom_quien.Remove(freeRandom_quien[randomQuestionIndex]);
        }

        return randomQuestion;
    }
}
