using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetAllCanvas : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        print("RESETEOOOOOOOOO");

        GameManager.Instance.SetAllCanvas();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
