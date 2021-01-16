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
        ConfigManager.FetchCompleted += ChangeNamesMenu;
        ConfigManager.FetchCompleted += SetMenuUI;
        ConfigManager.FetchConfigs<userAtributtes, appAtributtes>(new userAtributtes(), new appAtributtes());
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

    void ChangeNamesMenu(ConfigResponse response)
    {
        modes[2].name = ConfigManager.appConfig.GetString("YoNuncaName");
        modes[3].name = ConfigManager.appConfig.GetString("MimicaName");
        modes[4].name = ConfigManager.appConfig.GetString("QuienName");
        modes[5].name = ConfigManager.appConfig.GetString("VoRName");
        modes[6].name = ConfigManager.appConfig.GetString("TabuName");
        modes[7].name = ConfigManager.appConfig.GetString("CitaName");
        modes[8].name = ConfigManager.appConfig.GetString("LetrasName");
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
