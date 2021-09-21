using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSpritesScript : MonoBehaviour
{
    [Header("Normal objects")]
    [SerializeField] private SpriteRenderer _mainLogoInScene;
    [SerializeField] private Sprite _normalLogoSprite; 

    [Header("Event objects")]
    [SerializeField]
    private List<GameObject> _eventSpritesList;
    [SerializeField]
    private Sprite _logoSprite;


    public List<GameObject> EventSpritesList => _eventSpritesList;

    //---INFO LOGOS---
    //size logo normal 90/90/x
    //size halloween logo 150/150/x

    public void EnableEventSprites()
    {
        if (_eventSpritesList[0] != null)
        {
            for (int i = 0; i < _eventSpritesList.Count; i++)
            {
                _eventSpritesList[i].SetActive(true);
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
}
