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

    private void Start()
    {
        GameManager.Instance.allowHot = allowHot;
        GameManager.Instance.withTime = withTime;
        GameManager.Instance.hotLevel = hotLevel;
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
}
