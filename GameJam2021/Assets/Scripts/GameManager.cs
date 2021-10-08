using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    public PlayerMovement player;

    public List<PlatformController> allPlatforms = new List<PlatformController>();
    public List<ObjectiveController> objectives = new List<ObjectiveController>();

    public int collectedObjectives;
    public int totalObjectives;

    public bool isImaginaryWorld;
    public bool isPaused;

    public Text collectedObjectiveText;
    public Text totalObjectivesText;

    public GameObject win;
    public GameObject pauseCanvas;
    private void Start()
    {
        collectedObjectives = 0;
        totalObjectives = objectives.Count;
        totalObjectivesText.text = totalObjectives.ToString();
        isImaginaryWorld = false;
        SwapBetweenWorlds();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
    }

    public void PauseGame()
    {
        isPaused = !isPaused;
        if (isPaused)
        {
            Time.timeScale = 0;
            pauseCanvas.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            pauseCanvas.SetActive(false);
        }
    }

    public void SwapBetweenWorlds()
    {
        foreach (PlatformController platform in allPlatforms)
        {
            platform.gameObject.GetComponent<SpriteRenderer>().sprite = isImaginaryWorld ? platform.imaginaryWorldSprite : platform.realWorldSprite;
            if(platform.GetComponent<BoxCollider2D>() != null)
            platform.GetComponent<BoxCollider2D>().enabled = platform.isImaginary ? isImaginaryWorld : !isImaginaryWorld;

            if(platform.animator != null && isImaginaryWorld)
            {
                platform.animator.enabled = true;
            }
            else if(platform.animator != null && !isImaginaryWorld)
            {
                platform.animator.enabled = false;
            }
        }
    }

    public void CollectObjective()
    {
        collectedObjectives++;
        collectedObjectiveText.text = collectedObjectives.ToString();
        print(collectedObjectives + " / " + totalObjectives + " items collected!");
        if(collectedObjectives == totalObjectives)
        {
            StartCoroutine(Victory());
        }
    }

    public IEnumerator Victory()
    {
        Time.timeScale = 0.3f;
        yield return new WaitForSeconds(.5f);
        Time.timeScale = 1f;
        win.GetComponent<AudioSource>().Play();
        print("You won!");
        ResetLevel();
    }

    public void ResetLevel()
    {
        player.ResetPosition();
        objectives.ForEach(o => o.gameObject.SetActive(true));
        collectedObjectives = 0;
        isImaginaryWorld = false;
        SwapBetweenWorlds();
    }
}
