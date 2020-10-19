using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SendTusFrases : MonoBehaviour
{
    public GameObject username;
    public GameObject userfrase;

    private string Name;
    private string Frase;

    [SerializeField]
    private string BASE_URL = "https://docs.google.com/forms/u/0/d/e/1FAIpQLSd0NYr5Ek7KZrz2uaFi0qfcSQshyc5gCax3EXIaUb_8n-hMzg/formResponse";

    IEnumerator Post(string name, string frase)
    {
        WWWForm form = new WWWForm();
        form.AddField("entry.1141775659", name);
        form.AddField("entry.1066077310", frase);

        byte[] rawdata = form.data;
        WWW www = new WWW(BASE_URL, rawdata);

        yield return www;
    }

    public void Send()
    {
        Name = username.GetComponent<InputField>().text;
        Frase = userfrase.GetComponent<InputField>().text;

        StartCoroutine(Post(Name, Frase));

    }

}
