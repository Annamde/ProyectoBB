using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SocialMediaScript : MonoBehaviour
{
  public void OpenInstagram()
    {
        Application.OpenURL("https://www.instagram.com/drinkkinggame/?hl=es");
    }

    public void OpenTwitter()
    {
        Application.OpenURL("https://twitter.com/DrinkKingGame_");
    }

}
