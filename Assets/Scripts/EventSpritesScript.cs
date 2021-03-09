using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSpritesScript : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> _eventSpritesList;


    public List<GameObject> EventSpritesList => _eventSpritesList;


    public void SetStateSpritesEvents(bool isActive)
    {
        for (int i = 0; i < _eventSpritesList.Count; i++)
        {
            _eventSpritesList[i].SetActive(isActive);
        }
    }
}
