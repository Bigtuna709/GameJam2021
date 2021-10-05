using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public List<PlatformController> allPlatforms = new List<PlatformController>();
    public ObjectiveController objective;
    public PlayerMovement player;

    public bool isImaginaryWorld;

    private void Start()
    {
        isImaginaryWorld = false;
        SwapBetweenWorlds(allPlatforms);
    }

    public void SwapBetweenWorlds(List<PlatformController> allPlatforms)
    {
        foreach (PlatformController platform in allPlatforms)
        {
            platform.gameObject.GetComponent<SpriteRenderer>().sprite = isImaginaryWorld ? platform.imaginaryWorldSprite : platform.realWorldSprite;
            platform.GetComponent<BoxCollider2D>().enabled = platform.isImaginary ? isImaginaryWorld : !isImaginaryWorld;
        }
    }

    public void Victory()
    {
        print("You won!");
        player.ResetPosition();
    }
}
