using UnityEngine;

public class XmasInScene : EventInScene
{
    protected override void InitEvent()
    {
        if (GameManager.Instance.ActiveXmasEvent)
        {
            ActiveEventObjects();
        }
    }
}
