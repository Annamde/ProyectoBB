using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class TextManager_EoB : MonoBehaviour
{
    [Header("Text options")]
    public string messagesDoc;
    public string namesDoc;
    public Canvas instructionsCanvas;

    TextAsset messagesTextFile, namesTextFile;
    public Text messagesScreenText, namesScreenText;
    string[] messagesList, namesList;

    bool nameShown = false;

    public int clicks;

    private void Start()
    {
        messagesTextFile = Resources.Load<TextAsset>(messagesDoc);
        namesTextFile = Resources.Load<TextAsset>(namesDoc);

        if (messagesTextFile != null)
        {
            messagesList = messagesTextFile.text.Split('\n');

            print("mensajes: " + messagesList.Length);
        }

        else
            messagesScreenText.text = "No message file found";

        if (namesTextFile != null)
        {
            namesList = namesTextFile.text.Split('\n');

            print("names: " + namesList.Length);
        }

        else
            namesScreenText.text = "No names file found";

            messagesScreenText.text = RandomMessage();

        clicks = 0;
    }

    public void OnScreenTap()
    {
        if (!instructionsCanvas.enabled)
        {
            if (nameShown)
            {
                namesScreenText.text = "";
                messagesScreenText.text = RandomMessage();
                nameShown = false;
            }

            else
            {
                namesScreenText.text = RandomName();
                nameShown = true;
                clicks++;
            }
        }
    }

    string RandomMessage()
    {
        string generatedRandom = messagesList[Random.Range(0, messagesList.Length)];

        return generatedRandom;
    }

    string RandomName()
    {
        string generatedRandom = namesList[Random.Range(0, namesList.Length)];

        return generatedRandom;
    }
}
