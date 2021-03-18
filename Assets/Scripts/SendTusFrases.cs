using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class SendTusFrases : MonoBehaviour
{
    public GameObject username;
    public GameObject userfrase;
    public GameObject thanku;

    private string Name;
    private string Frase;

    private TMPro.TextMeshProUGUI thankuText;

    [SerializeField]
    private string BASE_URL = "https://docs.google.com/forms/u/0/d/e/1FAIpQLSd0NYr5Ek7KZrz2uaFi0qfcSQshyc5gCax3EXIaUb_8n-hMzg/formResponse";

    private void Start()
    {
       thankuText = thanku.GetComponent<TMPro.TextMeshProUGUI>();
       thankuText.enabled = false;
    }


    IEnumerator Post(string name, string frase)
    {
        WWWForm form = new WWWForm();
        form.AddField("entry.1141775659", name);
        form.AddField("entry.1066077310", frase);

        // byte[] rawdata = form.data;
        // WWW www = new WWW(BASE_URL, rawdata);
        UnityWebRequest www = UnityWebRequest.Post(BASE_URL, form);

        yield return www.SendWebRequest();
    }

    public void Send()
    {
        Name = username.GetComponent<InputField>().text;
        Frase = userfrase.GetComponent<InputField>().text;
        
        if (Name != "" && Frase != "")
        {
            thankuText.enabled = true;
            StartCoroutine(Post(Name, Frase));

        }

    }

    public void ClearFrases()
    {
        username.GetComponent<InputField>().text = string.Empty;
        userfrase.GetComponent<InputField>().text = string.Empty;
    }

}
