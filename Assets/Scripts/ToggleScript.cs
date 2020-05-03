using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleScript : MonoBehaviour
{
    public bool allowHot = true;

    public bool withTime = true;

    private void Start()
    {
        GameManager.Instance.allowHot = allowHot;
        GameManager.Instance.withTime = withTime;
    }

    public void OnHotCheck()
    {
        allowHot = !allowHot;
        GameManager.Instance.allowHot = allowHot;
        GameManager.Instance.switching = true;
    }

    public void WithTimeCheck()
    {
        withTime = !withTime;
        GameManager.Instance.withTime = withTime;
        GameManager.Instance.switching = true;
    }
}
