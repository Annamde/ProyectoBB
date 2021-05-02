using UnityEngine;
using System.Collections;


public class ForceUpdateScript : MonoBehaviour
{
    public Canvas myCanvas;

    public void Awake()
    {
        StartCoroutine(Check());
    }

    private IEnumerator Check()
    {
        WWW w = new WWW("https://docs.google.com/document/d/1SEtCZWAVMjCSD1LAn1Ckf2DIVmOBV40Yb9xji6FpA88/edit?usp=sharing");
        yield return w;
        if (w.error != null)
        {
            Debug.Log("Error .. " + w.error);
        }
        else
        {
            if(w.text.Contains(Application.version.ToString()))
            {
                DisableCanvas();
            }
            else
            {
                ForceCanvas();
            }
        }
    }

    void ForceCanvas()
    {
        myCanvas.enabled = true;
    }

    void DisableCanvas()
    {
        myCanvas.enabled = false;
    }
}
