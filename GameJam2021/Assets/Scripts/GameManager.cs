using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public List<PlatformController> allPlatforms = new List<PlatformController>();
    public List<ObjectiveController> objectives = new List<ObjectiveController>();
    public int collectedObjectives;
    public int totalObjectives;
    public PlayerMovement player;

    public bool isImaginaryWorld;

    private void Start()
    {
        collectedObjectives = 0;
        totalObjectives = objectives.Count;
        isImaginaryWorld = false;
        SwapBetweenWorlds();
    }

    public void SwapBetweenWorlds()
    {
        foreach (PlatformController platform in allPlatforms)
        {
            platform.gameObject.GetComponent<SpriteRenderer>().sprite = isImaginaryWorld ? platform.imaginaryWorldSprite : platform.realWorldSprite;
            platform.GetComponent<BoxCollider2D>().enabled = platform.isImaginary ? isImaginaryWorld : !isImaginaryWorld;
        }
    }

    public void CollectObjective()
    {
        collectedObjectives++;
        print(collectedObjectives + " / " + totalObjectives + " items collected!");
        if(collectedObjectives == totalObjectives)
        {
            Victory();
        }
    }

    public void Victory()
    {
        print("You won!");
        ResetLevel();
    }

    public void ResetLevel()
    {
        player.ResetPosition();
        collectedObjectives = 0;
        objectives.ForEach(o => o.gameObject.SetActive(true));
        isImaginaryWorld = false;
        SwapBetweenWorlds();
    }
}
