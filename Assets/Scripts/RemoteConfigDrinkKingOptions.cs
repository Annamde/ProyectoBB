using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Unity.RemoteConfig;

public class RemoteConfigDrinkKingOptions : MonoBehaviour //esta en el modoscontainer
{
    [Header("Sort Menu")]
    [SerializeField] private List<GameObject> _modes;
    [SerializeField] private bool _changeMenu = false;
    [SerializeField] private GameObject _newObject;

    [Header("Halloween")]
    [SerializeField] private EventSpritesScript _halloweenEventScript;
    private bool _halloweenActive;

    public struct userAtributtes { }
    public struct appAtributtes { }

    private bool _isActive = true;

    public void Awake()
    {
        ConfigManager.FetchCompleted += ChangeNamesMenu;
        ConfigManager.FetchCompleted += SetMenuUI;
        ConfigManager.FetchCompleted += SetActivePrefabNew;
        ConfigManager.FetchCompleted += EnableHalloweenAssets;
        ConfigManager.FetchConfigs<userAtributtes, appAtributtes>(new userAtributtes(), new appAtributtes());
    }

    private void SetMenuUI(ConfigResponse response)
    {
        _changeMenu = ConfigManager.appConfig.GetBool("changeMenu");

        if (_changeMenu && _modes[2] != null)
        {
            _modes = _modes.OrderBy(go => go.name).ToList();

            for (int i = 0; i < _modes.Count; i++)
            {
                _modes[i].transform.SetSiblingIndex(i);
            }
        }
    }

    private void ChangeNamesMenu(ConfigResponse response)
    {
        if (_modes[2] != null)
        {
           _modes[2].name = ConfigManager.appConfig.GetString("YoNuncaName");
           _modes[3].name = ConfigManager.appConfig.GetString("MimicaName");
           _modes[4].name = ConfigManager.appConfig.GetString("QuienName");
           _modes[5].name = ConfigManager.appConfig.GetString("VoRName");
           _modes[6].name = ConfigManager.appConfig.GetString("TabuName");
           _modes[7].name = ConfigManager.appConfig.GetString("CitaName");
           _modes[8].name = ConfigManager.appConfig.GetString("LetrasName");
           _modes[9].name = ConfigManager.appConfig.GetString("EnviaOBebeName");
           _modes[10].name = ConfigManager.appConfig.GetString("UltraHotName");
        }
    }

    private void SetActivePrefabNew(ConfigResponse response)
    {
        if (_newObject != null)
        {
            _newObject.SetActive(ConfigManager.appConfig.GetBool("NewPrefab"));
        }
    }

    private void EnableHalloweenAssets(ConfigResponse response)
    {
        _halloweenActive = ConfigManager.appConfig.GetBool("HalloweenActive");
        if (_halloweenActive)
        {
            _halloweenEventScript.EnableEventSprites();
            _halloweenEventScript.ChangeLogoSprite();
        }
    }

}
