using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class EventInScene : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> _eventObjects;

    private void Start()
    {
        InitEvent();
    }

    protected abstract void InitEvent();

    protected void ActiveEventObjects()
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
