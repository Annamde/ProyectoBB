using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonGooglePlayScript : MonoBehaviour
{
    public void OpenStore()
    {
        if(Application.platform == RuntimePlatform.Android)
            Application.OpenURL("market://details?id=" + Application.identifier);
        //else if (Application.platform == RuntimePlatform.IPhonePlayer)
            //url de la app una vez este publicada
    }
}
