using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LetrasManager : MonoBehaviour
{
    public Text outlineText;
    public Text topText;
    public Text centerText;
    public Text bottomText;

    char[] abcArray = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
    string[] parametersArray = {"famoso", "comida", "país", "objeto", "marca", "animal", "bebida alcohólica", "discoteca"};

    void Start()
    {
        RandomLetter();
        RandomParameters();
    }

    void RandomLetter()
    {
        int random = Random.Range(0, abcArray.Length);

        outlineText.text = abcArray[random].ToString();
    }

    void RandomParameters()
    {
        int r1 = Random.Range(0, parametersArray.Length);
        topText.text = parametersArray[r1].ToString();

        int r2 = Random.Range(0, parametersArray.Length);
        while(r1==r2)
        {
            r2 = Random.Range(0, parametersArray.Length);
        }
        centerText.text = parametersArray[r2].ToString();

        int r3 = Random.Range(0, parametersArray.Length);
        while (r1 == r3 || r2 == r3)
        {
            r3 = Random.Range(0, parametersArray.Length);
        }
        bottomText.text = parametersArray[r3].ToString();
    }

    public void OnScreenTap()
    {
        RandomLetter();
        RandomParameters();
    }
}
