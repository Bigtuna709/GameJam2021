using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public List<GameObject> realWorldPlatforms = new List<GameObject>();
    public List<GameObject> imaginaryWorldPlatforms = new List<GameObject>();

    public bool isImaginaryWorld;

    private void Start()
    {
        isImaginaryWorld = false;
        SwapBetweenWorlds(realWorldPlatforms, imaginaryWorldPlatforms);
    }

    public void SwapBetweenWorlds(List<GameObject> realPlatforms, List<GameObject> imaginaryPlatforms)
    {
        if(!isImaginaryWorld)
        {
            foreach(GameObject platform in realPlatforms)
            {
                platform.SetActive(true);
            }
            foreach(GameObject platform in imaginaryPlatforms)
            {
                platform.SetActive(false);
            }
        }
        else
        {
            foreach (GameObject platform in realPlatforms)
            {
                platform.SetActive(false);
            }
            foreach (GameObject platform in imaginaryPlatforms)
            {
                platform.SetActive(true);
            }
        }
    }
}
