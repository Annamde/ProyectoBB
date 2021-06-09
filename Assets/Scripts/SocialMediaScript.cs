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

    public void OpenTikTok()
    {
        Application.OpenURL("https://www.tiktok.com/@drinkkinggame?lang=es");
    }
}
