using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Unity.RemoteConfig;

public class SortMenuScript : MonoBehaviour
{
    //public GameObject[] modes;
    public List<GameObject> modes = new List<GameObject>();
    public bool changeMenu = false;

    public struct userAtributtes { }
    public struct appAtributtes { }

    public void Awake()
    {
        ConfigManager.FetchCompleted += SetMenuUI;
        ConfigManager.FetchConfigs<userAtributtes, appAtributtes>(new userAtributtes(), new appAtributtes());
        print("AWAKE " + changeMenu);
    }

    // Start is called before the first frame update
    void Start()
    {
       
    }

    void SetMenuUI(ConfigResponse response)
    {
        changeMenu = ConfigManager.appConfig.GetBool("changeMenu");
        if (changeMenu)
        {
            modes = modes.OrderBy(go => go.name).ToList();

            for (int i = 0; i < modes.Count; i++)
            {
                modes[i].transform.SetSiblingIndex(i);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        print("UPDATE" + changeMenu);
    }


}
