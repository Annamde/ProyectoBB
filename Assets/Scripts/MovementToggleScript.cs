using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementToggleScript : MonoBehaviour
{
    public bool isTime;

    public bool isOn;

    public GameObject handle;
    private RectTransform handleTransform;
    private float handleSize;
    public RectTransform toggle;
    private float onPosX;
    private float offPosX;
    public float handleOffset;
    

    public float moveSpeed;
    public float t = 0.0f;


    // Start is called before the first frame update
    void Start()
    {
        CheckToggle();

        handleTransform = handle.GetComponent<RectTransform>();

        handleSize = handleTransform.sizeDelta.x;
        float toggleSizeX = toggle.sizeDelta.x;
        onPosX = (toggleSizeX / 2) - (handleSize / 2) - handleOffset;
        offPosX = onPosX * -1;
        

        if (isOn)
        {
            handleTransform.localPosition = new Vector3(onPosX, 0, 0);
           
        }
        else
        {
            handleTransform.localPosition = new Vector3(offPosX, 0, 0);
            
        }
        print(isOn);
    }

    // Update is called once per frame
    void Update()
    {
        print(isOn);
        CheckToggle();

        if (GameManager.Instance.switching)
        {
            StartToggleing(isOn);
        }
    }


    private void CheckToggle()
    {
        if (isTime)
        {
            isOn = GameManager.Instance.withTime;
        }
        else
        {
            isOn = GameManager.Instance.allowHot;
        }
        print(isOn + "  " + GameManager.Instance.allowHot +"   " + GameManager.Instance.withTime);
    }

    private void StartToggleing(bool tStatus)
    {
      
        if (tStatus)
        {
            handleTransform.localPosition = SmoothlyMove(handle, onPosX, offPosX);
           
        }
        else
        {
            handleTransform.localPosition = SmoothlyMove(handle, offPosX, onPosX);
            
        }
    }

    private Vector3 SmoothlyMove(GameObject handle, float startPosX, float endPosX)
    {
        Vector3 position = new Vector3(Mathf.Lerp(startPosX, endPosX, t += moveSpeed * Time.deltaTime), 0, 0);
        StopSwitching();
        return position;
    }


    private void StopSwitching()
    {
        if (t > 1.0f)
        {
            GameManager.Instance.switching = false;
            t = 0.0f;
            switch (isOn)
            {
                case true:
                    isOn = false;
                    break;
                case false:
                    isOn = true;
                    break;
            }
        }
    }
   
}
