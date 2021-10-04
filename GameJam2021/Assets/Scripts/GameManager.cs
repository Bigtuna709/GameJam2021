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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            isImaginaryWorld = !isImaginaryWorld;
            GameManager.Instance.SwapBetweenWorlds(GameManager.Instance.realWorldPlatforms, GameManager.Instance.imaginaryWorldPlatforms);
        }
    }
    public void SwapBetweenWorlds(List<PlatformController> realPlatforms, List<PlatformController> imaginaryPlatforms)
    {
        if(!isImaginaryWorld)
        {
            foreach(PlatformController platform in realPlatforms)
            {
                platform.gameObject.GetComponent<SpriteRenderer>().sprite = platform.realWorldSprite; 
            }
            foreach(PlatformController platform in imaginaryPlatforms)
            {
                platform.gameObject.GetComponent<SpriteRenderer>().sprite = platform.realWorldSprite;
            }
        }
        else
        {
            foreach (PlatformController platform in realPlatforms)
            {
                platform.gameObject.GetComponent<SpriteRenderer>().sprite = platform.imaginaryWorldSprite;
            }
            foreach (PlatformController platform in imaginaryPlatforms)
            {
                platform.gameObject.GetComponent<SpriteRenderer>().sprite = platform.imaginaryWorldSprite;
            }
        }
    }
}
