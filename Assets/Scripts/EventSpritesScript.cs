using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EventSpritesScript : MonoBehaviour
{
    [Header("Normal objects")]
    [SerializeField] private SpriteRenderer _mainLogoInScene;
    [SerializeField] private Sprite _normalLogoSprite;
    [SerializeField] private List<GameObject> _instrucctionsPanelObjects;
    [SerializeField] private List<GameObject> _toggleNormalObject;

    [Header("Event objects")]
    [SerializeField] private List<GameObject> _eventSpritesList;
    [SerializeField] private Sprite _logoSprite;
    [SerializeField] private List<GameObject> _instrucctionsEventObjects;
    [SerializeField] private List<Sprite> _toggleSpritesEvent;
          

    public List<GameObject> EventSpritesList => _eventSpritesList;
   // public List<GameObject> 


    //---INFO LOGOS---
    //size logo normal 90/90/x
    //size halloween logo 150/150/x

    public void EnableEventSprites()
    {
        if (ListIsNotEmpty(_eventSpritesList))
        {
            for (int i = 0; i < _eventSpritesList.Count; i++)
            {
                _eventSpritesList[i].SetActive(true);
            }
        }
        if (ListIsNotEmpty(_instrucctionsEventObjects))
        {
            for (int i = 0; i < _instrucctionsEventObjects.Count; i++)
            {
                _instrucctionsEventObjects[i].SetActive(true);
            }
        }
        if (ListIsNotEmpty(_instrucctionsPanelObjects))
        {
            for (int i = 0; i < _instrucctionsPanelObjects.Count; i++)
            {
                _instrucctionsPanelObjects[i].SetActive(false);
            }
        }
    }

    public void ChangeLogoSprite()
    {
        if (_logoSprite != null && _mainLogoInScene != null)
        {
            _mainLogoInScene.transform.localScale = new Vector3(150, 150, 1);
            _mainLogoInScene.sprite = _logoSprite;
        }
    }

    private bool ListIsNotEmpty(List<GameObject> list)
    {
        return list.First() != null;
    }
}
