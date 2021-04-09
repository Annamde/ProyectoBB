using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class SendTusFrases : MonoBehaviour
{
    public GameObject username;
    public GameObject userfrase;
    public GameObject thanku;

    private string _name;
    private string _frase;
    private string _mode;

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
        form.AddField("entry.2055374227", _mode);

        // byte[] rawdata = form.data;
        // WWW www = new WWW(BASE_URL, rawdata);
        UnityWebRequest www = UnityWebRequest.Post(BASE_URL, form);

        yield return www.SendWebRequest();
    }

    public void Send()
    {
        _name = username.GetComponent<InputField>().text;
        _frase = userfrase.GetComponent<InputField>().text;
        
        if (_name != "" && _frase != "")
        {
            thankuText.enabled = true;
            StartCoroutine(Post(_name, _frase));

        }

    }

    public void ClearFrases()
    {
        username.GetComponent<InputField>().text = string.Empty;
        userfrase.GetComponent<InputField>().text = string.Empty;
    }

    public void SetModeActive(string mode)
    {
        _mode = mode;
    }
    
}
