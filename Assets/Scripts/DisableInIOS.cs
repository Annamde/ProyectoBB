using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableInIOS : MonoBehaviour
{
    void Start()
    {
        if(Application.platform == RuntimePlatform.IPhonePlayer)
        {
            this.gameObject.SetActive(false);
        }
    }
}
