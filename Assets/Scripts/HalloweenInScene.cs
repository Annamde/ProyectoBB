using UnityEngine;

public class HalloweenInScene : EventInScene
{
    protected override void InitEvent()
    {
        if (GameManager.Instance.ActiveHalloweenEvent)
        {
            ActiveEventObjects();
        }
    }
}
