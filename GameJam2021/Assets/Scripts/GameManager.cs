using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public List<PlatformController> realWorldPlatforms = new List<PlatformController>();
    public List<PlatformController> imaginaryWorldPlatforms = new List<PlatformController>();

    public bool isImaginaryWorld;

    private void Start()
    {
        isImaginaryWorld = false;
        SwapBetweenWorlds(realWorldPlatforms, imaginaryWorldPlatforms);
    }

    public void SwapBetweenWorlds(List<PlatformController> realPlatforms, List<PlatformController> imaginaryPlatforms)
    {
        if(!isImaginaryWorld)
        {
            foreach(PlatformController platform in realPlatforms)
            {
                platform.gameObject.GetComponent<SpriteRenderer>().sprite = platform.realWorldSprite;
                platform.GetComponent<BoxCollider2D>().enabled = true;
            }
            foreach(PlatformController platform in imaginaryPlatforms)
            {
                platform.gameObject.GetComponent<SpriteRenderer>().sprite = platform.realWorldSprite;
                platform.GetComponent<BoxCollider2D>().enabled = false;
            }
        }
        else
        {
            foreach (PlatformController platform in realPlatforms)
            {
                platform.gameObject.GetComponent<SpriteRenderer>().sprite = platform.imaginaryWorldSprite;
                platform.GetComponent<BoxCollider2D>().enabled = false;
            }
            foreach (PlatformController platform in imaginaryPlatforms)
            {
                platform.gameObject.GetComponent<SpriteRenderer>().sprite = platform.imaginaryWorldSprite;
                platform.GetComponent<BoxCollider2D>().enabled = true;
            }
        }
    }
}
