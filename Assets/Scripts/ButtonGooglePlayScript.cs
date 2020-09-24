using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonGooglePlayScript : MonoBehaviour
{
    public void OpenStore()
    {
        Application.OpenURL("market://details?id=" + Application.identifier);
    }
}
