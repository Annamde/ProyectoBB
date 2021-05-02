using UnityEngine;

public class MovementToggleScript : MonoBehaviour
{

    public GameObject onSprite;
    public GameObject offSprite;

   
    // Start is called before the first frame update
    void Start()
    {
        onSprite.SetActive(true);
        offSprite.SetActive(false);
    }
   

    public void CheckToggleSprite(bool isActive)
    {
        if (isActive)
        {
            onSprite.SetActive(true);
            offSprite.SetActive(false);
        }
        else
        {
            onSprite.SetActive(false);
            offSprite.SetActive(true);
        }


    }

   
}
