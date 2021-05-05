using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Unity.RemoteConfig;

public class SortMenuScript : MonoBehaviour
{
    public List<GameObject> modes;
    public bool changeMenu = false;
    public GameObject newEoB;

    public struct userAtributtes { }
    public struct appAtributtes { }

    private bool _isActive = true;
    
    public void Awake()
    {
        ConfigManager.FetchCompleted += ChangeNamesMenu;
        ConfigManager.FetchCompleted += SetMenuUI;
        ConfigManager.FetchCompleted += SetActivePrefabNew;
        ConfigManager.FetchConfigs<userAtributtes, appAtributtes>(new userAtributtes(), new appAtributtes());
    }
    void SetMenuUI(ConfigResponse response)
    {
        changeMenu = ConfigManager.appConfig.GetBool("changeMenu");

        if (changeMenu && modes[2] != null)
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
        if (modes[2] != null)
        {
            modes[2].name = ConfigManager.appConfig.GetString("YoNuncaName");
            modes[3].name = ConfigManager.appConfig.GetString("MimicaName");
            modes[4].name = ConfigManager.appConfig.GetString("QuienName");
            modes[5].name = ConfigManager.appConfig.GetString("VoRName");
            modes[6].name = ConfigManager.appConfig.GetString("TabuName");
            modes[7].name = ConfigManager.appConfig.GetString("CitaName");
            modes[8].name = ConfigManager.appConfig.GetString("LetrasName");
            modes[9].name = ConfigManager.appConfig.GetString("EnviaOBebeName");
            modes[10].name = ConfigManager.appConfig.GetString("UltraHotName");
        }
    }
    void SetActivePrefabNew(ConfigResponse response)
    {
        if (newEoB != null)
        {
            newEoB.SetActive(ConfigManager.appConfig.GetBool("NewPrefab"));
        }
    }
}
