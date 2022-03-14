using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleScript : MonoBehaviour
{
    public bool allowHot = true;

    public bool withTime = true;

    public float hotLevel = 1;

    public bool includeYN = true;
    public bool includeVoR = true;
    public bool includeQuien = true;

    private void Start()
    {
        GameManager.Instance.allowHot = allowHot;
        GameManager.Instance.withTime = withTime;
        GameManager.Instance.hotLevel = hotLevel;

        GameManager.Instance.includeYN = includeYN;
        GameManager.Instance.includeVoR = includeVoR;
        GameManager.Instance.includeQuien = includeQuien;
    }

    public void OnHotCheck(MovementToggleScript a)
    {
        allowHot = !allowHot;
        GameManager.Instance.allowHot = allowHot;
        a.CheckToggleSprite(allowHot);

        //GameManager.Instance.switching = true;
    }

    public void WithTimeCheck(MovementToggleScript a)
    {
        withTime = !withTime;
        GameManager.Instance.withTime = withTime;
        a.CheckToggleSprite(withTime);

        //GameManager.Instance.switching = true;
    }

    public void HotLevelCheck(float level)
    {
        hotLevel = level;
        GameManager.Instance.hotLevel = hotLevel;
    }

    public void OnYNCheck(MovementToggleScript a)
    {
        includeYN = !includeYN;
        GameManager.Instance.includeYN = includeYN;
        a.CheckToggleSprite(includeYN);
    }

    public void OnVoRCheck(MovementToggleScript a)
    {
        includeVoR = !includeVoR;
        GameManager.Instance.includeVoR = includeVoR;
        a.CheckToggleSprite(includeVoR);
    }

    public void OnQuienCheck(MovementToggleScript a)
    {
        includeQuien = !includeQuien;
        GameManager.Instance.includeQuien = includeQuien;
        a.CheckToggleSprite(includeQuien);
    }
}
