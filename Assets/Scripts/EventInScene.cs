using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EventInScene : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> _eventObjects;

    private void Awake()
    {
        if (GameManager.Instance.ActiveHalloweenEvent)
        {
            ActiveEventObjects();
        }
    }

    private void ActiveEventObjects()
    {
        if (ListIsNotEmpty(_eventObjects))
        {
            for (int i = 0; i < _eventObjects.Count; i++)
            {
                _eventObjects[i].SetActive(true);
            }
        }
    }

    private bool ListIsNotEmpty(List<GameObject> list)
    {
        return list.First() != null;
    }
}
